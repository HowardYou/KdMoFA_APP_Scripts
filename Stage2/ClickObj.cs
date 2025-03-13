using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObj : MonoBehaviour
{
    public GameStart GS;
    public MenuController MC;
    public Camera Cam;
    public LayerMask mask;
    public GameObject Player;
    public GameObject Character;
    public GameObject Timer;

    public GameObject[] ChoosePoint;
    private int CurrentPoint = 0;


    public bool Pass;
    public bool notPass;
    public bool Moving;
    public bool endMoving;
    public bool AnimationPlaying;
    public bool Attacked;

    public float Speed;

    RaycastHit hit;
    Ray ray;
    Vector3 hitOBJ;
    Vector3 hitOBJ_P;
    Animator animator;
    Animator animator2;
    public Animator CharacterAnimator;

    Transform MovePoint;

    private void Start()
    {
        print(Cam.name);
    }

    void Update()
    {
        if (Character.transform.position.z == ChoosePoint[10].transform.position.z)
        {
            Timer.GetComponent<GameTime>().levelCompleted = true;
        }
        // Detect touch input
        if (Input.touchCount > 0 && !Moving && !Pass && !notPass && !AnimationPlaying && !GS.isOpening && !MC.isPaused)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                ray = Cam.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit, 100f, mask))
                {
                    MovePoint = FindChildWithTag(hit.transform.parent, "StopPoint");

                    Transform FindChildWithTag(Transform parent, string tag)
                    {
                        foreach (Transform child in parent)
                        {
                            if (child.CompareTag(tag))
                            {
                                return child;
                            }
                        }
                        return null;
                    }

                    hitOBJ_P = hit.transform.parent.position;
                    hitOBJ = MovePoint.position;

                    if (hit.collider.tag == "RightDoor")
                    {
                        Debug.Log(hit.transform.name);
                        Pass = true;
                        CurrentPoint = (CurrentPoint + 1) % ChoosePoint.Length;
                        Debug.Log("Current Point: " + CurrentPoint);
                        Debug.Log(ChoosePoint[CurrentPoint].transform.position.z);
                        animator = hit.transform.Find("CorrectDoor").GetComponent<Animator>();
                        animator2 = hit.transform.Find("EndDoor").GetComponent<Animator>();
                        AnimationPlaying = true;
                        if (animator != null)
                        {
                            Debug.Log("FindAnimator");
                            animator.enabled = true;
                        }
                        if (animator2 != null)
                        {
                            Debug.Log("FindAnimator2");
                            animator2.enabled = true;
                        }
                    }
                    if (hit.collider.tag == "WrongDoor")
                    {
                        Debug.Log(hit.transform.name);
                        notPass = true;
                        Debug.Log("Current Point: " + CurrentPoint);
                        Debug.Log(ChoosePoint[CurrentPoint].transform.position.z);
                        animator = hit.transform.Find("WrongDoor").GetComponent<Animator>();
                        AnimationPlaying = true;
                        if (animator != null)
                        {
                            Debug.Log("FindAnimator");
                            animator.enabled = true;
                        }
                    }
                    if (hit.collider.tag == "TrapDoor")
                    {
                        Debug.Log(hit.transform.name);
                        notPass = true;
                        Attacked = true;
                        Debug.Log("Current Point: " + CurrentPoint);
                        Debug.Log(ChoosePoint[CurrentPoint].transform.position.z);
                        animator = hit.transform.Find("WrongDoor").GetComponent<Animator>();
                        AnimationPlaying = true;
                        if (animator != null)
                        {
                            Debug.Log("FindAnimator");
                            animator.enabled = true;
                        }
                    }
                }               
            }
        }

        if (Pass)
        {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, hitOBJ, Speed * Time.deltaTime);
            if (Vector3.Distance(Player.transform.position, hitOBJ) < 0.1f) // 你可以調整這個閾值
            {
                Pass = false;
                Moving = true;
            }
        }

        if (notPass)
        {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, hitOBJ, Speed * Time.deltaTime);
        }

        if (Moving)
        {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y, ChoosePoint[CurrentPoint].transform.position.z), Speed * Time.deltaTime);
            if (Player.transform.position.z == ChoosePoint[CurrentPoint].transform.position.z)
            {
                endMoving = true;
            }
        }

        if (endMoving && Moving)
        {
            Player.transform.position = Vector3.MoveTowards(Player.transform.position, ChoosePoint[CurrentPoint].transform.position, Speed * Time.deltaTime);
            {
                if (Player.transform.position == ChoosePoint[CurrentPoint].transform.position)
                {
                    Moving = false;
                    endMoving = false;
                    Character.transform.position = new Vector3(ChoosePoint[CurrentPoint].transform.position.x, ChoosePoint[CurrentPoint].transform.position.y, ChoosePoint[CurrentPoint].transform.position.z);
                    if (Attacked)
                    {
                        StartCoroutine(WaitForSecondsCoroutine(3.0f, () => Attacked = false));
                    }
                }
            }
        }
    }


    private IEnumerator WaitForSecondsCoroutine(float seconds, System.Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
        Debug.Log("Trap effect ended");
    }

}
