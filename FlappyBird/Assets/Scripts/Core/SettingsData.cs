using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsData : MonoBehaviour
{
    public int musicVol = 4;
    public int sfxVol = 4;
    public static SettingsData instance;

    private SettingsData() { }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}