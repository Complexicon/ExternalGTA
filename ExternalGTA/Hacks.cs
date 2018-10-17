using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ExternalGTA
{
    public class Hacks : Memory
    {

        Thread wantedThread;
        Thread cheatThread;
        bool trRunning = false;
        bool cheatTrRunning = false;

        bool superJump;
        bool explosiveAmmo;
        bool explosiveMelee;

        int targetWantedLevel = 0;

        int WorldPTR;

        int idkPTR;
        //int idk2PTR;

        int[] oGodmode = new int[] { 0x08, 0x189 };
        int[] oMaxHealth = new int[] { 0x08, 0x2A0 };
        int[] oSeatbelt = new int[] { 0x08, 0x13EC };
        int[] oWantedLevel = new int[] { 0x08, 0x10b8, 0x818 };
        int[] oSprintSpeed = new int[] { 0x08, 0x10b8, 0x14C };
        int[] oSwimSpeed = new int[] { 0x08, 0x10b8, 0x148 };
        int[] oPlayerFlags = new int[] { 0x08, 0x10b8, 0x1F9 };

        int[] oCarGodmode = new int[] { 0x08, 0xd28, 0x189 };
        int[] oCarGravity = new int[] { 0x08, 0xd28, 0xBCC };
        int[] oCarAcceleration = new int[] { 0x08, 0xd28, 0x8C8, 0x4C };

        public Hacks(string exeName, string processName, bool isSC)
        {
            ExeName = exeName;
            ProcessName = processName;
            BaseAddress = GetBaseAddress(ProcessName);
            pHandle = GetProcessHandle();

            if (isSC)
            {
                WorldPTR = 0x240EDC8;

                idkPTR = 0xEC98F4;
                //idk2PTR = 0xEC9939;
            }
            else
            {
                WorldPTR = 0x2413410;

                idkPTR = 0xEC975C;
                //idk2PTR = 0xEC97A1;
            }

        }

        // God Mode.
        public byte[] getGodmode()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, oGodmode);
            return ReadBytes(pointer, 1);
        }

        public void setGodmode(bool? enabled)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, oGodmode);
            if (enabled == true)
            {
                WriteBytes(pointer, new byte[] { 0x1 });
            }
            else
            {
                WriteBytes(pointer, new byte[] { 0x0 });
            }
        }

        //Off the Radar
        public float getHealth()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, oMaxHealth);
            return ReadFloat(pointer);
        }

        public void setHealth(bool? enabled)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, oMaxHealth);
            if (enabled == true)
            {
                WriteFloat(pointer, 0);
            }
            else
            {
                WriteFloat(pointer, 328);
            }
        }

        //Wanted Level
        public int getWanted()
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, oWantedLevel);
            return ReadInteger(pointer, 4);
        }

        public void setWanted(int level)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, oWantedLevel);

            WriteInteger(pointer, level, 4);

        }

        //Sprint
        public void setSprint(float f)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, oSprintSpeed);
            WriteFloat(pointer, f);
        }

        //Swim
        public void setSwim(float f)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, oSwimSpeed);
            WriteFloat(pointer, f);
        }

        //Cargod
        public void setCargod(bool? enabled)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, oCarGodmode);
            if (enabled == true)
            {
                WriteBytes(pointer, new byte[] { 0x1 });
            }
            else
            {
                WriteBytes(pointer, new byte[] { 0x0 });
            }
        }

        //Seatbelt
        public void setSeatbelt(bool? enabled)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, oSeatbelt);
            if (enabled == true)
            {
                WriteBytes(pointer, new byte[] { 0x1 });
            }
            else
            {
                WriteBytes(pointer, new byte[] { 0x0 });
            }
        }

        //Acceleration
        public void setAccel(float f)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, oCarAcceleration);
            WriteFloat(pointer, f);
        }

        //Gravity
        public void setGravity(float f)
        {
            long pointer = GetPointerAddress(BaseAddress + WorldPTR, oCarGravity);
            WriteFloat(pointer, f);
        }

        //Infinite Ammo
        public void setAmmo(bool? enabled)
        {
            long pointer = GetPointerAddress(BaseAddress + idkPTR);
            if (enabled == true)
            {
				WriteBytes(pointer, new byte[] { 0x41, 0x2B, 0xC9 });
			}
            else
            {
                WriteBytes(pointer, new byte[] { 0x8A, 0x48, 0x78 });
            }
        }

        /*Infinite Ammo (Old)
        public void setAmmo(bool? enabled)
        {
            long pointer = GetPointerAddress(BaseAddress + idk2PTR);
            if (enabled == true)
            {
                WriteBytes(pointer, new byte[] { 0x90, 0x90, 0x90 });
            }
            else
            {
                WriteBytes(pointer, new byte[] { 0x41, 0x2B, 0xD1 });
            }
        }*/

        public void startCheatThread()
        {
            if (!cheatTrRunning)
            {
                cheatTrRunning = true;
				cheatThread = new Thread(() =>
                {
                    while (cheatTrRunning)
                    {

                        if (superJump)
                        {
                            long pointer = GetPointerAddress(BaseAddress + WorldPTR, oPlayerFlags);
                            WriteBytes(pointer, new byte[] { 0x40 });
                        }

                        Thread.Sleep(1);
                    }

                })
                {
                    IsBackground = true
                };
				cheatThread.SetApartmentState(ApartmentState.STA);
				cheatThread.Name = "CheatFlagThread";
				cheatThread.Start();
            }
        }

        public void runHack(MenuEntry entry)
        {
            MenuEntry e = entry;
            HackList h = e.hack;

            switch (h)
            {
                case HackList.GODMODE:
                    setGodmode(e.toggled);
                    break;
                case HackList.OTR:
                    setHealth(e.toggled);
                    break;
                case HackList.NEVERWANTED:
                    if (e.toggled)
                    {
                        if (!trRunning)
                        {
                            trRunning = true;
                            wantedThread = new Thread(() =>
                            {
                                while (trRunning)
                                {

                                    if (getWanted() != targetWantedLevel)
                                    {
                                        setWanted(targetWantedLevel);
                                    }

                                    Thread.Sleep(100);
                                }

                            })
                            {
                                IsBackground = true
                            };
                            wantedThread.SetApartmentState(ApartmentState.STA);
                            wantedThread.Name = "WantedLevelThread";
                            wantedThread.Start();
                        }
                    }
                    else
                    {
                        trRunning = false;
                    }
                    break;
                case HackList.WANTEDLVL:
                    setWanted((int)e.value);
                    break;
                case HackList.SPRINT:
                    setSprint((int)e.value);
                    break;
                case HackList.SWIM:
                    setSwim((int)e.value);
                    break;
                case HackList.CARGOD:
                    setCargod(e.toggled);
                    break;
                case HackList.SEATBELT:
                    setSeatbelt(e.toggled);
                    break;
                case HackList.ACCELERATION:
                    setAccel((int)e.value);
                    break;
                case HackList.GRAVITY:
                    setGravity((float)e.value);
                    break;
                case HackList.INFAMMO:
                    setAmmo(e.toggled);
                    break;
                case HackList.SUPERJUMP:
                    startCheatThread();
                    superJump = e.toggled;
                    break;
                case HackList.DUMMY:
                    break;
                default:
                    break;
            }

        }

        public bool IsGameRunning()
        {
            Process[] process = Process.GetProcessesByName(ExeName);
            if (process.Length == 0)
            {
                if (MessageBox.Show("You need to run " + ExeName, "Critical Scary Error") == System.Windows.Forms.DialogResult.OK)
                {
                    
                }
                Application.Exit();
                return false;
            }
            return true;
        }
    }
}
