using UnityEngine;
using System.Collections;

public class Platformer2DCharacterControl : MonoBehaviour {

    private PlatformerCharacterControl m_Character;
    private bool m_Jump;
    public bool HumanPlayer;
    private float movementReset;
    private int movementBinary;
    private bool AIMoveRight;
    private bool crouch;
    private float horizontal;
    private int jumpBinary;

    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacterControl>();
        HumanPlayer = false;
    }

    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = Input.GetButtonDown("Jump");
        }

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

            if(jumpBinary == 1)
            {
                m_Jump = true;
            }
            else
            {
                m_Jump = false;
            }
        }
    }



    private void FixedUpdate()
    {
        // Read the inputs.

            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = Input.GetAxis("Horizontal");

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

        // Pass all parameters to the character control script.
        m_Character.Move(h, crouch, m_Jump);
        m_Jump = false;
    }
}
