using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A struct to represent a customer with its unique identifier
public struct CustomerIdentifier
{
    public string customerType; // Type of the customer (could be the name or any other identifying feature)
    public int customerID; // Unique ID of the customer
}

public class CustomerController : MonoBehaviour
{
    public GameObject[] customers;
    public float checkInterval = 1f; // Interval to check for child objects in seconds

    private List<CustomerIdentifier> lastTenCustomers = new List<CustomerIdentifier>(); // List to keep track of last 10 customers

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
        GameObject selectedCustomer = null;
        do
        {
            selectedCustomer = customers[Random.Range(0, customers.Length)];
        } while (IsLastTenCustomer(selectedCustomer)); // Check if selected customer is in last 10 customers list

        // Spawn the selected customer at the current position of this object
        GameObject newCustomer = Instantiate(selectedCustomer, transform.position, Quaternion.identity);

        // Set the spawned customer as a child of Customers object
        newCustomer.transform.parent = transform;

        // Add the spawned customer to the last 10 customers list
        CustomerIdentifier newCustomerIdentifier = new CustomerIdentifier();
        newCustomerIdentifier.customerType = selectedCustomer.name; // You can adjust this according to your needs
        newCustomerIdentifier.customerID = lastTenCustomers.Count; // Assigning a simple incremental ID for now
        lastTenCustomers.Add(newCustomerIdentifier);

        // If the last 10 customers list exceeds 10, remove the oldest customer
        if (lastTenCustomers.Count > 10)
        {
            lastTenCustomers.RemoveAt(0);
        }
    }

    // Check if the selected customer is one of the last 10 customers
    private bool IsLastTenCustomer(GameObject selectedCustomer)
    {
        string selectedCustomerName = selectedCustomer.name;
        foreach (var customer in lastTenCustomers)
        {
            if (customer.customerType == selectedCustomerName)
            {
                return true;
            }
        }
        return false;
    }
}
