using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(LayoutElementFitChild))]
public class Message : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    private string message;

    public void SetMessage(string newMessage)
    {
        message = newMessage;
        text.text = message;
    }
}
