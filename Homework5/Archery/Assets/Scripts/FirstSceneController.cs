using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController
{
    public Camera mainCamera;
    public Camera childCamera;
    public ScoreRecorder recorder;
    public ArrowFactory arrowFactory;
    public ArrowFlyActionManager actionManager;
    public GameObject bow;
    private GameObject arrow;
    private GameObject target;
    private int[] targetscore = { 15, 30, 40, 50 };
    private int round = 0;
    private int arrowNum = 0;      

    private List<GameObject> store = new List<GameObject>();

    private bool gameOver = false;
    private bool gameStart = false;
    private string wind = "";
    private float windDirectX;      
    private float windDirectY;

    void Start()
    {
        SSDirector director = SSDirector.GetInstance();
        arrowFactory = Singleton<ArrowFactory>.Instance;
        recorder = Singleton<ScoreRecorder>.Instance;
        director.CurrentScenceController = this;
        actionManager = gameObject.AddComponent<ArrowFlyActionManager>() as ArrowFlyActionManager;
        LoadResources();
        mainCamera.GetComponent<CameraFlow>().bow = bow;
        windDirectX = Random.Range(-1, 1);
        windDirectY = Random.Range(-1, 1);
        CreateWind();
    }

    void Update()
    {
        if (gameStart)
        {
            for (int i = 0; i < store.Count; i++)
            {
                GameObject temp = store[i];
                if (temp.transform.position.z > 35 || store.Count > 5)
                {
                    arrowFactory.FreeArrow(store[i]);
                    store.Remove(store[i]);
                }
            }
        }
    }

    public void LoadResources()
    {
        bow = Instantiate(Resources.Load("Prefabs/Bow", typeof(GameObject))) as GameObject;
        target = Instantiate(Resources.Load("Prefabs/Target", typeof(GameObject))) as GameObject;
    }

    public void MoveBow(float offsetX, float offsetY)
    {
        if (gameOver || !gameStart)
            return;
        if (bow.transform.position.x > 5)
        {
            bow.transform.position = new Vector3(5, bow.transform.position.y, bow.transform.position.z);
            return;
        }
        else if (bow.transform.position.x < -5)
        {
            bow.transform.position = new Vector3(-5, bow.transform.position.y, bow.transform.position.z);
            return;
        }
        else if (bow.transform.position.y < -3)
        {
            bow.transform.position = new Vector3(bow.transform.position.x, -3, bow.transform.position.z);
            return;
        }
        else if (bow.transform.position.y > 5)
        {
            bow.transform.position = new Vector3(bow.transform.position.x, 5, bow.transform.position.z);
            return;
        }

        offsetY *= Time.deltaTime;
        offsetX *= Time.deltaTime;
        bow.transform.Translate(offsetX, 0, 0);
        bow.transform.Translate(0, offsetY, 0);
    }

    public void Shoot()
    {
        if ((!gameOver || gameStart) && arrowNum <= 10)
        {
            arrow = arrowFactory.GetArrow();
            store.Add(arrow);
            Vector3 wind = new Vector3(windDirectX, windDirectY, 0);
            actionManager.ArrowFly(arrow, wind);
            childCamera.GetComponent<ChildCamera>().StartShow();
            recorder.arrowNumber--;
            arrowNum++;
        }
    }

    public void CheckGamestatus()
    {

        if (recorder.arrowNumber <= 0 && recorder.score < recorder.targetScore)
        {
            gameOver = true;
            return;
        }
        else if (recorder.arrowNumber <= 0 && recorder.score >= recorder.targetScore)
        {
            round++;
            arrowNum = 0;
            if (round == 4)
                gameOver = true;
            for (int i = 0; i < store.Count; ++i)
            {
                arrowFactory.FreeArrow(store[i]);
            }
            store.Clear();
            recorder.arrowNumber = 10;
            recorder.score = 0;
            recorder.targetScore = targetscore[round];
        }
        windDirectX = Random.Range(-(round + 1), (round + 1));
        windDirectY = Random.Range(-(round + 1), (round + 1));
        CreateWind();
    }

    public void CreateWind()
    {
        string Horizontal = "", Vertical = "", level = "";
        if (windDirectX > 0)
        {
            Horizontal = "west";
        }
        else if (windDirectX <= 0)
        {
            Horizontal = "east";
        }
        if (windDirectY > 0)
        {
            Vertical = "South";
        }
        else if (windDirectY <= 0)
        {
            Vertical = "North";
        }

        if ((windDirectX + windDirectY) / 2 > -1 && (windDirectX + windDirectY) / 2 < 1)
        {
            level = "LV1";
        }
        else if ((windDirectX + windDirectY) / 2 > -2 && (windDirectX + windDirectY) / 2 < 2)
        {
            level = "LV2";
        }
        else if ((windDirectX + windDirectY) / 2 > -3 && (windDirectX + windDirectY) / 2 < 3)
        {
            level = "LV3";
        }
        else if ((windDirectX + windDirectY) / 2 > -5 && (windDirectX + windDirectY) / 2 < 5)
        {
            level = "LV4";
        }

        wind = Vertical + Horizontal + " " + level;
    }

    public void BeginGame()
    {
        gameStart = true;
    }

    public bool GetGameover()
    {
        return gameOver;
    }

    public int GetScore()
    {
        return recorder.score;
    }

    public int GetTargetScore()
    {
        return recorder.targetScore;
    }

    public int GetResidueNum()
    {
        return recorder.arrowNumber;
    }

    public string GetWind()
    {
        return wind;
    }

    public void Restart()
    {
        gameOver = false;
        recorder.arrowNumber = 10;
        recorder.score = 0;
        recorder.targetScore = 15;
        round = 0;
        arrowNum = 0;
        for (int i = 0; i < store.Count; ++ i)
        {
            arrowFactory.FreeArrow(store[i]);
        }
        store.Clear();
    }

}
