using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Facebook.WitAi.TTS.Utilities;




public class PartialVis : MonoBehaviour
{
    public Transform raycastOrigin;
    public InputActionProperty button;

    public TextMeshProUGUI interactable;
    public TextMeshProUGUI details;
    public TextMeshProUGUI obj_name;

    [Header("Colors")]
    [SerializeField] Color32 trueColor;
    [SerializeField] Color32 falseColor;

    [Header("Text-to-Speech")]
    [SerializeField] private bool ttsEnabled = true;
    [SerializeField] private TTSSpeaker ttsSpeaker;
    private Assets.VisionReader visionReader;



    // Start is called before the first frame update
    void Start()
    {
        trueColor = Color.green;
        falseColor = Color.red;

        visionReader = new Assets.VisionReader(ttsSpeaker);
    }

    // Update is called once per frame
    void Update()
    {
        if (button.action.WasPressedThisFrame())
        {
            Scan();
        }
    }



    void Scan()
    {
        Object objectInfo = new();
        bool isInteractable = false;
        //Raycast send
        Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);


        //If the raycast hits
        if (Physics.Raycast(ray, out RaycastHit hit, 100)) // Ray hit something
        {
            //Debug.Log(hit.collider.tag);
            //Debug.Log(hit.collider.gameObject);
            //Debug.Log(hit.collider.name);


            //If the object hit has an object script
            if (hit.collider.gameObject.GetComponent<Object>() != null)
            {
                objectInfo = hit.collider.gameObject.GetComponent<Object>();
                Debug.Log("This is a " + objectInfo.name);

                details.text = objectInfo.description + "\n";
                obj_name.text = objectInfo.name;
            }
            else
            {
                details.text = "None";
                obj_name.text = "None";
            }

            //Set the interactable field appropriately
            if (hit.collider.CompareTag("Interactable"))
            {
                interactable.text = "True";
                interactable.color = trueColor;
                isInteractable = true;
            }
            else
            {
                interactable.text = "False";
                interactable.color = falseColor;
            }

            //Speak the name and description of the object
            if (ttsEnabled) visionReader.Speak(objectInfo.name, objectInfo.description, isInteractable);
        }
    }
    public void ResetText()
    {
        interactable.text = "";
        details.text = "";
    }

    public void SetColors(Color32 trueC, Color32 falseC)
    {
        trueColor = trueC;
        falseColor = falseC;

        return;
    }
}

