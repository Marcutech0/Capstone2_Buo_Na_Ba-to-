using UnityEditor.Experimental;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Object_interactable : MonoBehaviour
{
    [SerializeField] public DialogueManager dialogueScript;
    [SerializeField] public bool SceneChecker;
    [SerializeField] public string[] DialogueLines;
    [SerializeField] public string[] DialogueSpeakers;
    [SerializeField] public string[] EmotionIndex;
    [SerializeField] public int[] EmotionCount;
    [SerializeField] public bool isCharacter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharacter)
        {
          

        }
    }

    private void OnMouseDown()
    {
        if (!dialogueScript._DialoguePanel.activeSelf)
        {
            dialogueScript._DialoguePanel.SetActive(true);

            dialogueScript.StartDialogueSet(DialogueLines, DialogueSpeakers, SceneChecker);
        }
     

    }
}
