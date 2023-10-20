using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Scene2MessageManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI nameField;
    [SerializeField]
    public TextMeshProUGUI dialogueField;
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
    public string nextScene;
    [SerializeField]
    public AudioSource audioSource;
    [SerializeField]
    public AudioClip loopClip;
    [SerializeField]
    public Animator animator;

    // [SerializeField]
    // public Slider trustVal;

    public MessagesData messages;

    private void Start()
    {
      //animator = GetComponent<Animator>();

        // Start the animation loop
       // animator.Play("Sitting Talking");
    
        StartDialogue();
        messages = JsonUtility.FromJson<MessagesData>(jsonFile.text);
        StartCoroutine(ExampleCoroutine());

    }

    private void changeMessage(string name, string dialogue){
      nameField.text = name;
      dialogueField.text = dialogue;
    }

    private IEnumerator ExampleCoroutine()
    {
      MessageData currentMessage = messages.messages[0];
      
      // if obj.next.length > 1

        while (true)
        {
          changeMessage(currentMessage.sender, currentMessage.message);
          Trust.changeTrust(currentMessage.impact);
          // trustVal.value = Trust.getTrust() / 100f;

          yield return new WaitForSeconds(6f);

          int len = currentMessage.next.Length;
          if(len == 0){
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
          }
          else if(len == 1){
            currentMessage = FindMessage(currentMessage.next[0]);
          } else{
            changeMessage("", "");
            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            text1.text = FindMessage(currentMessage.next[0]).message;
            text2.text = FindMessage(currentMessage.next[1]).message;

            yield return StartCoroutine(getInput());
            int input = buttons.currentOption;
            buttons.currentOption = -1;
            button1.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            
            currentMessage = FindMessage(currentMessage.next[input]);
          }
          
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


  void StartDialogue()
    {
        audioSource.clip = loopClip;

        // Set the AudioSource to loop
        audioSource.loop = true;

        // Play the audio clip
        audioSource.Play();
    }

    //  void LoopAnimation(string animationName)
    // {
    //     // Make sure the animation exists in the Animator Controller
    //     if (animator != null)
    //     {
    //         animator.Play(animationName, 0, 0f);
    //         animator.speed = 1f; // Set animation speed if needed
    //     }
    //     else
    //     {
    //         Debug.LogError("Animator component not found.");
    //     }
    // }
}