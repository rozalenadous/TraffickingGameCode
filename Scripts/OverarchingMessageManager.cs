using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OverarchingMessageManager : MonoBehaviour
{
    [Header("Sending Messages")]
    [Space(5)]
    [SerializeField]
    private GameObject yourMessagePrefab;
    [SerializeField]
    private GameObject otherMessagePrefab;
    [SerializeField]
    public TextMeshProUGUI nameField;
    [SerializeField]
    public TextMeshProUGUI dialogueField;

    [Tooltip("See notes below")]
    [SerializeField]
    public bool editsDialogue;

    [Tooltip("Notes for editsDialogue field")]
    [TextArea]
    public string Notes = "The editsDialogue field should be checked if the messages between players will both be displayed in the bottom bar. If the messages are displayed in text bubbles, for example, this field should remain unchecked.";

    [Space(20)]

    [Header("Choices")]
    [Space(5)]
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

    [Space(20)]
    [Header("Music & Animations")]
    [Space(5)]

    [SerializeField]
    public AudioSource audioSource;
    [SerializeField]
    public AudioClip loopClip;
    [SerializeField]
    private Animator character1Animator; // plays animation first
    [SerializeField]
    private Animator character2Animator;
     [SerializeField]
    private Animator character3Animator; //optional animator for Jack

    [Space(20)]
    [Header("Other")]
    [Space(5)]
    [SerializeField]
    public TextAsset jsonFile;
    [SerializeField]
    public string nextScene;
    [SerializeField]
    public GameObject runAfterScene;
    public MessagesData messages;
    [SerializeField]
    public bool waitTime;
    public Toggle toggle;
    private double choiceTime;
    // Start the coroutine
    private void Start()
    {
        messages = JsonUtility.FromJson<MessagesData>(jsonFile.text);
        StartCoroutine(ExampleCoroutine());
        yourMessagePrefab  = Resources.Load<GameObject>("YourMessage");
        otherMessagePrefab  = Resources.Load<GameObject>("OtherMessage");
        PlayMusic();
    }

    // Add incoming message when using prefabs
    private void SendOtherMessage(string message)
    {
        GameObject otherMessage = Instantiate(otherMessagePrefab, transform);
        otherMessage.GetComponent<Message>().SetMessage(message);
    }

    // Add outgoing message when using prefabs
    private void SendMyMessage(string message)
    {
        GameObject myMessage = Instantiate(yourMessagePrefab, transform);
        myMessage.GetComponent<Message>().SetMessage(message);
    }

    // Add name and dialogue when NOT using prefabs
    private void changeDialogue(string name, string dialogue){
        nameField.text = name;
        dialogueField.text = dialogue;
      
    }

    // Main function
    private IEnumerator ExampleCoroutine()
    {
       
      MessageData currentMessage = messages.messages[0];
      

        while (true)
        {
         
          // Update the trust value based on message choice
          Trust.changeTrust(currentMessage.impact);

          /* 
            Determines whether to change the bottom drawer with
            each message or add message prefabs in the scroll container
          */
          if(editsDialogue){
            if(currentMessage.sender == "System") { //"system"
              nameField.text = "";
              dialogueField.text = "<align=center><b>" + currentMessage.message + "</b></align>";
    
              Debug.Log("Bolded");
              }
            else
           { changeDialogue(currentMessage.sender, currentMessage.message);}
          } else {
            if(currentMessage.sender == "You"){
              Debug.Log("player speaking " + currentMessage.message);
              SendMyMessage(currentMessage.message);
            } else if(currentMessage.sender == "System" || currentMessage.sender == "Systems" ) {
              Debug.Log("system speaking");
              // update bottom drawer
              dialogueField.text = "<align=center><b>" + currentMessage.message + "</b></align>";
    
            } else  {
              Debug.Log("other message " + currentMessage.sender);
              SendOtherMessage(currentMessage.message);
            }
          }
          if (character1Animator != null && !character1Animator.enabled)
          { character1Animator.enabled = true;
          }
          // Wait between each message
          if ((waitTime && toggle.isOn) || !waitTime && !toggle.isOn){
            Debug.Log("toggle on fr");
          yield return new WaitForSeconds(6f);
          }
          else if (waitTime){
          yield return new WaitForSeconds(9f);
          } 
          else if (!waitTime && toggle.isOn){
            yield return new WaitForSeconds(4f);
          } else{
            yield return new WaitForSeconds(6f);
          }
           if (character2Animator != null && !character2Animator.enabled)
          { character2Animator.enabled = true;
          }
          int len = currentMessage.next.Length;
          Debug.Log("Msg: " + currentMessage.message + ", Threshold: " + currentMessage.threshold);
          if(len == 0) {
            if(runAfterScene){
              runAfterScene.SetActive(true); 
            } else{
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);

            }
            
          } else if(len == 1){
            currentMessage = FindMessage(currentMessage.next[0]);
          } else if(currentMessage.threshold > 0){
            int idx = Trust.getTrust() >= currentMessage.threshold ? 1 : 0;
            currentMessage = FindMessage(currentMessage.next[idx]);
          } else{
            changeDialogue("", "");
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
         
            Telemetry.Send("1-msg-" + currentMessage.id.ToString(), FindMessage(currentMessage.next[input]).message + " - " + Trust.getTrust(), Math.Round(choiceTime, 2));
            currentMessage = FindMessage(currentMessage.next[input]);
          }

          dialogueField.text = "";
          
           if (character3Animator != null && !character3Animator.enabled)
          { character3Animator.enabled = true;
          }
        }
        
    }

    void PlayMusic()
    {
        if(!audioSource || !loopClip) return;
        audioSource.clip = loopClip;

        audioSource.loop = true;

        audioSource.Play();
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
      choiceTime = 0;
      while (buttons.currentOption == -1)
        {choiceTime += 0.01;
          yield return new WaitForSeconds(0.01f);}
    }



}

[System.Serializable]
public class MessagesData {
  public MessageData[] messages;
}

[System.Serializable]
public class MessageData {
  public int id;
  public string sender;
  public string message;
  public int[] next;
  public int threshold;
  public int impact;
}
