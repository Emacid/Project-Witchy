using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite itemSprite;
    public bool isPickable = false; // Pickable olup olmad���n� belirten bool de�i�ken
    private GameObject itemSocket;
    public int ID;

    // Prefab objeleri Unity Edit�r� �zerinden atanabilir hale getirmek i�in kullan�l�r.
    public GameObject[] prefabs;

    // Start is called before the first frame update
    void Start()
    {
        itemSocket = GameObject.Find("ItemSocket");

        // Item_Base objesinin alt�ndaki ilk SpriteRenderer'� al
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

            // ID'ye g�re Instantiate i�lemi ger�ekle�tirilir
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
