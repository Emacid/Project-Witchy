using UnityEngine;

public class MoodBarController : MonoBehaviour
{
    public RectTransform kediRectTransform; // Kedi resminin RectTransform bileþeni

    // Mood deðeri
    public float moodValue = 50; // Default mood deðeri

    // Bar'ýn RectTransform bileþeni
    private RectTransform barRectTransform;

    // Kedinin hýzý
    public float kediHizi = 5f;

    public GameObject happyCat;
    public GameObject AngryCat;

    private void Start()
    {
        // Bar'ýn RectTransform bileþenini al
        barRectTransform = GetComponent<RectTransform>();

        // Kedi resmini baþlangýçta barýn tam ortasýna yerleþtir
        float barHeight = barRectTransform.rect.height;
        kediRectTransform.localPosition = new Vector3(0f, -barHeight / 2f, 0f);

        // Mood deðerini güncelle
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

        // Kedi pozisyonunu güncelle
        UpdateKediPosition();
    }

    // Mood deðerine göre kedinin pozisyonunu güncelle
    private void UpdateKediPosition()
    {
        float barHeight = barRectTransform.rect.height;
        float kediYPosition = Mathf.Lerp(-barHeight / 2f, barHeight / 2f, moodValue / 100f);
        kediRectTransform.localPosition = Vector3.Lerp(kediRectTransform.localPosition, new Vector3(0f, kediYPosition, 0f), Time.deltaTime * kediHizi);
    }

    // Mood deðerini güncelle
    public void SetMood(float value)
    {
        moodValue = Mathf.Clamp(value, 0, 100);
    }
}
