using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DialogueManager : MonoBehaviour
{
    public GameObject _DialoguePanel;
    public GameObject _BlackScreenPanel;
    public GameObject _CutSceneImage;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    public TextMeshProUGUI _BlackScreenText;
    public string[] _CurrentLines;
    public string[] _CurrentSpeakers;
    public string _NextScene;
    public int _CurrentLineIndex;
    public int _BlackScreenIndex;
    public Fade _FadeTransition;
    public bool _Continue;

    void Start()
    { // Initialize the dialogue system and start the first dialogue sequence
        _CutSceneImage.SetActive(true);
        _CurrentLineIndex = 0;
        _Continue = false;

        if (_CurrentLines.Length > 0)
        {
            StartCoroutine(_TypeLine(
                _CurrentLines[_CurrentLineIndex],
                _CurrentSpeakers[_CurrentLineIndex]
            ));

            _CurrentLineIndex++;
        }
    }

    void Update()
    { // Listen for player input to continue the dialogue
        if (Input.GetKeyDown(KeyCode.Mouse0) && _Continue)
        {
            _Continue = false;

            // Trigger the black screen sequence based on the specified index
            if (_CurrentLineIndex == _BlackScreenIndex) 
            {
                StartCoroutine(BlackScreenSequence());
                return;
            }


            // If we've reached the end of the dialogue lines, end the dialogue
            if (_CurrentLineIndex >= _CurrentLines.Length)
            {
                _EndDialogue();
                return;
            }

            // Otherwise, type the next line
            StartCoroutine(_TypeLine(
                _CurrentLines[_CurrentLineIndex],
                _CurrentSpeakers[_CurrentLineIndex]
            ));
            _CurrentLineIndex++;
        }
    }

    // Type out the dialogue line by line
    IEnumerator _TypeLine(string _Line, string _Speaker)
    {
        _StoryText.text = "";
        _NpcName.text = _Speaker;
        foreach (char c in _Line)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _Continue = true;
    }

    // Start a new dialogue set with given lines and speakers (For example, when the player makes a choice, for Choice Manager)
    void StartDialogueSet(string[] _Lines, string[] _Speakers)
    {
        _CurrentLines = _Lines;
        _CurrentSpeakers = _Speakers;
        _CurrentLineIndex = 0;

        StartCoroutine(_TypeLine(_CurrentLines[_CurrentLineIndex], _CurrentSpeakers[_CurrentLineIndex]));
    }

    // End the dialogue and transition to the next scene
    public void _EndDialogue()
    {
        _DialoguePanel.SetActive(false);
        _FadeTransition.FadeOut();
        StartCoroutine(CallNextScene());
    }

    // Coroutine to load the next scene after a short delay
    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_NextScene);
    }

    // Coroutine to handle the black screen sequence with text updates and transitions
    IEnumerator BlackScreenSequence() 
    {
        _CutSceneImage.SetActive(false);
        _Continue = false;
        _FadeTransition.FadeOut();
        yield return new WaitForSeconds(1f);

        _DialoguePanel.SetActive(false);
        _BlackScreenPanel.SetActive(true);

        _BlackScreenText.text = "8 Hours Later";
        yield return new WaitForSeconds(2f);

        _BlackScreenText.text = "Knock Knock";

        yield return new WaitForSeconds(1f);

        _FadeTransition.FadeIn();
        _BlackScreenPanel.SetActive(false);
        _DialoguePanel.SetActive(false);

        yield return new WaitForSeconds(1f);
        _FadeTransition.FadeOut();
        StartCoroutine(CallNextScene());

    }
}
