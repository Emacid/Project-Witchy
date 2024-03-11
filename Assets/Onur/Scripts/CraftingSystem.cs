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

    private bool canCraft = false;
    private GameObject itemSocket;
    private GameObject craftingSocket_1;
    private GameObject craftingSocket_2;
    private GameObject craftingSocket_Final;
    // Start is called before the first frame update
    void Start()
    {
        itemSocket = GameObject.Find("ItemSocket");
        craftingSocket_1 = GameObject.Find("CraftingSocket_1");
        craftingSocket_2 = GameObject.Find("CraftingSocket_2");
        craftingSocket_Final = GameObject.Find("CraftingSocket_Final");
    }

    // Update is called once per frame
    void Update()
    {
        if(canCraft)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Yarramý ye x3");
                InstantiateObjects();
                DestroyObjectInHand();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            canCraft = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canCraft = false;
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
        if (craftingSocket_1.transform.childCount == 0)
        {
            Debug.Log("yarramý ye x2");
            Transform[] itemChildrenTransforms = itemSocket.GetComponentsInChildren<Transform>();

            // itemSocket'in altýndaki tüm çocuklarýn transformunu al
            foreach (Transform childTransform in itemChildrenTransforms)
            {
                // itemSocket'in kendisini geç, sadece alt çocuklarla ilgilen
                if (childTransform != itemSocket.transform && childTransform.parent == itemSocket.transform)
                {
                    childTransform.SetParent(craftingSocket_1.transform, false);
                    childTransform.position = craftingSocket_1.transform.position;
                    id_1 = childTransform.GetComponent<Item>().ID;
                }

            }
        }

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
                    FinalProduct(spawn_id);
                }

            }
        }
        else
        {
            Debug.Log("yarramý ye!");
        }
    }

    private void FinalProduct(int spawn_id) 
    {
        GameObject newItem;
        switch (spawn_id)
        {
            case 1:
                Debug.Log("nothingness");
                break;
            case 2:
                Debug.Log("nothingness");
                break;
            case 3:
                newItem = Instantiate(items[0], Vector3.zero, Quaternion.identity);
                newItem.GetComponent<Transform>().SetParent(craftingSocket_Final.transform, false);
                break;
            case 4:
                Debug.Log("nothingness");
                break;
            default:
                Debug.LogWarning("Unknown ID for instantiation.");
                return;
        }

    }

}
