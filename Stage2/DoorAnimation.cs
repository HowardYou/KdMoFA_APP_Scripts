using UnityEngine;
using System.Collections;

public class DoorAnimation : MonoBehaviour
{
    public Animator animator;
    public GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("Cam");
    }
    public void DisableAnimatorComponent()
    {
        Debug.Log("DisableAnimatorComponent called.");
        if (animator != null)
        {
            Player.GetComponent<ClickObj>().AnimationPlaying = false;
            animator.enabled = false;
            Debug.Log("Animator component disabled.");
        }
        else
        {
            Debug.LogError("Animator is not assigned.");
        }
    }

}
