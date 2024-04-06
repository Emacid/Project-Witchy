using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public bool canDestroyItem = false;
    private GameObject itemSocket;
    private Item itemInHand = null;
    private CraftingSystem craftingSystemScript;
    [SerializeField]
    private GameObject item_discard_vfx;

    // Start is called before the first frame update
    void Start()
    {
        itemSocket = GameObject.Find("ItemSocket");
        craftingSystemScript = GameObject.Find("CraftingSystem").GetComponent<CraftingSystem>();
        item_discard_vfx.SetActive(false); // Baþlangýçta efekt devre dýþý olmalý
    }

    // Update is called once per frame
    void Update()
    {
        if (canDestroyItem && Input.GetKeyDown(KeyCode.E))
        {
            if (HasChildren(itemSocket))
            {
                StartCoroutine(PlayParticles());
                DestroyObjectInHand();
                craftingSystemScript.isItemInHandFinal = false;
            }
            else
            {
                Debug.Log("No item in hand to destroy.");
            }
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

    private IEnumerator PlayParticles()
    {
        item_discard_vfx.SetActive(true);
        yield return new WaitForSeconds(1f);
        item_discard_vfx.SetActive(false);
    }

    private bool HasChildren(GameObject obj)
    {
        return obj.transform.childCount > 0;
    }
}
