using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterRandom : MonoBehaviour
{
    public GameObject[] MonsterObjs;
    public GameObject BoxObjs;

    public Quaternion QI = Quaternion.identity; 


    public GameManger gameManger;
    public Transform MonsterGroup;
    int fox = 0;

    int boxApp = 1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnMonster();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManger.Con == 3)
        {
            gameManger.MonsterNum = 3;
        }
    }

    public void SpawnMonster()
    {

        
        if(gameManger.iceSK == 0)
        {

            List<Vector2> SpawnList = new List<Vector2>();
            for (int i = 0; i < 5; i++)
                SpawnList.Add(new Vector2(9, -4 + i * 2f));



            for (int i = 0; i < gameManger.MonsterNum; i++)
            {
                int rand = Random.Range(0, SpawnList.Count);
                // 소환될 적
                int ranMonster = RandomMonster();

                Transform TR = Instantiate(MonsterObjs[ranMonster], SpawnList[rand], QI).transform;
                TR.SetParent(MonsterGroup);



                SpawnList.RemoveAt(rand);
            }

            if (boxApp == 1)
            {
                Instantiate(BoxObjs, SpawnList[Random.Range(0, SpawnList.Count)], QI).transform.SetParent(MonsterGroup);

            }


            boxApp++;

            if (boxApp > 3)
                boxApp = 1;

            fox = 0;

            if (gameManger.Con != 3)
            {
                if (gameManger.MonsterNum < 3)
                    gameManger.MonsterNum++;
                else
                    gameManger.MonsterNum = 1;
            }



            gameManger.isBlockMoving = true;


            for (int i = 0; i < MonsterGroup.childCount; i++)
                StartCoroutine(MonsterMoveLeft(MonsterGroup.GetChild(i)));
        }
        else
        {
            gameManger.iceSK = 0;
        }
        
    }

    IEnumerator MonsterMoveLeft(Transform TR)
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 targetPos = TR.position + new Vector3(-2f, 0, 0);

       
        float TT = 100f;
        while (true)
        {
            yield return null;
            TT -= Time.deltaTime * 10f;
            TR.position = Vector3.MoveTowards(TR.position, targetPos + new Vector3(-1, 0, 0), TT);
            if (TR.position == targetPos + new Vector3(-1, 0, 0))
                break;

        }

        TT = 0.9f;
        while (true)
        {
            yield return null;
            TT -= Time.deltaTime;
            TR.position = Vector3.MoveTowards(TR.position, targetPos, TT);
            if (TR.position == targetPos) break;
        }

        gameManger.isBlockMoving = false;

        //끝까지 갔을때 데미지 주고 파괴
        if (targetPos.x < -6)
        {
            if(TR.CompareTag("Monster"))
            {
                gameManger.playerHP--;
            }
            Destroy(TR.gameObject);
        }
    }

    int RandomMonster()
    {
        int rand = 0;
        if (gameManger.Con == 1)
        {
            return 0;
        }
        else if(gameManger.Con == 2)
        {
            rand = Random.Range(0, 10);

            if (rand < 8)
                return 0;
            else
                return 1;
        }
        else if(gameManger.Con == 3)
        {
            if(fox == 0)
            {
                fox = 1;
                return 2;
            }

            rand = Random.Range(0, 2);
            return rand;
        }
        return 0;
    }

    int MonsterHP(int mon)
    {
        Debug.Log("1");
        if (mon == 0)
            return 10;
        else if (mon == 1)
            return 30;
        else if (mon == 2)
            return 120;


        return 0;
    }

    
}
