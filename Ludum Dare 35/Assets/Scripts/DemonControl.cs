using UnityEngine;
using System.Collections;

public class DemonControl : MonoBehaviour {

    private float speed;
    private float fireRate;
    public bool TargetAcquired;
    public GameObject TargetEnemy;
    public GameObject Target;
    public bool HumanPlayer;
    public Vector2 projectileDirectionHeading;
    public float projectileDirectionMag;
    public Vector2 projectileDirection;
    private Vector3 rotationDirection;
    private float fireRateChoice = 2F;
    private GameObject ProjectileClone;
    public GameObject ShootPoint;
    public GameObject Projectile;
    private float projectileForce = 200F;
    private Rigidbody2D demonRigidBody;
    private bool AIMoveRight;
    private float movementReset = 5F;
    private int movementBinary;
    public bool PlayerFound;
    public GameObject TargetToMoveTo;
    public float dirNum;
    public Vector3 movementHeading;
    AngleDirection AngleDir;

    void Start ()
    {
        speed = 2F;
        fireRate = 0F;
        HumanPlayer = true;
        demonRigidBody = GetComponent<Rigidbody2D>();
        AngleDir = FindObjectOfType<AngleDirection>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (TargetAcquired && !HumanPlayer)
        {
            TargetShooting();
        }
        if(fireRate >= 0)
        {
            fireRate -= Time.deltaTime;
        }

        if (!HumanPlayer)
        {
            AIMovement();
        }

        if (HumanPlayer)
        {
            HumanMovement();
        }

        if(movementReset >= 0)
        {
            movementReset -= Time.deltaTime;
        }

        if(movementReset <= 0)
        {
            movementBinary = Mathf.RoundToInt(Random.value);
            movementReset = Random.Range(2, 6);
            if (movementBinary == 1)
            {
                AIMoveRight = true;
            }

            else
            {
                AIMoveRight = false;
            }
        }
	}

    void TargetShooting()
    {
        if (!HumanPlayer)
        {
            Target = TargetEnemy;

            if(Target != null)
            {
                projectileDirectionHeading = (Target.transform.position) - ShootPoint.transform.position;
                projectileDirectionMag = projectileDirectionHeading.magnitude;
                projectileDirection = projectileDirectionHeading / projectileDirectionMag;

                Debug.DrawLine(ShootPoint.transform.position, Target.transform.position);
                if (fireRate <= 0)
                {
                    fireRate = fireRateChoice;
                    ProjectileClone = Instantiate(Projectile, ShootPoint.transform.position, Quaternion.identity) as GameObject;
                    ProjectileClone.GetComponent<Rigidbody2D>().AddForce(projectileDirection * projectileForce);
                    fireRate = fireRateChoice;
                }
            }
        }

        if (HumanPlayer)
        {
            if (fireRate <= 0)
            {
                fireRate = fireRateChoice;
                ProjectileClone = Instantiate(Projectile, ShootPoint.transform.position, Quaternion.identity) as GameObject;
                ProjectileClone.GetComponent<Rigidbody2D>().AddForce(Vector3.down * projectileForce);
                fireRate = fireRateChoice;
            }
        }
    }

    void AIMovement()
    {
        if (!PlayerFound)
        {
            if (AIMoveRight)
            {
                demonRigidBody.velocity = new Vector2(speed, demonRigidBody.velocity.y);
            }

            else
            {
                demonRigidBody.velocity = new Vector2(-speed, demonRigidBody.velocity.y);
            }
        }

        else if (PlayerFound)
        {
            if(TargetToMoveTo != null)
            {
                //Adapted from http://forum.unity3d.com/threads/left-right-test-function.31420/
                movementHeading = TargetToMoveTo.transform.position - transform.position;
                dirNum = AngleDir.AngleDir(transform.forward, movementHeading, transform.up);
                if (dirNum == 1)
                {
                    demonRigidBody.velocity = new Vector2(speed, demonRigidBody.velocity.y);
                }
                else if(dirNum == -1)
                {
                    demonRigidBody.velocity = new Vector2(-speed, demonRigidBody.velocity.y);
                }
                
            }
        }

    }

    void HumanMovement()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(x, y, 0);
        if (Input.GetButton("Fire1"))
        {
            TargetShooting();
        }
    }
    
}
