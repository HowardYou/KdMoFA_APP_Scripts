using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PassText : MonoBehaviour
{
    public Text resultText;
    public GameTime GT;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GT.levelCompleted)
        {
            // 當關卡完成時顯示剩餘的時間
            resultText.text = GT.GetCountdownMessage();
        }
    }
}
