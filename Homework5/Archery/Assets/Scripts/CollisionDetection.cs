using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public FirstSceneController sceneController;         
    public ScoreRecorder recorder;                        

    void Start()
    {
        sceneController = SSDirector.GetInstance().CurrentScenceController as FirstSceneController;
        recorder = Singleton<ScoreRecorder>.Instance;
    }

    void OnTriggerEnter(Collider arrowHead)
    {
        Debug.Log("TriggerEnter");
        Transform arrow = arrowHead.gameObject.transform.parent;
        if (arrow == null)
        {
            Debug.Log("arrow is null");
            return;
        }
        if (arrow.tag == "arrow")
        {
            arrow.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            arrow.GetComponent<Rigidbody>().isKinematic = true;
            recorder.Record(this.gameObject);
            arrowHead.gameObject.gameObject.SetActive(false);
            arrow.tag = "hit";
        }
    }
}
