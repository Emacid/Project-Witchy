using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerInfoArea : MonoBehaviour
{
    private GameObject itemSocket;
    public GameObject customerInfoText;

    // Start is called before the first frame update
    void Start()
    {
        itemSocket = GameObject.Find("ItemSocket");
        customerInfoText.SetActive(false); // �lk ba�ta kapal� olacak
    }

    // Trigger alan�na bir nesne girerse
    private void OnTriggerEnter(Collider other)
    {
        // Giren nesne "Player" tag'�na sahip mi kontrol et
        if (other.CompareTag("Player"))
        {
            // E�er itemSocket'in child objesi varsa ve customerInfoText aktif de�ilse customerInfoText'i aktif et
            if (itemSocket.transform.childCount > 0 && !customerInfoText.activeSelf)
            {
                customerInfoText.SetActive(true);
            }
        }
    }

    // Trigger alan�ndan bir nesne ��karsa
    private void OnTriggerExit(Collider other)
    {
        // ��kan nesne "Player" tag'�na sahip mi kontrol et
        if (other.CompareTag("Player"))
        {
            // customerInfoText'i tekrar kapal� hale getir
            customerInfoText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // E�er itemSocket'in child objesi yoksa ve customerInfoText aktifse, customerInfoText'i kapat
        if (itemSocket.transform.childCount == 0 && customerInfoText.activeSelf)
        {
            customerInfoText.SetActive(false);
        }
    }
}
