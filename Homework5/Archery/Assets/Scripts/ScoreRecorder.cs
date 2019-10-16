using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    public int score;
    public int targetScore;            
    public int arrowNumber;

    void Start()
    {
        score = 0;
        targetScore = 15;
        arrowNumber = 10;
    }

    public void Record(GameObject gameObject)
    {
        int temp = gameObject.GetComponent<RingData>().score;
        score = temp + score;
    }
}
