using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class TimerText
{
    public Text GameTimerTextMinutes, GameTimerTextSeconds;
}

public class GameTimer : MonoBehaviour {

    public int GameTimerMinutes;
    public int GameTimerSeconds;
    private string gameTimerSecondsString;
    public float GameTimerF;
    public TimerText timerText;

	
	// Update is called once per frame
	void Update ()
    {
	    if(GameTimerF >= 0)
        {
            GameTimerF -= Time.deltaTime;
        }

        GameTimerMinutes = Mathf.FloorToInt(GameTimerF / 60);
        GameTimerSeconds = Mathf.FloorToInt(GameTimerF - (GameTimerMinutes * 60));
        timerText.GameTimerTextMinutes.text = GameTimerMinutes.ToString();
        gameTimerSecondsString = GameTimerSeconds.ToString();
        if(gameTimerSecondsString.Length < 2)
        {
            gameTimerSecondsString = "0" + GameTimerSeconds;
        }
        timerText.GameTimerTextSeconds.text = gameTimerSecondsString;
	}
}
