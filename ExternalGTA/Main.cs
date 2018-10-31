using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ExternalGTA
{
    public partial class Main : Form
    {
		bool infoTrRunning = false;
		bool skipClear = false;

		bool moveMode = false;
		bool colorMode = false;
		int colorIndex = 0;

        Hacks h;

        bool isMain = true;
        Menu currentMenu;
        List<Menu> menus = new List<Menu>();
        List<String> listBoxEntrys = new List<String>();

		List<Color> colorList = new List<Color>() { Color.DarkRed, Color.DarkOliveGreen, Color.Orange, Color.BlueViolet, Color.Gold, Color.HotPink, Color.Purple };

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        public Main()
        {
            InitializeComponent();
            KeyBoardHooking();
            setupMenus();
            listMenu.DataSource = listBoxEntrys;
            //Steam
            h = new Hacks("GTA5", "GTA5.exe", false, this);

			//SC
			//h = new Hacks("GTA5", "GTA5.exe", true, this);

			this.StartPosition = FormStartPosition.Manual;
			this.Location = new Point(50, 50);
			setColor(colorList[colorIndex]);

        }

        public void setupMenus()
        {
            //Player
            Menu player = new Menu("Player>>");

            player.addEntry(new ToggleEntry(HackID.GODMODE, "Godmode"));
			player.addEntry(new ToggleEntry(HackID.OTR, "Off the Radar"));
			player.addEntry(new ToggleEntry(HackID.NEVERWANTED, "Never Wanted"));
			player.addEntry(new ValueEntry(HackID.WANTEDLVL, "Target Wanted Level", 5, 0));
            player.addEntry(new ValueEntry(HackID.SPRINT, "Sprint Speed", 5, 1, 1, 1));
            player.addEntry(new ValueEntry(HackID.SWIM, "Swim Speed", 5, 1, 1, 1));
            player.addEntry(new ToggleEntry(HackID.SUPERJUMP, "Sumper Jump"));

            menus.Add(player);

            //Vehicle
            Menu vehicle = new Menu("Vehicle>>");

            vehicle.addEntry(new ToggleEntry(HackID.CARGOD, "Vehicle Godmode"));
            vehicle.addEntry(new ToggleEntry(HackID.SEATBELT, "Seatbelt"));
            vehicle.addEntry(new ValueEntry(HackID.GRAVITY, "Gravity", 49, 0, 0.98, 9.8));
            vehicle.addEntry(new ValueEntry(HackID.ACCELERATION, "Acceleration", 10, 1, 0.5, 1));

            menus.Add(vehicle);
            
            //Weapon
            Menu weapon = new Menu("Weapon>>");

            weapon.addEntry(new ToggleEntry(HackID.INFAMMO, "Infinite Ammo"));
			weapon.addEntry(new ToggleEntry(HackID.EXPLOSIVEAMMO, "Explosive Ammo"));
			weapon.addEntry(new ToggleEntry(HackID.SPREAD, "Disable Spread (Held Weap)"));
			weapon.addEntry(new ToggleEntry(HackID.RECOIL, "Disable Recoil (Held Weap)"));
			weapon.addEntry(new ToggleEntry(HackID.FIRERATE, "Rapid Fire (Held Weap)"));

			menus.Add(weapon);

            foreach (Menu obj in menus)
            {
                listBoxEntrys.Add(obj.menuDisplayName);
            }

        }
		
        public void updateMenu()
        {
            int indexBefore = listMenu.SelectedIndex;
            currentMenu.updateList();
            listMenu.DataSource = currentMenu.menuList;
            listMenu.SelectedIndex = indexBefore;
        }

		public void showInfo(string s)
		{

			Invoke((MethodInvoker)delegate ()
			{
				this.infoBox.Text = s;
			});

			if (infoTrRunning)
			{
				skipClear = true;
			}

			infoTrRunning = true;

			new Thread(() =>
			   {

				   Thread.Sleep(3000);
				   Invoke((MethodInvoker)delegate ()
				   {
					   if (!skipClear)
					   {
						   infoBox.Text = "";
					   }

					   skipClear = false;
					   infoTrRunning = false;

				   });

			   })
			{
				IsBackground = true
			}.Start();
		}

        public void KeyBoardHooking()
        {
			// Keyboard hooking
			Thread Thread = new Thread(() =>
			{

				while (true)
				{

					Thread.Sleep(1);

					if (GetAsyncKeyState(Keys.Subtract) == -32767)
					{
						if (this.Visible)
						{
							Invoke(new MethodInvoker(Hide));
						}
						else
						{
							Invoke(new MethodInvoker(Show));
							Invoke((MethodInvoker)delegate ()
						   {
							   this.TopMost = true;
						   });
						}
					}

					if (GetAsyncKeyState(Keys.F9) == -32767)
					{
						System.Media.SystemSounds.Beep.Play();
						Application.Exit();
					}

					if (!this.Visible)
					{
						continue;
					}

					if (GetAsyncKeyState(Keys.NumPad7) == -32767)
					{

						if (colorMode)
						{
							colorMode = false;
						}

						moveMode = !moveMode;
						showInfo((moveMode ? "Enabled" : "Disabled") + " Move Mode!");
					}

					if (GetAsyncKeyState(Keys.NumPad9) == -32767)
					{
						if (moveMode)
						{
							moveMode = false;
						}

						colorMode = !colorMode;
						showInfo((colorMode ? "Enabled" : "Disabled") + " Color Mode!");
					}

					if (GetAsyncKeyState(Keys.NumPad5) == -32767)
					{
						Invoke((MethodInvoker)delegate ()
						{
							if (isMain)
							{
								currentMenu = menus[listMenu.SelectedIndex];
								currentMenu.updateList();
								listMenu.DataSource = currentMenu.menuList;
								isMain = false;
							}
							else
							{
								if (currentMenu.getEntryAt(listMenu.SelectedIndex) is ToggleEntry)
								{
									ToggleEntry tEnt = currentMenu.getEntryAt(listMenu.SelectedIndex) as ToggleEntry;
									tEnt.toggle();
									h.runHack(tEnt);
									updateMenu();
								}
							}

						});
					}

					if (GetAsyncKeyState(Keys.NumPad0) == -32767)
					{
						Invoke((MethodInvoker)delegate ()
						{
							if (!isMain)
							{
								listMenu.DataSource = listBoxEntrys;
								isMain = true;
								currentMenu = null;
							}

						});
					}

					if (GetAsyncKeyState(Keys.NumPad8) == -32767)
					{

						if (moveMode)
						{
							Invoke((MethodInvoker)delegate ()
							{
								this.Top -= 10;
							});
							continue;
						}

						Invoke((MethodInvoker)delegate ()
						{
							if (!(listMenu.SelectedIndex - 1 < 0))
							{
								listMenu.SelectedIndex -= 1;
							}
						});

					}

					if (GetAsyncKeyState(Keys.NumPad2) == -32767)
					{
						if (moveMode)
						{
							Invoke((MethodInvoker)delegate ()
							{
								this.Top += 10;
							});
							continue;
						}

						Invoke((MethodInvoker)delegate ()
						{
							if (!(listMenu.SelectedIndex + 1 > listMenu.Items.Count - 1))
							{
								listMenu.SelectedIndex += 1;
							}
						});

					}

					if (GetAsyncKeyState(Keys.NumPad4) == -32767)
					{

						if (colorMode)
						{
							Invoke((MethodInvoker)delegate ()
							{
								colorIndex--;
								if(colorIndex < 0)
								{
									colorIndex = colorList.Count -1;
								}
								setColor(colorList[colorIndex]);
							});
							continue;
						}

						if (moveMode)
						{
							Invoke((MethodInvoker)delegate ()
							{
								this.Left -= 10;
							});
							continue;
						}

						Invoke((MethodInvoker)delegate ()
						{
							if (!isMain)
							{
								if (currentMenu.getEntryAt(listMenu.SelectedIndex) is ValueEntry)
								{
									ValueEntry tEnt = currentMenu.getEntryAt(listMenu.SelectedIndex) as ValueEntry;
									tEnt.decrVal();
									h.runHack(tEnt);
									updateMenu();
								}
							}

						});
					}

					if (GetAsyncKeyState(Keys.NumPad6) == -32767)
					{

						if (colorMode)
						{
							Invoke((MethodInvoker)delegate ()
							{
								colorIndex++;
								if (colorIndex > colorList.Count -1)
								{
									colorIndex = 0;
								}
								setColor(colorList[colorIndex]);
							});
							continue;
						}

						if (moveMode)
						{
							Invoke((MethodInvoker)delegate ()
							{
								this.Left += 10;
							});
							continue;
						}

						Invoke((MethodInvoker)delegate ()
						{
							if (!isMain)
							{
								if (currentMenu.getEntryAt(listMenu.SelectedIndex) is ValueEntry)
								{
									ValueEntry tEnt = currentMenu.getEntryAt(listMenu.SelectedIndex) as ValueEntry;
									tEnt.incrVal();
									h.runHack(tEnt);
									updateMenu();
								}
							}

						});
					}

				}
			});
			Thread.IsBackground = true;
            Thread.Name = "KeyHandler";
            Thread.Start();
        }

		public void setColor(Color c)
		{
			this.listMenu.BackColor = c;
			this.BackColor = c;
		}

        protected override void OnLoad(EventArgs e)
        {
            Hacks.IsGameRunning();
        }
	}
}
