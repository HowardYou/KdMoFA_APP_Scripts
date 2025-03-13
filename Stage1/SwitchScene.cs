using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScene : MonoBehaviour
{
    public GameObject Scene;
    private GameObject Player;
    public GameObject flash;
    public bool Rotate;
    public bool Exit;

    public GameObject firstTrigger;

    // Start is called before the first frame update
    void Start()
    {
        firstTrigger.SetActive(false);
        if (Player == null)
        {
            Player = GameObject.Find("Player(WaterBear)");
            if (Player == null)
            {
                Player = GameObject.Find("Player(Satsan)");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Rotate && !Exit)
        {
            Scene.transform.Rotate(0, 90, 0);
            // Player.transform.Rotate(0, 90, 0);
            Player.transform.position = this.gameObject.transform.position;
            Rotate = false;
            Exit = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(FlashCoroutine());
            Rotate = true;
            firstTrigger.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Exit = false;
    }

    private IEnumerator FlashCoroutine()
    {
        flash.SetActive(true);
        yield return new WaitForSeconds(1f);
        flash.SetActive(false);
    }
}
