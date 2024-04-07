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

    private List<CustomerIdentifier> lastSixteenCustomers = new List<CustomerIdentifier>(); // List to keep track of last 16 customers

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
        } while (IsLastSixteenCustomer(selectedCustomer)); // Check if selected customer is in last 16 customers list

        // Spawn the selected customer at the current position of this object
        GameObject newCustomer = Instantiate(selectedCustomer, transform.position, Quaternion.identity);

        // Set the spawned customer as a child of Customers object
        newCustomer.transform.parent = transform;

        // Add the spawned customer to the last 16 customers list
        CustomerIdentifier newCustomerIdentifier = new CustomerIdentifier();
        newCustomerIdentifier.customerType = selectedCustomer.name; // You can adjust this according to your needs
        newCustomerIdentifier.customerID = lastSixteenCustomers.Count; // Assigning a simple incremental ID for now
        lastSixteenCustomers.Add(newCustomerIdentifier);

        // If the last 16 customers list exceeds 16, remove the oldest customer
        if (lastSixteenCustomers.Count > 16)
        {
            lastSixteenCustomers.RemoveAt(0);
        }
    }

    // Check if the selected customer is one of the last 16 customers
    private bool IsLastSixteenCustomer(GameObject selectedCustomer)
    {
        string selectedCustomerName = selectedCustomer.name;
        foreach (var customer in lastSixteenCustomers)
        {
            if (customer.customerType == selectedCustomerName)
            {
                return true;
            }
        }
        return false;
    }
}
