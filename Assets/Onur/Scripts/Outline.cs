using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    private Material material;
    private Vector3 initialScale;
    private bool wasLooking = false; // isLooking'in �nceki durumunu saklar
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
        // isLooking durumu de�i�ti�inde
        if (isLooking != wasLooking)
        {
            if (isLooking)
            {
                // B�y�tme i�lemi sadece bir kez ger�ekle�ecek
                material.SetFloat("_Thickness", 15f);
                transform.localScale += new Vector3(0.1f, 0.1f, 0.1f); // Increase scale gradually
            }
            else
            {
                // K���ltme i�lemi sadece bir kez ger�ekle�ecek
                transform.localScale = initialScale;
                material.SetFloat("_Thickness", 0);
            }

            wasLooking = isLooking; // �nceki durumu g�ncelle
        }
    }

}
