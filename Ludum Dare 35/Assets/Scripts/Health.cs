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
        if(gameObject.tag == "Shelter")
        {
            currentPlayerHealth = 1;
        }
        gameManager = FindObjectOfType<GameManagerScript>();
        if (!gameManager.DemonControlTrue && gameObject.tag == "Baby")
        {
            currentPlayerHealth = gameManager.Lives;
        }
    }

    void Update()
    {
        if (!gameManager.DemonControlTrue && gameObject.tag == "Baby")
        {
            gameManager.Lives = currentPlayerHealth;
        }
    }

	public void TakeDamage(int amount, string originator)
    {
        currentPlayerHealth -= amount;
        if(currentPlayerHealth <= 0 && !playerDead)
        {
            if(gameManager.DemonControlTrue)
            {
                gameManager.Kills++;
                gameManager.numberOfBabies--;
            }
            playerDead = true;
            StartCoroutine("PlayerDeath");
        }
    }

    IEnumerator PlayerDeath()
    {
        if(gameObject.tag == "Baby")
        {
            playerCharDead = Instantiate(PlayerDeadCorpse, gameObject.transform.position, transform.rotation) as GameObject;
        }
        yield return new WaitForSeconds(0.01F);
        Destroy(gameObject);
    }
}
