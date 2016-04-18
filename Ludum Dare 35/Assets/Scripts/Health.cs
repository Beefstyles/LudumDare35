using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int PlayerHealth;
    public int currentPlayerHealth;
    private bool playerDead;
    private GameObject playerCharDead;
    public GameObject PlayerDeadCorpse;
    GameManagerScript gameManager;
    public bool Invicible;
    private float invicTimer;
    ParticleSystem invicParticles;


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
        Invicible = false;
        if (gameObject.tag == "Baby")
        {
            Invicible = true;
            invicTimer = 3F;
            invicParticles = GetComponent<ParticleSystem>();
            invicParticles.Play();
        }
    }

    void Update()
    {
        if (!gameManager.DemonControlTrue && gameObject.tag == "Baby")
        {
            gameManager.Lives = currentPlayerHealth;
        }

        if(gameObject.tag == "Baby")
        {
            if (invicTimer >= 0)
            {
                invicTimer -= Time.deltaTime;
            }

            if (invicTimer <= 0 && invicParticles.isPlaying)
            {
                Invicible = false;
                invicParticles.Stop();
            }
        }  
    }

	public void TakeDamage(int amount, string originator)
    {
        if (!Invicible)
        {
            currentPlayerHealth -= amount;
            if (currentPlayerHealth <= 0 && !playerDead)
            {
                if (gameManager.DemonControlTrue && gameObject.tag == "Baby")
                {
                    gameManager.Kills++;
                    gameManager.numberOfBabies--;
                }
                playerDead = true;
                StartCoroutine("PlayerDeath");
            }
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
