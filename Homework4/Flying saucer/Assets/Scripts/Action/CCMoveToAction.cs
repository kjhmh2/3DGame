using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMoveToAction : SSAction
{
    public float speedx;
    public float speedy = 0;

    private CCMoveToAction() {}

    public static CCMoveToAction getAction(float speedx)
    {
        CCMoveToAction action = CreateInstance<CCMoveToAction>();
        action.speedx = speedx;
        return action;
    }

    public override void Start() {}

    public override void Update()
    {
        this.transform.position += new Vector3(speedx * Time.deltaTime, -speedy * Time.deltaTime + (float)-0.5 * 10 * Time.deltaTime * Time.deltaTime, 0);
        speedy += 10 * Time.deltaTime;
        if (transform.position.z == -1)
        {
            Debug.Log("Hit");
            destroy = true;
            CallBack.SSActionCallback(this, true);
        }
        else if (transform.position.y <= -45)
        {
            Debug.Log("Missing");
            destroy = true;
            CallBack.SSActionCallback(this, false);
        }
    }

}
