using UnityEngine;
using System.Collections;

public class ProjectileControl : MonoBehaviour {

    private int attackDamage = 1;
    private float lifeTime;
    Health health;
    GameManagerScript gameManager;
    PlatformerCharacterControl platformerCharacterControl;
    DemonControl demonControl;
    public AudioSource ShootAudio;

    void Start ()
    {
        lifeTime = 4F;
        gameManager = FindObjectOfType<GameManagerScript>();
        demonControl = FindObjectOfType<DemonControl>();
        ShootAudio = GetComponent<AudioSource>();
        if (gameManager.DemonControlTrue)
        {
            GetComponent<Rigidbody2D>().velocity = (Vector3.down * demonControl.ProjectileForce);
        }
        ShootAudio.Play();
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
                    if (platformerCharacterControl.maxSpeed >= 4F)
                    {
                        platformerCharacterControl.maxSpeed = 4F;
                    }

                }
            }
            KillObject();
        }
        
    }
}
