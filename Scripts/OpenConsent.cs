using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenConsent : MonoBehaviour
{
    public void open(){
      Application.OpenURL("https://forms.gle/odYfJZx5RD6eHV948");
    }
}
