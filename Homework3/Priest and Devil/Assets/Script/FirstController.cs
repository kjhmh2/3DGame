using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class FirstController : MonoBehaviour, ISceneController, UserAction
{
    InteracteGUI UserGUI;
    public CoastController fromCoast;
    public CoastController toCoast;
    public BoatController boat;
    private GameObjects[] GameObjects;

    private FirstSceneActionManager FCActionManager;

    void Start()
    {
        FCActionManager = GetComponent<FirstSceneActionManager>();
    }

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
        fromCoast = new CoastController("from");
        toCoast = new CoastController("to");
        boat = new BoatController();
        GameObject water = Instantiate(Resources.Load("Perfabs/Water", typeof(GameObject)), new Vector3(0, 1F, 0), Quaternion.identity, null) as GameObject;
        //water.transform.Rotate(90, 0, 0);
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
        if (FCActionManager.Complete == SSActionEventType.Started)
            return;
        if (Objects.isOnBoat())
        {
            CoastController getCoast;
            if (boat.get_State() == -1)
                getCoast = toCoast;
            else
                getCoast = fromCoast;
            boat.GetOffBoat(Objects.getName());
            FCActionManager.GameObjectsMove(Objects, getCoast.getEmptyPosition());
            Objects.getOnCoast(getCoast);
            getCoast.getOnCoast(Objects);
        }
        else
        {
            CoastController getCoast = Objects.getCoastController();
            if (boat.getEmptyIndex() == -1)
                return;
            if (getCoast.get_State() != boat.get_State())
                return;
            getCoast.getOffCoast(Objects.getName());
            FCActionManager.GameObjectsMove(Objects, boat.getEmptyPosition());
            Objects.getOnBoat(boat);
            boat.GetOnBoat(Objects);
        }
        Judger judge = new Judger(fromCoast, toCoast, boat);
        UserGUI.SetState = judge.GameState();
    }

    public void MoveBoat()
    {
        if (FCActionManager.Complete == SSActionEventType.Started || boat.isEmpty())
            return;
        FCActionManager.BoatMove(boat);
        Judger judge = new Judger(fromCoast, toCoast, boat);
        UserGUI.SetState = judge.GameState();
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