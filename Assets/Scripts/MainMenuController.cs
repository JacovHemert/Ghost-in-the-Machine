using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
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
        SceneManager.LoadSceneAsync("Scenes/Main Scene");
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
        Application.Quit();
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

    // Converts a float value (0 to 1) to a decibel value (-80 to 20)
    private float FloatToDb(float value)
    {
        // Don't ask where these coefficients came from
        float db = 50 * Mathf.Log10(value * 100 + 1) - 80;
        return Mathf.Clamp(db, -80, 20);
    }

    // Converts a decibel value (-80 to 20) to a float value (0 to 1)
    private float DbToFloat(float db)
    {
        float value = (Mathf.Pow(10, (db + 80)/50) - 1) / 100;
        return Mathf.Clamp(value, 0, 1);
    }
}
