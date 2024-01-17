using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using AccessibilityTags;

// Editor script that Adds AltText to a GameObject
[CustomEditor(typeof(GameObject))]
public class ManualAccessibility : Editor
{    
    // Right-click option for GameObjects
    [MenuItem("GameObject/Add Accessible Field(s)")]
    private static void AddField(MenuCommand menuCommand)
    {
        // Store selected GameObject
        GameObject selectedObject = menuCommand.context as GameObject;
        // Check if object exists and has a Mesh Collider script attached (may not be necessary for manual addition of tags by devs)
        if (selectedObject != null && selectedObject.GetComponent<MeshCollider>() != null)
        {
            // Store object's AccessibilityTags script, if it exists
            AccessibilityTags.AccessibilityTags script = selectedObject.GetComponent<AccessibilityTags.AccessibilityTags>();
            // If script exists, update altText to object's name
            if (script != null)
            {
                script.AltText = selectedObject.name;
                Debug.Log("Alt Text successfully updated for " + selectedObject.name);
                // Mark selected GameObject dirty to save changes
                EditorUtility.SetDirty(selectedObject);
                // Mark scene dirty to save changes to the scene
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
            else // Otherwise, add altText as object's name
            {
                script = Undo.AddComponent<AccessibilityTags.AccessibilityTags>((selectedObject));
                script.AltText = selectedObject.name;
                Debug.Log("Alt Text successfully added to " + selectedObject.name);
                // Mark selected GameObject as dirty to save changes
                EditorUtility.SetDirty(selectedObject);
                // Mark scene dirty to save changes to the scene
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }
        else
        {
            Debug.Log("Failed to add Alt Text (selected object may not have a Mesh Collider script)");
        }
    }

    // private void OnEnable()
    // {
    //      altText = serializedObject.FindProperty("altText");

    //     // If altText property doesn't exist, create it
    //     if (altText == null)
    //     {
    //         serializedObject.Update();
            
    //         // Create new serialized altText property by adding new field after all other fields (m_Script)
    //         SerializedProperty serialProps = serializedObject.FindProperty("m_Script");
    //         altText = serialProps.Copy();
    //         altText.Next(false);
            
    //         // Set altText to the empty string
    //         altText.stringValue = "";
    //         serializedObject.ApplyModifiedProperties();
    //         EditorUtility.SetDirty(target);
    //         // Save the changes to the scene
    //         EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    //     }
    // }

    // public override void OnInspectorGUI()
    // {
       
    //     serializedObject.Update();

    //     DrawDefaultInspector(); // Draw the default inspector for the target script

    //     EditorGUILayout.Space(); // Add space between default inspector and custom buttons

    //     EditorGUILayout.PropertyField(altText, new GUIContent("Alt-Text"));

    //     serializedObject.ApplyModifiedProperties();
            
    //     // Mark selected GameObject as dirty to save changes
    //     EditorUtility.SetDirty(target);
    //     // Save the changes to the scene
    //     EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    // }
}
