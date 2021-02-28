using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIEvents : MonoBehaviour
{

    [SerializeField] GameObject popSound;
    [SerializeField] GameObject clickSound;

    [SerializeField] Button menuButton;
    [SerializeField] Button SettingsButton;

    [SerializeField] AudioMixer SoundMixer;
    [SerializeField] AudioMixer MusicMixer;
    [SerializeField] Slider MusicVolumeSlider;
    [SerializeField] Slider SoundVolumeSlider;
    [SerializeField] Toggle Fullscreentoggle;

    private void Awake()
    {
        if (Fullscreentoggle)
            Fullscreentoggle.isOn = Screen.fullScreen;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("SoundVolume"))
            PlayerPrefs.SetFloat("SoundVolume", 0.5f);
        float _volume = PlayerPrefs.GetFloat("SoundVolume");
        ChangeSoundVolume(_volume);
        if (SoundVolumeSlider)
            SoundVolumeSlider.value = _volume;

        if (!PlayerPrefs.HasKey("MusicVolume"))
            PlayerPrefs.SetFloat("MusicVolume", 0.5f);
        _volume = PlayerPrefs.GetFloat("MusicVolume");
        ChangeMusicVolume(_volume);
        if (MusicVolumeSlider)
            MusicVolumeSlider.value = _volume;
    }

    public void ToggleGameObject(GameObject _gameObject)
    {
        _gameObject.SetActive(!_gameObject.activeSelf);
    }

    public void ClickSound()
    {
        GameObject sound = Instantiate(clickSound);
        sound.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
        Destroy(sound, 1f);
    }

    public void PopSound()
    {
        GameObject sound = Instantiate(popSound);
        sound.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
        Destroy(sound, 1f);
    }

    public void SelectButtonOnRegainedDevice(PlayerInput _context)
    {
        if (_context.currentControlScheme == "Gamepad")
        {
            if (menuButton.transform.parent.gameObject.activeSelf)
                SelectButton(menuButton);
            else if (SettingsButton.transform.parent.gameObject.activeSelf)
                SelectButton(SettingsButton);
        }
        else
            DeselectButtons();
    }

    public void SelectRestartButtonOnSwitched(PlayerInput _context)
    {
        if (_context.currentControlScheme == "Gamepad")
        {
            if (menuButton.transform.parent.gameObject.activeSelf)
                SelectButton(menuButton);
        }
        else
            DeselectButtons();
    }

    public void DeselectButtons()
    {
        EventSystem eventSystem = GetComponent<EventSystem>();
        eventSystem.SetSelectedGameObject(null);
    }

    public void SelectButton(Button button)
    {
        button.Select();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeMusicVolume(float _volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", _volume);
        PlayerPrefs.Save();
        _volume = Mathf.Log(_volume) * 20;
        MusicMixer.SetFloat("volume", _volume);
    }

    public void ChangeSoundVolume(float _volume)
    {
        PopSound();

        PlayerPrefs.SetFloat("SoundVolume", _volume);
        PlayerPrefs.Save();
        _volume = Mathf.Log(_volume) * 20;
        SoundMixer.SetFloat("volume", _volume);
    }

    public void ToggleFullscreen(bool _toggle)
    {
        Screen.fullScreen = _toggle;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }


}
