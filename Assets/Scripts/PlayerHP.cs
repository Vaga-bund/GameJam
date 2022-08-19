using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHP : MonoBehaviour
{
    public GameManger gameManger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<Text>().text = gameManger.playerHP.ToString();
    }
}
