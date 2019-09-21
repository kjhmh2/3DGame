using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    readonly float Speed = 20;

    Vector3 Target;
    Vector3 Middle;
    int state = 0;  // 0->no need to move, 1->object moving , 2->boat moving to dest
    bool To_Middle = true;

    void Update()
    {
        // object moving
        if (state == 1)
        {
            if (To_Middle)
            {
                transform.position = Vector3.MoveTowards(transform.position, Middle, Speed * Time.deltaTime);
                if (transform.position == Middle)
                    To_Middle = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
                if (transform.position == Target)
                    state = 0;
            }
        }
        // boat moving
        else if (state == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
            if (transform.position == Target)
            {
                To_Middle = true;
                state = 0;
            }
        }
    }

    public void SetDestination(Vector3 position)
    {
        if (state != 0) return;
        Target = Middle = position;
        To_Middle = true;
        if (transform.position.y == Target.y)
        {
            state = 2;
        }
        else
        {
            state = 1;
            if (transform.position.y < Target.y)
            {
                Middle.x = transform.position.x;
            }
            else if (transform.position.y > Target.y)
            {
                Middle.y = transform.position.y;
            }
        }
    }

    public void Reset()
    {
        state = 0;
        To_Middle = true;
    }
}