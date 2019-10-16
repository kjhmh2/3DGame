using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class FirstSceneController : MonoBehaviour, ISceneController, UserAction
{
    int score = 0;
    int round = 1;
    int tral = 0;
    int health = 5;
    bool start = false;
    bool gameOver = false;
    public bool PhysicManager = false;
    IActionManager Manager;
    DiskFactory DF;

    void Awake()
    {
        SSDirector director = SSDirector.getInstance();
        director.currentScenceController = this;
        DF = DiskFactory.DF;
        //Manager = GetComponent<CCActionManager>();
        if (PhysicManager)
        {
            Manager = this.gameObject.AddComponent<CCPhysisActionManager>() as IActionManager;
        }
        else
        {
            Manager = this.gameObject.AddComponent<CCActionManager>() as IActionManager;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    int count = 0;
    void Update()
    {
        if (health <= 0)
            gameOver = true;
        if (gameOver)
            return;
        if (start == true)
        {
            count ++;
            if (count >= 80)
            {
                count = 0;

                if (DF == null)
                {
                    Debug.LogWarning("DF is NUll!");
                    return;
                }
                tral ++;
                Disk d = DF.GetDisk(round);
                if (Manager == null)
                {
                    Debug.LogWarning("Manager is NULL!");
                    return;
                }
                Manager.PlayDisk(d);
                //Manager.MoveDisk(d);
                if (tral == 10)
                {
                    round ++;
                    tral = 0;
                }
            }
        }
    }

    public void LoadResources()
    {
 
    }

    public void Hit(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i ++)
        {
            RaycastHit hit = hits[i];

            if (hit.collider.gameObject.GetComponent<Disk>() != null)
            {
                Color c = hit.collider.gameObject.GetComponent<Renderer>().material.color;
                if (c == Color.yellow)
                    score += 1;
                if (c == Color.red)
                    score += 2;
                if (c == Color.black)
                    score += 3;
                GameObject explosion = Instantiate(Resources.Load<GameObject>("Prefabs/ParticleSystem"), hit.collider.gameObject.transform.position, Quaternion.identity);
                explosion.GetComponent<ParticleSystem>().Play();
                Object.Destroy(explosion, 0.1f);
                hit.collider.gameObject.transform.position = new Vector3(0, -400, -1);
            }
        }
    }

    public int GetScore()
    {
        return score;
    }

    public int GetRound()
    {
        return round;
    }

    public int GetHealth()
    {
        return health;
    }

    public void ReduceHealth()
    {
        health -= 1;
        if (health < 0)
            health = 0;
    }

    public void GameOver()
    {
        gameOver = true;
    }

    public bool RoundStop()
    {
        if (round > 3 || health <= 0)
        {
            start = false;
            return Manager.IsAllFinished();
        }
        else
            return false;
    }

    public void Restart()
    {
        score = 0;
        round = 1;
        health = 5;
        start = true;
        gameOver = false;
    }
}