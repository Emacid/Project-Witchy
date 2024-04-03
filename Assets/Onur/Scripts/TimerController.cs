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

    // Baþlangýç konumlarý için bir Dictionary kullanacaðýz
    private Dictionary<Transform, Vector3> initialPositions = new Dictionary<Transform, Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        timerRadial = GameObject.Find("Timer").GetComponent<Image>();
        timerLine = GameObject.Find("TimerLinePivot").GetComponent<Transform>();

        // Her nesnenin baþlangýç konumunu kaydet
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

            // TimerLine'ýn dönüþü
            float rotationAngle = 360f * (1f - timeRemaining / maxTime); // 360 derece dönüþ
            timerLine.localRotation = Quaternion.Euler(0f, 0f, -rotationAngle); // Yelkovanýn dönüþü, burada -rotationAngle kullanýldý.
        }
        else if (!angryFaceCalled)
        {
            Debug.Log("ZAMAN DOLDU! ÖLDÜN!");
            FindActiveCustomerScript()?.AngryFace();
            angryFaceCalled = true;
        }
    }

    public Customer FindActiveCustomerScript()
    {
        Customer activeCustomer = FindObjectOfType<Customer>(); // Aktif Customer scriptini bul

        if (activeCustomer != null && activeCustomer.isActiveAndEnabled)
        {
            return activeCustomer; // Eðer script etkinse, bu scripti döndür
        }
        else
        {
            return null; // Eðer aktif bir Customer scripti bulunamazsa veya etkin deðilse, null döndür
        }
    }

    private void TimeReset()
    {
        // Her nesneyi baþlangýç konumuna geri döndür
        foreach (KeyValuePair<Transform, Vector3> pair in initialPositions)
        {
            pair.Key.position = pair.Value;
        }

        // Zamaný sýfýrla
        timeRemaining = maxTime;
        angryFaceCalled = false;
    }
}
