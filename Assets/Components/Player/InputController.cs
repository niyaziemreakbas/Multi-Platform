using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class InputController : MonoBehaviourPunCallbacks, IObserver
{

    Rigidbody2D playerRb;
    Animator playerAnim;

    public KeyCode jumpKey;
    public KeyCode shieldKey;
    public string moveInput;
    public float horizontal;


    public bool directRight = true;

    public Transform groundCheck;

    private float groundCheckRadius = 0.2f;

    private float jumpforce = 10f;

    public float speed = 5f;

    bool jumpInput;

    bool shieldInput;


    public LayerMask collisionMask;

    private float jumpsLeft;

    public float startJumpAmount = 2;

    //public GameObject shield;

    PhotonView pw;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();

        playerAnim = GetComponent<Animator>();

        jumpsLeft = startJumpAmount;

        pw = GetComponent<PhotonView>();

        PlayerDataManager.Instance.RegisterObserver(this);

    }

    public void OnDataChanged()
    {
        //Player's health changed so jump should assign to start amount again.
        jumpsLeft = startJumpAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

        if (photonView.IsMine)
        {
            horizontal = Input.GetAxisRaw(moveInput);

            jumpInput = Input.GetKeyDown(jumpKey);

            shieldInput = Input.GetKeyDown(shieldKey);
            /*

            if (shieldInput)
            {
                shield.SetActive(!shield.activeSelf);
            }

            */
            if (jumpInput && jumpsLeft > 0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpforce);
                jumpsLeft -= 1;
                playerAnim.SetBool("isJumping", true);
            }


            //Assign jump amount to start amount
            if (IsGrounded())
            {
                jumpsLeft = startJumpAmount;
                playerAnim.SetBool("isJumping", false);
            }


            if (horizontal == -1 && directRight)
            {
                directRight = false;

                transform.Rotate(0, 180, 0);
            }
            else if (horizontal == 1 && directRight == false)
            {

                directRight = true;

                transform.Rotate(0, 180, 0);
            }

            // Hareket animasyonu için
            playerAnim.SetBool("isMoving", horizontal != 0);

        }
    }


    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            playerRb.velocity = new Vector2(horizontal * speed, playerRb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionMask);
    }
    /*
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red; // Çemberin rengini ayarlayabilirsin
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); // Çemberi sahneye çiz
        }
    }
    */
}
