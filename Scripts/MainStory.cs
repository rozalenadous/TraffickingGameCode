using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using System;

// class CustomException : Exception
// {
//     public CustomException(string message)
//     {
//     }
// }

public class MainStory : MonoBehaviour
{
    
    void OnEnable()
    {

        // throw new CustomException("eee");

        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

 
}
