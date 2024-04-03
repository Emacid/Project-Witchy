using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

public class Customer : MonoBehaviour
{
    public Sprite[] headSprites; // Kafa sprite'lar�n�n dizisi
    public Sprite[] bodySprites; // V�cut sprite'lar�n�n dizisi
    public int customerID = 0;
    private string[] DESIRED_ITEM_TAGS; // Desired item tag'lar�n dizisi
    public TMP_Text textObject;
    public string customerText = string.Empty;
    private GameObject itemSocket;
    public bool inInteractableArea = false;
    private ProgressBarRatio progressBarRatio;
    private CraftingSystem craftingSystemScript;
    private GameObject talkBubbleObject; // talk_bubble gameobjesini tutacak de�i�ken
    public float waitBeforeTalkBubbleActivation = 2.5f;
    public float waitBeforeTalkSoundActivation = 2.5f;

    private AudioSource audioSource;
    public AudioClip PopClip;

    void Start()
    {

        StartCoroutine(ActivateTalkBubble());
        StartCoroutine(PopClipPlay());

        audioSource = GetComponent<AudioSource>();

        craftingSystemScript = GameObject.Find("CraftingSystem").GetComponent<CraftingSystem>();

        progressBarRatio = GameObject.Find("MoodBar_Adjuster").GetComponent<ProgressBarRatio>();

        textObject.text = customerText;
        itemSocket = GameObject.Find("ItemSocket");

        // Head ve Body child objelerini buluyoruz
        Transform head = transform.Find("Head");
        Transform body = transform.Find("Body");

        if (head == null || body == null)
        {
            Debug.LogError("Head veya Body objeleri bulunamad�!");
            return;
        }

        // Head ve Body sprite renderer'lar�n� al�yoruz
        SpriteRenderer headRenderer = head.GetComponent<SpriteRenderer>();
        SpriteRenderer bodyRenderer = body.GetComponent<SpriteRenderer>();

        // Rastgele kafa ve v�cut sprite'lar�n� se�iyoruz
        Sprite randomHeadSprite = headSprites[Random.Range(0, headSprites.Length)];
        Sprite randomBodySprite = bodySprites[Random.Range(0, bodySprites.Length)];

        // Se�ilen sprite'lar� at�yoruz
        headRenderer.sprite = randomHeadSprite;
        bodyRenderer.sprite = randomBodySprite;

        SetCustomerData();

        // talk_bubble alt ��esini bulma ve atama
        foreach (Transform child in transform)
        {
            if (child.name == "talk_bubble")
            {
                talkBubbleObject = child.gameObject;
                break;
            }
        }

        // talk_bubble objesi bulunamazsa uyar� ver
        if (talkBubbleObject == null)
        {
            Debug.LogWarning("talk_bubble objesi bulunamad�!");
        }
    }

    private void Update()
    {

        if (inInteractableArea && Input.GetKeyDown(KeyCode.E))
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
            Debug.Log("Player alandan ��kt�!");
            inInteractableArea = false;
        }
    }

    private void CheckDesiredItem()
    {
        bool foundDesiredItem = false;

        foreach (string tag in DESIRED_ITEM_TAGS)
        {
            foreach (Transform child in itemSocket.transform)
            {
                if (child.CompareTag(tag))
                {
                    foundDesiredItem = true;
                    Debug.Log("Mutlu surat");
                    progressBarRatio.moodValue += 10;
                    DestroyObjectInHand();
                    // Mutlu surat ekle
                    return;
                }
            }
        }

        if (!foundDesiredItem && itemSocket.transform.childCount >= 1)
        {
            Debug.Log("Mutsuz surat");
            // Mutsuz surat ekle
            progressBarRatio.moodValue -= 10;
        }

        DestroyObjectInHand();
        Debug.Log("Herhangi bir istenilen nesne yok");
        // Oyuncuyu efektle g�nder
    }

    private void SetCustomerData()
    {
        switch (customerID)
        {
            case 0:
                Debug.Log("I want bira �i�esi / pelu�!");
                DESIRED_ITEM_TAGS = new string[] { "Beer", "Toothless" };
                break;
            case 1:
                Debug.Log("Third Eye / healing potion / Gong");
                DESIRED_ITEM_TAGS = new string[] { "Gong", "HealingPotion", "TheThirdEye" };
                break;
            case 2:
                Debug.Log("Nightstick / �nsan yiyen bitki / zehir"); // �ki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            case 3:
                Debug.Log("Nightstick / �nsan yiyen bitki / zehir"); // �ki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            case 4:
                Debug.Log("Nightstick / �nsan yiyen bitki / zehir"); // �ki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            case 5:
                Debug.Log("Nightstick / �nsan yiyen bitki / zehir"); // �ki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            case 6:
                Debug.Log("Nightstick / �nsan yiyen bitki / zehir"); // �ki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            case 7:
                Debug.Log("Nightstick / �nsan yiyen bitki / zehir"); // �ki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            case 8:
                Debug.Log("Nightstick / �nsan yiyen bitki / zehir"); // �ki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            case 9:
                Debug.Log("Nightstick / �nsan yiyen bitki / zehir"); // �ki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            case 10:
                Debug.Log("Nightstick / �nsan yiyen bitki / zehir"); // �ki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            // Di�er case'ler i�in gerekli etiketleri ekleyin
            default:
                Debug.LogWarning("Unknown ID for Customer.");
                break;
        }
    }

    private void DestroyObjectInHand()
    {
        foreach (Transform child in itemSocket.transform)
        {
            Destroy(child.gameObject);
        }
        craftingSystemScript.isItemInHandFinal = false;
    }

    private IEnumerator ActivateTalkBubble()
    {
        yield return new WaitForSeconds(waitBeforeTalkBubbleActivation);
        talkBubbleObject.gameObject.SetActive(true);
        //audioSource.PlayOneShot(PopClip);
    }
    private IEnumerator PopClipPlay()
    {
        yield return new WaitForSeconds(waitBeforeTalkSoundActivation);
        audioSource.PlayOneShot(PopClip);
    }

}
