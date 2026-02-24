using System.Collections;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public CanvasGroup _FadePanel;
    public Canvas _FadeCanvas;
    public float _FadeDuration = 1.0f;


    public void Start()
    {
        FadeIn(); // Start with a fade-in effect when the scene loads
    }

    // Coroutine to handle the fade effect
    IEnumerator FadeCanvasGroup(CanvasGroup _Cutscene, float _Start, float _End, float _Duration) 
    {
        float _Elapsedtime = 0.0f;
        while (_Elapsedtime < _FadeDuration) 
        {
            _Elapsedtime += Time.deltaTime;
            _Cutscene.alpha = Mathf.Lerp(_Start, _End, _Elapsedtime / _Duration);
            yield return null;
        }
        _Cutscene.alpha = _End;
    }


    // Public methods to trigger fade in and fade out effects
    public void FadeOut() 
    {
        StartCoroutine(FadeCanvasGroup(_FadePanel, _FadePanel.alpha, 1, _FadeDuration));
        _FadeCanvas.sortingOrder = 10;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(_FadePanel, _FadePanel.alpha, 0, _FadeDuration));
        _FadeCanvas.sortingOrder = 0; 
    }
}
