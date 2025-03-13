using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRoom_Light : MonoBehaviour
{
    public Light directionalLight;
    public float dimmedIntensity = 0.5f;
    private float originalIntensity;

    void Start()
    {
        if (directionalLight != null)
        {
            originalIntensity = directionalLight.intensity;
        }
    }

    void Update()
    {
        if (directionalLight != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                directionalLight.intensity = dimmedIntensity;
            }

            if (Input.GetKeyUp(KeyCode.F))
            {
                directionalLight.intensity = originalIntensity;
            }
        }

    }

}
