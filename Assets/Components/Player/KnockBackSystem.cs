using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackSystem : MonoBehaviour
{
    Rigidbody2D playerRb;
    private bool isKnockedBack = false;
    private Vector2 knockbackDirection;
    private float knockbackTimer;
    float knockbackForce;
    public float knockbackDuration = 0.1f;  // Knockback süresi

    private BulletType bulletType;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            bulletType = collision.gameObject.GetComponent<Bullet>().bulletType;
            knockbackForce = bulletType.pushPower;

            Vector2 hitDirection = collision.transform.position - transform.position;

            hitDirection.y = 0f;
            hitDirection.x = -hitDirection.x;

            Knockback(hitDirection);
            Destroy(collision.gameObject);
        }
    }


    void Knockback(Vector2 direction)
    {
        isKnockedBack = true;
        knockbackDirection = direction;
        knockbackTimer = knockbackDuration;
    }

    private void FixedUpdate()
    {
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0)
            {
                playerRb.velocity = Vector2.zero;
                isKnockedBack = false;

            }
            else
            {
                //playerRb.velocity = knockbackDirection * (knockbackTimer / knockbackDuration) * knockbackForce;
                playerRb.MovePosition(playerRb.position + knockbackDirection * (knockbackTimer / knockbackDuration) * knockbackForce);
            }
        }
    }
}
