using UnityEngine;

[System.Serializable]
public struct DialogueEmotionRange
{
    public int StartLineIndex;
    public int EndLineIndex; // inclusive
    public Emotion EmotionType;
}

public class Object_interactable : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] public DialogueManager dialogueScript;
    [SerializeField] public bool SceneChecker;
    [SerializeField] public string[] DialogueLines;
    [SerializeField] public string[] DialogueSpeakers;

    [Header("Character Visibility")]
    [SerializeField] private GameObject characterObject;
    [SerializeField] private int showAtLineIndex = 0;
    [SerializeField] private int hideAtLineIndex = -1;

    [Header("Emotions (Range Based)")]
    [SerializeField] private EmotionController emotionController;
    [SerializeField] private DialogueEmotionRange[] emotionRanges;

    private int lastCheckedLineIndex = -1;
    private bool dialogueRunning = false;

    void Update()
    {
        if (!dialogueRunning) return;

        if (dialogueScript._DialoguePanel.activeSelf)
        {
            int currentIndex = dialogueScript._CurrentLineIndex;

            if (currentIndex != lastCheckedLineIndex)
            {
                lastCheckedLineIndex = currentIndex;

                HandleCharacterVisibility(currentIndex);
                HandleEmotionRange(currentIndex);
            }
        }
        else
        {
            // Dialogue ended
            dialogueRunning = false;

            // Reset to neutral when dialogue closes
            if (emotionController != null)
                emotionController.SetEmotion(Emotion.Neutral);
        }
    }

    private void OnMouseDown()
    {
        if (!dialogueScript._DialoguePanel.activeSelf)
        {
            dialogueScript._DialoguePanel.SetActive(true);

            lastCheckedLineIndex = -1;
            dialogueRunning = true;

            dialogueScript.StartDialogueSet(
                DialogueLines,
                DialogueSpeakers,
                SceneChecker
            );

            // FORCE neutral at start
            if (emotionController != null)
                emotionController.SetEmotion(Emotion.Neutral);
        }
    }

    private void HandleCharacterVisibility(int lineIndex)
    {
        if (characterObject == null) return;

        if (lineIndex == showAtLineIndex)
            characterObject.SetActive(true);

        if (hideAtLineIndex >= 0 && lineIndex == hideAtLineIndex)
            characterObject.SetActive(false);
    }

    private void HandleEmotionRange(int lineIndex)
    {
        if (emotionController == null) return;

        Emotion selectedEmotion = Emotion.Neutral;
        bool foundMatch = false;

        foreach (var range in emotionRanges)
        {
            if (lineIndex >= range.StartLineIndex &&
                lineIndex <= range.EndLineIndex)
            {
                selectedEmotion = range.EmotionType;
                foundMatch = true;
                break;
            }
        }

        // Always explicitly set emotion
        emotionController.SetEmotion(selectedEmotion);
        Debug.Log("Current Line: " + lineIndex + " Emotion: " + selectedEmotion);
    }
}