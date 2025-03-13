using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCon : MonoBehaviour
{

    public ParticleSystem Particle;
    public GameObject Player;
    public GameObject mushroom;
    private float timer;
    private float onTime = 8f;
    private float end = 11f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Cam");        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Particle.isPlaying)
        {
            timer += Time.deltaTime;
            if(timer >= onTime)
            {
                Player.GetComponent<ClickObj>().notPass = false;
                Player.GetComponent<ClickObj>().Moving = true;
            }
            if(timer >= end)
            {
                Particle.Stop();
                timer = 0f;
                mushroom.SetActive(false);
            }
        }
    }
}
