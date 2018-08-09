using System;

namespace ExternalGTA
{
    public class MenuEntry
    {
        private String name;
        private double max = 0;
        private double min = 0;
        private double step = 1;
        public double value = 0;
        private bool toggleable;
        public bool toggled = false;
        public HackList hack = HackList.DUMMY;

        public MenuEntry(String name, bool toggleable, HackList h)
        {
            this.name = name;
            this.toggleable = toggleable;
            this.hack = h;
        }

        public MenuEntry(String name, double max, double min, bool toggleable, HackList h)
        {
            this.name = name;
            this.max = max;
            this.min = min;
            this.toggleable = toggleable;
            this.hack = h;
            this.value = min;
        }

        public MenuEntry(String name, double max, double min, double defaultVal, bool toggleable, HackList h)
        {
            this.name = name;
            this.max = max;
            this.min = min;
            this.value = defaultVal;
            this.toggleable = toggleable;
            this.hack = h;
        }

        public MenuEntry(String name, double max, double min, double defaultVal, double step, bool toggleable, HackList h)
        {
            this.name = name;
            this.max = max;
            this.min = min;
            this.value = defaultVal;
            this.step = step;
            this.toggleable = toggleable;
            this.hack = h;
        }

        public void incrVal()
        {
            if (!(value + step > max))
            {
                value += step;
            }
        }

        public void decrVal()
        {
            if (!(value - step < min))
            {
                value -= step;
            }
        }

        public String listValue()
        {
            if (!toggleable)
            {
                return name + " <" + value + ">";
            }
            else
            {
                if (toggled)
                {
                    return name + " : <ON>";
                }
                else
                {
                    return name + " : <OFF>";
                }
            }
        }

        public bool isToggleable()
        {
            return toggleable;
        }

        public void toggle()
        {
            toggled = !toggled;
        }

    }
}