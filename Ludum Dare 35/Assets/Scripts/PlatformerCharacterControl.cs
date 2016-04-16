using UnityEngine;
using System.Collections;

public class PlatformerCharacterControl : MonoBehaviour {

    [SerializeField]
    private float maxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField]
    private float jumpForce = 400f;                  // Amount of force added when the player jumps.
    [Range(0, 1)]
    [SerializeField]
    private float crouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [SerializeField]
    private bool airControl = false;                 // Whether or not a player can steer while jumping;
    [SerializeField]
    private LayerMask whatIsGround;                  // A mask determining what is ground to the character

    private Transform groundCheck;    // A position marking where to check if the player is grounded.
    const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool grounded;            // Whether or not the player is grounded.
    private Transform ceilingCheck;   // A position marking where to check for ceilings
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D playerRigidBody;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Animator playerAnimator;


    private void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("GroundCheck");
        ceilingCheck = transform.Find("CeilingCheck");
        playerAnimator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
 
    }

    private void FixedUpdate()
    {
        grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                grounded = true;
        }
        
        playerAnimator.SetBool("Ground", grounded);

        // Set the vertical animation
        playerAnimator.SetFloat("vSpeed", playerRigidBody.velocity.y);
    }


    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch && playerAnimator.GetBool("Crouch"))
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(ceilingCheck.position, k_CeilingRadius, whatIsGround))
            {
                crouch = true;
            }
        }

        // Set whether or not the character is crouching in the animator
        playerAnimator.SetBool("Crouch", crouch);

        //only control the player if grounded or airControl is turned on
        if (grounded || airControl)
        {
            // Reduce the speed if crouching by the crouchSpeed multiplier
            move = (crouch ? move * crouchSpeed : move);

            // The Speed animator parameter is set to the absolute value of the horizontal input.
            playerAnimator.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            playerRigidBody.velocity = new Vector2(move * maxSpeed, playerRigidBody.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (grounded && jump && playerAnimator.GetBool("Ground"))
        {
            // Add a vertical force to the player.
            grounded = false;
            playerAnimator.SetBool("Ground", false);
            playerRigidBody.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

