using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRule : MonoBehaviour
{

    public GameObject Template;
    public GameObject TamplateEmpty;
    public GameObject SpawnTo;

    private float DistanceTravelled;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Spawned = Instantiate(Template, SpawnTo.transform);
        Spawned.transform.parent = transform;
        SpawnTo.transform.position += new Vector3(0, 0, -20);
        GameObject Spawned2 = Instantiate(Template, SpawnTo.transform);
        Spawned2.transform.parent = transform;
        SpawnTo.transform.position += new Vector3(0, 0, -20);
        GameObject Spawned3 = Instantiate(Template, SpawnTo.transform);
        Spawned3.transform.parent = transform;
        SpawnTo.transform.position += new Vector3(0, 0, -20);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, 5 * Time.deltaTime);
        if(transform.position.z - DistanceTravelled >= 20)
        {
            DistanceTravelled = transform.position.z;
            GameObject Spawned = Instantiate(Template, SpawnTo.transform);
            Spawned.transform.parent = transform;
        }
    }
}
