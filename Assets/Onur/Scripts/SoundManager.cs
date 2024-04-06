using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    float maxVolume = 1.0f;
    float minVolume = 0.0f;

    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            IncreaseVolume();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DecreaseVolume();
        }
    }

    void IncreaseVolume()
    {
        float newVolume = Mathf.Min(AudioListener.volume + 0.1f, maxVolume);
        volumeSlider.value = newVolume;
        AudioListener.volume = newVolume;
        Save();
    }

    void DecreaseVolume()
    {
        float newVolume = Mathf.Max(AudioListener.volume - 0.1f, minVolume);
        volumeSlider.value = newVolume;
        AudioListener.volume = newVolume;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        AudioListener.volume = volumeSlider.value;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
