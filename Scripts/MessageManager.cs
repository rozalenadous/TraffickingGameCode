using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MessageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject yourMessagePrefab;
    [SerializeField]
    private GameObject otherMessagePrefab;
    [SerializeField]
    public TextAsset jsonFile;
    [SerializeField]
    public Buttons buttons;
    [SerializeField]
    public Button button1;
    [SerializeField]
    public Button button2;
    [SerializeField]
    public TextMeshProUGUI text1;
    [SerializeField]
    public TextMeshProUGUI text2;
    [SerializeField]
    public TextMeshProUGUI dialogueField;

    public MessagesData messages;

    private void Start()
    {
        messages = JsonUtility.FromJson<MessagesData>(jsonFile.text);
        StartCoroutine(ExampleCoroutine());

    }

    private void SendOtherMessage(string message)
    {
        GameObject otherMessage = Instantiate(otherMessagePrefab, transform);
        otherMessage.GetComponent<Message>().SetMessage(message);
    }

    private void SendMyMessage(string message)
    {
        GameObject myMessage = Instantiate(yourMessagePrefab, transform);
        myMessage.GetComponent<Message>().SetMessage(message);
    }

    private IEnumerator ExampleCoroutine()
    {
      MessageData currentMessage = messages.messages[0];
      
      // if obj.next.length > 1

        while (true)
        {
          if(currentMessage.sender == "You"){
            SendMyMessage(currentMessage.message);
          } else if(currentMessage.sender == "Sys") {
            // update text field
            dialogueField.text = currentMessage.message;
          } else  {
            SendOtherMessage(currentMessage.message);
          }

          // yield return new WaitForSeconds(0.1f);
          yield return new WaitForSeconds(4f);

          int len = currentMessage.next.Length;
          if(len == 0) {
            SceneManager.LoadScene("Scene2", LoadSceneMode.Single);
          } else if(len == 1){
            currentMessage = FindMessage(currentMessage.next[0]);
          } else{
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            text1.text = FindMessage(currentMessage.next[0]).message;
            text2.text = FindMessage(currentMessage.next[1]).message;

            yield return StartCoroutine(getInput());
            int input = buttons.currentOption;
            buttons.currentOption = -1;
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);

            Trust.changeTrust(FindMessage(currentMessage.next[input]).impact);
            double choiceTime = 2.5;
            Telemetry.Send("1-msg-" + currentMessage.id.ToString(), FindMessage(currentMessage.next[input]).message + " - " + Trust.getTrust(), choiceTime);
            currentMessage = FindMessage(currentMessage.next[input]);
          }

          dialogueField.text = "";

          
        }
        
    }

    public MessageData FindMessage(int id){
      foreach (MessageData msg in messages.messages)
        {
          if(msg.id == id) return msg;
        }
      return null;

    }

     // inside manager
    private IEnumerator getInput() {
      // spin lock until the user has entered something
      while (buttons.currentOption == -1)
        yield return new WaitForSeconds(0.01f);
    }


}