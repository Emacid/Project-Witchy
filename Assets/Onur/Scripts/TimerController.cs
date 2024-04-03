using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    private Image timerRadial;
    private Transform timerLine;
    private float timeRemaining;
    private bool angryFaceCalled = false;

    public float maxTime = 5.0f;

    // Ba�lang�� konumlar� i�in bir Dictionary kullanaca��z
    private Dictionary<Transform, Vector3> initialPositions = new Dictionary<Transform, Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        timerRadial = GameObject.Find("Timer").GetComponent<Image>();
        timerLine = GameObject.Find("TimerLinePivot").GetComponent<Transform>();

        // Her nesnenin ba�lang�� konumunu kaydet
        foreach (Transform child in transform)
        {
            initialPositions[child] = child.position;
        }

        TimeReset();
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
        else if (!angryFaceCalled)
        {
            Debug.Log("ZAMAN DOLDU! �LD�N!");
            FindActiveCustomerScript()?.AngryFace();
            angryFaceCalled = true;
        }
    }

    public Customer FindActiveCustomerScript()
    {
        Customer activeCustomer = FindObjectOfType<Customer>(); // Aktif Customer scriptini bul

        if (activeCustomer != null && activeCustomer.isActiveAndEnabled)
        {
            return activeCustomer; // E�er script etkinse, bu scripti d�nd�r
        }
        else
        {
            return null; // E�er aktif bir Customer scripti bulunamazsa veya etkin de�ilse, null d�nd�r
        }
    }

    private void TimeReset()
    {
        // Her nesneyi ba�lang�� konumuna geri d�nd�r
        foreach (KeyValuePair<Transform, Vector3> pair in initialPositions)
        {
            pair.Key.position = pair.Value;
        }

        // Zaman� s�f�rla
        timeRemaining = maxTime;
        angryFaceCalled = false;
    }
}
