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
    // Check if altText is empty


    // Check if altText is filled out


    // Check if altText is a duplicate (same name as another object or ends with a number)


    // Check if altText is only one word

}