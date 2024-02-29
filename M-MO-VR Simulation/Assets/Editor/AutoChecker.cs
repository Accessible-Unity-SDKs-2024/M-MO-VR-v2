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

using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using AccessibilityTags;

// Editor script that Adds AltText to ALL GameObjects
[CustomEditor(typeof(GameObject))]

public class AutoChecker : Editor
{    
    AccessibilityTags.AccessibilityTags script = selectedObject.GetComponent<AccessibilityTags.AccessibilityTags>();

    if (script.AltText != null)
    {
        // If altText is empty
        if (script.AltText == "")
        {
            // run AutoAccessibility.cs to fill it out probably or alert developer
            AutoAccessibility.AddField();
        }
        // If altText is filled out
        else
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                // Check if altText is a duplicate (same name as another object or ends with a number)
                CheckForDuplicateAltText(object, script);
                // code here to compare altText of one object to altText of another object (above)

                // Check if altText is only one word
                if (script.AltText.Contains(" "))
                {
                    Debug.Log("Alt Text is more than one word for " + object.name);
                }
                else
                {
                    if(script.AltText.)
                    // alert developer asking if they are sure about keeping it as one word
                }
            }
        }
    }
    else
    {
        Debug.Log("Alt Text is null for " + object.name);
        // alert developer
    }

    private void CheckForDuplicateAltText(GameObject obj, AccessibilityTags.AccessibilityTags script)
    {
        GameObject[] objectsInScene = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject otherObj in objectsInScene)
        {
            if (otherObj != obj)
            {
                AccessibilityTags.AccessibilityTags otherScript = otherObj.GetComponent<AccessibilityTags.AccessibilityTags>();
                //check for duplicate alt-text
                //SHOULD WE ONLY CHECK THE ALT TEXT AND IGNORE THE NAME
                //LIKE CHECK THE NAME AND TEXT SEPARATELY. 
                //two things with diff names could have the same alt-text and vice versa?
                if (otherScript != null && script.AltText == otherScript.AltText)
                {
                    Debug.LogWarning("Duplicate altText found for objects: " + obj.name + " and " + otherObj.name);
                }
                //check for duplicate object names
                if(obj.name == otherObj.name){
                    Debug.LogWarning("Duplicate name found for objects with name: " + obj.name);

                }
            }
        }
    }
}

//Kasidy
public class AutoChecker2 : Editor{

    // Store GameObjects
    GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

    foreach (GameObject obj in objects){
        //only check objects with the accessibility tad
        AccessibilityTags.AccessibilityTags script = obj.GetComponent<AccessibilityTags.AccessibilityTags>();
        // If script exists, start checking
        if (script != null){
            //check if alt text is null or just " "
            if(script.AltText == null || script.AltText == " "){
                Debug.LogWarning("Object with name: " + obj.Name + " has no alt-text.");
            }
                
            //check for duplicates
            GameObject[] otherObjects = GameObject.FindObjectsOfType<GameObject>();

            foreach (GameObject otherObj in otherObjects){
                if (otherObj != obj){
                    AccessibilityTags.AccessibilityTags otherScript = otherObj.GetComponent<AccessibilityTags.AccessibilityTags>();
                    //check for duplicate alt-text
                    
                    //!!!!!!!!!!
                    //SHOULD WE ONLY CHECK THE ALT TEXT AND IGNORE THE NAME
                    //LIKE CHECK THE NAME AND TEXT SEPARATELY. 
                    //two things with diff names could have the same alt-text and vice versa?
                    //!!!!!!!!!!

                    if (otherScript != null && script.AltText == otherScript.AltText){
                        Debug.LogWarning("Duplicate altText found for objects: " + obj.name + " and " + otherObj.name);
                    }
                    //check for duplicate object names
                    if(obj.name == otherObj.name){
                        Debug.LogWarning("Duplicate name found for objects with name: " + obj.name);

                    }
                }
            }
                
            //check if it is short
            //change number???
            //alt text format: "This is a " + obj.name + ". " + description + " ";
            if(script.AltText.length < 30){
                Debug.LogWarning("the alt-text for object with name: " + obj.Name + " is short. Consider adding more detail.")
            }
         }
    }

        
}