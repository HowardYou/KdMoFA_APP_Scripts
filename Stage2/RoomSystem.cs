using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerat : MonoBehaviour
{
    public GameObject Player;
    public GameObject specialRoom;
    public GameObject[] category1Prefabs; // 第一類房間
    public GameObject[] category2Prefabs; // 第二類房間
    public GameObject[] otherObjects;

    private List<GameObject> generatedRooms = new List<GameObject>();
    public float activationDistance = 50f;

    void Start()
    {
        Player = GameObject.Find("Cam");
        StartCoroutine(CheckRoomActivation());
        GenerateRooms();
    }

    public void GenerateRooms()
    {
        if (specialRoom == null)
        {
            Debug.LogError("Special room not assigned!");
            return;
        }

        if (otherObjects.Length < 10)
        {
            Debug.LogError("Not enough objects in otherObjects array!");
            return;
        }

        Vector3 previousSpecialRoomPosition = Vector3.zero;

        for (int z = 0; z < 10; z++)
        {
            List<GameObject> chosenRooms = new List<GameObject>();

            chosenRooms.Add(specialRoom);

            // 從兩個類別中各選一個房間
            int firstIndex = Random.Range(0, category1Prefabs.Length);
            int secondIndex = Random.Range(0, category2Prefabs.Length);

            chosenRooms.Add(category1Prefabs[firstIndex]);
            chosenRooms.Add(category2Prefabs[secondIndex]);

            // 隨機打亂順序
            for (int i = 0; i < chosenRooms.Count; i++)
            {
                GameObject temp = chosenRooms[i];
                int randomIndex = Random.Range(i, chosenRooms.Count);
                chosenRooms[i] = chosenRooms[randomIndex];
                chosenRooms[randomIndex] = temp;
            }

            float startX = previousSpecialRoomPosition.x - 10;

            // 生成房間
            for (int x = 0; x < chosenRooms.Count; x++)
            {
                Vector3 position = new Vector3(startX + x * 10, 0, z * 40);
                GameObject room = Instantiate(chosenRooms[x], position, Quaternion.identity);
                generatedRooms.Add(room);

                if (chosenRooms[x] == specialRoom)
                {
                    previousSpecialRoomPosition = position;
                }
            }

            // 設置其他物件的位置
            if (z < otherObjects.Length)
            {
                Vector3 otherObjectPosition = new Vector3(previousSpecialRoomPosition.x, 0, previousSpecialRoomPosition.z + 30);
                otherObjects[z].transform.position = otherObjectPosition;
            }
        }
    }

    IEnumerator CheckRoomActivation()
    {
        while (true)
        {
            foreach (GameObject room in generatedRooms)
            {
                float distance = Vector3.Distance(room.transform.position, Player.transform.position);
                room.SetActive(distance <= activationDistance);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
