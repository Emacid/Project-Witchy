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
        if (isPickable && Input.GetKeyDown(KeyCode.E))
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

            if (isFinalItem)
            {
                Destroy(gameObject);
            }
        }
    }

    void SetNewItemProperties(GameObject newItem)
    {
        newItem.GetComponentInChildren<Outline>().isLooking = false;
        newItem.GetComponentInChildren<Transform>().GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }


    private void DestroyObjectInHand()
    {
        foreach (Transform child in itemSocket.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
