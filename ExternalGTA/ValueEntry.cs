namespace ExternalGTA
{
	public class ValueEntry : Entry
	{

		private double max = 0;
		private double min = 0;
		private double step = 1;
		public double val = 0;

		public ValueEntry(HackID id, string displayName, double max, double min) : base(id, displayName)
		{
			this.min = min;
			this.max = max;
		}

		public ValueEntry(HackID id, string displayName, double max, double min, double step) : this(id, displayName, max, min)
		{
			this.step = step;
		}

		public ValueEntry(HackID id, string displayName, double max, double min, double step, double init) : this(id, displayName, max, min, step)
		{
			this.val = init;
		}

		public void incrVal()
		{
			if (val + step > max)
			{
				val = max;
			}
			else
			{
				val += step;
			}
		}

		public void decrVal()
		{
			if (val - step < min)
			{
				val = min;
			}
			else
			{
				val -= step;
			}
		}

		public override string getValue()
		{
			return displayName + ":   [" + val + "]";
		}

	}
}
