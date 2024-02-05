using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using AccessibilityTags;

// Editor script that Adds AltText to ALL GameObjects
[CustomEditor(typeof(GameObject))]
public class AutoAccessibility : Editor
{    
    // Right-click option for GameObjects
    [MenuItem("Tools/Add Accessible Field(s) to entire scene")]
    private static void AddField(MenuCommand menuCommand)
    {
        // Store GameObjects
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
        Renderer renderer;
        // Iterate through each object
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                renderer = obj.GetComponent<Renderer>();
                // Check if object exists and has a Mesh Collider script attached
                if (renderer != null && renderer.enabled == true)
                {
                    // Store object's AccessibilityTags script, if it exists
                    AccessibilityTags.AccessibilityTags script = obj.GetComponent<AccessibilityTags.AccessibilityTags>();
                    string text = "This is a " + obj.name + ". ";
                    Object objectScript = obj.GetComponent<Object>();
                    if (objectScript != null)
                    {
                        text += objectScript.description + " ";
                    }
                    // If script exists, update altText to object's name
                    if (script != null)
                    {
                        // if (obj.interactable == true)
                        // {
                        //     text += "It is interactible.";
                        // }
                        // else
                        // {
                        //     text += "It is NOT interactible.";
                        // }
                        script.AltText = text;
                        Debug.Log("Alt Text successfully updated for " + obj.name);
                    }
                    else // Otherwise, add altText as object's name
                    {
                        script = Undo.AddComponent<AccessibilityTags.AccessibilityTags>(obj);
                        script.AltText = text;
                        Debug.Log("Alt Text successfully added to " + obj.name);
                    }
                    // Mark selected GameObject as dirty to save changes
                    EditorUtility.SetDirty(obj);
                }
                else
                {
                    Debug.Log("Failed to add Alt Text to " + obj.name + " (selected object may not have a Mesh Collider script)");
                }
            }
        }
        // Mark scene dirty to save changes to the scene
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
}
