using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{

    public GameObject Hand;
    public FirstPersonLook firstPersonLook;
    public FirstPersonMovement firstPersonMovement;
    public float waitTime = 5.0f;
    public GameObject firstCustomer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivatePlayer());
        StartCoroutine(ActivateFirstCustomer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ActivatePlayer() 
    {
        yield return new WaitForSeconds(waitTime);
        Hand.gameObject.SetActive(true);
        firstPersonLook.enabled = true;
        firstPersonMovement.enabled = true;
    }

    private IEnumerator ActivateFirstCustomer()
    {
        yield return new WaitForSeconds(41.0f);
        firstCustomer.gameObject.SetActive(true);
    }

}
