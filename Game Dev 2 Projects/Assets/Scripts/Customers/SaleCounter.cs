using System.Collections.Generic;
using UnityEngine;

public class SaleCounter : MonoBehaviour
{
    // VARIABLES
    private List<Customer> queueingCustomers = new List<Customer>();
    public float queueDist = 5f;

    // REFERENCES
    public Transform queueStartPos;

    void Update()
    {
        RemoveUnqueuedCustomers();
        UpdateQueueingPositions();
    }

    public void AddCustomerToQueue(Customer customer)
    {
        queueingCustomers.Add(customer);
    }

    private void RemoveUnqueuedCustomers()
    {
        List<Customer> unqueuedCustomers = new List<Customer>();

        foreach (Customer customer in queueingCustomers) 
        { 
            if(customer.state != Customer.CustomerState.IN_QUEUE)
            {
                unqueuedCustomers.Add(customer);
            }
        }

        foreach (Customer customer in unqueuedCustomers)
        {
            queueingCustomers.Remove(customer);
        }
    }

    private void UpdateQueueingPositions()
    {
        if (queueingCustomers.Count > 0)
        {
            for (int i = 0; i < queueingCustomers.Count; i++)
            {
                queueingCustomers[i].queuePos = queueStartPos.position + (transform.forward * i * queueDist);
            }
        }
    }
}
