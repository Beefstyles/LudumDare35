using UnityEngine;
using System.Collections;

public class DeathByTime : MonoBehaviour {

    private float lifeTime;

    void Start()
    {
        lifeTime = 3F;
    }

    void Update()
    {
        if(lifeTime >= 0)
        {
            lifeTime -= Time.deltaTime;
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
