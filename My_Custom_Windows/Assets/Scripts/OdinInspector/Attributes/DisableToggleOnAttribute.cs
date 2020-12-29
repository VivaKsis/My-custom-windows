using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableToggleOnAttribute : PropertyAttribute
{
    public float xCoordinate = 0f;
    public DisableToggleOnAttribute(float x)
    {
        this.xCoordinate = x;
    }

    public DisableToggleOnAttribute()
    {
    }
}
