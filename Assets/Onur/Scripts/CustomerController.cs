using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public GameObject[] customers;
    public float checkInterval = 1f; // Interval to check for child objects in seconds

    private void Start()
    {
        InvokeRepeating("CheckChildObjects", 0f, checkInterval);
    }

    // Check if Customers object has any child objects
    private void CheckChildObjects()
    {
        if (transform.childCount == 0)
        {
            SpawnCustomer();
        }
    }

    // Spawn a customer
    private void SpawnCustomer()
    {
        // Choose a random customer from the array
        GameObject selectedCustomer = customers[Random.Range(0, customers.Length)];

        // Spawn the selected customer at the current position of this object
        GameObject newCustomer = Instantiate(selectedCustomer, transform.position, Quaternion.identity);

        // Set the spawned customer as a child of Customers object
        newCustomer.transform.parent = transform;
    }
}
