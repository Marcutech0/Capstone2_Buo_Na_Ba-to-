using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    public string _NextScene;

    [TextArea] public string _Storyline;

    public Fade _FadeTransition;
    [SerializeField] private int _DialogueIndex;
    [SerializeField] private bool _Continue;
    [SerializeField] private string[] _CurrentLines;
    [SerializeField] private string[] _CurrentSpeakers;
    [SerializeField] public int _CurrentLineIndex;
    [SerializeField] private bool _DialogueEnd;
    [SerializeField] private GameObject[] _Items;
    [SerializeField] private bool _IsTyping;
    private Coroutine _TypingCoroutine;
    private bool scenechecker;
    // Public Methods
    public void StartDialogueSet(string[] _Lines, string[] _Speakers, bool SceneChecker)
    {
        // Stop any ongoing typing coroutine before starting a new dialogue set
        if (_TypingCoroutine != null)
        {
            StopCoroutine(_TypingCoroutine);
        }

        _StoryText.text = "";
        _CurrentLines = _Lines;
        _CurrentSpeakers = _Speakers;
        _CurrentLineIndex = 0;
        scenechecker = SceneChecker;

        _TypingCoroutine = StartCoroutine(TypeLine(_CurrentLines[_CurrentLineIndex], _CurrentSpeakers[_CurrentLineIndex]));
    }


    public void Update()
    {
        // Check for mouse click to continue dialogue only if the dialogue panel is active
        if (_DialoguePanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            ContinueDialogue();
        }
    }

    // Ends the dialogue by deactivating the dialogue panel
    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);
        if (scenechecker)
        {
            NextScene();
        }
    }

    // Initiates the scene transition by fading out and then loading the next scene after a short delay
    public void NextScene()
    {
        _FadeTransition.FadeOut();
        StartCoroutine(CallNextScene());
    }

    public void ContinueDialogue()
    {
        // If currently typing, stop the coroutine and display the full line immediately
        if (_IsTyping)
        {
            StopCoroutine(_TypingCoroutine);
            _StoryText.text = _CurrentLines[_CurrentLineIndex];
            _IsTyping = false;
            _Continue = true;
            return;
        }

        // If not typing, proceed to the next line or end dialogue if there are no more lines
        if (_Continue)
        {
            _CurrentLineIndex++;
            if (_CurrentLineIndex < _CurrentLines.Length)
            {
                _TypingCoroutine = StartCoroutine(TypeLine(_CurrentLines[_CurrentLineIndex], _CurrentSpeakers[_CurrentLineIndex]));
            }
            else
            {
                EndDialogue();       
            }
        }
    }


    //Coroutines

    // Scene Transition Coroutine
    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_NextScene);
    }

    // Dialogue Typing Effect Coroutine
    IEnumerator TypeLine(string _Line, string _Speaker)
    {
        _IsTyping = true;
        _Continue = false;

        _StoryText.text = "";
        _NpcName.text = _Speaker;

        foreach (char c in _Line)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        _IsTyping = false;
        _Continue = true;
    }
}