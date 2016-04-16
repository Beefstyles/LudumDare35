using UnityEngine;
using System.Collections;

public class AreaOfDetection : MonoBehaviour {

    DemonControl demonControl;
    private float targetResetTimer;
    private bool targetInSight;

    void Start()
    {
        demonControl = GetComponentInParent<DemonControl>();
        targetResetTimer = 1F;
    }

    void OnTriggerEnter2D(Collider2D targetColl2D)
    {
        if (targetColl2D.gameObject.tag == "Player")
        {
            demonControl.TargetToMoveTo = targetColl2D.gameObject;
            demonControl.PlayerFound = true;
        }
    }

    void OnTriggerStay2D(Collider2D targetColl2D)
    {
        if (targetColl2D.gameObject.tag == "Player")
        {
            if (targetColl2D.gameObject != null)
            {
                demonControl.TargetToMoveTo = targetColl2D.gameObject;
                demonControl.PlayerFound = true;
                targetInSight = true;

            }
        }
    }

    void OnTriggerExit2D(Collider2D targetColl2D)
    {
        if (targetColl2D.gameObject.tag == "Player")
        {
            demonControl.TargetToMoveTo = null;
            demonControl.PlayerFound = false;
            targetInSight = false;
        }
    }

    void Update()
    {
        if (targetResetTimer >= 0)
        {
            targetResetTimer -= Time.deltaTime;
        }

        else if (targetResetTimer <= 0 && !targetInSight)
        {
            demonControl.TargetToMoveTo = null;
            demonControl.PlayerFound = false;
            targetResetTimer = 1F;
        }
    }
}
