using UnityEngine;
using System.Collections;

public class CameraXMove : MonoBehaviour {

    public Camera MainCamera;
    public GameObject CameraTarget;
    GameManagerScript gameManager;
    public GameObject EastTrigger, WestTrigger;

	void Start ()
    {
        gameManager = FindObjectOfType<GameManagerScript>();
        MainCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameManager.DemonControlTrue)
        {
            CameraTarget = GameObject.FindGameObjectWithTag("Demon");
        }
        else
        {
            CameraTarget = GameObject.FindGameObjectWithTag("Baby");
        }

        if(CameraTarget != null)
        {
            MainCamera.transform.position = new Vector3(CameraTarget.transform.position.x, 0, -10);          
        }
        
    }
}
