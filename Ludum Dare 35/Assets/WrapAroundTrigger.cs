using UnityEngine;
using System.Collections;

public class WrapAroundTrigger : MonoBehaviour {

    private float Y_Position;
    public GameObject EastTriggerSpawn, WestTriggerSpawn;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Player")
        {
            if (gameObject.name == "WestTrigger")
            {
                coll.gameObject.transform.position = new Vector3(EastTriggerSpawn.transform.position.x, coll.gameObject.transform.position.y, 0);
                
            }

            else if(gameObject.name == "EastTrigger")
            {
                coll.gameObject.transform.position = new Vector3(WestTriggerSpawn.transform.position.x, coll.gameObject.transform.position.y, 0);

            }
        }
        
    }
}
