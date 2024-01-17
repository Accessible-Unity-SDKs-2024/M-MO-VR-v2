using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessibilityTags : MonoBehaviour
{
    [SerializeField]
    private string altText;

    public string AltText
    {
        get;
        set;
    }
}
