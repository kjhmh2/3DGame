using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController
{
    readonly GameObject boat;
    readonly Vector3 fromPosition = new Vector3(4.7F, 1, 0);
    readonly Vector3 toPosition = new Vector3(-4.7F, 1, 0);
    readonly Vector3[] startPos;
    readonly Vector3[] destPos;

    GameObjects[] passenger = new GameObjects[2];
    int State;
    int Speed = 10;
    int MovingState = -1;
    bool needChangeDirection = false;

    public BoatController()
    {
        State = 1;
        MovingState = -1;
        needChangeDirection = false;
        startPos = new Vector3[] { new Vector3(4.5F, 1.5F, 0), new Vector3(5.5F, 1.5F, 0) };
        destPos = new Vector3[] { new Vector3(-5.5F, 1.5F, 0), new Vector3(-4.5F, 1.5F, 0) };

        boat = Object.Instantiate(Resources.Load("Perfabs/Boat", typeof(GameObject)), fromPosition, Quaternion.identity, null) as GameObject;
        boat.transform.Rotate(-90, 180, 90);
        boat.name = "boat";

        boat.AddComponent(typeof(ClickGUI));
    }

    //find an empty seat
    public int getEmptyIndex()
    {
        for (int i = 0; i < passenger.Length; i ++)
        {
            if (passenger[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    //whether the boat is empty
    public bool isEmpty()
    {
        for (int i = 0; i < passenger.Length; i ++)
        {
            if (passenger[i] != null)
                return false;
        }
        return true;
    }

    //calculate the position
    public Vector3 getEmptyPosition()
    {
        Vector3 pos;
        int emptyIndex = getEmptyIndex();
        if (State == -1)
            pos = destPos[emptyIndex];
        else
            pos = startPos[emptyIndex];
        return pos;
    }

    public void GetOnBoat(GameObjects ObjectControl)
    {
        int index = getEmptyIndex();
        passenger[index] = ObjectControl;
    }

    public GameObjects GetOffBoat(string name)
    {
        for (int i = 0; i < passenger.Length; i ++)
        {
            if (passenger[i] != null && passenger[i].getName() == name)
            {
                GameObjects charactorCtrl = passenger[i];
                passenger[i] = null;
                return charactorCtrl;
            }
        }
        Debug.Log("Cannot find the passenger in boat: " + name);
        return null;
    }

    public GameObject GetGameObject()
    {
        return boat;
    }

    public void ChangeState()
    {
        State = -State;
        needChangeDirection = true;
    }

    public int get_State()
    {
        return State;
    }

    public int[] GetobjectsNumber()
    {
        // [0]->priest, [1]->devil
        int[] count = {0, 0};
        for (int i = 0; i < passenger.Length; i ++)
        {
            if (passenger[i] == null)
                continue;
            if (passenger[i].getType() == 0)
                count[0] ++;
            else
                count[1] ++;
        }
        return count;
    }

    public Vector3 GetDestination()
    {
        if (State == 1)
            return toPosition;
        else
            return fromPosition;
    }

    public int GetMoveSpeed()
    {
        return Speed;
    }

    public int GetMovingState()
    {
        return MovingState;
    }

    public void ChangeMovingstate()
    {
        MovingState = -MovingState;
    }

    //change boat's direction
    public void changeDirection()
    {
        if (needChangeDirection)
        {
            boat.transform.Rotate(0, 0, 180);
            needChangeDirection = false;
        }
    }

    public void reset()
    {
        State = 1;
        boat.transform.position = fromPosition;
        passenger = new GameObjects[2];
        MovingState = -1;
        needChangeDirection = false;
    }
}