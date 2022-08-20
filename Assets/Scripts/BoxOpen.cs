using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxOpen : MonoBehaviour
{
    public Health health;
    public static bool Boxop;
    public static int Boxrand = 100;
    public Image[] SkillUI;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Boxop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(health.BoxDie)
        {
            health.BoxDie = false;
            
            anim.SetTrigger("BoxOpening");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            SkillUpgrade();
            Boxop = true;
            Destroy(this.gameObject, 1);
        }
    }

    void SkillUpgrade()
    {
        Boxrand = Random.Range(0, 100);
        // 검의 갯수
        if (Boxrand < 35)
        {
            SkillUI[0].gameObject.SetActive(true);
           /* while(true)
            {
                SkillUI[0].transform.position = Vector3.MoveTowards(SkillUI[0].transform.position, Point.position, 0.01f);
                if (SkillUI[0].transform.position == Point.position)
                    break;
            }*/
            
        }
        // 공격력 보너스
        else if (Boxrand < 70)
        {
            SkillUI[1].gameObject.SetActive(true);



        }
        // 체력회복
        else if (Boxrand < 85)
        {
            SkillUI[2].gameObject.SetActive(true);

        }
        //아이스
        else if (Boxrand < 100)
        {
            SkillUI[3].gameObject.SetActive(true);
        }
    }
}
