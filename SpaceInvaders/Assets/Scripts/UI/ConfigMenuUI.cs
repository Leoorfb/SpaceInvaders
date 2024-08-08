using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe que controla a UI do Menu de configurações.
/// </summary>
public class ConfigMenuUI : MonoBehaviour
{
    [SerializeField] GameObject ReturnMenu;
    [SerializeField] Slider mainVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] Button returnMenuButton;

    AudioManager audioManager;



    private void Start()
    {
        audioManager = AudioManager.Instance;

        mainVolumeSlider.onValueChanged.AddListener(OnMainVolumeSliderChange);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeSliderChange);
        sfxVolumeSlider.onValueChanged.AddListener(OnEffectsVolumeSliderChange);
        returnMenuButton.onClick.AddListener(OnReturnButtonClick);
    }

    public void OnMainVolumeSliderChange(float value)
    {
        audioManager.mainVolume = value;
        audioManager.UpdateVolumes();
    }

    public void OnMusicVolumeSliderChange(float value)
    {
        audioManager.musicVolume = value;
        audioManager.UpdateVolumes();
    }

    public void OnEffectsVolumeSliderChange(float value)
    {
        audioManager.effectsVolume = value;
        audioManager.UpdateVolumes();
        audioManager.Play("MenuConfirm");
    }


    public void OnReturnButtonClick()
    {
        audioManager.Play("MenuConfirm");
        ReturnMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
