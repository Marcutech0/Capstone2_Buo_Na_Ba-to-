using UnityEngine;
using UnityEngine.AI;

public class Exploration1_1 : MonoBehaviour
{
    [SerializeField] public DialogueManager dialogueScript;
    [SerializeField] public PlayerMovement playerMovementScript;
    [SerializeField] public string[] DialogueLines;
    [SerializeField] public string[] DialogueSpeakers;
    [SerializeField] public int[] restrictionPoints;
    [SerializeField] public bool hasInteractedArtifact1 = false;
    [SerializeField] public bool hasInteractedArtifact2 = false;
    [SerializeField] public bool hasInteractedArtifact3 = false;
    [SerializeField] public string[] DialogueLines2;
    [SerializeField] public string[] DialogueSpeakers2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueScript.StartDialogueSet(DialogueLines, DialogueSpeakers, false, restrictionPoints);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasInteractedArtifact1 && hasInteractedArtifact2 && hasInteractedArtifact3)
        {
            hasInteractedArtifact1 = false;
            hasInteractedArtifact2 = false;
            hasInteractedArtifact3 = false;
            //playerMovementScript.LockMovement();
            dialogueScript.StartDialogueSet(DialogueLines2, DialogueSpeakers2, true, restrictionPoints);

        }
    }
}
