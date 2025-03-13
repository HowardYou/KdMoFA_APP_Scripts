using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject FollowTarget;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(FollowTarget.transform.position.x, -10f, 10f),
            Mathf.Clamp(FollowTarget.transform.position.y, -10f, 10f),
            transform.position.z);
    }
}
