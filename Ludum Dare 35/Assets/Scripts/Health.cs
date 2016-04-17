using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int PlayerHealth;
    public int currentPlayerHealth;
    private bool playerDead;
    private GameObject playerCharDead;
    public GameObject PlayerDeadCorpse;
    GameManagerScript gameManager;


    void Start()
    {
        playerDead = false;
        PlayerHealth = 1;
        currentPlayerHealth = PlayerHealth;
        gameManager = FindObjectOfType<GameManagerScript>();
    }

    void Update()
    {
        if (!gameManager.DemonControlTrue)
        {
            gameManager.Lives = currentPlayerHealth;
        }
    }

	public void TakeDamage(int amount, string originator)
    {
        currentPlayerHealth -= amount;
        if(currentPlayerHealth <= 0 && !playerDead)
        {
            if(originator == "HumanShot")
            {
                gameManager.Kills++;
            }
            playerDead = true;
            StartCoroutine("PlayerDeath");
        }
    }

    IEnumerator PlayerDeath()
    {
        playerCharDead = Instantiate(PlayerDeadCorpse, gameObject.transform.position, transform.rotation) as GameObject;
        yield return new WaitForSeconds(0.01F);
        Destroy(gameObject);
    }
}
