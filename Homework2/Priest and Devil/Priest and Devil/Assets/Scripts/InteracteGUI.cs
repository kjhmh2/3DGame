using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class InteracteGUI : MonoBehaviour {
    Objects ObjectsController;
    static int GameState = 0;
    public int SetState { get { return GameState; } set { GameState = value; } }

	void Start () {
        ObjectsController = SSDirector.getInstance().currentScenceController as Objects;
    }

    private void OnGUI()
    {
        //GameOver
        if (GameState == 1)
        {
            GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 70, 300, 300), "Gameover!");
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2 - 15, 140, 70), "Restart"))
            {
                GameState = 0;
                ObjectsController.Restart();
            }
        }
        //Win the Game
        else if (GameState == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 70 , 100, 50), "Win!");
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2 - 15, 140, 70), "Restart"))
            {
                GameState = 0;
                ObjectsController.Restart();
            }
        }
    }
}