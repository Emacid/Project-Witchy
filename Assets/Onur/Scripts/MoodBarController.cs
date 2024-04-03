using UnityEngine;

public class MoodBarController : MonoBehaviour
{
    public RectTransform kediRectTransform; // Kedi resminin RectTransform bile�eni

    // Mood de�eri
    public float moodValue = 50; // Default mood de�eri

    // Bar'�n RectTransform bile�eni
    private RectTransform barRectTransform;

    // Kedinin h�z�
    public float kediHizi = 5f;

    public GameObject happyCat;
    public GameObject AngryCat;

    private void Start()
    {
        // Bar'�n RectTransform bile�enini al
        barRectTransform = GetComponent<RectTransform>();

        // Kedi resmini ba�lang��ta bar�n tam ortas�na yerle�tir
        float barHeight = barRectTransform.rect.height;
        kediRectTransform.localPosition = new Vector3(0f, -barHeight / 2f, 0f);

        // Mood de�erini g�ncelle
        UpdateKediPosition();
    }

    private void Update()
    {
        if(moodValue <= 30) 
        {
            AngryCat.SetActive(true);
        }
        else 
        {
            AngryCat.SetActive(false);
        }

        if(moodValue >= 70) 
        {
            happyCat.SetActive(true);
        }
        else 
        {
            happyCat.SetActive(false);
        }

        // Kedi pozisyonunu g�ncelle
        UpdateKediPosition();
    }

    // Mood de�erine g�re kedinin pozisyonunu g�ncelle
    private void UpdateKediPosition()
    {
        float barHeight = barRectTransform.rect.height;
        float kediYPosition = Mathf.Lerp(-barHeight / 2f, barHeight / 2f, moodValue / 100f);
        kediRectTransform.localPosition = Vector3.Lerp(kediRectTransform.localPosition, new Vector3(0f, kediYPosition, 0f), Time.deltaTime * kediHizi);
    }

    // Mood de�erini g�ncelle
    public void SetMood(float value)
    {
        moodValue = Mathf.Clamp(value, 0, 100);
    }
}
