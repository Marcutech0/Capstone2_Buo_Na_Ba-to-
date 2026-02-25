using UnityEditor.Experimental;
using UnityEngine;


public class DebugArtifactDetailPanel : MonoBehaviour
{
    public ArtifactData artifact; // Assign in Inspector
    public ArtifactDetailPanel detailPanel; // Assign in Inspector

    public void OpenArtifactPanel()
    {
        if (artifact != null && detailPanel != null)
        {
            detailPanel.OpenPanel(artifact);
        }
    }
}
