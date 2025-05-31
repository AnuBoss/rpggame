using UnityEngine;
using UnityEngine.UI;
public class SettingsAudio : MonoBehaviour
{
    [SerializeField] Slider MasterVolume;
    [SerializeField] Slider BGMVolume;
    [SerializeField] Slider SFXVolume;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMasterVolumeChanged()
    {
        float value = MasterVolume.value;
        AudioManager.instance.SetMasterVolume(value);
    }

    public void OnBGMVolumeChanged()
    {
        float value = BGMVolume.value;
        AudioManager.instance.SetBGMVolume(value);
    }

    public void OnSFXVolumeChanged()
    {
        float value = SFXVolume.value;
        AudioManager.instance.SetSFXVolume(value);
    }
}
