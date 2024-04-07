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
        customerInfoText.SetActive(false); // Ýlk baþta kapalý olacak
    }

    // Trigger alanýna bir nesne girerse
    private void OnTriggerEnter(Collider other)
    {
        // Giren nesne "Player" tag'ýna sahip mi kontrol et
        if (other.CompareTag("Player"))
        {
            // Eðer itemSocket'in child objesi varsa ve customerInfoText aktif deðilse customerInfoText'i aktif et
            if (itemSocket.transform.childCount > 0 && !customerInfoText.activeSelf)
            {
                customerInfoText.SetActive(true);
            }
        }
    }

    // Trigger alanýndan bir nesne çýkarsa
    private void OnTriggerExit(Collider other)
    {
        // Çýkan nesne "Player" tag'ýna sahip mi kontrol et
        if (other.CompareTag("Player"))
        {
            // customerInfoText'i tekrar kapalý hale getir
            customerInfoText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Eðer itemSocket'in child objesi yoksa ve customerInfoText aktifse, customerInfoText'i kapat
        if (itemSocket.transform.childCount == 0 && customerInfoText.activeSelf)
        {
            customerInfoText.SetActive(false);
        }
    }
}
