using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteract : MonoBehaviour
{
    public float interactDistance = 0.5f; // Ýnteraksiyon mesafesi
    public LayerMask interactLayer; // Ýnteraksiyon yapýlacak katmanlar
    public Color highlightColor; // Editörde belirlenecek renk

    private GameObject lastHitObject; // Son temas edilen nesne
    private Color originalColor; // Orijinal renk

    void Start()
    {
        originalColor = Color.white; // Varsayýlan renk
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, interactLayer))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Debug ray'ý çiz
            Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.red);

            // Son temas edilen nesnenin rengini deðiþtirme
            if (lastHitObject != hitObject)
            {
                ResetLastHitObjectColor();
            }

            // Temas edilen nesnenin rengini deðiþtirme
            SpriteRenderer spriteRenderer = hitObject.GetComponent<SpriteRenderer>();
            Item itemScript = hitObject.GetComponentInParent<Item>(); // Item scriptini al

            if (spriteRenderer != null && spriteRenderer.color == highlightColor)
            {
                // Eðer belirlenen renk ise, pickable yap
                if (itemScript != null)
                {
                    itemScript.isPickable = true;
                    Debug.Log(hitObject.name + " is now pickable!");
                }
            }
            else
            {
                // Belirlenen renk deðilse, pickable'ý false yap
                if (itemScript != null)
                {
                    itemScript.isPickable = false;
                }
            }

            if (spriteRenderer != null)
            {
                spriteRenderer.color = highlightColor;
                lastHitObject = hitObject;
            }
        }
        else
        {
            // Eðer hiçbir nesne temas edilmiyorsa, son temas edilen nesnenin rengini sýfýrla
            ResetLastHitObjectColor();
            // Debug ray'ý çiz
            Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.red);
        }
    }

    // Son temas edilen nesnenin rengini sýfýrlama
    void ResetLastHitObjectColor()
    {
        if (lastHitObject != null)
        {
            SpriteRenderer lastSpriteRenderer = lastHitObject.GetComponent<SpriteRenderer>();
            if (lastSpriteRenderer != null)
            {
                // Orjinal rengine geri döndür
                lastSpriteRenderer.color = originalColor;
            }

            Item itemScript = lastHitObject.GetComponentInParent<Item>();
            if (itemScript != null)
            {
                // Pickable'ý false yap
                itemScript.isPickable = false;
            }

            lastHitObject = null;
        }
    }
}
