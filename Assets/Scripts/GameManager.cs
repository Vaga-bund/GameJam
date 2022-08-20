using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image[] UIHeart;

    public ArkanoidManager arkanoidManager;

    public bool isBlockMoving;

    public int MonsterNum = 1;
    public int Con = 1;
    public int playerHP = 3;
    public int iceSK = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHP == 3)
        {
            UIHeart[2].color = new Color(1f, 1f, 1f, 1);
            UIHeart[1].color = new Color(1f, 1f, 1f, 1);
            UIHeart[0].color = new Color(1f, 1f, 1f, 1);
        }
        if (playerHP < 3)
        {
            UIHeart[2].color = new Color(0.5f, 0.5f, 0.5f, 1);
            UIHeart[1].color = new Color(1f, 1f, 1f, 1);
            UIHeart[0].color = new Color(1f, 1f, 1f, 1);
        }
        if (playerHP < 2)
        {
            UIHeart[1].color = new Color(0.5f, 0.5f, 0.5f, 1);
            UIHeart[0].color = new Color(1f, 1f, 1f, 1);
        }
        if (playerHP < 1)
            UIHeart[0].color = new Color(0.5f, 0.5f, 0.5f, 1);

        if (playerHP <= 0)
        {
            playerHP = 0;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (BoxOpen.Boxop)
        {
            BoxOpen.Boxop = false;
            BoxSkill();
        }
    }

    void BoxSkill()
    {
        // 검의 갯수
        if (BoxOpen.Boxrand < 35)
        {
            Debug.Log(BoxOpen.Boxrand + " 검의 갯수");
            arkanoidManager.IncreaseAmmo();
        }
        // 공격력 보너스
        else if(BoxOpen.Boxrand < 70)
        {
            Debug.Log(BoxOpen.Boxrand + " 공격력 보너스");
        }
        // 체력회복
        else if(BoxOpen.Boxrand < 85)
        {
            Debug.Log(BoxOpen.Boxrand + " 체력회복");
            if(playerHP < 3)
                playerHP++;
        }
        //아이스
        else if(BoxOpen.Boxrand < 100)
        {
            Debug.Log(BoxOpen.Boxrand + " 아이스");
            iceSK++;
        }
    }
}
