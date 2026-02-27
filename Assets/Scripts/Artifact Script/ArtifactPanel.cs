using UnityEditor.Experimental;
using UnityEngine;

public class ArtifactPanel : MonoBehaviour
{
    public GameObject panel;
    public void Open()
    {
        panel.gameObject.SetActive(true);
    }

    public void Close()
    {
        panel.gameObject.SetActive(false);
    }
}
