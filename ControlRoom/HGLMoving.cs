using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGLMoving : MonoBehaviour
{

    public float HGLSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.back * HGLSpeed * Time.deltaTime);
        if (this.gameObject.transform.position.z < -10)
        {
            Destroy(this.gameObject);
        }
    }
}
