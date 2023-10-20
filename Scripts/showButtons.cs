using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showButtons : MonoBehaviour
{
    public Button button1;
    public Button button2;
    // Start is called before the first frame update
    void Start()
    {
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);

    }

}
