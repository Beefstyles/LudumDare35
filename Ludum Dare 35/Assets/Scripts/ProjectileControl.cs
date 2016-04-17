using UnityEngine;
using System.Collections;

public class ProjectileControl : MonoBehaviour {

    private int attackDamage = 1;
    private float lifeTime;
    Health health;
    GameManagerScript gameManager;
    PlatformerCharacterControl platformerCharacterControl;

    void Start ()
    {
        lifeTime = 4F;
        gameManager = FindObjectOfType<GameManagerScript>();
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
        if(coll.gameObject.tag == "Baby" || coll.gameObject.tag == "Shelter")
        {
            if(gameObject.tag == "KillShot")
            {
                health = coll.GetComponent<Health>();
                health.TakeDamage(attackDamage, gameObject.tag);
            }

            if(coll.gameObject.tag == "Baby")
            {
                if (gameObject.tag == "SlowShot")
                {
                    platformerCharacterControl = coll.GetComponentInParent<PlatformerCharacterControl>();
                    if (platformerCharacterControl.maxSpeed >= 5F)
                    {
                        platformerCharacterControl.maxSpeed = 5F;
                    }

                }
            }
            KillObject();
        }
        
    }
}
