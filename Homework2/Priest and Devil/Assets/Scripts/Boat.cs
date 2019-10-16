using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat
{
    readonly GameObject boat;
    readonly Move Moving;
    readonly Vector3 startPosition = new Vector3(5, 1, 0);
    readonly Vector3 destPosition = new Vector3(-5, 1, 0);
    readonly Vector3[] startPos;
    readonly Vector3[] destPos;

    int state;
    GameObjects[] passengers = new GameObjects[2];

    public Boat()
    {
        state = 1;

        startPos = new Vector3[] { new Vector3(4.5F, 1.5F, 0), new Vector3(5.5F, 1.5F, 0) };
        destPos = new Vector3[] { new Vector3(-5.5F, 1.5F, 0), new Vector3(-4.5F, 1.5F, 0) };

        boat = Object.Instantiate(Resources.Load("Boat", typeof(GameObject)), startPosition, Quaternion.identity, null) as GameObject;
        boat.name = "boat";

        Moving = boat.AddComponent(typeof(Move)) as Move;
        boat.AddComponent(typeof(ClickGUI));
    }

    //find an empty seat
    public int getEmptyIndex()
    {
        for (int i = 0; i < passengers.Length; i ++)
        {
            if (passengers[i] == null)
                return i;
        }
        return -1;
    }

    //whether the boat is empty
    public bool isEmpty()
    {
        for (int i = 0; i < passengers.Length; i ++)
        {
            if (passengers[i] != null)
                return false;
        }
        return true;
    }

    //calculate the position
    public Vector3 getEmptyPosition()
    {
        Vector3 pos;
        int emptyIndex = getEmptyIndex();
        if (state == -1)
            pos = destPos[emptyIndex];
        else
            pos = startPos[emptyIndex];
        return pos;
    }

    public void GetOnBoat(GameObjects ObjectControl)
    {
        int index = getEmptyIndex();
        passengers[index] = ObjectControl;
    }

    public GameObjects GetOffBoat(string name)
    {
        for (int i = 0; i < passengers.Length; i ++)
        {
            if (passengers[i] != null && passengers[i].getName() == name)
            {
                GameObjects charactorCtrl = passengers[i];
                passengers[i] = null;
                return charactorCtrl;
            }
        }
        Debug.Log("Cant find passenger in boat: " + name);
        return null;
    }

    public GameObject getGameobj()
    {
        return boat;
    }

    public int getState()
    {
        return state;
    }

    public int[] GetobjectsNumber()
    {
        int[] cnt = { 0, 0 };// [0]->priest, [1]->devil
        for (int i = 0; i < passengers.Length; i++)
        {
            if (passengers[i] == null)
                continue;
            if (passengers[i].getType() == 0)
                cnt[0]++;
            else
                cnt[1]++;
        }
        return cnt;
    }

    public void Move()
    {
        if (state == -1)
        {
            Moving.SetDestination(startPosition);
            state = 1;
        }
        else
        {
            Moving.SetDestination(destPosition);
            state = -1;
        }
    }

    public void reset()
    {
        Moving.Reset();
        if (state == -1)
            Move();
        passengers = new GameObjects[2];
    }
}