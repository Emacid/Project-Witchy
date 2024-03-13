using UnityEngine;

public class Customer : MonoBehaviour
{
    public Sprite[] headSprites; // Kafa sprite'lar�n�n dizisi
    public Sprite[] bodySprites; // V�cut sprite'lar�n�n dizisi

    void Start()
    {
        // Head ve Body child objelerini buluyoruz
        Transform head = transform.Find("Head");
        Transform body = transform.Find("Body");

        if (head == null || body == null)
        {
            Debug.LogError("Head veya Body objeleri bulunamad�!");
            return;
        }

        // Head ve Body sprite renderer'lar�n� al�yoruz
        SpriteRenderer headRenderer = head.GetComponent<SpriteRenderer>();
        SpriteRenderer bodyRenderer = body.GetComponent<SpriteRenderer>();

        // Rastgele kafa ve v�cut sprite'lar�n� se�iyoruz
        Sprite randomHeadSprite = headSprites[Random.Range(0, headSprites.Length)];
        Sprite randomBodySprite = bodySprites[Random.Range(0, bodySprites.Length)];

        // Se�ilen sprite'lar� at�yoruz
        headRenderer.sprite = randomHeadSprite;
        bodyRenderer.sprite = randomBodySprite;
    }
}
