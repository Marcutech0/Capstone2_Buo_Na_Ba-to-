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

    [Header("Player")]
    public PlayerMovement _PlayerMovement;
    public SpriteRenderer _PlayerSprite;

    public Fade _FadeTransition;

    [SerializeField] private string[] _CurrentLines;
    [SerializeField] private string[] _CurrentSpeakers;
    [SerializeField] public int _CurrentLineIndex;

    private bool _Continue;
    private bool _IsTyping;
    private Coroutine _TypingCoroutine;
    private bool scenechecker;

    private int[] _RestrictionIndices;
    private bool _PlayerRestricted;          // Index restriction
    private bool _DialogueMovementLock;      // Dialogue lock

    public bool IsDialogueRunning { get; private set; }

    // =========================================================
    public void StartDialogueSet(
        string[] _Lines,
        string[] _Speakers,
        bool SceneChecker,
        int[] RestrictionIndices
    )
    {
        IsDialogueRunning = true;

        if (_TypingCoroutine != null)
            StopCoroutine(_TypingCoroutine);

        _DialoguePanel.SetActive(true);

        _CurrentLines = _Lines;
        _CurrentSpeakers = _Speakers;
        _CurrentLineIndex = 0;
        scenechecker = SceneChecker;

        _RestrictionIndices = RestrictionIndices;
        _PlayerRestricted = false;

        // 🔒 Dialogue always locks movement
        _DialogueMovementLock = true;
        _PlayerMovement?.LockMovement();

        CheckRestriction();

        _TypingCoroutine = StartCoroutine(
            TypeLine(_CurrentLines[_CurrentLineIndex],
            _CurrentSpeakers[_CurrentLineIndex])
        );
    }

    // =========================================================
    void Update()
    {
        if (_DialoguePanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            ContinueDialogue();
        }
    }

    // =========================================================
    public void ContinueDialogue()
    {
        if (_IsTyping)
        {
            StopCoroutine(_TypingCoroutine);
            _StoryText.text = _CurrentLines[_CurrentLineIndex];
            _IsTyping = false;
            _Continue = true;
            return;
        }

        if (_Continue)
        {
            _CurrentLineIndex++;

            if (_CurrentLineIndex < _CurrentLines.Length)
            {
                CheckRestriction();

                _TypingCoroutine = StartCoroutine(
                    TypeLine(_CurrentLines[_CurrentLineIndex],
                    _CurrentSpeakers[_CurrentLineIndex])
                );
            }
            else
            {
                EndDialogue();
            }
        }
    }

    // =========================================================
    void CheckRestriction()
    {
        if (_RestrictionIndices == null || _RestrictionIndices.Length == 0)
            return;

        foreach (int index in _RestrictionIndices)
        {
            if (_CurrentLineIndex == index)
            {
                TogglePlayerRestriction();
            }
        }
    }

    public void TogglePlayerRestriction()
    {
        _PlayerRestricted = !_PlayerRestricted;

        // Sprite controlled ONLY by index system
        if (_PlayerSprite != null)
            _PlayerSprite.enabled = !_PlayerRestricted;

        // Movement respects BOTH systems
        if (_PlayerMovement != null)
        {
            if (_PlayerRestricted || _DialogueMovementLock)
                _PlayerMovement.LockMovement();
            else
                _PlayerMovement.UnlockMovement();
        }
    }

    // =========================================================
    public void EndDialogue()
    {
        _DialoguePanel.SetActive(false);

        IsDialogueRunning = false;
        _DialogueMovementLock = false;

        if (_PlayerMovement != null)
        {
            if (_PlayerRestricted)
                _PlayerMovement.LockMovement();
            else
                _PlayerMovement.UnlockMovement();
        }

        if (_PlayerSprite != null)
            _PlayerSprite.enabled = !_PlayerRestricted;

        if (scenechecker)
            NextScene();
    }

    // =========================================================
    public void NextScene()
    {
        _FadeTransition.FadeOut();
        StartCoroutine(CallNextScene());
    }

    IEnumerator CallNextScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_NextScene);
    }

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