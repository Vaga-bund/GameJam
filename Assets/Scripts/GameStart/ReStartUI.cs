using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReStartUI : MonoBehaviour
{
    Text flashingText;


    // Use this for initialization


    void Start()
    {



        flashingText = GetComponent<Text>();



        StartCoroutine(BlinkText());



    }



    public IEnumerator BlinkText()
    {



        while (true)
        {


            flashingText.text = "";



            yield return new WaitForSeconds(0.5f);



            flashingText.text = ">클릭시 재시작<";



            yield return new WaitForSeconds(0.75f);


        }



    }
}
