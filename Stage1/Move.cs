using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float JumpSpeed;
    public float MaxSpeed = 500;
    private float Speed ;
    Rigidbody Player;
    public bool Traped;
    public bool binding;
    public bool Finish;
    public float clickThreshold = 5f;
    public float timeWindow = 1f;
    private int clickCount = 0;
    private float timer = 0f;
    public Animator anomator;
    public GameObject Shock;
    public GameObject Bind;
    public GameObject Timer;
    public GameObject Target;
    public MenuController menuC;

    public bool Slow;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody>();
        Speed = MaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > timeWindow)
        {
            clickCount = 0;
            timer = 0f;
        }
        Player.AddForce(transform.forward * Speed * Time.deltaTime);
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !Traped && !binding && menuC.isPaused == false && !Finish)
        {
            Player.AddForce(transform.up * JumpSpeed);
        }


        if (binding)
        {
            Bind.SetActive(true);
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    clickCount++;
                    timer = 0f;

                    if (clickCount >= clickThreshold)
                    {
                        Bind.SetActive(false);
                        clickCount = 0;
                        timer = 0f;
                        binding = false;
                        Speed = MaxSpeed;
                        JumpSpeed = 200;
                    }
                }
            }
        }
        if(Traped)
        {
            Shock.SetActive(true);
            JumpSpeed = 100;
            anomator.SetBool("isShock", true);
        }

        if(!Traped)
        {
            Shock.SetActive(false);
            JumpSpeed = 200f;
            anomator.SetBool("isShock", false);
        }

        if (Slow)
        {
            Speed = Mathf.Max(0, Speed - 0.1f); 
        }
        else if (Speed < MaxSpeed)
        {
            Speed = Mathf.Min(MaxSpeed, Speed + 0.1f); 
        }

        if(Finish)
        {
            Player.transform.position = Target.transform.position;
            Player.useGravity = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            Traped = true;
            StartCoroutine(WaitForSecondsCoroutine(2.0f, () => Traped = false));
        }
        if (collision.gameObject.tag == "Bind")
        {
            binding = true;
            Speed = 0;
            JumpSpeed = 0;
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Target")
        {        
            Timer.GetComponent<GameTime>().levelCompleted = true;
            Finish = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "WindZone")
        {
            Slow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "WindZone")
        {
            Slow = false;
        }
    }
    private IEnumerator WaitForSecondsCoroutine(float seconds, System.Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
        Debug.Log("Trap effect ended");
    }
}
