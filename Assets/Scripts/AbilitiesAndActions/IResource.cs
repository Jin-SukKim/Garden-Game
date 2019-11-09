using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResource {
    bool ResourceCheck(float cost);
    void PayCost(float cost);
}