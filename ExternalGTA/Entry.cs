namespace ExternalGTA
{
	public class Entry
	{

		protected HackID hackID = HackID.DUMMY;
		public string displayName = "Dummy";

		public Entry(HackID id, string displayName)
		{
			this.hackID = id;
			this.displayName = displayName;
		}

		public HackID getID()
		{
			return hackID;
		}

		public virtual string getValue()
		{
			return displayName;
		}

	}
}
