using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class Controller : MonoBehaviour, Scene, Objects
{
    InteracteGUI UserGUI;
    public Boat boat;
    public Coast fromCoast;
    public Coast toCoast;
    private GameObjects[] GameObjects;

    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.currentScenceController = this;
        UserGUI = gameObject.AddComponent<InteracteGUI>() as InteracteGUI;
        GameObjects = new GameObjects[6];
        LoadResources();
    }

    public void LoadResources()
    {
        fromCoast = new Coast("from");
        toCoast = new Coast("to");
        boat = new Boat();
        GameObject water = Instantiate(Resources.Load("Water", typeof(GameObject)), new Vector3(0, 0.5F, 0), Quaternion.identity, null) as GameObject;
        water.name = "water";
        for (int i = 0; i < 3; i ++)
        {
            GameObjects s = new GameObjects("priest");
            s.setName("priest" + i);
            s.setPosition(fromCoast.getEmptyPosition());
            s.getOnCoast(fromCoast);
            fromCoast.getOnCoast(s);
            GameObjects[i] = s;
        }

        for (int i = 0; i < 3; i ++)
        {
            GameObjects s = new GameObjects("devil");
            s.setName("devil" + i);
            s.setPosition(fromCoast.getEmptyPosition());
            s.getOnCoast(fromCoast);
            fromCoast.getOnCoast(s);
            GameObjects[i + 3] = s;
        }
    }

    public void ObjectIsClicked(GameObjects Objects)
    {
        if (Objects.isOnBoat())
        {
            Coast getCoast;
            if (boat.getState() == -1)
                getCoast = toCoast;
            else
                getCoast = fromCoast;
            boat.GetOffBoat(Objects.getName());
            Objects.moveToPosition(getCoast.getEmptyPosition());
            Objects.getOnCoast(getCoast);
            getCoast.getOnCoast(Objects);
        }
        else
        {
            Coast getCoast = Objects.getCoastController(); // obejects on coast

            if (boat.getEmptyIndex() == -1)
            {
                return;
            }

            if (getCoast.get_State() != boat.getState())
            {// boat is not on the side of character

                return;
            }

            getCoast.getOffCoast(Objects.getName());
            Objects.moveToPosition(boat.getEmptyPosition());
            Objects.getOnBoat(boat);
            boat.GetOnBoat(Objects);
        }
        UserGUI.SetState = Check();
    }

    public void MoveBoat()
    {
        if (boat.isEmpty()) return;
        boat.Move();
        UserGUI.SetState = Check();
    }

    int Check()
    {
        int from_priest = 0;
        int from_devil = 0;
        int to_priest = 0;
        int to_devil = 0;

        int[] fromCount = fromCoast.GetobjectsNumber();
        from_priest += fromCount[0];
        from_devil += fromCount[1];

        int[] toCount = toCoast.GetobjectsNumber();
        to_priest += toCount[0];
        to_devil += toCount[1];

        // wins
        if (to_priest + to_devil == 6)
            return 2;

        int[] boatCount = boat.GetobjectsNumber();
        // on destination
        if (boat.getState() == -1)
        {
            to_priest += boatCount[0];
            to_devil += boatCount[1];
        }
        // at start
        else
        {
            from_priest += boatCount[0];
            from_devil += boatCount[1];
        }
        // lose
        if ((from_priest < from_devil && from_priest > 0) || (to_priest < to_devil && to_priest > 0))
            return 1;
        return 0;           // not finish
    }

    public void Restart()
    {
        boat.reset();
        fromCoast.reset();
        toCoast.reset();
        for (int i = 0; i < GameObjects.Length; i ++)
            GameObjects[i].reset();
    }
}

