using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // explosion radius
    public float explosionRadius = 2f;
    private tankType type;

	public void setTankType(tankType type)
    {
        this.type = type;
    }

    private void Update()
    {
        if (this.transform.position.y < 0 && this.gameObject.activeSelf)
        {
            GameObjectFactory mf = Singleton<GameObjectFactory>.Instance;
            ParticleSystem explosion = mf.getPs();
            explosion.transform.position = transform.position;
            explosion.Play();
            mf.recycleBullet(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        GameObjectFactory mf = Singleton<GameObjectFactory>.Instance;
        ParticleSystem explosion = mf.getPs();
        explosion.transform.position = transform.position;

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < colliders.Length; ++ i)
        {
            if (colliders[i].tag == "tankPlayer" && this.type == tankType.Enemy || colliders[i].tag == "tankEnemy" && this.type == tankType.Player)
            {
                float distance = Vector3.Distance(colliders[i].transform.position, transform.position);
                float hurt = 100f / distance;
                float current = colliders[i].GetComponent<Tank>().getHp();
                colliders[i].GetComponent<Tank>().setHp(current - hurt);
            }
        }

        explosion.Play();
        if (this.gameObject.activeSelf)
        {
            mf.recycleBullet(this.gameObject);
        }
    }
}
