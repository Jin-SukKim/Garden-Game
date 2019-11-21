using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this interface can be used for any sort of resource
public interface IResource {
    // this should check if there enough of a resource left to pay a cost
    bool CanPayCost(float cost);

    // this should subtract the cost from the resource
    void PayCost(float cost);
}