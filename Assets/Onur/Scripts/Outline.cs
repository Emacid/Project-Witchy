using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    private Material material;
    private Vector3 initialScale;
    private bool wasLooking = false; // isLooking'in önceki durumunu saklar
    public bool isLooking = false;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        material.SetFloat("_Thickness", 0);
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // isLooking durumu deðiþtiðinde
        if (isLooking != wasLooking)
        {
            if (isLooking)
            {
                // Büyütme iþlemi sadece bir kez gerçekleþecek
                material.SetFloat("_Thickness", 15f);
                transform.localScale += new Vector3(0.1f, 0.1f, 0.1f); // Increase scale gradually
            }
            else
            {
                // Küçültme iþlemi sadece bir kez gerçekleþecek
                transform.localScale = initialScale;
                material.SetFloat("_Thickness", 0);
            }

            wasLooking = isLooking; // Önceki durumu güncelle
        }
    }

}
