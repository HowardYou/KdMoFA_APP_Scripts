using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_S : MonoBehaviour
{

    public GameObject[] extraTrap;
    [SerializeField] private LightSettings LS;

    private bool m_BlockFood = false;
    public void OnSetFoodEvent(bool enable)
    {
        if (m_BlockFood || !enable)
        {
            return;
        }
        ControlTrap(true);
        Invoke(nameof(StopFood), 5f);
    }

    private void StopFood()
    {
        ControlTrap(false);
        m_BlockFood = false;
    }

    public void OnSetLightEvent(bool enable)
    {
        LS.ToggleLightmapSet(!enable);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                bool isActive = !extraTrap[0].activeSelf;
                ControlTrap(isActive);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (LS != null)
            {
                LS.ToggleLightmapSet();
            }
        }
    }
    public void ControlTrap(bool enable)
    {
        foreach (GameObject trap in extraTrap)
        {
            trap.SetActive(enable);
        }
    }
}
