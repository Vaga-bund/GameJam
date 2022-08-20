using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public ArkanoidManager arkanoidManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;

        if(col.CompareTag("Bullet"))
        {
            arkanoidManager.CountOnGround();
        }
    }
}
