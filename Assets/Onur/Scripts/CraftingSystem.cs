using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    int id_1 = 0;
    int id_2 = 0;
    int spawn_id = 0;
    public GameObject[] items;

    public float holdThrow = 1.5f;
    float holdTimer;

    public bool isItemInHandFinal = false;
    private bool canCraft = false;
    private GameObject itemSocket;
    private GameObject craftingSocket_1;
    private GameObject craftingSocket_2;
    private GameObject craftingSocket_Final;
    public StressReceiver stressReceiver;

    public GameObject craftingInfoText;
    public AudioSource audioSource;
    public AudioClip errorSound;
    //public AudioClip craftSound;
    public AudioClip putSound;

    public GameObject craftParticle;

    // Start is called before the first frame update
    void Start()
    {
        itemSocket = GameObject.Find("ItemSocket");
        craftingSocket_1 = GameObject.Find("CraftingSocket_1");
        craftingSocket_2 = GameObject.Find("CraftingSocket_2");
        craftingSocket_Final = GameObject.Find("CraftingSocket_Final");

        holdTimer = holdThrow;
    }

    // Update is called once per frame
    void Update()
    {
        if(canCraft)
        {
            if (Input.GetKeyDown(KeyCode.E) && isItemInHandFinal) 
            {
                audioSource.PlayOneShot(errorSound);
                stressReceiver.InduceStress(0.3f);
            }
            else if(Input.GetKeyDown(KeyCode.E))
            {
                InstantiateObjects();
                DestroyObjectInHand();
            }

            if (Input.GetKey(KeyCode.E))
            {
                holdTimer -= Time.deltaTime;
                if (holdTimer < 0)
                    //function thats being called=
                    CleanCraftingTable();
            }
            else
                holdTimer = holdThrow;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            canCraft = true;
            craftingInfoText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canCraft = false;
        craftingInfoText.gameObject.SetActive(false);
    }

    private void DestroyObjectInHand() 
    {
        foreach (Transform child in itemSocket.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void InstantiateObjects() 
    {
        if (craftingSocket_1.transform.childCount == 0 && !isItemInHandFinal)
        {
            
            Transform[] itemChildrenTransforms = itemSocket.GetComponentsInChildren<Transform>();

            // itemSocket'in altýndaki tüm çocuklarýn transformunu al
            foreach (Transform childTransform in itemChildrenTransforms)
            {
                // itemSocket'in kendisini geç, sadece alt çocuklarla ilgilen
                if (childTransform != itemSocket.transform && childTransform.parent == itemSocket.transform)
                {
                    audioSource.PlayOneShot(putSound);
                    childTransform.SetParent(craftingSocket_1.transform, false);
                    childTransform.position = craftingSocket_1.transform.position;
                    childTransform.GetComponent<Billboarding>().enabled = true;
                    id_1 = childTransform.GetComponent<Item>().ID;
                }

            }
        }

        /*else if (craftingSocket_1.transform.childCount == 0 && isItemInHandFinal)
        {
            stressReceiver.InduceStress(0.3f);
        }*/

        else if (craftingSocket_2.transform.childCount == 0) 
        {
            
            Transform[] itemChildrenTransforms = itemSocket.GetComponentsInChildren<Transform>();

            // itemSocket'in altýndaki tüm çocuklarýn transformunu al
            foreach (Transform childTransform in itemChildrenTransforms)
            {
                // itemSocket'in kendisini geç, sadece alt çocuklarla ilgilen
                if (childTransform != itemSocket.transform && childTransform.parent == itemSocket.transform)
                {
                    childTransform.SetParent(craftingSocket_2.transform, false);
                    childTransform.position = craftingSocket_2.transform.position;
                    id_2 = childTransform.GetComponent<Item>().ID;
                    spawn_id = id_1 + id_2;
                    //audioSource.PlayOneShot(craftSound);
                    craftParticle.SetActive(true);
                    FinalProduct(spawn_id);
                }

            }
        }
        else
        {
            
        }
    }

    public void CleanCraftingTable()
    {
        if (craftingSocket_1.transform.childCount > 0)
        {
            Debug.Log("CraftingSocket_1 temizlendi!");
            CleanSocket(craftingSocket_1);
        }

        if (craftingSocket_2.transform.childCount > 0)
        {
            Debug.Log("CraftingSocket_2 temizlendi!");
            CleanSocket(craftingSocket_2);
        }

        if (craftingSocket_Final.transform.childCount > 0)
        {
            Debug.Log("CraftingSocket_Final temizlendi!");
            CleanSocket(craftingSocket_Final);
        }
    }

    public void CleanIngredients()
    {
        if (craftingSocket_1.transform.childCount > 0)
        {
            Debug.Log("CraftingSocket_1 temizlendi!");
            CleanSocket(craftingSocket_1);
        }

        if (craftingSocket_2.transform.childCount > 0)
        {
            Debug.Log("CraftingSocket_2 temizlendi!");
            CleanSocket(craftingSocket_2);
        }
    }

    private void CleanSocket(GameObject socket)
    {
        foreach (Transform child in socket.transform)
        {
            Destroy(child.gameObject);
        }
    }


    private void FinalProduct(int spawn_id)
    {
        CleanIngredients();

        GameObject newItem = null;
        if (spawnItemsDictionary.TryGetValue(spawn_id, out int itemIndex))
        {
            newItem = Instantiate(items[itemIndex], Vector3.zero, Quaternion.identity);
            newItem.transform.SetParent(craftingSocket_Final.transform, false);
        }
        else if (spawn_id == 68 || spawn_id == 9046 || spawn_id == 9999 || spawn_id == 65118 || spawn_id == 66071 || spawn_id == 217497 || spawn_id == 19855)
        {
            newItem = Instantiate(items[8], Vector3.zero, Quaternion.identity);
            newItem.transform.SetParent(craftingSocket_Final.transform, false);
        }
        else
        {
            throw new System.Exception("Unknown ID for instantiation.");
        }

        if (newItem == null)
        {
            throw new System.Exception("Instantiated item is null.");
        }
    }

    private Dictionary<int, int> spawnItemsDictionary = new Dictionary<int, int>()
    {
        { 24, 0 },
        { 35, 1 },
        { 57, 2 },
        { 56129, 3 },
        { 207555, 4 },
        { 101, 5 },
        { 9013, 6 },
        { 9913, 7 },
        { 9966, 8 },
        { 1012, 9 },
        { 46, 10 },
        { 56140, 11 },
        { 207566, 12 },
        { 112, 13 },
        { 9024, 14 },
        { 9924, 15 },
        { 9977, 16 },
        { 1023, 17 },
        { 90, 18 },
        { 56162, 19 },
        { 207588, 20 },
        { 134, 21 },
        { 9946, 22 },
        { 1045, 23 },
        { 112234, 24 },
        { 263660, 25 },
        { 56206, 26 },
        { 66018, 27 },
        { 57117, 28 },
        { 415086, 29 },
        { 207632, 30 },
        { 216544, 31 },
        { 217444, 32 },
        { 208543, 33 },
        { 178, 34 },
        { 9090, 35 },
        { 9990, 36 },
        { 10043, 37 },
        { 1089, 38 },
        { 18002, 39 },
        { 18902, 40 },
        { 18955, 41 },
        { 10001, 42 },
        { 19802, 43 },
        { 10901, 44 },
        { 19908, 45 },
        { 10954, 46 },
        { 2000, 47 }
    };

}
