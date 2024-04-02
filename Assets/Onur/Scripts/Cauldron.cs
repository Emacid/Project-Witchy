using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public bool canDestroyItem = false;
    private GameObject itemSocket;
    private Item itemInHand = null;
    private CraftingSystem craftingSystemScript;

    // Start is called before the first frame update
    void Start()
    {
        itemSocket = GameObject.Find("ItemSocket");
        craftingSystemScript = GameObject.Find("CraftingSystem").GetComponent<CraftingSystem>();
    }

    // Update is called once per frame
    void Update()
    {
       if(canDestroyItem && Input.GetKeyDown(KeyCode.E)) 
        {
           DestroyObjectInHand();
           craftingSystemScript.isItemInHandFinal = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canDestroyItem = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canDestroyItem = false;
    }

     private void DestroyObjectInHand()
     {
       foreach (Transform child in itemSocket.transform)
         {
            Destroy(child.gameObject);
         }
     }

    }
