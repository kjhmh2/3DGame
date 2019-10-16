using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFlyActionManager : SSActionManager
{
    private ArrowFlyAction fly;
    public FirstSceneController sceneController;

    protected void Start()
    {
        sceneController = (FirstSceneController)SSDirector.GetInstance().CurrentScenceController;
        sceneController.actionManager = this;
    }

    public void ArrowFly(GameObject arrow, Vector3 wind)
    {
        fly = ArrowFlyAction.GetSSAction(wind);
        this.RunAction(arrow, fly, this);
    }
}
