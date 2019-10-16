using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory
{
    public GameObject diskPrefab;
    public static DiskFactory DF = new DiskFactory();

    private Dictionary<int, Disk> used = new Dictionary<int, Disk>();
    private List<Disk> free = new List<Disk>();

    private DiskFactory()
    {
        diskPrefab = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Disk"));
        diskPrefab.AddComponent<Disk>();
        diskPrefab.SetActive(false);
    }

    public void FreeDisk()
    {
        foreach (Disk x in used.Values)
        {
            if (x.gameObject.activeSelf == false)
            {
                free.Add(x);
                used.Remove(x.GetInstanceID());
                return;
            }
        }
    }

    public Disk GetDisk(int round)
    {

        FreeDisk();
        GameObject newObject = null;
        Disk newDisk;
        if (free.Count > 0)
        {
            newObject = free[0].gameObject;
            free.Remove(free[0]);
        }
        else
            newObject = GameObject.Instantiate<GameObject>(diskPrefab, Vector3.zero, Quaternion.identity);
        newObject.SetActive(true);
        newDisk = newObject.AddComponent<Disk>();

        int swith;


        float s;
        if (round == 1)
        {
            swith = Random.Range(0, 3);
            s = Random.Range(30, 40);
        }
        else if (round == 2)
        {
            swith = Random.Range(0, 4);
            s = Random.Range(40, 50);
        }
        else
        {
            swith = Random.Range(0, 6);
            s = Random.Range(50, 60);
        }

        float PointX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
        newDisk.Direction = new Vector3(PointX, 1, 0);
        switch (swith)
        {
            case 0:
                newDisk.color = Color.yellow;
                newDisk.speed = s;
                newDisk.StartPoint = new Vector3(Random.Range(-130, -110), Random.Range(30, 90), Random.Range(110, 140));
                break;
            case 1:
                newDisk.color = Color.red;
                newDisk.speed = s + 10;
                newDisk.StartPoint = new Vector3(Random.Range(-130, -110), Random.Range(30, 80), Random.Range(110, 130));
                break;
            case 2:
                newDisk.color = Color.black;
                newDisk.speed = s + 15;
                newDisk.StartPoint = new Vector3(Random.Range(-130, -110), Random.Range(30, 70), Random.Range(90, 120));
                break;
            case 3:
                newDisk.color = Color.yellow;
                newDisk.speed = -s;
                newDisk.StartPoint = new Vector3(Random.Range(130, 110), Random.Range(30, 90), Random.Range(110, 140));
                break;
            case 4:
                newDisk.color = Color.red;
                newDisk.speed = -s - 10;
                newDisk.StartPoint = new Vector3(Random.Range(130, 110), Random.Range(30, 80), Random.Range(110, 130));
                break;
            case 5:
                newDisk.color = Color.black;
                newDisk.speed = -s - 15;
                newDisk.StartPoint = new Vector3(Random.Range(130, 110), Random.Range(30, 70), Random.Range(90, 120));
                break;
        }
        used.Add(newDisk.GetInstanceID(), newDisk);
        newDisk.name = newDisk.GetInstanceID().ToString();
        return newDisk;
    }
}
