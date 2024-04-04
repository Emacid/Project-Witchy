using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource musicSource;
    private bool fadingOut = false;
    public GameObject fadeout;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        StartCoroutine(FadeInMusic(5f)); // 2 saniyede fade in yapmak için
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            fadingOut = true;
            fadeout.SetActive(true);
            StartCoroutine(FadeOutMusic(5f)); // 2 saniyede fade out yapmak için
        }
    }

    IEnumerator FadeInMusic(float duration)
    {
        musicSource.volume = 0f;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0f, 1f, currentTime / duration);
            yield return null;
        }
        musicSource.volume = 1f;
    }

    IEnumerator FadeOutMusic(float duration)
    {
        float startVolume = musicSource.volume;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, 0f, EaseOut(currentTime / duration));
            musicSource.volume = newVolume;
            yield return null;
        }
        musicSource.volume = 0f;
        musicSource.Stop();
    }

    // Ease-Out fonksiyonu
    float EaseOut(float t)
    {
        return Mathf.Sin(t * Mathf.PI * 0.5f);
    }
}
