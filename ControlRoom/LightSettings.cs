using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LightSettings : MonoBehaviour
{
    [SerializeField] private Texture2D[] lightsSet1;
    [SerializeField] private Texture2D[] lightmapDirSet1;
    [SerializeField] private Texture2D[] lightsSet2;
    [SerializeField] private Texture2D[] lightmapDirSet2;

    private LightmapData[] originalLightmaps;
    public bool isUsingSet1 = true;

    void Start()
    {
        originalLightmaps = LightmapSettings.lightmaps;

        lightsSet1 = Resources.LoadAll<Texture2D>("SpicyLevel_Light/Color").OrderBy(t => t.name).ToArray();
        lightmapDirSet1 = Resources.LoadAll<Texture2D>("SpicyLevel_Light/Dir").OrderBy(t => t.name).ToArray();
        lightsSet2 = Resources.LoadAll<Texture2D>("SpicyLevel_Dark/Color").OrderBy(t => t.name).ToArray();
        lightmapDirSet2 = Resources.LoadAll<Texture2D>("SpicyLevel_Dark/Dir").OrderBy(t => t.name).ToArray();

        // Apply the initial lightmap data
        ApplyLightmapData(isUsingSet1);
    }

    public void ToggleLightmapSet(bool bEnable)
    {
        isUsingSet1 = bEnable;
        ApplyLightmapData(isUsingSet1);
    }

    public void ToggleLightmapSet()
    {
        isUsingSet1 = !isUsingSet1;
        ApplyLightmapData(isUsingSet1);
    }

    void ApplyLightmapData(bool enableSet1)
    {
        Texture2D[] lights = enableSet1 ? lightsSet1 : lightsSet2;
        Texture2D[] lightmapDirs = enableSet1 ? lightmapDirSet1 : lightmapDirSet2;

        LightmapData[] newLightmaps = new LightmapData[lights.Length];

        for (int i = 0; i < newLightmaps.Length; i++)
        {
            newLightmaps[i] = new LightmapData
            {
                lightmapColor = lights[i],
                lightmapDir = lightmapDirs[i],
                shadowMask = originalLightmaps[i].shadowMask
            };
        }

        LightmapSettings.lightmaps = newLightmaps;
    }
}
