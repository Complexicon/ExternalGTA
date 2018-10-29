using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ExternalGTA
{
    public class Hacks : Memory
    {

        Thread wantedThread;
        Thread cheatThread;
		Thread carThread;
		Thread weapThread;
		Thread godThread;
		bool wantedTrRunning = false;
        bool cheatTrRunning = false;
		bool carTrRunning = false;
		bool weapTrRunning = false;
		bool godTrRunning = false;

		bool superJump;
        bool explosiveAmmo;

        int targetWantedLevel = 0;

		float targetGravity = 9.8F;
		int targetAcceleration = 1;
		bool carGod = false;
		bool seatbelt = false;

		bool disableSpread = false;
		bool disableRecoil = false;

		bool enableGod = false;

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

		int[] oCar = new int[] { 0x08, 0xd28 };
        int[] oCarGodmode = new int[] { 0x08, 0xd28, 0x189 };
        int[] oCarGravity = new int[] { 0x08, 0xd28, 0xBCC };
        int[] oCarAcceleration = new int[] { 0x08, 0xd28, 0x8C8, 0x4C };

		int[] oWeaponSpread = new int[] { 0x08, 0x10C8, 0x20, 0x74 };
		int[] oWeaponRecoil = new int[] { 0x08, 0x10C8, 0x20, 0x2D8 };
		int[] oFirerate = new int[] { 0x08, 0x10C8, 0x20, 0x134 };

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

		//Weapon Spread
		public void setSpread(bool toggle)
		{
			long pointer = GetPointerAddress(BaseAddress + WorldPTR, oWeaponSpread);
			WriteFloat(pointer, toggle ? 0F : 1F);
		}

		//Weapon Spread
		public void setRecoil(bool toggle)
		{
			long pointer = GetPointerAddress(BaseAddress + WorldPTR, oWeaponRecoil);
			WriteFloat(pointer, toggle ? 0F : 1F);
		}

		public void setRapidfire(bool toggle)
		{
			long pointer = GetPointerAddress(BaseAddress + WorldPTR, oFirerate);
			WriteFloat(pointer, toggle ? 0.01F : 1F);
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

					long pointer = GetPointerAddress(BaseAddress + WorldPTR, oPlayerFlags);

					while (cheatTrRunning)
                    {

						Thread.Sleep(1);

						if (superJump && explosiveAmmo)
						{
							WriteBytes(pointer, new byte[] { 0x48 });
							continue;
						}

						if (superJump)
                        {
                            WriteBytes(pointer, new byte[] { 0x40 });
							continue;
                        }

						if (explosiveAmmo)
						{
							WriteBytes(pointer, new byte[] { 0x08 });
						}

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

		public void startGodThread()
		{
			if (!godTrRunning)
			{
				godTrRunning = true;
				godThread = new Thread(() =>
				{

					long pointer = GetPointerAddress(BaseAddress + WorldPTR, oPlayerFlags);

					while (godTrRunning)
					{

						setGodmode(enableGod);
						Thread.Sleep(100);

					}

				})
				{
					IsBackground = true
				};
				godThread.SetApartmentState(ApartmentState.STA);
				godThread.Name = "GodThread";
				godThread.Start();
			}
		}

		public void startAntiWantedThread()
		{
			if (!wantedTrRunning)
			{
				wantedTrRunning = true;
				wantedThread = new Thread(() =>
				{
					while (wantedTrRunning)
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

		public void startCarThread()
		{
			if (!carTrRunning)
			{
				carTrRunning = true;
				carThread = new Thread(() =>
				{
					while (carTrRunning)
					{

						setGravity(targetGravity);
						setAccel(targetAcceleration);
						setCargod(carGod);
						setSeatbelt(seatbelt);

						Thread.Sleep(100);
					}

				})
				{
					IsBackground = true
				};
				carThread.SetApartmentState(ApartmentState.STA);
				carThread.Name = "CarThread";
				carThread.Start();
			}
		}

		public void startWeapThread()
		{
			if (!weapTrRunning)
			{
				weapTrRunning = true;
				weapThread = new Thread(() =>
				{
					while (weapTrRunning)
					{
						setRecoil(disableRecoil);
						setSpread(disableSpread);

						Thread.Sleep(100);
					}

				})
				{
					IsBackground = true
				};
				weapThread.SetApartmentState(ApartmentState.STA);
				weapThread.Name = "WeapThread";
				weapThread.Start();
			}
		}

		public void runHack(Entry entry)
        {
            Entry e = entry;
			HackID h = e.getID();

			if (e is ToggleEntry)
			{
				ToggleEntry tEnt = e as ToggleEntry;

				switch (h)
				{
					case HackID.GODMODE:
						enableGod = tEnt.state;
						if (tEnt.state) startGodThread();
						else godTrRunning = false;
						break;
					case HackID.OTR:
						setHealth(tEnt.state);
						break;
					case HackID.FIRERATE:
						setRapidfire(tEnt.state);
						tEnt.toggle();
						break;
					case HackID.NEVERWANTED:
						if (tEnt.state)startAntiWantedThread();
						else wantedTrRunning = false;
						break;
					case HackID.CARGOD:
						if (tEnt.state) startCarThread();
						carGod = tEnt.state;
						break;
					case HackID.SPREAD:
						if (tEnt.state) startWeapThread();
						disableSpread = tEnt.state;
						break;
					case HackID.RECOIL:
						if (tEnt.state) startWeapThread();
						disableRecoil = tEnt.state;
						break;
					case HackID.SEATBELT:
						if (tEnt.state) startCarThread();
						seatbelt = tEnt.state;
						break;
					case HackID.INFAMMO:
						setAmmo(tEnt.state);
						break;
					case HackID.SUPERJUMP:
						if (tEnt.state) startCheatThread();
						superJump = tEnt.state;
						break;
					case HackID.EXPLOSIVEAMMO:
						if (tEnt.state) startCheatThread();
						explosiveAmmo = tEnt.state;
						break;
					case HackID.DUMMY:
						break;
					default:
						break;
				}

			}
			else
			{
				ValueEntry valEnt = e as ValueEntry;

				switch (h)
				{
					case HackID.WANTEDLVL:
						setWanted((int)valEnt.val);
						break;
					case HackID.SPRINT:
						setSprint((int)valEnt.val);
						break;
					case HackID.SWIM:
						setSwim((int)valEnt.val);
						break;
					case HackID.ACCELERATION:
						startCarThread();
						targetAcceleration = (int)valEnt.val;
						break;
					case HackID.GRAVITY:
						startCarThread();
						targetGravity = (float)valEnt.val;
						break;
					default:
						break;
				}

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
