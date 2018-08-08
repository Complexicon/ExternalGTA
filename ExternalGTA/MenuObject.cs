using System;
using System.Collections.Generic;

namespace ExternalGTA
{
    public class MenuObject
    {

        List<MenuEntry> entryList = new List<MenuEntry>();
        public List<String> menuList = new List<String>();

        public String menuDisplayName;

        public MenuObject(String name)
        {
            this.menuDisplayName = name;
        }

        public void addEntry(MenuEntry e)
        {
            entryList.Add(e);
        }

        public MenuEntry getEntryAt(int index)
        {
            return entryList[index];
        }

        public void updateList()
        {
            List<String> tmpList = new List<String>();
            foreach (MenuEntry e in entryList)
            {
                tmpList.Add(e.listValue());
            }

            menuList = tmpList;

        }

    }
}
