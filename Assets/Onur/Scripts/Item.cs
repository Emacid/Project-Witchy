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

            // ID'ye göre Instantiate iþlemi gerçekleþtirilir
            GameObject newItem;
            switch (ID)
            {
                case 1:
                    newItem = Instantiate(prefabs[0], Vector3.zero, Quaternion.identity);
                    break;
                case 2:
                    newItem = Instantiate(prefabs[1], Vector3.zero, Quaternion.identity);
                    break;
                case 3:
                    newItem = Instantiate(prefabs[2], Vector3.zero, Quaternion.identity);
                    break;
                default:
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

    private void DestroyObjectInHand()
    {
        foreach (Transform child in itemSocket.transform)
        {
            Destroy(child.gameObject);
        }
    }

}
