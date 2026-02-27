using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Cuaritito_Int : MonoBehaviour
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
    [SerializeField] private int _CurrentLineIndex;
    [SerializeField] private bool _DialogueEnd;
    [SerializeField] private GameObject[] _Items;
    void Start()
    {
        _NpcName.text = "";
        _CurrentLines = new string[] { _Storyline };
        _CurrentSpeakers = new string[] { "" };
        _CurrentLineIndex = 0;

        StartCoroutine(TypeLine(_CurrentLines[_CurrentLineIndex], _CurrentSpeakers[_CurrentLineIndex]));
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

                else if (_DialogueIndex <= 5)
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
                new string[] { "Well, that's what it used to be… Now it's just muddied, destroyed, and worn down." },
                new string[] { "" }
            );
        }

        else if (_DialogueIndex == 2)
        {
            StartDialogueSet(
                new string[] { "Hearing the news about Malabon’s precious heritage zone being severely flooded the moment I woke up really put me on edge. Seeing what was left made my heart sink." },
                new string[] { "" }
            );
        }

        else if (_DialogueIndex == 3)
        {
            StartDialogueSet(
                new string[] { "I decided to become a historian so I can study the conservation of international history, but the Raymundo house… I left it as is despite my parents saying it had belonged to one of our distant relatives." },
                new string[] { "" }
            );

        }
        else if (_DialogueIndex == 4)
        {
            StartDialogueSet(
                 new string[] { "And right now I’m standing inside the living room, seeing it destroyed alongside the other remaining heritage houses. I couldn't help but feel like I had failed my mission as the house’s caretaker and historian, even though the flooding wasn’t my fault." },
                 new string[] { "" }
            );
        }

        else if (_DialogueIndex == 5)
        {
            StartDialogueSet(
                new string[] { "There’s no use moping what’s done is done. I might as well find anything left that is worth salvaging in this mess.." },
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

    // This method is called when the dialogue ends. It hides the dialogue panel
    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_NextScene);
    }
}
