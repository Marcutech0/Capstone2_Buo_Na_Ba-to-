using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
public class CutScene1_2 : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public GameObject _BlackScreenPanel;
    public GameObject _CutSceneImage;
    public TextMeshProUGUI _BlackScreenText;
    public TextMeshProUGUI _NpcName;
    public TextMeshProUGUI _StoryText;
    public string _NextScene;

    [TextArea] public string _Storyline;

    public Fade _FadeTransition;
    [SerializeField] private int _DialogueIndex;
    [SerializeField] private int _CurrentLineIndex;
    [SerializeField] private bool _Continue;
    [SerializeField] private bool _DialogueEnd;
    [SerializeField] private string[] _CurrentLines;
    [SerializeField] private string[] _CurrentSpeakers;

    // Start is called before the first frame update and initializes the dialogue by setting the current lines, speakers, and starting the typing effect for the first line.
    void Start()
    {
        _NpcName.text = "";
        _CurrentLines = new string[] { _Storyline };
        _CurrentSpeakers = new string[] { "" };
        _CurrentLineIndex = 0;

        StartCoroutine(TypeLine(_CurrentLines[_CurrentLineIndex], _CurrentSpeakers[_CurrentLineIndex]));
        _CutSceneImage.SetActive(true);
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
                    _DialoguePanel .SetActive(false);
                    StartCoroutine(BlackScreenSequence());                
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
                new string[] { "You think about what was lost, even though personally you never felt a familial connection to these relatives - you still can’t help but mourn over all the cultural and historical artifacts that were lost." },
                new string[] { "" }
            );
        }

        else if (_DialogueIndex == 2)
        {
            StartDialogueSet(
                new string[] { "I wish there was something I could’ve done to prevent all of this." },
                new string[] { "Aleks" }
            );
        }

        else if (_DialogueIndex == 3)
        {
            StartDialogueSet(
                new string[] { "Eventually after all that mourning you manage to fall asleep." },
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
        _CutSceneImage.SetActive(false);
        _FadeTransition.FadeOut();
        StartCoroutine(CallNextScene());
    }

    // Calls Next scene after a delay of 1 second to allow the fade-out effect to complete before transitioning to the next scene.
    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_NextScene);
    }

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
        _BlackScreenText.text = "";

        yield return new WaitForSeconds(1f);
        _FadeTransition.FadeOut();
        StartCoroutine(CallNextScene());

    }
}
