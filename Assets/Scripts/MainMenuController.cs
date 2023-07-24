using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject settingMenu;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] AudioMixer audioMixer;

    private const string MusicVolumeParam = "Music Volume";
    private const string SFXVolumeParam = "SFX Volume";

    // Start is called before the first frame update
    void Start()
    {
        settingMenu.SetActive(false);

        musicSlider.value = GetMusicVolume();
        sfxSlider.value = GetSFXVolume();
    }

    public void OnStartButtonClick()
    {
       
    }

    public void OnSettingsButtonClick()
    {
        settingMenu.SetActive(true);
    }

    public void OnCloseSettingsClick()
    {
        settingMenu.SetActive(false);
    }

    public void OnMusicVolumeChanged(float value)
    {
        SetMusicVolume(value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        SetSFXVolume(value);
    }

    public void OnExitButtonClick()
    {

    }

    private float GetMusicVolume()
    {
        audioMixer.GetFloat(MusicVolumeParam, out float volume);
        return DbToFloat(volume);
    }

    private void SetMusicVolume(float volume)
    {
        float db = FloatToDb(volume);
        audioMixer.SetFloat(MusicVolumeParam, db);
        
    }

    private float GetSFXVolume()
    {
        audioMixer.GetFloat(SFXVolumeParam, out float volume);
        return DbToFloat(volume);
    }

    private void SetSFXVolume(float volume)
    {
        float db = FloatToDb(volume);
        audioMixer.SetFloat(SFXVolumeParam, db);
    }

    private float FloatToDb(float value)
    {
        float db = 75 * Mathf.Log10(value * 20 + 1) - 80;
        return Mathf.Clamp(db, -80, 20);
    }

    private float DbToFloat(float db)
    {
        float value = (Mathf.Pow(10, (db + 80)/75) - 1) / 20;
        return Mathf.Clamp(value, 0, 1);
    }
}
