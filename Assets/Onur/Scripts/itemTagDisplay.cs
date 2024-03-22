using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class itemTagDisplay : MonoBehaviour
{
    private RaycastInteract raycastInteract;
    private TMP_Text itemName;
    // Start is called before the first frame update
    void Start()
    {
        raycastInteract = GameObject.Find("First Person Camera").GetComponent<RaycastInteract>();
        itemName = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        itemName.text = raycastInteract.itemName;
    }
}
