using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

public class Customer : MonoBehaviour
{
    public Sprite[] headSprites; // Kafa sprite'larýnýn dizisi
    public Sprite[] bodySprites; // Vücut sprite'larýnýn dizisi

    public int customerID = 0;
    private string[] DESIRED_ITEM_TAGS; // Desired item tag'larýn dizisi
    public TMP_Text textObject;
    public string customerText = string.Empty;

    private GameObject itemSocket;
    public bool inInteractableArea = false;
    private ProgressBarRatio progressBarRatio;
    private CraftingSystem craftingSystemScript;
    private GameObject talkBubbleObject; // talk_bubble gameobjesini tutacak deðiþken
    public float waitBeforeTalkBubbleActivation = 2.5f;
    public float waitBeforeTalkSoundActivation = 2.5f;

    private StressReceiver stressReceiver;
    private Animator notCraftedTextAnim;

    private AudioSource audioSource;
    public AudioClip PopClip;
    public AudioClip[] angrySounds;
    public AudioClip corretSound;
    public AudioClip poofSound;
    public AudioClip errorSound;
    [SerializeField]
    private GameObject happyFace;
    [SerializeField]
    private GameObject angryFace;
    [SerializeField]
    private GameObject dissappearEffect;

    void Start()
    {

        StartCoroutine(ActivateTalkBubble());
        StartCoroutine(PopClipPlay());

        audioSource = GetComponent<AudioSource>();
        craftingSystemScript = GameObject.Find("CraftingSystem").GetComponent<CraftingSystem>();
        stressReceiver = GameObject.Find("First Person Camera").GetComponent<StressReceiver>();
        progressBarRatio = GameObject.Find("MoodBar_Adjuster").GetComponent<ProgressBarRatio>();
        notCraftedTextAnim = GameObject.Find("NotCraftedText").GetComponent<Animator>();

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

        // talk_bubble alt öðesini bulma ve atama
        foreach (Transform child in transform)
        {
            if (child.name == "talk_bubble")
            {
                talkBubbleObject = child.gameObject;
                break;
            }
        }

        // talk_bubble objesi bulunamazsa uyarý ver
        if (talkBubbleObject == null)
        {
            Debug.LogWarning("talk_bubble objesi bulunamadý!");
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
            Debug.Log("Player alandan çýktý!");
            inInteractableArea = false;
        }
    }

    private void CheckDesiredItem()
    {
        // Eldeki nesnenin kontrolü
        if (!craftingSystemScript.isItemInHandFinal)
        {
            Debug.Log("Bu item final deðildir!");
            notCraftedTextAnim.SetTrigger("NotCraftedTextTrigger");
            audioSource.PlayOneShot(errorSound);
            stressReceiver.InduceStress(0.25f);
            return;
        }

        // Ýstenilen itemin kontrolü
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
                    audioSource.PlayOneShot(corretSound);
                    DestroyObjectInHand();
                    happyFace.gameObject.SetActive(true);
                    StartCoroutine(CustomerVanishVfx());
                    StartCoroutine(DestroyCustomer());
                    return;
                }
            }
        }

        if (!foundDesiredItem && itemSocket.transform.childCount >= 1)
        {
            AngryFace();
        }

        DestroyObjectInHand();
        Debug.Log("Herhangi bir istenilen nesne yok");
        // Oyuncuyu efektle gönder
    }


    private void SetCustomerData()
    {
        switch (customerID)
        {
            case 0:
                Debug.Log("I want bira þiþesi / peluþ!");
                DESIRED_ITEM_TAGS = new string[] { "Beer", "Toothless" };
                break;
            case 1:
                Debug.Log("Third Eye / healing potion / Gong");
                DESIRED_ITEM_TAGS = new string[] { "Gong", "HealingPotion", "TheThirdEye" };
                break;
            case 2:
                Debug.Log("Nightstick / Ýnsan yiyen bitki / zehir"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            case 3:
                Debug.Log("Nightstick / Ýnsan yiyen bitki / zehir"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            case 4:
                Debug.Log("Nightstick / Ýnsan yiyen bitki / zehir"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            case 5:
                Debug.Log("Nightstick / Ýnsan yiyen bitki / zehir"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            case 6:
                Debug.Log("Slingshot"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Slingshot" };
                break;
            case 7:
                Debug.Log("Nightstick / Ýnsan yiyen bitki / zehir"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            case 8:
                Debug.Log("Nightstick / Ýnsan yiyen bitki / zehir"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            case 9:
                Debug.Log("Nightstick / Ýnsan yiyen bitki / zehir"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            case 10:
                Debug.Log("Nightstick / Ýnsan yiyen bitki / zehir"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Carnivorous", "Acid" };
                break;
            // Diðer case'ler için gerekli etiketleri ekleyin
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

    private IEnumerator DestroyCustomer()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
    private IEnumerator CustomerVanishVfx()
    {
        audioSource.PlayOneShot(poofSound);
        dissappearEffect.gameObject.SetActive(true);
        GameObject.Find("Head").gameObject.SetActive(false);
        GameObject.Find("Body").gameObject.SetActive(false);
        GameObject.Find("talk_bubble").gameObject.SetActive(false);
        stressReceiver.InduceStress(0.08f);
        yield return new WaitForSeconds(0.02f);
    }

    public void AngryFace() 
    {
        Debug.Log("Mutsuz surat");
        angryFace.gameObject.SetActive(true);
        int randomIndex = Random.Range(0, angrySounds.Length); // Rastgele bir index seç
        audioSource.PlayOneShot(angrySounds[randomIndex]); // Seçilen sesi oynat
        angryFace.SetActive(true);
        progressBarRatio.moodValue -= 10;
        StartCoroutine(CustomerVanishVfx());
        StartCoroutine(DestroyCustomer());
    }

}
