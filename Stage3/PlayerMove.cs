using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject Character;
    public GameObject Player;
    public GameObject DizzyEffect;
    public GameObject Target;
    public Animator animator;
    public GameObject Timer;
    public float speed = 5;
    public float dropSpeed = 10;
    public float StopSpeed = 0;
    public float trapSpeed = 3;
    private float MoveSpeed;

    private bool Stop;
    private bool Traped;
    private bool Slow;
    public bool Blind;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = true;
        Stop = false;
        Traped = false;
        MoveSpeed = dropSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        UpdateMoveSpeed();

        Player.transform.position = Vector3.MoveTowards(Player.transform.position, Target.transform.position, MoveSpeed * Time.deltaTime);
        Character.transform.position = new Vector3(Character.transform.position.x, Character.transform.position.y, Player.transform.position.z + 1);
        if(Player.transform.position.z == Target.transform.position.z)
        {
            Timer.GetComponent<GameTime>().levelCompleted = true;
        }
    }

    private void HandleMovement()
    {

        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;

            if (Input.gyro.enabled)
            {
                Vector3 gyroRotationRate = Input.gyro.rotationRate;

                float moveX = gyroRotationRate.y;
                float moveY = -gyroRotationRate.x;

                Character.transform.Translate(new Vector3(moveX, moveY, 0) * speed * Time.deltaTime);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            Character.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Character.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Character.transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            Character.transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

    private void UpdateMoveSpeed()
    {
        if (Traped)
        {
            MoveSpeed = trapSpeed;
            animator.SetBool("Dizzy", true);
            DizzyEffect.SetActive(true);
        }
        else if (Stop)
        {
            MoveSpeed = StopSpeed;
        }
        else if (Slow)
        {
            MoveSpeed -= 5f * Time.deltaTime;
        }
        else
        {
            MoveSpeed = dropSpeed;
            animator.SetBool("Dizzy", false);
            DizzyEffect.SetActive(false);
        }

        if (Blind)
        {
            RenderSettings.fogEndDistance = 30f;
            MoveSpeed = 5f;
        }
        else
        {
            RenderSettings.fogEndDistance = 100f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stop"))
        {
            Stop = true;
            Debug.Log("Stopped");
        }

        if (collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Trapped");
            Traped = true;
            Destroy(collision.gameObject);
            StartCoroutine(WaitForSecondsCoroutine(2.0f, () => Traped = false));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DecreaseZone")
        {
            Debug.Log("SlowMode");
            Slow = true;
        }

        if (other.gameObject.tag == "Blind")
        {
            Debug.Log("Blind");
            Blind = true;
            StartCoroutine(WaitForSecondsCoroutine(2.0f, () => Blind = false));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "DecreaseZone")
        {
            Debug.Log("SlowModeEnd");
            Slow = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stop"))
        {
            Stop = false;
            Debug.Log("Unstopped");
        }
    }

    private IEnumerator WaitForSecondsCoroutine(float seconds, System.Action callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
        Debug.Log("Trap effect ended");
    }
}
