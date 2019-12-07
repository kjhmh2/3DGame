using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITank : Tank
{
    public delegate void recycle(GameObject tank);
    public static event recycle recycleEvent;

    private Vector3 target;
    private bool gameover;

    private int destPoint = 0;
    private NavMeshAgent agent;
    private bool isPatrol = false;

    // move points
    private static Vector3[] points =
    {
        new Vector3(35f, 0, 0), new Vector3(40f, 0, 39), new Vector3(15f, 0, 39),
        new Vector3(15f, 0, 21), new Vector3(0, 0, 0), new Vector3(-20, 0, 0.3f), new Vector3(-20, 0, 35f),
        new Vector3(-35f, 0, 40f), new Vector3(-40f, 0, 10f), new Vector3(-40f, 0, -25f), new Vector3(-15f, 0, -40f),
        new Vector3(20f, 0, -40f), new Vector3(40f, 0, -20f)
    };

    private void Awake()
    {
        destPoint = UnityEngine.Random.Range(0, 13);
    }

    // Use this for initialization
    void Start ()
    {
        setHp(100f);
        StartCoroutine(shoot());
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update ()
    {
        gameover = GameDirector.getInstance().currentSceneController.isGameOver();
        if (!gameover)
        {
            target = GameDirector.getInstance().currentSceneController.getPlayerPos();
            // have been destoryed
            if (getHp() <= 0 && recycleEvent != null)
            {
                recycleEvent(this.gameObject);
            }
            else
            {
                // approach the player
                if (Vector3.Distance(transform.position, target) <= 30)
                {
                    isPatrol = false;
                    agent.autoBraking = true;
                    agent.SetDestination(target);
                }
                // patrol
                else
                {
                    patrol();
                }
            }
        }
        else
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.velocity = Vector3.zero;
            agent.ResetPath();
        }
    }

    // shoot
    private IEnumerator shoot()
    {
        while (!gameover)
        {
            for (float i = 1; i > 0; i -= Time.deltaTime)
            {
                yield return 0;
            }
            // start shooting
            if (Vector3.Distance(transform.position, target) < 20)
            {
                GameObjectFactory mf = Singleton<GameObjectFactory>.Instance;
                GameObject bullet = mf.getBullet(tankType.Enemy);
                bullet.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z) + transform.forward * 1.5f;
                bullet.transform.forward = transform.forward;

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(bullet.transform.forward * 20, ForceMode.Impulse);
            }
        }
    }

    // patrol
    private void patrol()
    {
        if (isPatrol)
        {
            if(!agent.pathPending && agent.remainingDistance < 0.5f)
                GoOn();
        }
        else
        {
            agent.autoBraking = false;
            GoOn();
        }
        isPatrol = true;
    }

    // move to the next point
    private void GoOn()
    {
        agent.SetDestination(points[destPoint]);
        destPoint = (destPoint + 1) % points.Length;
    }
}
