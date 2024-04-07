//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using TMPro;
//using Unity.VisualScripting;
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
                Debug.Log("I want bira þiþesi / peluþ!, eyepatch, pillz");
                DESIRED_ITEM_TAGS = new string[] { "Beer", "Toothless", "EyePatch", "Pills", "HealingPotion" };
                break;
            case 1:
                Debug.Log("Third Eye / healing potion / Gong");
                DESIRED_ITEM_TAGS = new string[] { "Gong", "HealingPotion", "TheThirdEye", "EvilEye", "CrystalBall", "Beer" };
                break;
            case 2:
                Debug.Log("Nightstick / büyütec,pelerin / zehir,azit, kamuflaj, fireball, kriztalball"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Nightstick", "Acid", "PoisonBottle", "MagnifyingGlass", "CrystalBall", "Camouflage", "FireBall" };
                break;
            case 3:
                Debug.Log("Helmet / ZlingZhot / MagicWand, zcroll, göz bandý tahta bacak, cloack, nightztick, kamuflaj"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Helmet", "Slingshot", "MagicWand", "MagicScroll", "EyePatch", "PegLeg", "Cloak", "Nightstick", "Camouflage" };
                break;
            case 4:
                Debug.Log("Toothless / Balloon / Kite, PogoStick, Basketball, OrigamiFrog"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Toothless", "Balloon", "Kite", "PogoStick", "Basketball", "OrigamiFrog" };
                break;
            case 5:
                Debug.Log("ejder garlic monzter beer bloodbag"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "DragonEgg", "Garlic", "AMonster", "Beer", "BloodBag" };
                break;
            case 6:
                Debug.Log("Slingshot, nightztick, azit, fireball, poizon, helmet, "); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Slingshot", "Nightstick", "PoisonBottle", "Acid", "FireBall", "Helmet", "MagicWand" };
                break;
            case 7:
                Debug.Log("calziyum / Ýzkelet pozteri ve yerden ölü kaldýrma izkirizi, magic zcroll"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Calcium", "SkeletalSystemPoster", "RaiseDeadPotion", "MagicScroll", "RaiseDeadPotion" };
                break;
            case 8:
                Debug.Log("PegLeg / GlassHeels / Cloak / Necklace / EyePatch / SunGlasses / AluminiumFoil / Helmet / Camouflage/ Stethoscope"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "PegLeg", "GlassHeels", "Cloak", "Necklace", "EyePatch", "SunGlasses", "AluminiumFoil", "Helmet", "Camouflage", "Stethoscope", "Diamond" };
                break;
            case 9:
                Debug.Log("Evil eye, third eye, gonggg,Nightstick"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "EvilEye", "TheThirdEye", "Gong", "Nightstick" };
                break;
            case 10:
                Debug.Log("BloodBag / Camouflage / BurnCream / Cloak / SunGlasses / Calcium / Beer"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "BloodBag", "BurnCream", "Camouflage", "Acid", "Cloak", "SunGlasses", "Calcium", "Beer" };
                break;
            case 11:
                Debug.Log("FARELERRR!!"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Acid", "FireBall", "MagicScroll", "Slingshot", "PoisonBottle", "MagicWand", "Nightstick", "Soap", "Gasoline" };
                break;
            case 12:
                Debug.Log("HERZEYY!!!"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Calcium", "Stethoscope", "Camouflage", "Helmet", "DragonEgg", "Acid", "Ivy", "Gasoline", "Carnivorous", "RaiseDeadPotion", "HealingPotion", "Diamond", "PoisonBottle", "Mirror", "SkeletalSystemPoster", "AluminiumFoil", "WallPaint", "Beer", "OrigamiFrog", "MagicWand", "SunGlasses", "MagnifyingGlass", "EyePatch", "TheThirdEye", "Necklace", "FireBall", "CrystalBall", "MagicScroll", "EvilEye", "Pills", "Nightstick", "BloodBag", "Toothless", "Garlic", "Gong", "Kite", "AMonster", "Cloak", "Soap", "Trash", "BurnCream", "Glue", "GlassHeels", "PegLeg", "Slingshot", "Basketball", "Balloon", "PogoStick"};
                break;
            case 13:
                Debug.Log("Ayrýlýk!"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "GlassHeels", "Diamond", "Mirror", "Carnivorous", "Necklace" };
                break;
            case 14:
                Debug.Log("Komþum vampir!"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Mirror", "BloodBag", "Garlic", "Fireball", "Nightstick", "Slingshot" };
                break;
            case 15:
                Debug.Log("Lordz towera sýzma!"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Camouflage", "Ivy", "PogoStick", "SunGlasses", "EyePatch", "Cloak", "Helmet" };
                break;
            case 16:
                Debug.Log("piknik!"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "BurnCream", "Garlic", "Basketball", "SunGlasses", "Balloon", "Kite", "PogoStick", "OrigamiFrog", "Beer", "Slingshot", "DragonEgg" };
                break;
            case 17:
                Debug.Log("stars!"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Cloak", "MagicScroll", "PogoStick", "MagnifyingGlass", "Balloon", "Ivy", "DragonEgg" };
                break;
            case 18:
                Debug.Log("invention!"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Gasoline", "Glue", "OrigamiFrog", "MagnifyingGlass", "SkeletalSystemPoster", "Kite", "PogoStick", "AluminiumFoil", "Beer", "PegLeg", "Slingshot" };
                break;
            case 19:
                Debug.Log("pet gargoyle bulamadým fikrini deðiþtircek baþka biþi!"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "AMonster", "DragonEgg", "Toothless", "OrigamiFrog", "RaiseDeadPotion" };
                break;
            case 20:
                Debug.Log("HELP DIYE BAGIRAN BIREY"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "HealingPotion", "BloodBag", "Glue", "Calcium", "Stethoscope", "RaiseDeadPotion", "EyePatch" };
                break;
            case 21:
                Debug.Log("Parlayan eþyalar lazým abiye"); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "AluminiumFoil", "Diamond", "CrystalBall", "Mirror", "MagnifyingGlass", "GlassHeels", "Necklace" };
                break;
            case 22:
                Debug.Log("Eþek þakasý yapacak eþya lazým! "); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Soap", "Glue", "Camouflage", "Gasoline", "Carnivorous", "AluminiumFoil", "BloodBag", "Gong" };
                break;
            case 23:
                Debug.Log("Train my Dragon! "); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Toothless" };
                break;
            case 24:
                Debug.Log("Train my Dragon! "); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "GlassHeels" };
                break;
            case 25:
                Debug.Log("Train my Dragon! "); // Ýki etiketi de kontrol ediyoruz
                DESIRED_ITEM_TAGS = new string[] { "Pills" };
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
