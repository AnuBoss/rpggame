using UnityEngine;
using UnityEngine.UI;
public class SettingsAudio : MonoBehaviour
{
    [SerializeField] Slider bgmVloume;
    [SerializeField] Slider masterVloume;
    [SerializeField] Slider sfxVloume;
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
        float value = masterVloume.value;
        AudioManager.instance.SetMasterVolume(value);
    }

    public void OnBGMVolumeChanged()
    {
        float value = bgmVloume.value;
        AudioManager.instance.SetBGMVolume(value);
    }

    public void OnSFXVolumeChanged()
    {
        float value = sfxVloume.value;
        AudioManager.instance.SetSFXVolume(value);
    }
}
