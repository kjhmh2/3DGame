using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFactory
{
    public static PatrolFactory PF = new PatrolFactory();
    private Dictionary<int, GameObject> store = new Dictionary<int, GameObject>();

    private PatrolFactory() {}

    int[] posX = { -7, 1, 7 };
    int[] posZ = { 8, 2, -8 };

    public Dictionary<int, GameObject> GetPatrol()
    {
        for (int i = 0; i < 3; ++ i)
        {
            for (int j = 0; j < 3; ++ j)
            {
                GameObject newPatrol = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Patrol"));
                newPatrol.AddComponent<Patrol>();
                newPatrol.transform.position = new Vector3(posX[j], 0, posZ[i]);
                newPatrol.GetComponent<Patrol>().block = i * 3 + j;
                newPatrol.SetActive(true);
                store.Add(i * 3 + j, newPatrol);
            }
        }
        return store;
    }

    public void StopPatrol()
    {
        for (int i = 0; i < 3; ++ i)
        {
            for (int j = 0; j < 3; ++ j)
            {
                store[i * 3 + j].transform.position = new Vector3(posX[j], 0, posZ[i]);
            }
        }
    }
}

