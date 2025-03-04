using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;

    public BulletType bulletType;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //// Karakterin yönünü belirlemek için GameObject'in localScale.x deðerini kullanýyoruz
        //float direction = Mathf.Sign(transform.localScale.x);

        //// Yönü ve hýzý belirle
        //rb.velocity = new Vector2(direction * bulletType.bulletSpeed, rb.velocity.y);

        rb.velocity = transform.right * bulletType.bulletSpeed;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BulletClear"))
        {
            Destroy(gameObject);
        }
    }
}
