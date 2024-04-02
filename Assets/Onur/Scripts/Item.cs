using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite itemSprite;
    public bool isPickable = false; // Pickable olup olmadýðýný belirten bool deðiþken
    private GameObject itemSocket;
    public int ID;
    private CraftingSystem craftingSystemScript;
    public bool isFinalItem = false;

    // Prefab objeleri Unity Editörü üzerinden atanabilir hale getirmek için kullanýlýr.
    public GameObject[] prefabs;

    // Start is called before the first frame update
    void Start()
    {
        itemSocket = GameObject.Find("ItemSocket");
        craftingSystemScript = GameObject.Find("CraftingSystem").GetComponent<CraftingSystem>();

        // Item_Base objesinin altýndaki ilk SpriteRenderer'ý al
        SpriteRenderer spriteRenderer = GetComponentInChildren<Transform>().GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null && itemSprite != null)
        {
            spriteRenderer.sprite = itemSprite;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer or itemSprite is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickable && Input.GetKeyDown(KeyCode.E) && isFinalItem) 
        {
            Debug.Log(gameObject.name + " is on players hand now! YEAH");

            DestroyObjectInHand();

            GameObject newItem = null;
            spawnItemsDictionary.TryGetValue(ID, out int itemIndex);
            newItem = Instantiate(craftingSystemScript.items[itemIndex] , Vector3.zero, Quaternion.identity);
            SetNewItemProperties(newItem);
            newItem.GetComponent<Billboarding>().enabled = false;
            newItem.transform.SetParent(itemSocket.transform, false);
            newItem.GetComponentInChildren<Collider>().enabled = false;
            craftingSystemScript.isItemInHandFinal = true;
            craftingSystemScript.CleanCraftingTable();
        }
        else if (isPickable && Input.GetKeyDown(KeyCode.E) && !craftingSystemScript.isItemInHandFinal)
        {
            DestroyObjectInHand();

            GameObject newItem = null;

            if (ID >= 1 && ID <= prefabs.Length)
            {
                newItem = Instantiate(prefabs[ID - 1], Vector3.zero, Quaternion.identity);
                SetNewItemProperties(newItem);
            }
            else
            {
                Debug.LogWarning("Unknown ID for instantiation.");
                return;
            }

            newItem.transform.SetParent(itemSocket.transform, false);
            newItem.GetComponentInChildren<Collider>().enabled = false;

            /*if (isFinalItem)
            {
                Destroy(gameObject);
            }*/
        }
    }

    void SetNewItemProperties(GameObject newItem)
    {
        newItem.GetComponentInChildren<Outline>().isLooking = false;
        newItem.GetComponentInChildren<Transform>().GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }


    public void DestroyObjectInHand()
    {
        foreach (Transform child in itemSocket.transform)
        {
            Destroy(child.gameObject);
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
