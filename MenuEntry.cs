using System;

public class MenuObject
{
    private String name;
    private double max = 0;
    private double min = 0;
    int id;
    private double step = 1;
    double value = 0;
    private bool toggleable;
    bool toggled = false;

    public MenuObject(String name, int id, bool toggleable)
    {
        this.name = name;
        this.id = id;
        this.toggleable = toggleable;
    }

    public MenuObject(String name, double max, double min, int id, bool toggleable)
	{
        this.name = name;
        this.max = max;
        this.min = min;
        this.id = id;
        this.toggleable = toggleable;
    }

    public MenuObject(String name, double max, double min, int id, double defaultVal, bool toggleable)
    {
        this.name = name;
        this.max = max;
        this.min = min;
        this.id = id;
        this.value = defaultVal;
        this.toggleable = toggleable;
    }

    public MenuObject(String name, double max, double min, int id, double defaultVal, double step, bool toggleable)
    {
        this.name = name;
        this.max = max;
        this.min = min;
        this.id = id;
        this.value = defaultVal;
        this.step = step;
        this.toggleable = toggleable;
    }

    public void incrVal()
    {
        if(!value + step > max){
            value = +step;
        }
    }

    public void decrVal()
    {
        if (!value - step < min)
        {
            value =- step;
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

    public void toggle()
    {
         toggled = !toggled;
    }

}
