using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HGLShooter : MonoBehaviour
{
    public GameObject[] HGLArray; 
    public GameObject Player;
    public float Speed;
    public bool shooting;


    void Start()
    {
        Player = GameObject.Find("Player");
    }


    void Update()
    {
        if (Vector3.Distance(Player.transform.position, this.gameObject.transform.position) < 100 && !shooting)
        {
            int randomIndex = Random.Range(0, HGLArray.Length); 
            GameObject obj = Instantiate(HGLArray[randomIndex], transform.position, Quaternion.identity); 
            shooting = true;
        }
    }
}
