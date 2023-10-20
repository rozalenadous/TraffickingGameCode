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

public class PlayGame : MonoBehaviour
{
    public void play(){
SceneManager.LoadScene("Scene1", LoadSceneMode.Single);
    }

}