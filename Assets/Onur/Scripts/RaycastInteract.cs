using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteract : MonoBehaviour
{
    public float interactDistance = 0.5f; // Interaksiyon mesafesi
    public LayerMask interactLayer; // Interaksiyon yap�lacak katmanlar
    public Color highlightColor; // Edit�rde belirlenecek renk

    private GameObject lastHitObject; // Son temas edilen nesne
    private Color originalColor; // Orijinal renk

    private Outline outlineOfObject;

    public string itemName = string.Empty;

    void Start()
    {
        originalColor = Color.white; // Varsay�lan renk
        outlineOfObject = null;
    }

    void Update()
    {
        Debug.Log(itemName);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, interactLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Debug ray'� �iz
            Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.red);

            // Son temas edilen nesnenin rengini de�i�tirme
            if (lastHitObject != hitObject)
            {
                ResetLastHitObjectColor();
            }

            // Temas edilen nesnenin rengini de�i�tirme
            SpriteRenderer spriteRenderer = hitObject.GetComponent<SpriteRenderer>();
            Item itemScript = hitObject.GetComponentInParent<Item>(); // Item scriptini al

            if (spriteRenderer != null && spriteRenderer.color == highlightColor)
            {
                // E�er belirlenen renk ise, pickable yap
                if (itemScript != null)
                {
                    itemScript.isPickable = true;
                    Debug.Log(hitObject.name + " is now pickable!");
                    outlineOfObject = hitObject.GetComponent<Outline>();
                    outlineOfObject.isLooking = true;

                    // itemName'i g�ncelle
                    itemName = spriteRenderer.sprite.name;
                }

            }
            else
            {
                //outlineOfObject.isLooking = false;

                // Belirlenen renk de�ilse, pickable'� false yap
                if (itemScript != null)
                {
                    itemScript.isPickable = false;
                }

                // itemName'i bo� yap
                itemName = string.Empty;
            }

            if (spriteRenderer != null)
            {
                spriteRenderer.color = highlightColor;
                lastHitObject = hitObject;
            }

        }
        else
        {
            // E�er hi�bir nesne temas edilmiyorsa, son temas edilen nesnenin rengini s�f�rla
            ResetLastHitObjectColor();
            // Debug ray'� �iz
            Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.red);

            // itemName'i bo� yap
            itemName = string.Empty;
        }
    }

    // Son temas edilen nesnenin rengini s�f�rlama

    void ResetLastHitObjectColor()
    {
        if (lastHitObject != null)
        {
            SpriteRenderer lastSpriteRenderer = lastHitObject.GetComponent<SpriteRenderer>();
            if (lastSpriteRenderer != null)
            {
                // Orjinal rengine geri d�nd�r
                lastSpriteRenderer.color = originalColor;
            }

            Item itemScript = lastHitObject.GetComponentInParent<Item>();
            if (itemScript != null)
            {
                // Pickable'� false yap
                itemScript.isPickable = false;
            }

            outlineOfObject = lastHitObject.GetComponent<Outline>(); // outlineOfObject'i s�f�rla
            if (outlineOfObject != null)
            {
                outlineOfObject.isLooking = false; // outlineOfObject'i s�f�rla
            }

            lastHitObject = null;
        }
    }
}
