using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    public GameObject HGL;
    public GameObject Player;
    public float Speed;
    public bool shooting;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Player.transform.position, this.gameObject.transform.position) < 100 && !shooting)
        {
            GameObject obj = Instantiate(HGL, transform.position, Quaternion.identity);
            shooting = true;
        }
    }
}
