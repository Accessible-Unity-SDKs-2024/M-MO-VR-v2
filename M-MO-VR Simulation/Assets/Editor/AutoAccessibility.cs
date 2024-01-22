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
        // Iterate through each object
        foreach (GameObject obj in objects)
        {
            // Check if object exists and has a Mesh Collider script attached
            if (obj != null && obj.GetComponent<MeshCollider>() != null)
            {
                // Store object's AccessibilityTags script, if it exists
                AccessibilityTags.AccessibilityTags script = obj.GetComponent<AccessibilityTags.AccessibilityTags>();
                // If script exists, update altText to object's name
                if (script != null)
                {
                    script.AltText = obj.name;
                    Debug.Log("Alt Text successfully updated for " + obj.name);
                }
                else // Otherwise, add altText as object's name
                {
                    script = Undo.AddComponent<AccessibilityTags.AccessibilityTags>(obj);
                    script.AltText = obj.name;
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
        // Mark scene dirty to save changes to the scene
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
}
