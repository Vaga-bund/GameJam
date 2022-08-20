using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private Rigidbody2D rb;

    private float shootSpeed = 1.2f;
    public float degreePerSec;

    public Health test;

    public ArkanoidManager pointShoot;

    Vector3 lastVeclocity;
    // Start is called before the first frame update
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        //rb.AddForce(new Vector2(9.8f * 25f, 9.8f * 25f));
    }

    // Update is called once per frame
    void Update()
    {
        lastVeclocity = rb.velocity;

        transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f) * degreePerSec);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;

        var speed = lastVeclocity.magnitude;
        var direction = Vector3.Reflect(lastVeclocity.normalized, collision.contacts[0].normal);

        rb.velocity = direction * Mathf.Max(speed, shootSpeed);

        //// 공 버그나면 지울 것!
        //if (rb.velocity.magnitude != 0 && rb.velocity.x < 0.15f && rb.velocity.x > -0.15f)
        //{
        //    rb.velocity = Vector2.zero;
        //    rb.AddForce(new Vector2(-0.4f, rb.velocity.y > 0 ? 1 : -1).normalized);
        //}
        //if (rb.velocity.magnitude != 0 && rb.velocity.y < 0.15f && rb.velocity.y > -0.15f)
        //{
        //    rb.velocity = Vector2.zero;
        //    rb.AddForce(new Vector2(rb.velocity.x > 0 ? 1 : -1, 0.4f).normalized);
        //}

        Vector3 currentEuler = transform.rotation.eulerAngles;
        float rotation = currentEuler.z;

        rotation += transform.forward.z +  direction.magnitude * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(currentEuler.x, currentEuler.y, rotation));

        //transform.Rotate(0.0f, 0.0f, direction.magnitude * 90.0f);
        

        if (col.CompareTag("Bullet"))
            Physics2D.IgnoreCollision(col.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        if(col.CompareTag("Monster"))
        {
            test = collision.transform.GetComponent<Health>();
            test.TakeDamage(1);
        }
        if(col.CompareTag("Box"))
        {
            test = collision.transform.GetComponent<Health>();
            test.TakeDamage(1);
        }

        if (col.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
