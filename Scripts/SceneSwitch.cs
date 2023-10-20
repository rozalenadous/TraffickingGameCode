using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int trustVal = Trust.getTrust();
        Debug.Log("The trust is " + trustVal);
        if(trustVal > 80){
            SceneManager.LoadScene("MeetJack", LoadSceneMode.Single);
        } else if(trustVal > 65){
            SceneManager.LoadScene("Scene3", LoadSceneMode.Single);
        }else{
            SceneManager.LoadScene("classmateScene", LoadSceneMode.Single);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
