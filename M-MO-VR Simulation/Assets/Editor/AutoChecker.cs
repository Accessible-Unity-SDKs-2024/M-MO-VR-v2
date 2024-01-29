// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class AutoChecker : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using AccessibilityTags;

// Editor script that Adds AltText to ALL GameObjects
[CustomEditor(typeof(GameObject))]

public class AutoChecker : Editor
{    
    if (script.AltText != null)
    {
        // If altText is empty
        if (script.AltText == "")
        {
            // run AutoAccessibility.cs to fill it out probably or alert developer
        }
        // If altText is filled out
        else
        {
            // Check if altText is a duplicate (same name as another object or ends with a number)
            
            // code here to compare altText of one object to altText of another object

            // Check if altText is only one word
            if (script.AltText.Contains(" "))
            {
                Debug.Log("Alt Text is more than one word for " + obj.name);
            }
            else
            {
                // alert developer asking if they are sure about keeping it as one word
            }
        }
    }
    else
    {
        Debug.Log("Alt Text is null for " + obj.name);
        // alert developer
    }

}