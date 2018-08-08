using System;

public class ToggleObject
{

    private String name;
    int id;
    bool toggled;

	public ToggleObject(String name, int id)
	{
        this.name = name;
        this.id = id;
	}

    public void toggle()
    {
        toggled = !toggled;
    }

    public String listVal()
    {
        if (toggled)
        {
            return name + " : <ON>";
        }
        else
        {
            return name +" : <OFF>";
        }
    }

}
