using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRoom_Add : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform referenceObject;
    public float spawnRate = 1f;
    private float nextSpawnTime;
    public bool setFood = false;
    private Vector3 spawnArea = new Vector3(10, 0, 10);

    // This method can be called from another script
    public void SpawnObject(bool enable)
    {
        if (enable && Time.time >= nextSpawnTime)
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

            Destroy(instance, 5f);
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    public void SpawnObject()
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

        Destroy(instance, 5f);
    }
}
