using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showBackgroundImage : MonoBehaviour
{
 public Image imageToDisplay;

 void OnEnable(){

        imageToDisplay.enabled = true;
        
    }
void onDisable(){
        imageToDisplay.enabled = false;
    }
}
