using UnityEngine;
using System.Collections;

public class ProjectileControl : MonoBehaviour {

    public int AttackDamage;
    public float LifeTime;

	void Start ()
    {
        LifeTime = 4F;
    }
	
	
	void Update ()
    {
	    if(LifeTime >= 0)
        {
            LifeTime -= Time.deltaTime;
        }

        else if (LifeTime <= 0)
        {
            KillObject();
        }
	}

    void KillObject()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            KillObject();
        }
    }
}
