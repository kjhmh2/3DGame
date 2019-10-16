using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using UnityEngine.UI;

public class InterfaceGUI : MonoBehaviour
{
    UserAction UserActionController;
    public GameObject t;
    bool isStart = false;
    bool gameOver = false;
    float S;
    float Now;
    int round = 1;

    // Use this for initialization
    void Start()
    {
        UserActionController = SSDirector.getInstance().currentScenceController as UserAction;
        S = Time.time;
    }

    private void OnGUI()
    {
        if (gameOver)
        {
            isStart = false;
            GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 50, 100, 20), "GameOver!");
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 30, 100, 50), "Confirm"))
            {
                gameOver = false;
                return;
            }
            return;
        }

        if (!isStart)
            S = Time.time;
        GUI.Label(new Rect(10, 10, 200, 20), "Time:  " + ((int)(Time.time - S)).ToString());
        GUI.Label(new Rect(10, 30, 200, 20), "Round:  " + round);
        GUI.Label(new Rect(10, 50, 200, 20), "Score: " + UserActionController.GetScore().ToString());
        GUI.Label(new Rect(10, 70, 200, 20), "Health:  " + UserActionController.GetHealth().ToString());

        if (!isStart && GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 30, 100, 50), "Start"))
        {
            S = Time.time;
            isStart = true;
            UserActionController.Restart();
        }

        if (isStart)
        {
            round = UserActionController.GetRound();
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 pos = Input.mousePosition;
                UserActionController.Hit(pos);
            }
            if (round > 3)
            {
                round = 3;
                if (UserActionController.RoundStop())
                {
                    isStart = false;
                }
            }
        }

        if (isStart && UserActionController.GetHealth() == 0)
        {
            gameOver = true;
        }
    }
}
