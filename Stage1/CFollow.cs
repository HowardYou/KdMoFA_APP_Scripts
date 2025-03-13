using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFollow : MonoBehaviour
{

    public GameObject Cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Cam.transform.position = new Vector3(Cam.transform.position.x, this.gameObject.transform.position.y, Cam.transform.position.z);
        }
    }
}
