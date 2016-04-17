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
    public bool DemonControlTrue;
    public int Lives, Kills;

	void Start ()
    {
        gameTimer = FindObjectOfType<GameTimer>();
        gameTimer.GameTimerF = 500;
        DemonControlTrue = false;
    }

	void Update ()
    {
        if (DemonControlTrue)
        {
            textClass.LivesOrKillsLabel.text = "Kills: ";
            textClass.LivesOrKillsText.text = Kills.ToString();
        }
        else
        {
            textClass.LivesOrKillsLabel.text = "Lives: ";
            textClass.LivesOrKillsText.text = Lives.ToString();
        }
    }
}
