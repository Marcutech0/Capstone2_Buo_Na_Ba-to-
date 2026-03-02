using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[System.Serializable]
public struct DialogueEmotionRange
{
    public int StartLineIndex;
    public int EndLineIndex;
    public Emotion EmotionType;
}

[System.Serializable]
public struct CharacterDialogueData
{
    public GameObject characterObject;
    public EmotionController emotionController;
    public int showAtLineIndex;
    public int hideAtLineIndex;
    public DialogueEmotionRange[] emotionRanges;
}

public class Object_interactable : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] public DialogueManager dialogueScript;
    [SerializeField] public bool SceneChecker;
    [SerializeField] public string[] DialogueLines;
    [SerializeField] public string[] DialogueSpeakers;

    [Header("Player Movement")]
    [SerializeField] private NavMeshAgent playerAgent;
    [SerializeField] private float interactionDistance = 1f;

    [Header("Multiple Characters")]
    [SerializeField] private CharacterDialogueData[] characters;

    private int lastCheckedLineIndex = -1;
    private bool dialogueRunning = false;
    private bool isInteracting = false;

    void Update()
    {
        if (!dialogueRunning) return;

        if (dialogueScript._DialoguePanel.activeSelf)
        {
            int currentIndex = dialogueScript._CurrentLineIndex;

            if (currentIndex != lastCheckedLineIndex)
            {
                lastCheckedLineIndex = currentIndex;
                HandleCharacters(currentIndex);
            }
        }
        else
        {
            dialogueRunning = false;

            foreach (var character in characters)
            {
                if (character.emotionController != null)
                    character.emotionController.SetEmotion(Emotion.Neutral);
            }
        }
    }

    private void OnMouseDown()
    {
        if (!dialogueScript._DialoguePanel.activeSelf && !isInteracting)
        {
            StartCoroutine(MoveAndStartDialogue());
        }
    }

    private IEnumerator MoveAndStartDialogue()
    {
        isInteracting = true;

        playerAgent.SetDestination(transform.position);

        while (Vector3.Distance(playerAgent.transform.position, transform.position) > interactionDistance)
        {
            yield return null;
        }

        playerAgent.ResetPath();

        dialogueScript._DialoguePanel.SetActive(true);
        dialogueRunning = true;
        lastCheckedLineIndex = -1;

        dialogueScript.StartDialogueSet(
            DialogueLines,
            DialogueSpeakers,
            SceneChecker,
            new int[] { -1 }
        );

        foreach (var character in characters)
        {
            if (character.emotionController != null)
                character.emotionController.SetEmotion(Emotion.Neutral);
        }

        isInteracting = false;
    }

    private void HandleCharacters(int lineIndex)
    {
        foreach (var character in characters)
        {
            if (character.characterObject != null)
            {
                if (lineIndex == character.showAtLineIndex)
                    character.characterObject.SetActive(true);

                if (character.hideAtLineIndex >= 0 &&
                    lineIndex == character.hideAtLineIndex)
                    character.characterObject.SetActive(false);
            }

            if (character.emotionController != null)
            {
                Emotion selectedEmotion = Emotion.Neutral;

                foreach (var range in character.emotionRanges)
                {
                    if (lineIndex >= range.StartLineIndex &&
                        lineIndex <= range.EndLineIndex)
                    {
                        selectedEmotion = range.EmotionType;
                        break;
                    }
                }

                character.emotionController.SetEmotion(selectedEmotion);
            }
        }
    }
}