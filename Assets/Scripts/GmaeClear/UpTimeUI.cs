using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpTimeUI : MonoBehaviour
{
    public float time;
    public Text[] text;
    public int mun = 0;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("UpTime", time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpTime()
    {
        if(mun < 8)
        text[mun].gameObject.SetActive(true);

        mun++;
        if (mun < 13)
            Invoke("UpTime", time);
        else
            Application.Quit();
    }
}
