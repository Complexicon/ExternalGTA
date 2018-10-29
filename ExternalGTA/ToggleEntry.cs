namespace ExternalGTA
{
	public class ToggleEntry : Entry
	{

		public bool state = false;
		private string val = "OFF";

		public ToggleEntry(HackID id, string displayName) : base(id, displayName){}

		public void toggle()
		{
			state = !state;
			val = state ? "ON" : "OFF";

		}

		public override string getValue()
		{
			return displayName + ": <" + val + ">";
		}

	}
}
