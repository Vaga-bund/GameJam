using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSword : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(10, 0, 0), 0.09f);

        if(transform.position.x > 10)
            Destroy(gameObject);
    }
}
