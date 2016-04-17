using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	void Update ()
    {
        if (Input.GetButton("Fire1") || Input.GetButton("Fire2"))
        {
            CarryOverInfo.LevelNumber = 1;
            CarryOverInfo.DemonControlRound = false;
            CarryOverInfo.StartLives = 1;
            CarryOverInfo.GameTimerFloat = 15;
            SceneManager.LoadScene(1);
        }
    }
}
