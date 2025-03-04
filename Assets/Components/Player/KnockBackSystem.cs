using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class KnockBackSystem : MonoBehaviourPun
{
    Rigidbody2D playerRb;
    private bool isKnockedBack = false;
    float knockbackForce;
    public float knockbackDuration = 0.1f;  // Knockback s�resi

    private BulletType bulletType;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    /*
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
    */

    [PunRPC]
    void ApplyKnockback(Vector2 direction, float force)
    {
        // Knockback i�lemi burada uygulan�r
        playerRb.velocity = Vector2.zero; // H�z� s�f�rlay�n
        playerRb.AddForce(direction * force, ForceMode2D.Impulse); // Knockback kuvveti uygulay�n
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            bulletType = collision.gameObject.GetComponent<Bullet>().bulletType;
            knockbackForce = bulletType.pushPower;

            // T�m oyuncular i�in Knockback uygulay�n
            this.gameObject.GetComponent<PhotonView>().RPC("ApplyKnockback", RpcTarget.All, knockbackDirection, knockbackForce);

            Destroy(collision.gameObject);
        }
    }
}
