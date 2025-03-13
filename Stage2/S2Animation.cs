using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2Animation : MonoBehaviour
{

    public Animator animator;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.GetComponent<ClickObj>().Attacked)
        {
            animator.SetBool("Attacked", true);
        }
        if (Player.GetComponent<ClickObj>().Attacked == false)
        {
            animator.SetBool("Attacked", false);
        }
    }
}
