using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class AudioFade : MonoBehaviour
{
    public PlayableDirector timeline;
    AudioListener audioListener;

    bool isFadeInComplete = false;
    bool isFadeOutComplete = false;
    float fadeInDuration = 2f;
    float fadeOutStartTime = 6f; // Fade out'un baþlama zamanýný 6 saniye olarak ayarlayalým
    float fadeOutDuration = 4f; // Fade out süresini 4 saniyeye ayarlayalým
    float fadeInTimer = 0f;
    float fadeOutTimer = 0f;
    float initialVolume = 0f;

    void Start()
    {
        audioListener = Camera.main.GetComponent<AudioListener>();
        if (audioListener == null)
        {
            Debug.LogError("Audio listener not found on the main camera.");
            return;
        }

        initialVolume = AudioListener.volume;
        AudioListener.volume = 0f; // Baþlangýçta sesi kapat
        StartCoroutine(FadeInAudio());
    }

    void Update()
    {
        if (!isFadeInComplete)
        {
            fadeInTimer += Time.deltaTime;
            float progress = fadeInTimer / fadeInDuration;
            float smoothProgress = SmoothStep(0f, 1f, progress); // Smoothstep kullanarak smooth geçiþ yap
            AudioListener.volume = Mathf.Lerp(0f, initialVolume, smoothProgress);
            if (fadeInTimer >= fadeInDuration)
            {
                isFadeInComplete = true;
            }
        }
        else if (!isFadeOutComplete)
        {
            if (Time.timeSinceLevelLoad >= fadeOutStartTime)
            {
                fadeOutTimer += Time.deltaTime;
                float progress = fadeOutTimer / fadeOutDuration;
                float smoothProgress = SmoothStep(0f, 1f, progress); // Smoothstep kullanarak smooth geçiþ yap
                AudioListener.volume = Mathf.Lerp(initialVolume, 0f, smoothProgress);
                if (fadeOutTimer >= fadeOutDuration)
                {
                    isFadeOutComplete = true;
                }
            }
        }
    }

    IEnumerator FadeInAudio()
    {
        yield return new WaitForSeconds(2f); // Sahne baþladýktan 2 saniye sonra baþlat
        isFadeInComplete = false;
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOutAudio());
    }

    IEnumerator FadeOutAudio()
    {
        yield return new WaitForSeconds(9f); // Sahne baþladýktan 9 saniye sonra baþlat
        isFadeOutComplete = false;
    }

    float SmoothStep(float edge0, float edge1, float x)
    {
        x = Mathf.Clamp01((x - edge0) / (edge1 - edge0)); // Deðeri 0 ve 1 arasýnda kýrp
        return x * x * (3 - 2 * x); // Smoothstep fonksiyonu uygula
    }
}
