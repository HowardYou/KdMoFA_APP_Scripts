using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_K : MonoBehaviour
{
    public ControlRoom_Add controlRoom;
    public KelpLight kelpLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private bool m_BlockFood = false;
    public void OnSetFoodEvent(bool enable)
    {
        if (m_BlockFood || !enable)
        {
            return;
        }
        m_BlockFood = true;
        InvokeRepeating(nameof(CreatFoodByTime), 0f, 0.2f);
        Invoke(nameof(StopFood), 5f);
    }

    private void CreatFoodByTime()
    {
        controlRoom.SpawnObject();
    }

    private void StopFood()
    {
        CancelInvoke(nameof(CreatFoodByTime));
        m_BlockFood = false;
    }

    public void OnSetLightEvent(bool enable)
    {
        kelpLight.ToggleLightmapSet(!enable);
    }
    //------------------------------------------------------------------------------

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            controlRoom.setFood = !controlRoom.setFood; // Toggle the setFood state
        }

        // Call SpawnObject with the setFood state
        if (controlRoom.setFood)
        {
            controlRoom.SpawnObject(true);
        }

        if (Input.GetKeyDown(KeyCode.F)) // Change to your preferred key
        {
            // Toggle lightmap set
            kelpLight.ToggleLightmapSet();
        }
    }
}
