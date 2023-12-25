using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class InputController : MonoBehaviour
{

    Rigidbody2D rb;

    public KeyCode jumpKey;
    public KeyCode fireKey;
    public KeyCode shieldKey;
    public string moveInput;
    public float horizontal;

    

    public Transform groundCheck;

    private float groundCheckRadius = 0.2f;

    private float jumpforce = 10f;

    public float speed = 5f;

    

    bool jumpInput;

    bool shieldInput;

    bool fireInput;

    public LayerMask collisionMask;

    private float jumpsLeft;

    public float startJumpAmount = 2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


        jumpsLeft = startJumpAmount;

        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw(moveInput);


        jumpInput = Input.GetKeyDown(jumpKey);

        shieldInput = Input.GetKeyDown(shieldKey);

        fireInput = Input.GetKeyDown(fireKey);

        
        if (fireInput)
        {
            
        }

        if (shieldInput)
        {
            
        }
        

        if (jumpInput && jumpsLeft > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jumpsLeft -= 1;
        }
        if(IsGrounded())
        {
            jumpsLeft = startJumpAmount; 
        }

        //turn to move direction
        if(horizontal != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontal), 1, 1);
        }
        

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionMask);
    }

    private void FixedUpdate()
    {


        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);   
    }
}
