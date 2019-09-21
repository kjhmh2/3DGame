using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjects
{
    readonly GameObject Instance;
    readonly Move Move;
    readonly ClickGUI clickGUI;
    readonly int type;

    bool OnBoat = false;
    Coast coast;


    public GameObjects(string Type)
    {
        // is priest
        if (Type == "priest")
        {
            Instance = Object.Instantiate(Resources.Load("Priest", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
            type = 0;
        }
        // is devil
        else
        {
            Instance = Object.Instantiate(Resources.Load("Devil", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
            type = 1;
        }
        Move = Instance.AddComponent(typeof(Move)) as Move;

        clickGUI = Instance.AddComponent(typeof(ClickGUI)) as ClickGUI;
        clickGUI.setController(this);
    }

    public void setName(string name)
    {
        Instance.name = name;
    }

    public void setPosition(Vector3 position)
    {
        Instance.transform.position = position;
    }

    public void moveToPosition(Vector3 position)
    {
        Move.SetDestination(position);
    }

    public int getType()
    {   // 0->priest, 1->devil
        return type;
    }

    public string getName()
    {
        return Instance.name;
    }

    public void getOnBoat(Boat boat)
    {
        coast = null;
        Instance.transform.parent = boat.getGameobj().transform;
        OnBoat = true;
    }

    public void getOnCoast(Coast Coast)
    {
        coast = Coast;
        Instance.transform.parent = null;
        OnBoat = false;
    }

    public bool isOnBoat()
    {
        return OnBoat;
    }

    public Coast getCoastController()
    {
        return coast;
    }

    public void reset()
    {
        Move.Reset();
        coast = (SSDirector.getInstance().currentScenceController as Controller).fromCoast;
        getOnCoast(coast);
        setPosition(coast.getEmptyPosition());
        coast.getOnCoast(this);
    }
}