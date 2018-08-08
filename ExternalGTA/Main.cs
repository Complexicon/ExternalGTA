using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace ExternalGTA
{
    public partial class Main : Form
    {

        Hacks h;

        bool isMain = true;
        MenuObject currentMenu;
        List<MenuObject> menus = new List<MenuObject>();
        List<String> listBoxEntrys = new List<String>();

        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        public Main()
        {
            InitializeComponent();
            KeyBoardHooking();
            setupMenus();
            listMenu.DataSource = listBoxEntrys;
            h = new Hacks("GTA5", "GTA5.exe");
        }

        public void setupMenus()
        {
            //Player
            MenuObject player = new MenuObject("Player>>");

            player.addEntry(new MenuEntry("Godmode", true, HackList.GODMODE));
            player.addEntry(new MenuEntry("Off the Radar", true, HackList.OTR));
            player.addEntry(new MenuEntry("Never Wanted", true, HackList.NEVERWANTED));
            player.addEntry(new MenuEntry("Target Wanted Level", 5, 0, false, HackList.WANTEDLVL));
            player.addEntry(new MenuEntry("Sprint Speed", 5, 1, false, HackList.SPRINT));
            player.addEntry(new MenuEntry("Swim Speed", 5, 1, false, HackList.SWIM));

            menus.Add(player);

            //Vehicle
            MenuObject vehicle = new MenuObject("Vehicle>>");

            vehicle.addEntry(new MenuEntry("Vehicle Godmode", true, HackList.CARGOD));
            vehicle.addEntry(new MenuEntry("Seatbelt", true, HackList.SEATBELT));
            vehicle.addEntry(new MenuEntry("Gravity", 49, 0, 9.8, 0.98, false, HackList.GRAVITY));
            vehicle.addEntry(new MenuEntry("Acceleration", 10, 1, 1, 0.5, false, HackList.ACCELERATION));

            menus.Add(vehicle);
            
            //Weapon
            MenuObject weapon = new MenuObject("Weapon>>");

            weapon.addEntry(new MenuEntry("Infinite Clip", true, HackList.INFCLIP));
            weapon.addEntry(new MenuEntry("Infinite Ammo", true, HackList.INFAMMO));

            menus.Add(weapon);


            foreach (MenuObject obj in menus)
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
                                currentMenu.getEntryAt(listMenu.SelectedIndex).toggle();
                                h.runHack(currentMenu.getEntryAt(listMenu.SelectedIndex));
                                updateMenu();
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
                                if (!currentMenu.getEntryAt(listMenu.SelectedIndex).isToggleable())
                                {
                                    currentMenu.getEntryAt(listMenu.SelectedIndex).decrVal();
                                    h.runHack(currentMenu.getEntryAt(listMenu.SelectedIndex));
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
                                if (!currentMenu.getEntryAt(listMenu.SelectedIndex).isToggleable())
                                {
                                    currentMenu.getEntryAt(listMenu.SelectedIndex).incrVal();
                                    h.runHack(currentMenu.getEntryAt(listMenu.SelectedIndex));
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

    }
}
