using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class CutScene1_3 : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    public string _NextScene;
    public GameObject _CutsceneImage;

    [TextArea] public string _Storyline;

    public Fade _FadeTransition;
    [SerializeField] private int _DialogueIndex;
    [SerializeField] private bool _Continue;
    [SerializeField] private string[] _CurrentLines;
    [SerializeField] private string[] _CurrentSpeakers;
    [SerializeField] private int _CurrentLineIndex;
    [SerializeField] private bool _DialogueEnd;

    // Start is called before the first frame update and initializes the dialogue by setting the current lines, speakers, and starting the typing effect for the first line.
    void Start()
    {
        _NpcName.text = "";
        _CurrentLines = new string[] { _Storyline };
        _CurrentSpeakers = new string[] { "" };
        _CurrentLineIndex = 0;

        StartCoroutine(TypeLine(_CurrentLines[_CurrentLineIndex], _CurrentSpeakers[_CurrentLineIndex]));
        _CutsceneImage.SetActive(true);
    }

    // Update is called once per frame and checks for user input to continue the dialogue. If the player clicks the mouse button, it advances to the next line of dialogue or ends the dialogue if there are no more lines to display.
    void Update()
    {
        if (_Continue && Input.GetKeyDown(KeyCode.Mouse0))
        {
            _Continue = false;
            _CurrentLineIndex++;

            if (_CurrentLineIndex < _CurrentLines.Length)
            {
                StartCoroutine(TypeLine(_CurrentLines[_CurrentLineIndex], _CurrentSpeakers[_CurrentLineIndex]));
            }
            else
            {
                if (_DialogueEnd)
                {
                    _DialogueEnd = false;
                    EndDialogue();
                }

                else if (_DialogueIndex <= 3)
                {
                    _ContinueMainDialogue();
                }
            }
        }
    }

    // This method continues the main dialogue by incrementing the dialogue index and setting the appropriate lines and speakers for each stage of the dialogue. It also manages the visibility of certain pages based on the dialogue index.
    public void _ContinueMainDialogue()
    {
        _DialogueIndex++;

        if (_DialogueIndex == 1)
        {
            StartDialogueSet(
                new string[] { "Sino nandiyan?!" },
                new string[] { "Aleks" }
            );
        }

        else if (_DialogueIndex == 2)
        {
            StartDialogueSet(
                new string[] { "I shouted in fear but soon realized that I wasn’t in my usual bed. Taking quick glances around to realize this really isn’t my room, let alone my house! The place looks older…much older to be exact." },
                new string[] { "" }
            );
            Debug.Log("Knock Knock SFX");
        }

        else if (_DialogueIndex == 3)
        {
            StartDialogueSet(
                new string[] { "The knocking continues. It doesn’t seem like whoever is behind the door was a threat, otherwise they would’ve barged in already, so I might as well answer the door. After all, there was nothing else to do." },
                new string[] { "" }
            );
            _DialogueEnd = true;
        }
    }

    // This method initializes the dialogue by setting the current lines, speakers, and starting the typing effect for the first line.
    void StartDialogueSet(string[] _Lines, string[] _Speakers)
    {
        _CurrentLines = _Lines;
        _CurrentSpeakers = _Speakers;
        _CurrentLineIndex = 0;

        StartCoroutine(TypeLine(_CurrentLines[_CurrentLineIndex], _CurrentSpeakers[_CurrentLineIndex]));
    }

    // This coroutine handles the typing effect for the dialogue. It takes a line of text and a speaker's name, and gradually displays the text character by character with a short delay between each character.
    IEnumerator TypeLine(string _Line, string _Speaker)
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

    // This method is called when the dialogue ends. It hides the dialogue panel, triggers a fade-out effect, and starts a coroutine to load the next scene after a short delay.
    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);
        _CutsceneImage.SetActive(false);
        _FadeTransition.FadeOut();
        StartCoroutine(CallNextScene());
    }

    // Calls Next scene after a delay of 1 second to allow the fade-out effect to complete before transitioning to the next scene.
    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_NextScene);
    }
}
