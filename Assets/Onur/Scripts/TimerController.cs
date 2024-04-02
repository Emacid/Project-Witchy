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

    // Baþlangýç konumlarý için bir Dictionary kullanacaðýz
    Dictionary<Transform, Vector3> initialPositions = new Dictionary<Transform, Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = maxTime;

        // Her nesnenin baþlangýç konumunu kaydet
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

            // TimerLine'ýn dönüþü
            float rotationAngle = 360f * (1f - timeRemaining / maxTime); // 360 derece dönüþ
            timerLine.localRotation = Quaternion.Euler(0f, 0f, -rotationAngle); // Yelkovanýn dönüþü, burada -rotationAngle kullanýldý.
        }
        else
        {
            Debug.Log("ZAMAN DOLDU! ÖLDÜN!");
        }

        // K tuþuna basýldýðýnda
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Her nesneyi baþlangýç konumuna geri döndür
            foreach (KeyValuePair<Transform, Vector3> pair in initialPositions)
            {
                pair.Key.position = pair.Value;
            }

            // Zamaný sýfýrla
            timeRemaining = maxTime;
        }
    }
}
