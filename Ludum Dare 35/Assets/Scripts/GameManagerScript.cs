using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class TextClass
{
    public Text LivesOrKillsLabel, StatusText, LivesOrKillsText, EndGameText, EndGameLevel;
}

public class GameManagerScript : CarryOverInfo {

    GameTimer gameTimer;
    public TextClass textClass;
    DemonControl demonControl;
    Health health;
    public bool DemonControlTrue;
    public int Lives, Kills;
    public int numberOfBabies;
    public int numberOfDemons;
    public GameObject Demon;
    public GameObject Babies;
    BabySpawn[] bSpawn;
    DemonSpawn[] dSpawn;
    private int prevSpawnIndex;
    private bool gameOver;
    public GameObject GameOnScreen, GameOverScreen;
    private float delayTimer;

    void Start ()
    {
        DemonControlTrue = DemonControlRound;
        GameOverScreen.SetActive(false);
        numberOfBabies = 0;
        numberOfDemons = 0;
        gameOver = false;
        bSpawn = FindObjectsOfType<BabySpawn>();
        dSpawn = FindObjectsOfType<DemonSpawn>();
        Lives = StartLives;
        if (DemonControlTrue)
        {
            BabySpawn("");
            DemonSpawn("Player");
        }

        else if (!DemonControlTrue)
        {
            BabySpawn("Player");
            DemonSpawn("");
        }

        delayTimer = 3F;
        gameTimer = FindObjectOfType<GameTimer>();
        gameTimer.GameTimerF = GameTimerFloat;
    }

	void Update ()
    {
        if(delayTimer >= 0)
        {
            delayTimer -= Time.deltaTime;
        }

        if (gameOver)
        {
            if (Input.GetButton("Fire1") || Input.GetButton("Fire2"))
            {
                RestartLevel();
            }
        }
        if (!gameOver && delayTimer <= 0)
        {
            if (DemonControlTrue)
            {
                textClass.LivesOrKillsLabel.text = "Kills: ";
                textClass.LivesOrKillsText.text = Kills.ToString();
                textClass.StatusText.text = "Harvest Souls!";

                if (numberOfBabies <= 0)
                {
                    textClass.StatusText.text = "You've Harvested all Souls. Next Level starting Soon.";
                    LevelNumber++;
                    StartLives += Kills;
                    gameOver = true;
                }

                if (gameTimer.GameTimerF <= 0)
                {
                    textClass.StatusText.text = "You missed some souls! Next level still starting.";
                    LevelNumber++;
                    StartLives += Kills;
                    DemonControlRound = false;
                    gameOver = true;
                }
            }
            else if (!DemonControlTrue)
            {
                textClass.LivesOrKillsLabel.text = "Lives: ";
                textClass.LivesOrKillsText.text = Lives.ToString();
                textClass.StatusText.text = "Run Away!";
                if (Lives <= 0)
                {
                    Debug.Log("You are nothing!");
                    GameOnScreen.SetActive(false);
                    GameOverScreen.SetActive(true);
                    textClass.StatusText.text = "You died!";
                    textClass.EndGameText.text = "You survived until Level: ";
                    textClass.EndGameLevel.text = LevelNumber.ToString();
                    LevelNumber = 1;
                    gameOver = true;
                }

                if (gameTimer.GameTimerF <= 0)
                {
                    textClass.StatusText.text = "You survived. Now it's your turn";
                    DemonControlRound = true;
                    gameOver = true;
                }
            }
        }

    }

    void BabySpawn(string purpose)
    {
        if (purpose == "Player")
        {
            int spawnIndex = Random.Range(0, bSpawn.Length);
            if (prevSpawnIndex == spawnIndex)
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
        else
        {
            for (int i = 1; i <= LevelNumber; i++)
            {
                int spawnIndex = Random.Range(0, bSpawn.Length);
                if (prevSpawnIndex == spawnIndex)
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

    void DemonSpawn(string purpose)
    {
        if(purpose == "Player")
        {
            int spawnIndex = Random.Range(0, dSpawn.Length);
            if (prevSpawnIndex == spawnIndex)
            {
                if (spawnIndex < dSpawn.Length)
                {
                    spawnIndex++;
                }
                else
                {
                    spawnIndex--;
                }
            }
            Instantiate(Demon, dSpawn[spawnIndex].transform.position, Quaternion.identity);
            numberOfDemons++;
            prevSpawnIndex = spawnIndex;
        }
        else
        {
            for (int i = 1; i <= LevelNumber; i++)
            {
                int spawnIndex = Random.Range(0, dSpawn.Length);
                if (prevSpawnIndex == spawnIndex)
                {
                    if (spawnIndex < dSpawn.Length)
                    {
                        spawnIndex++;
                    }
                    else
                    {
                        spawnIndex--;
                    }
                }
                Instantiate(Demon, dSpawn[spawnIndex].transform.position, Quaternion.identity);
                numberOfDemons++;
                prevSpawnIndex = spawnIndex;
            }
        }
    }

    void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    
}
