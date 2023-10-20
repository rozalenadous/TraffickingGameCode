using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class nextScene1 : MonoBehaviour
{
    public Button thisButton;
    public Button otherButton;
    public PlayableDirector timeline;
    public GameObject timelineGameObject;

    public void onButtonClick(){
 
        Destroy(thisButton.gameObject);
        Destroy(otherButton.gameObject);
        timelineGameObject.SetActive(true);
    
        timeline.Play();
    }
}
