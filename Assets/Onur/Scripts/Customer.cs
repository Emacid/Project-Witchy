using UnityEngine;

public class Customer : MonoBehaviour
{
    public Sprite[] headSprites; // Kafa sprite'larýnýn dizisi
    public Sprite[] bodySprites; // Vücut sprite'larýnýn dizisi

    void Start()
    {
        // Head ve Body child objelerini buluyoruz
        Transform head = transform.Find("Head");
        Transform body = transform.Find("Body");

        if (head == null || body == null)
        {
            Debug.LogError("Head veya Body objeleri bulunamadý!");
            return;
        }

        // Head ve Body sprite renderer'larýný alýyoruz
        SpriteRenderer headRenderer = head.GetComponent<SpriteRenderer>();
        SpriteRenderer bodyRenderer = body.GetComponent<SpriteRenderer>();

        // Rastgele kafa ve vücut sprite'larýný seçiyoruz
        Sprite randomHeadSprite = headSprites[Random.Range(0, headSprites.Length)];
        Sprite randomBodySprite = bodySprites[Random.Range(0, bodySprites.Length)];

        // Seçilen sprite'larý atýyoruz
        headRenderer.sprite = randomHeadSprite;
        bodyRenderer.sprite = randomBodySprite;
    }
}
