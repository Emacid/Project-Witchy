using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    private Camera theCam;
    public bool useStaticBillboard;
    // Start is called before the first frame update
    void Start()
    {
        theCam = GameObject.Find("First Person Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!useStaticBillboard)
        {
            transform.LookAt(theCam.transform);
        }
        else
        {
            transform.rotation = theCam.transform.rotation;
        }

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y,0f);
    }

}
