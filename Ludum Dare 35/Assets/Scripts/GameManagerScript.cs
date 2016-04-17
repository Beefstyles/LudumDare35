using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class TextClass
{
    public Text LivesOrKillsLabel, StatusText, LivesOrKillsText;
}

public class GameManagerScript : MonoBehaviour {

    GameTimer gameTimer;
    public TextClass textClass;
    DemonControl demonControl;
    Health health;
    [HideInInspector]
    public bool DemonControlTrue;
    public int Lives, Kills;
    public bool NightTime;
    public int numberOfBabies;
    public int numberOfDemons;

	void Start ()
    {
        gameTimer = FindObjectOfType<GameTimer>();
        gameTimer.GameTimerF = 500;
        DemonControlTrue = false;
        NightTime = true;
    }

	void Update ()
    {
        if (DemonControlTrue)
        {
            textClass.LivesOrKillsLabel.text = "Kills: ";
            textClass.LivesOrKillsText.text = Kills.ToString();
            textClass.StatusText.text = "Harvest Souls!";
        }
        else
        {
            textClass.LivesOrKillsLabel.text = "Lives: ";
            textClass.LivesOrKillsText.text = Lives.ToString();
            textClass.StatusText.text = "Run Away!";
        }
    }
}
