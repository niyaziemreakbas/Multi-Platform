using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Realtime;

public class InputController : MonoBehaviourPunCallbacks
{
    Rigidbody2D playerRb;


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

    public GameObject shield;

    PhotonView pw;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();


        jumpsLeft = startJumpAmount;

        pw = GetComponent<PhotonView>();
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


            if (shieldInput)
            {
                shield.SetActive(!shield.activeSelf);
            }


            if (jumpInput && jumpsLeft > 0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpforce);
                jumpsLeft -= 1;
            }


            if (IsGrounded())
            {
                jumpsLeft = startJumpAmount;
            }


            //turn to move direction
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


}
