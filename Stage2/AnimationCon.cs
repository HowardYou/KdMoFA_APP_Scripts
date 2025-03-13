using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCon : MonoBehaviour
{

    public GameObject Player;
    public GameObject mushroom;
   
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Cam");
        //OnAnimationStart();
        
    }

    // Update is called once per frame
    void Update()
    {        
        
    }

    public void OnAnimationStart()
    {
        
    }

    public void OnAnimationEnd()
    {
        Player.GetComponent<ClickObj>().Moving = true;
        Player.GetComponent<ClickObj>().notPass = false;
    }

    public void ActiveOff()
    {
        mushroom.SetActive(false);
    }
}
