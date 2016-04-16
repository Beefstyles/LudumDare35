using UnityEngine;
using System.Collections;

public class WrapAroundTrigger : MonoBehaviour {

    private float Y_Position;
    public GameObject EastTrigger, WestTrigger;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Player")
        {
            if (this.gameObject.name == "WestTrigger")
            {
                coll.gameObject.transform.position = new Vector3(EastTrigger.transform.position.x, coll.gameObject.transform.position.y, 0);
            }

            else if(this.gameObject.name == "EastTrigger")
            {
                coll.gameObject.transform.position = new Vector3(WestTrigger.transform.position.x, coll.gameObject.transform.position.y, 0);
            }
        }
        
    }
}
