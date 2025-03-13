using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contorl_M : MonoBehaviour
{
    public Light[] directionalLights; // Change from single Light to array of Lights
    public float dimmedIntensity = 0.5f;
    private float[] originalIntensities; // Array to store the original intensities of each light

    public GameObject[] prefabs;
    Transform referenceObject;
    public float spawnRate = 1f;
    private float nextSpawnTime;
    private Vector3 spawnArea = new Vector3(10, 0, 10);

    private bool isLightOn = true; // 北縊ガ狶跑计
    private bool isSpawning = false; // 北琌ネΘンガ狶跑计


    private bool m_BlockFood = false;
    public void OnSetFoodEvent(bool enable)
    {
        if(m_BlockFood || !enable)
        {
            return;
        }
        m_BlockFood = true;
        InvokeRepeating(nameof(CreatFoodByTime), 0f, 0.2f);
        Invoke(nameof(StopFood), 5f);
    }

    private void CreatFoodByTime()
    {
        SpawnObject();
    }

    private void StopFood()
    {
        CancelInvoke(nameof(CreatFoodByTime));
        m_BlockFood = false;
    }

    public void OnSetLightEvent(bool enable)
    {
        SetLight(!enable);
    }

    //-------------------------------------------------------------------------------

    // Start 琌祘Α秨﹍磅︽
    void Start()
    {
        referenceObject = GameObject.Find("Character").transform;

        if (directionalLights != null && directionalLights.Length > 0)
        {
            originalIntensities = new float[directionalLights.Length];

            // Store the original intensity for each light
            for (int i = 0; i < directionalLights.Length; i++)
            {
                if (directionalLights[i] != null)
                {
                    originalIntensities[i] = directionalLights[i].intensity;
                }
            }
        }
    }

    // Update –碫常穦磅︽
    void Update()
    {
        //  E 龄ち传縊秨闽
        if (Input.GetKeyDown(KeyCode.E))
        {
            isLightOn = !isLightOn; // ち传縊篈
            SetLight(isLightOn);    // 沮ち传篈砞竚縊
        }

        //  F 龄ち传ンネΘ秨闽
        if (Input.GetKey(KeyCode.F))
        {
            isSpawning = !isSpawning; // ち传ネΘン篈
            SetFood(isSpawning);      // 沮ち传篈砞竚ンネΘ
        }
    }

    // 北縊秨币┪跑穞
    public void SetLight(bool enable)
    {
        for (int i = 0; i < directionalLights.Length; i++)
        {
            if (directionalLights[i] != null)
            {
                if (enable)
                {
                    directionalLights[i].intensity = originalIntensities[i]; // 砞﹚﹍獹
                }
                else
                {
                    directionalLights[i].intensity = dimmedIntensity; // 砞﹚跑穞獹
                }
            }
        }
    }

    // 北琌ネΘン
    public void SetFood(bool enable)
    {
        if (enable && Time.time > nextSpawnTime)
        {
            SpawnObject(); // ネΘン
            nextSpawnTime = Time.time + 1f / spawnRate; // 砞﹚ΩネΘ丁
        }
    }

    // ネΘ繦诀ン
    void SpawnObject()
    {
        Vector3 referencePosition = referenceObject.position;

        Vector3 spawnPosition = new Vector3(
            referencePosition.x + Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            referencePosition.y + 10,
            referencePosition.z + Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        );

        int prefabIndex = Random.Range(0, prefabs.Length);
        GameObject selectedPrefab = prefabs[prefabIndex];
        Quaternion randomRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        GameObject instance = Instantiate(selectedPrefab, spawnPosition, randomRotation);

        if (instance.GetComponent<Rigidbody>() == null)
        {
            instance.AddComponent<Rigidbody>();
        }

        Destroy(instance, 5f); // 5 綪反ネΘン
    }
}
