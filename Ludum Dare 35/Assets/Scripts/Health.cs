using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int PlayerHealth;
    public int currentPlayerHealth;
    private bool playerDead;
    private GameObject playerCharDead;
    public GameObject PlayerDeadCorpse;


    void Start()
    {
        playerDead = false;
        PlayerHealth = 1;
        currentPlayerHealth = PlayerHealth;
    }

	public void TakeDamage(int amount)
    {
        currentPlayerHealth -= amount;
        if(currentPlayerHealth <= 0 && !playerDead)
        {
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
