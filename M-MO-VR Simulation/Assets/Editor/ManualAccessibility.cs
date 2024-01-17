using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// Editor script that Adds AltText to an object inheriting Object
[CustomEditor(typeof(Object))]
public class ManualAccessibility : Editor
{
    private SerializedProperty altText;
    
    // Right-click option for GameObjects
    [MenuItem("GameObject/Add Accessible Field")]
    private static void AddField(MenuCommand menuCommand)
    {
        // Store selected GameObject
        GameObject selectedObject = menuCommand.context as GameObject;
        if (selectedObject != null)
        {
            // Store object's Object script
            Object script = selectedObject.GetComponent<Object>();
            if (script != null)
            {
                // Serialize script and find it's serialized altText field, if it exists
                SerializedObject obj = new SerializedObject(script);
                SerializedProperty altText = obj.FindProperty("altText");

                // If no altText
                if (altText == null)
                {
                    obj.Update();
                    // Create new serialized altText property by adding new field after all other fields (m_Script)
                    SerializedProperty serialProps = obj.FindProperty("m_Script");
                    altText = serialProps.Copy();
                    altText.Next(false);

                    // Set altText to script's name (might need to be changed to object's name?)
                    altText.stringValue = script.GetType().Name;
                    Debug.Log("Alt Text successfully added to " + script.GetType().Name);
                    obj.ApplyModifiedProperties();

                    // Mark selected GameObject as dirty to save changes
                    EditorUtility.SetDirty(selectedObject);
                }
                else
                {
                    obj.Update();
                    altText.stringValue = script.GetType().Name;
                    Debug.Log("Alt Text successfully updated for " + script.GetType().Name);
                    obj.ApplyModifiedProperties();
                    
                    // Mark selected GameObject as dirty to save changes
                    EditorUtility.SetDirty(selectedObject);
                    // Save the changes to the scene
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
            }
        }
    }

    private void OnEnable()
    {
         altText = serializedObject.FindProperty("altText");

        // If altText property doesn't exist, create it
        if (altText == null)
        {
            serializedObject.Update();
            
            // Create new serialized altText property by adding new field after all other fields (m_Script)
            SerializedProperty serialProps = serializedObject.FindProperty("m_Script");
            altText = serialProps.Copy();
            altText.Next(false);
            
            // Set altText to the empty string
            altText.stringValue = "";
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
            // Save the changes to the scene
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }

    public override void OnInspectorGUI()
    {
       
        serializedObject.Update();

        DrawDefaultInspector(); // Draw the default inspector for the target script

        EditorGUILayout.Space(); // Add space between default inspector and custom buttons

        EditorGUILayout.PropertyField(altText, new GUIContent("Alt-Text"));

        serializedObject.ApplyModifiedProperties();
            
        // Mark selected GameObject as dirty to save changes
        EditorUtility.SetDirty(target);
        // Save the changes to the scene
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    // public override void OnInspectorGUI()
    // {
    //     serializedObject.Update(); // Ensure serializedObject is up-to-date

    //     // Manually find the altText property in OnInspectorGUI
    //     altText = serializedObject.FindProperty("altText");

    //     DrawDefaultInspector(); // Draw the default inspector for the target script

    //     EditorGUILayout.Space(); // Add space between default inspector and custom buttons

    //     // Manually display the altText property field
    //     EditorGUILayout.PropertyField(altText, new GUIContent("Alt-Text"));

    //     serializedObject.ApplyModifiedProperties();

    //     // Mark selected GameObject as dirty to save changes
    //     EditorUtility.SetDirty(target);
    // }
}
