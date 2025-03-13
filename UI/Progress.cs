using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    public GameObject player;
    public Transform finishLine;
    public Slider progressBar;

    private float startDistance;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player(WaterBear)");
            if (player == null)
            {
                player = GameObject.Find("Cam");
                if (player == null)
                {
                    player = GameObject.Find("Player");
                }
            }
        }

        if (progressBar == null)
        {
            progressBar = GetComponent<Slider>();
        }

        startDistance = Vector3.Distance(player.transform.position, finishLine.position);
        progressBar.minValue = 0;
        progressBar.maxValue = startDistance;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, finishLine.position);
        progressBar.value = startDistance - distance;
    }
}
