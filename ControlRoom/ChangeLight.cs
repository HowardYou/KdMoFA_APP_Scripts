using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

public class ChangeLight : MonoBehaviour
{
    public Texture2D[] lightmapColorSet1; 
    public Texture2D[] lightmapDirSet1; 
    public Texture2D[] lightmapColorSet2; 
    public Texture2D[] lightmapDirSet2; 

    private bool isUsingSet1 = true;

    void Start()
    {
        ApplyLightmapData(lightmapColorSet1, lightmapDirSet1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (isUsingSet1)
            {
                ApplyLightmapData(lightmapColorSet2, lightmapDirSet2);
            }
            else
            {
                ApplyLightmapData(lightmapColorSet1, lightmapDirSet1);
            }
            isUsingSet1 = !isUsingSet1;
        }
    }

    void ApplyLightmapData(Texture2D[] lightmapColors, Texture2D[] lightmapDirs)
    {
        LightmapData[] newLightmaps = new LightmapData[lightmapColors.Length];

        for (int i = 0; i < newLightmaps.Length; i++)
        {
            newLightmaps[i] = new LightmapData();
            newLightmaps[i].lightmapColor = lightmapColors[i];
            newLightmaps[i].lightmapDir = lightmapDirs[i];
        }

        LightmapSettings.lightmaps = newLightmaps;
    }
}
