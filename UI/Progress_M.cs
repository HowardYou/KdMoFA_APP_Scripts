using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress_M : MonoBehaviour
{
    public GameObject player;
    public Transform finishLine;
    public Slider progressBar;

    private float startDistance;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Character");

        }
        if (progressBar == null)
        {
            progressBar = GetComponent<Slider>();
        }

        startDistance = Mathf.Abs(player.transform.position.z - finishLine.position.z);
        progressBar.minValue = 0;
        progressBar.maxValue = startDistance;

    }

    void Update()
    {
        float distance = Mathf.Abs(player.transform.position.z - finishLine.position.z);
        progressBar.value = startDistance - distance;
    }
}
