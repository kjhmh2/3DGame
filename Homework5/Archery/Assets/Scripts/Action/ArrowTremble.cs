using UnityEngine;
using System.Collections;

public class ArrowTremble : SSAction
{
    float radian = 0;
    float perRadian = 3f;
    float radius = 0.01f;
    Vector3 position;
    public float leftTime = 0.8f;

    private ArrowTremble() { }

    public override void Start()
    {
        position = transform.position;
    }

    public static ArrowTremble GetSSAction()
    {
        ArrowTremble action = CreateInstance<ArrowTremble>();
        return action;
    }

    public override void Update()
    {
        leftTime -= Time.deltaTime;
        if (leftTime <= 0)
        {
            transform.position = position;
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }

        radian += perRadian;
        float dy = Mathf.Cos(radian) * radius;
        transform.position = position + new Vector3(0, dy, 0);
    }

    public override void FixedUpdate() {}

}