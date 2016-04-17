using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {

    public Light SunMoon;
    GameManagerScript gameManager;
    private float maxRotY, minRotY;

	void Start ()
    {
        gameManager = FindObjectOfType<GameManagerScript>();
        maxRotY = -270;
        minRotY = -90;
    }
	
	
	void Update ()
    {
        if (gameManager.DemonControlTrue)
        {
            SunMoon.color = Color.grey;
        }

        if (!gameManager.DemonControlTrue)
        {
            SunMoon.color = Color.red;
        }

    }
    
}
