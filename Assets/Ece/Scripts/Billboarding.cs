using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private Camera theCam;
    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main; 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(theCam.transform);

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y,0f);
    }

}
