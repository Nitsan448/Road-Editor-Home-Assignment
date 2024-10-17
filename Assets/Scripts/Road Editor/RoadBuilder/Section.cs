using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    public Junction StartJunction;
    public Junction EndJunction;

    public SectionPersistentData GetSectionPersistentData()
    {
        return new SectionPersistentData(StartJunction.Id, EndJunction.Id);

    }
}
