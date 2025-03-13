using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;

public class KelpLight : MonoBehaviour
{
    [SerializeField] private Texture2D[] lightsSet1;
    [SerializeField] private Texture2D[] lightmapDirSet1;
    [SerializeField] private Texture2D[] lightsSet2;
    [SerializeField] private Texture2D[] lightmapDirSet2;

    public GameObject Light1;
    public GameObject Light2;
    public GameObject Light3;

    private LightmapData[] originalLightmaps;
    private bool isUsingSet1 = true;

    void Start()
    {
        originalLightmaps = LightmapSettings.lightmaps;
        LoadTextureSets();
        ApplyLightmapData(isUsingSet1);
    }

    public void ToggleLightmapSet()
    {
        isUsingSet1 = !isUsingSet1;
        ApplyLightmapData(isUsingSet1);
    }

    public void ToggleLightmapSet(bool bEnable)
    {
        isUsingSet1 = bEnable;
        ApplyLightmapData(isUsingSet1);
    }

    private void LoadTextureSets()
    {
        lightsSet1 = Resources.LoadAll<Texture2D>("KelpLevel_Light/Color").OrderBy(t => t.name).ToArray();
        lightmapDirSet1 = Resources.LoadAll<Texture2D>("KelpLevel_Light/Dir").OrderBy(t => t.name).ToArray();
        lightsSet2 = Resources.LoadAll<Texture2D>("KelpLevel_Dark/Color").OrderBy(t => t.name).ToArray();
        lightmapDirSet2 = Resources.LoadAll<Texture2D>("KelpLevel_Dark/Dir").OrderBy(t => t.name).ToArray();
    }

    private void ApplyLightmapData(bool useSet1)
    {
        Texture2D[] lights = useSet1 ? lightsSet1 : lightsSet2;
        Texture2D[] lightmapDirs = useSet1 ? lightmapDirSet1 : lightmapDirSet2;

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

        // 切換燈光的開啟狀態
        Light1.SetActive(useSet1);
        Light2.SetActive(useSet1);
        Light3.SetActive(useSet1);
    }
}
