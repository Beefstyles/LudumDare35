﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class TextClass
{
    public Text LivesOrKillsLabel, StatusText, LivesOrKillsText;
}

public class GameManagerScript : CarryOverInfo {

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
    public GameObject Demon;
    public GameObject Babies;
    BabySpawn[] bSpawn;
    DemonSpawn[] dSpawn;
    private int prevSpawnIndex;

    void Start ()
    {
        gameTimer = FindObjectOfType<GameTimer>();
        gameTimer.GameTimerF = 40;
        DemonControlTrue = false;
        NightTime = true;
        numberOfBabies = 0;
        numberOfDemons = 0;
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

    void BabySpawn()
    {
        for (int i = 1; i <= levelNumber; i++)
        {
            int spawnIndex = Random.Range(0, bSpawn.Length);
            if(prevSpawnIndex == spawnIndex)
            {
                if (spawnIndex < bSpawn.Length)
                {
                    spawnIndex++;
                }
                else
                {
                    spawnIndex--;
                }
            }
            Instantiate(Babies, bSpawn[spawnIndex].transform.position, Quaternion.identity);
            numberOfBabies++;
            prevSpawnIndex = spawnIndex;
        }
    }

    
}
