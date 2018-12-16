using System;
using System.Collections.Generic;

namespace ExternalGTA
{
    public class Menu
    {

		int strLength = 40;

        List<Entry> entryList = new List<Entry>();
        public List<String> menuList = new List<String>();

        public String menuDisplayName;

        public Menu(String name)
        {
			this.menuDisplayName = "► " + name;
		}

        public void addEntry(Entry e)
        {
            entryList.Add(e);
        }

        public Entry getEntryAt(int index)
        {
            return entryList[index];
        }

        public void updateList()
        {
            List<String> tmpList = new List<String>();
            foreach (Entry e in entryList)
            {
                tmpList.Add(e.getValue());
            }

            menuList = tmpList;

        }

    }
}
