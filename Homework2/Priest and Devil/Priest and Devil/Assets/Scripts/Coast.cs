using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coast
{
    readonly GameObject coast;
    readonly Vector3 startPos = new Vector3(9, 1, 0);
    readonly Vector3 destPos = new Vector3(-9, 1, 0);
    readonly Vector3[] positions;
    readonly int state;

    GameObjects[] stores;

    public Coast(string str)
    {
        positions = new Vector3[] {new Vector3(6.5F,2.25F,0), new Vector3(7.5F,2.25F,0), new Vector3(8.5F,2.25F,0), new Vector3(9.5F,2.25F,0), new Vector3(10.5F,2.25F,0), new Vector3(11.5F,2.25F,0)};

        stores = new GameObjects[6];

        if (str == "from")
        {
            coast = Object.Instantiate(Resources.Load("Coast", typeof(GameObject)), startPos, Quaternion.identity, null) as GameObject;
            coast.name = "from";
            state = 1;
        }
        else if (str == "to")
        {
            coast = Object.Instantiate(Resources.Load("Coast", typeof(GameObject)), destPos, Quaternion.identity, null) as GameObject;
            coast.name = "to";
            state = -1;
        }
    }

    public int getEmptyIndex()
    {
        for (int i = 0; i < stores.Length; i ++)
        {
            if (stores[i] == null)
                return i;
        }
        return -1;
    }

    public Vector3 getEmptyPosition()
    {
        Vector3 pos = positions[getEmptyIndex()];
        pos.x *= state;
        return pos;
    }

    public void getOnCoast(GameObjects ObjectControl)
    {
        int index = getEmptyIndex();
        stores[index] = ObjectControl;
    }

    public GameObjects getOffCoast(string passenger_name)
    {
        for (int i = 0; i < stores.Length; i ++)
        {
            if (stores[i] != null && stores[i].getName() == passenger_name)
            {
                GameObjects charactorCtrl = stores[i];
                stores[i] = null;
                return charactorCtrl;
            }
        }
        Debug.Log("cant find passenger on coast: " + passenger_name);
        return null;
    }

    //return the game state
    public int get_State()
    {
        return state;
    }

    //count the priests and devils
    public int[] GetobjectsNumber()
    {
        int[] cnts = {0, 0};
        for (int i = 0; i < stores.Length; i++)
        {
            if (stores[i] == null)
                continue;
            if (stores[i].getType() == 0)
                cnts[0]++;
            else
                cnts[1]++;
        }
        return cnts;
    }

    //reset the objects
    public void reset()
    {
        stores = new GameObjects[6];
    }
}