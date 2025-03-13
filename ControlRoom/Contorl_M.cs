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

    private bool isLightOn = true; // ����O�������L�ܼ�
    private bool isSpawning = false; // ����O�_�ͦ����󪺥��L�ܼ�


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

    // Start �O�b�{���}�l�ɰ��檺
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

    // Update �C�@�V���|����
    void Update()
    {
        // �� E ������O���}��
        if (Input.GetKeyDown(KeyCode.E))
        {
            isLightOn = !isLightOn; // �����O�����A
            SetLight(isLightOn);    // �ھڤ����᪺���A�]�m�O��
        }

        // �� F ���������ͦ��}��
        if (Input.GetKey(KeyCode.F))
        {
            isSpawning = !isSpawning; // �����ͦ����󪺪��A
            SetFood(isSpawning);      // �ھڤ����᪺���A�]�m����ͦ�
        }
    }

    // ����O���}�ҩ��ܷt
    public void SetLight(bool enable)
    {
        for (int i = 0; i < directionalLights.Length; i++)
        {
            if (directionalLights[i] != null)
            {
                if (enable)
                {
                    directionalLights[i].intensity = originalIntensities[i]; // �]�w��l�G��
                }
                else
                {
                    directionalLights[i].intensity = dimmedIntensity; // �]�w�ܷt�G��
                }
            }
        }
    }

    // ����O�_�ͦ�����
    public void SetFood(bool enable)
    {
        if (enable && Time.time > nextSpawnTime)
        {
            SpawnObject(); // �ͦ�����
            nextSpawnTime = Time.time + 1f / spawnRate; // �]�w�U�@���ͦ��ɶ�
        }
    }

    // �ͦ��H������
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

        Destroy(instance, 5f); // 5 ���P���ͦ�������
    }
}
