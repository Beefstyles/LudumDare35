using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class TextClass
{
    public Text LivesOrKillsLabel, StatusText, LivesOrKillsText, EndGameText, EndGameLevel, LevelNumberText, EndGameInstruction;
}

public class GameManagerScript : MonoBehaviour
{

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
    public float DelayTimer;
    public AudioSource[] gameMusic;

    void Start ()
    {
        Time.timeScale = 1;
        DemonControlTrue = CarryOverInfo.DemonControlRound;
        GameOverScreen.SetActive(false);
        numberOfBabies = 0;
        numberOfDemons = 0;
        gameOver = false;
        bSpawn = FindObjectsOfType<BabySpawn>();
        dSpawn = FindObjectsOfType<DemonSpawn>();
        gameMusic = GetComponents<AudioSource>();
        if (Lives <= 1)
        {
            Lives = CarryOverInfo.StartLives;
        }
   
        if (DemonControlTrue)
        {
            BabySpawn("");
            DemonSpawn("Player");
            gameMusic[1].Play();
        }

        else if (!DemonControlTrue)
        {
            BabySpawn("Player");
            DemonSpawn("");
            gameMusic[0].Play();
        }

        DelayTimer = 0.1F;
        gameTimer = FindObjectOfType<GameTimer>();
        gameTimer.GameTimerF = CarryOverInfo.GameTimerFloat;
        
    }

    IEnumerator RestartCheck()
    {
            if (Input.GetButton("Fire1") || Input.GetButton("Fire2"))
            {
                Time.timeScale = 1;
                RestartLevel();
            }
            yield return null;
    }

    void SetScreen()
    {
        GameOnScreen.SetActive(false);
        GameOverScreen.SetActive(true);
    }
	void Update ()
    {
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if(DelayTimer >= 0)
        {
            DelayTimer -= Time.deltaTime;
        }

        if (!gameOver && DelayTimer <= 0)
        {
            textClass.LevelNumberText.text = CarryOverInfo.LevelNumber.ToString();
            if (DemonControlTrue)
            {
                textClass.LivesOrKillsLabel.text = "Kills: ";
                textClass.LivesOrKillsText.text = Kills.ToString();
                textClass.StatusText.text = "Harvest Souls!";

                if (numberOfBabies <= 0)
                {
                    SetScreen();
                    textClass.EndGameText.text = "You've Harvested all Souls.";
                    textClass.EndGameInstruction.text = "Press Fire1/2 To Continue";
                    textClass.EndGameLevel.text = "";
                    CarryOverInfo.LevelNumber++;
                    CarryOverInfo.StartLives += Kills;
                    CarryOverInfo.DemonControlRound = false;
                    CarryOverInfo.GameTimerFloat += 5F;
                    gameOver = true;
                }

                if (gameTimer.GameTimerF <= 0)
                {
                    SetScreen();
                    textClass.EndGameText.text = "You missed some souls!";
                    textClass.EndGameInstruction.text = "Press Fire1/2 To Continue";
                    textClass.EndGameLevel.text = "";
                    CarryOverInfo.LevelNumber++;
                    CarryOverInfo.StartLives += Kills;
                    CarryOverInfo.DemonControlRound = false;
                    CarryOverInfo.GameTimerFloat += 5F;
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
                    SetScreen();
                    textClass.EndGameText.text = "You survived until Level: ";
                    textClass.EndGameLevel.text = CarryOverInfo.LevelNumber.ToString();
                    textClass.EndGameInstruction.text = "Press Fire1/2 To Restart Game";
                    CarryOverInfo.LevelNumber = 1;
                    gameOver = true;
                }

                if (gameTimer.GameTimerF <= 0)
                {
                    SetScreen();
                    textClass.EndGameText.text = "You survived. Now it's your turn";
                    textClass.EndGameInstruction.text = "Press Fire1/2 To Continue";
                    textClass.EndGameLevel.text = "";
                    CarryOverInfo.DemonControlRound = true;
                    gameOver = true;
                }
            }
        }

        if (gameOver && DelayTimer <= 0)
        {
            StartCoroutine(RestartCheck());
            Time.timeScale = 0;
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
            for (int i = 1; i <= CarryOverInfo.LevelNumber; i++)
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
            for (int i = 1; i <= CarryOverInfo.LevelNumber; i++)
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
