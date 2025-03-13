using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Template : MonoBehaviour
{

    public float Speed = -5;
    public float zBound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime);

        if(transform.position.z <= zBound)
        {
            Destroy(gameObject);
        }
    }
}
