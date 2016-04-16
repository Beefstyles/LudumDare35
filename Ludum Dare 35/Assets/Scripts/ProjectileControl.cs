using UnityEngine;
using System.Collections;

public class ProjectileControl : MonoBehaviour {

    private int attackDamage = 1;
    private float lifeTime;
    Health health;

	void Start ()
    {
        lifeTime = 4F;
    }
	
	
	void Update ()
    {
	    if(lifeTime >= 0)
        {
            lifeTime -= Time.deltaTime;
        }

        else if (lifeTime <= 0)
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
            health = coll.GetComponent<Health>();
            health.TakeDamage(attackDamage);
            KillObject();
        }
    }
}
