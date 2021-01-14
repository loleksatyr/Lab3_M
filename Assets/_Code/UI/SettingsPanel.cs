using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsPanel : UiPanel
{
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider effectsVolumeSlider;
    [SerializeField] private AudioMixer audioMixer;


    protected override void OnInitialise() { }

    float GetParameterVolumeNormalized(string parameterName)
    {
        float masterVolume = 0;
        audioMixer.GetFloat(parameterName, out masterVolume);
        return 1 - (masterVolume / -80f);
    }

    protected override void OnOpen()
    {
        masterVolumeSlider.value = GetParameterVolumeNormalized("Master");
        musicVolumeSlider.value = GetParameterVolumeNormalized("Music");
        effectsVolumeSlider.value = GetParameterVolumeNormalized("SFX");
    }

    protected override void OnClose() { }

    public void OnMasterSliderChange()
    {
        float masterVolume = (1 - masterVolumeSlider.value) * -80f;
        audioMixer.SetFloat("Master", masterVolume);
    }

    public void OnMusicSliderChange()
    {
        float masterVolume = (1 - musicVolumeSlider.value) * -80f;
        audioMixer.SetFloat("Music", masterVolume);
    }

    public void OnEffectsSliderChange()
    {
        float masterVolume = (1 - effectsVolumeSlider.value) * -80f;
        audioMixer.SetFloat("SFX", masterVolume);
    }
}
