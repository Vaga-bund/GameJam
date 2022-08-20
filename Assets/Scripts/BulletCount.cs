using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCount : MonoBehaviour
{
    Text text;
    public ArkanoidManager ark;
    float a1;
    float a2;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        a1 = ark.bulletAmmo - ark.bulletAmount;
        a2 = ark.bulletAmmo - ark.bulletCount;
        if (a1 > 0)
            text.text = a1.ToString();
        else
            text.text = a2.ToString();
    }
}
