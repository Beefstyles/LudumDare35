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
    private float projectileForce = 100F;

    void Start ()
    {
        speed = 200F;
        fireRate = 0F;
        HumanPlayer = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (TargetAcquired)
        {
            TargetShooting();
        }
        if(fireRate >= 0)
        {
            fireRate -= Time.deltaTime;
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
    }
}
