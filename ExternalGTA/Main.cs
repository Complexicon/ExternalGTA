using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ExternalGTA
{
    public partial class Main : Form
    {

        Hacks h;

        bool isMain = true;
        Menu currentMenu;
        List<Menu> menus = new List<Menu>();
        List<String> listBoxEntrys = new List<String>();

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
			weapon.addEntry(new ToggleEntry(HackID.SPREAD, "Disable Spread"));
			weapon.addEntry(new ToggleEntry(HackID.RECOIL, "Disable Recoil"));
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
			this.infoBox.Text = s;

			new Thread(() =>
			{

				Thread.Sleep(3000);
				Invoke((MethodInvoker)delegate ()
				{
					this.infoBox.Text = "";
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

                    Thread.Sleep(2);

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
                        Application.Exit();
                    }

                    if (!this.Visible)
                    {
                        continue;
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
            })
            {
                IsBackground = true
            };
            Thread.SetApartmentState(ApartmentState.STA);
            Thread.Name = "KeyHandler";
            Thread.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            h.IsGameRunning();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

		private void infoBox_Click(object sender, EventArgs e)
		{

		}
	}
}
