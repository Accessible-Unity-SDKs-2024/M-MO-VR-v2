using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MenuManager : MonoBehaviour
{   
    public Transform head;
    public float SpawnDistance = 2;
    public GameObject menu;
    public GameObject[] OtherUi;
    public bool[] wasActive;
    public InputActionProperty showButton;
    public InputActionProperty display;
    public InputActionProperty hide;
    public GameObject pvManager;
    PartialVis pv; 

    // Start is called before the first frame update
    void Start()
    {   
        //Initialize
        for(int i = 0;i<OtherUi.Length; i++){
            //Set up the wasActive Array
            wasActive[i] = false;
        }

        if(pvManager.GetComponent<PartialVis>() != null){
                pv = pvManager.GetComponent<PartialVis>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(showButton.action.WasPressedThisFrame()){
            
            //If the menu is closed upon button press
            if(!menu.activeSelf){
                //Check to see what UI elements are currently open, mark them to be re-opened, then hide them
                for(int i = 0;i<OtherUi.Length; i++){
                    if(OtherUi[i].activeSelf){
                        wasActive[i] = true;
                        OtherUi[i].SetActive(false);
                        pv.resetText();
                    }
                    else{
                        wasActive[i] = false;
                    }
                }
            }
            //If the menu is open
            else{
                //Re-open all menu items that were originally open
                for(int i = 0;i<OtherUi.Length; i++){
                    if(wasActive[i]){
                        OtherUi[i].SetActive(true);
                    }
                }
            }
            

            menu.SetActive(!menu.activeSelf);
            
            


        } else if(display.action.WasPerformedThisFrame() && !menu.activeSelf){
            menu.SetActive(true);
        } else if(hide.action.WasPerformedThisFrame()){
            menu.SetActive(false);
        }

        
        menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * SpawnDistance;
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
        
    }

    public void quitGame(){
        Application.Quit();
    }
}
