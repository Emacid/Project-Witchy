using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Customer : MonoBehaviour
{
    public Sprite[] headSprites; // Kafa sprite'larýnýn dizisi
    public Sprite[] bodySprites; // Vücut sprite'larýnýn dizisi
    public int customerID = 0;
    private string DESIRED_ITEM_TAG = string.Empty;
    public TMP_Text textObject;
    public string customerText = string.Empty;
    private GameObject itemSocket;
    public bool inInteractableArea = false;
    private ProgressBarRatio progressBarRatio;
    

    void Start()
    {
        progressBarRatio = GameObject.Find("ProgressBar_Adjuster").GetComponent<ProgressBarRatio>();

        textObject.text = customerText;
        itemSocket = GameObject.Find("ItemSocket");

        // Head ve Body child objelerini buluyoruz
        Transform head = transform.Find("Head");
        Transform body = transform.Find("Body");

        if (head == null || body == null)
        {
            Debug.LogError("Head veya Body objeleri bulunamadý!");
            return;
        }

        // Head ve Body sprite renderer'larýný alýyoruz
        SpriteRenderer headRenderer = head.GetComponent<SpriteRenderer>();
        SpriteRenderer bodyRenderer = body.GetComponent<SpriteRenderer>();

        // Rastgele kafa ve vücut sprite'larýný seçiyoruz
        Sprite randomHeadSprite = headSprites[Random.Range(0, headSprites.Length)];
        Sprite randomBodySprite = bodySprites[Random.Range(0, bodySprites.Length)];

        // Seçilen sprite'larý atýyoruz
        headRenderer.sprite = randomHeadSprite;
        bodyRenderer.sprite = randomBodySprite;

        SetCustomerData();

    }

    private void Update()
    {
        if(inInteractableArea && Input.GetKeyDown(KeyCode.E))
        {
            CheckDesiredItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player")) 
        {
            Debug.Log("Player alana girdi!");
            inInteractableArea = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player alandan çýktý!");
            inInteractableArea = false;
        }
    }

    private void CheckDesiredItem()
    {
        bool foundDesiredItem = false;

        foreach (Transform child in itemSocket.transform)
        {
            if (child.CompareTag(DESIRED_ITEM_TAG))
            {
                foundDesiredItem = true;
                Debug.Log("Mutlu surat");
                progressBarRatio.moodValue += 10;
                // Mutlu surat ekle
                break;
            }
        }

        if (!foundDesiredItem && itemSocket.transform.childCount >= 1)
        {
            Debug.Log("Mutsuz surat");
            // Mutsuz surat ekle
            progressBarRatio.moodValue -= 10;      
        }

        DestroyObjectInHand();
        Debug.Log(DESIRED_ITEM_TAG + " yok oldu");

        // Oyuncuyu efektle gönder
    }

    private void SetCustomerData() 
    {
        switch (customerID) 
        {
            case 0:
                Debug.Log("I want Pilates Ball!");
                DESIRED_ITEM_TAG = "PilatesBall";
                break;
            case 1:
                Debug.Log("I want Balloon!");
                DESIRED_ITEM_TAG = "Balloon"; 
                break;
            case 2:
                Debug.Log("nothingness");
                break;
            case 3:
                Debug.Log("nothingness");
                break;
            default:
                Debug.LogWarning("Unknown ID for Customer.");
                return;
        }
    }

    private void DestroyObjectInHand()
    {
        foreach (Transform child in itemSocket.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
