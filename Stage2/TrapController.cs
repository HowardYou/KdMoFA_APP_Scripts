using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public GameObject Player;
    public GameObject cameraLocation;
    public GameObject Trap;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Cam");
        Trap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Player.transform.position, cameraLocation.transform.position) < 0.1f)
        {
            Trap.SetActive(true);
            Debug.Log("AnimationPlay");
        }
    }
}
