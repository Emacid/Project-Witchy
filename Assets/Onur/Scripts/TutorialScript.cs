using System.Collections;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject Hand;
    public FirstPersonLook firstPersonLook;
    public FirstPersonMovement firstPersonMovement;
    public float waitTime = 5.0f;
    public GameObject firstCustomer;
    public GameObject cutsceneObject;
    public GameObject fadeIn;
    public GameObject musicController;
    public GameObject fadeoutCinem;
    public Camera mainCamera;
    public float fadeDuration = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CameraDeactivation());
        StartCoroutine(ActivatePlayer());
        StartCoroutine(ActivateFirstCustomer());
    }

    private IEnumerator ActivatePlayer()
    {
        yield return new WaitForSeconds(waitTime);
        fadeIn.SetActive(true);
        fadeoutCinem.SetActive(false);
        Hand.gameObject.SetActive(true);
        firstPersonLook.enabled = true;
        firstPersonMovement.enabled = true;

        StartCoroutine(StartMusic());
        StartCoroutine(CameraActivation());
        yield return new WaitForSeconds(0.5f);
        cutsceneObject.SetActive(false);
    }

    private IEnumerator ActivateFirstCustomer()
    {
        yield return new WaitForSeconds(41.0f);
        firstCustomer.gameObject.SetActive(true);
    }

    private IEnumerator CameraDeactivation()
    {
        yield return new WaitForSeconds(0.2f);
        mainCamera.enabled = false;
    }
    private IEnumerator CameraActivation()
    {
        yield return new WaitForSeconds(0.05f);
        mainCamera.enabled = true;
    }

    IEnumerator StartMusic()
    {
        AudioSource audioSource = musicController.GetComponent<AudioSource>();

        // Müziði baþa sarmak ve çalmak
        audioSource.Stop();
        audioSource.Play();

        // Sesin baþlangýç deðeri (0) ve hedef deðeri (1)
        float startVolume = 0f;
        float targetVolume = 1f;

        // Zaman sayacý
        float currentTime = 0f;

        // Fade iþlemi
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / fadeDuration);
            yield return null;
        }

        // Sesin hedef deðerini sabitle
        audioSource.volume = targetVolume;
    }
}
