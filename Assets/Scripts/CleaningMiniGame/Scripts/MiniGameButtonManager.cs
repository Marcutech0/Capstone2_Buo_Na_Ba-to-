using System.Runtime.CompilerServices;
using UnityEditor.Build.Content;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;

    public void CloseWinScreen()
    {
        _winScreen.SetActive(false);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("_"); //Next Scene, idk what's next
    }
}
