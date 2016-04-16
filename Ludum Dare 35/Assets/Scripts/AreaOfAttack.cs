using UnityEngine;
using System.Collections;

public class AreaOfAttack : MonoBehaviour {

    DemonControl demonControl;
    private float targetResetTimer;
    private bool targetInSight;

	void Start ()
    {
        demonControl = GetComponentInParent<DemonControl>();
        targetResetTimer = 1F;
    }

    void OnTriggerEnter2D(Collider2D targetColl2D)
    {
        if(targetColl2D.gameObject.tag == "Player")
        {
            demonControl.TargetEnemy = targetColl2D.gameObject;
            demonControl.TargetAcquired = true;
        }
    }

    void OnTriggerStay2D(Collider2D targetColl2D)
    {
        if (targetColl2D.gameObject.tag == "Player")
        {
            if (targetColl2D.gameObject != null)
            {
                demonControl.TargetEnemy = targetColl2D.gameObject;
                demonControl.TargetAcquired = true;
                targetInSight = true;

            }
        }
    }

    void OnTriggerExit2D(Collider2D targetColl2D)
    {
        if (targetColl2D.gameObject.tag == "Player")
        {
            demonControl.TargetEnemy = null;
            demonControl.TargetAcquired = false;
            targetInSight = false;
        }
    }

    void Update()
    {
        if(targetResetTimer >= 0)
        {
            targetResetTimer -= Time.deltaTime;
        }

        else if (targetResetTimer <= 0 && !targetInSight)
        {
            demonControl.TargetEnemy = null;
            demonControl.TargetAcquired = false;
            targetResetTimer = 1F;
        }
    }
}
