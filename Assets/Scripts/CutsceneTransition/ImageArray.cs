using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class ImageArray : MonoBehaviour
{
    [Header("UI Target")]
    [SerializeField] private Image _targetImage;

    [Header("Images to Display")]
    [SerializeField] private Sprite[] _frameImages;

    [Header("Slideshow Settings")]
    [SerializeField] private float _frameInterval = 1f;

    private int _currentIndex = 0;
    private Coroutine _slideshowCoroutine;
    [SerializeField] private string _nextScene;
    public GameObject[] _Buttons;
    public GameObject _TutorialUI;
    private void Start()
    {
        if (_frameImages.Length == 0 || _targetImage == null)
            return;

        _targetImage.sprite = _frameImages[_currentIndex];
    }

    public void EndTutorial() 
    {
        _TutorialUI.SetActive(false);
        SceneManager.LoadScene(_nextScene);
    }

    public void NextImage() 
    {
        if (_frameImages.Length == 0 || _targetImage == null) return;

        _currentIndex++;

        if (_currentIndex < _frameImages.Length)
        {
            _targetImage.sprite = _frameImages[_currentIndex];
            _Buttons[1].SetActive(true);
            _Buttons[2].SetActive(true);
        }

        _targetImage.sprite = _frameImages[_currentIndex];
    }

    public void PreviousImage() 
    {
        if (_frameImages.Length == 0) return;
        _currentIndex--;

        if (_currentIndex < 0) 
        {
            _currentIndex = 0;
            return;
        }

        _targetImage.sprite = _frameImages[_currentIndex];
        _Buttons[1].SetActive(false);
        _Buttons[2].SetActive(false);

    }
}
