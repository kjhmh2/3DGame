using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    private int emptyPlace = 9;
    private int turn = 1;
    private int[,] chess = new int[3, 3];
    private Texture CrossImg;
    private Texture CircleImg;


    void Awake()
    {
        CrossImg = Resources.Load("Cross") as Texture;
        CircleImg = Resources.Load("Circle") as Texture;
    }

    // Use this for initialization
    void Start()
    {
        emptyPlace = 9;
        turn = 1;
        for (int i = 0; i < 3; ++ i)
            for (int j = 0; j < 3; ++ j)
                chess[i, j] = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        GUI.skin.button.fontSize = 20;
        GUI.skin.label.fontSize = 20;
        if (GUI.Button(new Rect(225, 230, 100, 35), "Reset"))
            Start();
        int res = Win();
        if (res == 1)
            GUI.Label(new Rect(225, 20, 100, 50), "X wins");
        else if (res == 2)
            GUI.Label(new Rect(225, 20, 100, 50), "O wins");
        else if (res == 3)
            GUI.Label(new Rect(250, 20, 100, 50), "Tie");
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                if (chess[i, j] == 1)
                    GUI.Button(new Rect(i * 50 + 200, j * 50 + 60, 50, 50), CrossImg);
                if (chess[i, j] == 2)
                    GUI.Button(new Rect(i * 50 + 200, j * 50 + 60, 50, 50), CircleImg);
                if (GUI.Button(new Rect(i * 50 + 200, j * 50 + 60, 50, 50), ""))
                {
                    if (res == 0)
                    {
                        if (turn == 1)
                            chess[i, j] = 1;
                        if (turn == 2)
                            chess[i, j] = 2;
                        emptyPlace--;
                        if (emptyPlace % 2 == 1)
                            turn = 1;
                        else
                            turn = 2;
                    }
                }
            }
        }
    }

    int Win()
    {
        // 0 for not finished & 1 for "X" wins & 2 for "O" wins & 3 for a tie
        int center = chess[0, 0];
        if (center != 0)
        {
            if ((center == chess[0, 1] && center == chess[0, 2]) ||
                (center == chess[1, 0] && center == chess[2, 0]))
                return center;
        }
        center = chess[1, 1];
        if (center != 0)
        {
            if ((center == chess[0, 0] && center == chess[2, 2]) ||
                (center == chess[0, 1] && center == chess[2, 1]) ||
                (center == chess[1, 0] && center == chess[1, 2]) ||
                (center == chess[0, 2] && center == chess[2, 0]))
                return center;
        }
        center = chess[2, 2];
        if (center != 0)
        {
            if ((center == chess[2, 0] && center == chess[2, 1]) ||
                (center == chess[0, 2] && center == chess[1, 2]))
                return center;
        }
        if (emptyPlace == 0)
            return 3;
        else
            return 0;
    }
}

