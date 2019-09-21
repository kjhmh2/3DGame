using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class ClickGUI : MonoBehaviour
{
    Objects UserAcotionController;
    GameObjects GameObjectsInScene;

    public void setController(GameObjects characterCtrl)
    {
        GameObjectsInScene = characterCtrl;
    }

    void Start()
    {
        UserAcotionController = SSDirector.getInstance().currentScenceController as Objects;
    }

    void OnMouseDown()
    {
        if (gameObject.name == "boat")
        {
            UserAcotionController.MoveBoat();
        }
        else
        {
            UserAcotionController.ObjectIsClicked(GameObjectsInScene);
        }
    }
}
