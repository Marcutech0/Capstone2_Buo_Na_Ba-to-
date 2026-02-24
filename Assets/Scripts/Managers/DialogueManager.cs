using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;
public class DialogueManager : MonoBehaviour
{
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    public string[] _CurrentLines;
    public string[] _CurrentSpeakers;
    public string _NextScene;
    public int _CurrentLineIndex;
    public Fade _FadeTransition;
    public bool _Continue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the dialogue with the first set of lines and speakers
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _Continue)
        {
            _Continue = false;

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
}
