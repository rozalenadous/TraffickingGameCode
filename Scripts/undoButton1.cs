using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class undoButton1 : MonoBehaviour
{
    public Button thisButton;
    public Button otherButton;
    //public bool newTimeline;

    public void onButtonClick(){
 
        Destroy(thisButton.gameObject);
        Destroy(otherButton.gameObject);
        // if (newTimeline){
            
        // }
    }
}
