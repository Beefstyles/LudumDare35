using UnityEngine;
using System.Collections;

public class Platformer2DCharacterControl : MonoBehaviour {

    private PlatformerCharacterControl character;
    private bool jump;
    public bool HumanPlayer;
    private float movementReset;
    private int movementBinary;
    private bool AIMoveRight;
    private int jumpBinary;
    private bool crouch;
    private float h;
    GameManagerScript gameManager;
    
   

    private void Awake()
    {
        character = GetComponent<PlatformerCharacterControl>();
        gameManager = FindObjectOfType<GameManagerScript>();
    }

    private void Update()
    {

        if (gameManager.DemonControlTrue)
        {
            if (HumanPlayer)
            {
                HumanPlayer = false;
            }
            
        }
        else
        {
            if (!HumanPlayer)
            {
                HumanPlayer = true;
            } 
        }

        if (!jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            jump = Input.GetButtonDown("Jump");
        }
        if (!HumanPlayer)
        {
            if (movementReset >= 0)
            {
                movementReset -= Time.deltaTime;
            }

            if (movementReset <= 0)
            {
                movementBinary = Mathf.RoundToInt(Random.value);
                jumpBinary = Mathf.RoundToInt(Random.value);
                movementReset = Random.Range(2, 6);
                if (movementBinary == 1)
                {
                    AIMoveRight = true;
                }

                else
                {
                    AIMoveRight = false;
                }

                if (jumpBinary == 1)
                {
                    jump = true;
                }
                else
                {
                    jump = false;
                }
            }
        }
   }



    private void FixedUpdate()
    {

        if (HumanPlayer)
        {
            h = Input.GetAxis("Horizontal");
        }
            
        if (!HumanPlayer)
        {
            if (AIMoveRight)
            {
                h = 1;
            }
            else
            {
                h = -1;
            }
        }
        character.Move(h, jump);
        jump = false;
    }
}
