using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicResource : IResource
{
    float resource = 0;
    float maxAmount = 0;
    public BasicResource(float amount)
    {
        if (amount < 0)
            Debug.LogError("Made a resource with a negative amount?");

        resource = amount;
        maxAmount = amount;
    }

    public bool CanPayCost(float cost)
    {
        return resource >= cost;
    }

    public void PayCost(float cost)
    {
        resource -= (int)cost;
        if (resource < 0)
        {
            resource = 0;
        }
    }

    public void AddToResource(float amount)
    {
        resource += amount;
        if (resource > maxAmount)
        {
            amount = maxAmount;
        }
    }

    public void SetResourceAmount(int amount)
    {
        resource = 0;
        AddToResource(amount);
    }
}
