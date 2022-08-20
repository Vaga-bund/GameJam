using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScePlayer : MonoBehaviour
{
    public GameObject swordObj;
    public GameObject targetPosition;
    Transform trans;
    public Quaternion QI = Quaternion.identity;
    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        Invoke("SwordShooting", 2.4f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SwordShooting()
    {
        Transform TR = Instantiate(swordObj, trans.position + new Vector3(1, 0, 0), QI).transform;

        Invoke("SwordShooting", 2.5f);
    }
}
