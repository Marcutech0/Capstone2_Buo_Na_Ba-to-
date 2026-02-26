using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;
public class PrologueScenee : MonoBehaviour
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
    [SerializeField] private string [] _CurrentLines;
    [SerializeField] private string [] _CurrentSpeakers;
    [SerializeField] private int _CurrentLineIndex;
    [SerializeField] private bool _DialogueEnd;

    public GameObject[] _Pages;

    // Start is called before the first frame update and initializes the dialogue by setting the current lines, speakers, and starting the typing effect for the first line.
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

                else if (_DialogueIndex <= 7) 
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
                new string[] { "Built in the year 1861, the house was known for being constructed around the same time San Agustin Church was built, one of Malabon’s notable monuments, and the same year our national hero, Jose Rizal was born." },
                new string[] { "" }
            );
            _Pages[0].SetActive(true);
        }

        else if (_DialogueIndex == 2) 
        {
            StartDialogueSet(
                new string[] { "The house was built by Don Mariano Navales, a wealthy landowner and businessman in Malabon. It was originally used as a residence for the Navales family, but it also served as a social hub for the local community." },
                new string[] { "" }
            );
             _Pages[0].SetActive(false);
        }

        else if (_DialogueIndex == 3) 
        {
            StartDialogueSet(
                new string[] { "The house was home to Joe Raymundo with his wife Tina and their eight kids who all grew up adapting values and morals taught to them by their parents and their ‘Nanang’ in the Raymundo house." },
                new string[] { "" }
            );
             
        }
        else if (_DialogueIndex == 4) 
        {
           StartDialogueSet(
                new string[] { "Each member of the Raymundo family had their own stories created within the walls of the Raymundo home." },
                new string[] { "" }
           );
        }

        else if (_DialogueIndex == 5) 
        {
            StartDialogueSet(
                new string[] { "This house has been a significant part of Malabon’s heritage. It stands as a monument of history and cultural identity amongst the Malubenos." },
                new string[] { "" }
            );
            _Pages[1].SetActive(true);
        }

        else if (_DialogueIndex == 6) 
        {
           StartDialogueSet(
                new string[] { "The Raymundo house was always a haven and a place of friendship, where daily needs were met even in the most difficult circumstances by believers who knew the Lord would provide." },
                new string[] { "" }
           );
             _Pages[1].SetActive(false);
        }
        else if (_DialogueIndex == 7) 
        {
           StartDialogueSet(
                new string[] { "Unfortunately-" },
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
