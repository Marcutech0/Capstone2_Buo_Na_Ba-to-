using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using System.Collections;
public class ButtonManager : MonoBehaviour
{
    public Fade _FadeTransition;
    public GameObject _ExplorationTextObject;
    public GameObject _ArtifactPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Cutscene1.1 Buo na ba to");
    }

    public void ExplorationButton() 
    {
        _ExplorationTextObject.SetActive(true);
        Debug.Log("Clicked!");
        Debug.Log("Object State: " + _ExplorationTextObject.activeSelf);
        StartCoroutine(TurnOffExplorationGameobject());
    }
    
    public void Codex() 
    {
        SceneManager.LoadScene("CodexPanel");
    }

    public void Credits() 
    {
        SceneManager.LoadScene("Credits");
    }

    public void ArtifactShelf() 
    {
        SceneManager.LoadScene("ArtifactPanel");
    }

    public void ExitGame() 
    {
        Application.Quit();
    }

    public void BackToMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToGame() 
    {
        _ArtifactPanel.SetActive(false);
    }

    public void OpenArtifactLog() 
    {
        _ArtifactPanel.SetActive(true);
    }

    IEnumerator TurnOffExplorationGameobject() 
    {
        yield return new WaitForSeconds(0.5f);
        _ExplorationTextObject.SetActive(false);
    }
}
