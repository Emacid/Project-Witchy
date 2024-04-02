using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Image timerRadial;
    float timeRemaining;
    public float maxTime = 5.0f;
    public Transform timerLine;

    // Ba�lang�� konumlar� i�in bir Dictionary kullanaca��z
    Dictionary<Transform, Vector3> initialPositions = new Dictionary<Transform, Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = maxTime;

        // Her nesnenin ba�lang�� konumunu kaydet
        foreach (Transform child in transform)
        {
            initialPositions[child] = child.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerRadial.fillAmount = timeRemaining / maxTime;

            // TimerLine'�n d�n���
            float rotationAngle = 360f * (1f - timeRemaining / maxTime); // 360 derece d�n��
            timerLine.localRotation = Quaternion.Euler(0f, 0f, -rotationAngle); // Yelkovan�n d�n���, burada -rotationAngle kullan�ld�.
        }
        else
        {
            Debug.Log("ZAMAN DOLDU! �LD�N!");
        }

        // K tu�una bas�ld���nda
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Her nesneyi ba�lang�� konumuna geri d�nd�r
            foreach (KeyValuePair<Transform, Vector3> pair in initialPositions)
            {
                pair.Key.position = pair.Value;
            }

            // Zaman� s�f�rla
            timeRemaining = maxTime;
        }
    }
}
