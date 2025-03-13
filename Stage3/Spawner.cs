using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject ObjToSpawn;

    public float SpawnTime;

    float Timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= SpawnTime)
        {
            Timer = 0f;
            SpawnOBJ();
        }
    }

    void SpawnOBJ()
    {
        Vector3 SpawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Instantiate(ObjToSpawn, SpawnPos, Quaternion.Euler(90, 0, 0));
    }
}
