using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image[] UIHeart;



    public bool isBlockMoving;

    public int MonsterNum = 1;
    public int Con = 1;
    public int playerHP = 5;
    public int iceSK = 0;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (playerHP < 1)
            UIHeart[0].color = new Color(0.5f, 0.5f, 0.5f, 1);

        if (playerHP < 2)
            UIHeart[1].color = new Color(0.5f, 0.5f, 0.5f, 1);

        if (playerHP < 3)
            UIHeart[2].color = new Color(0.5f, 0.5f, 0.5f, 1);

        if (playerHP <= 0)
        {
            playerHP = 0;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        
    }

}
