using UnityEngine;
using UnityEngine.UI;

public class ArtifactSlotUI : MonoBehaviour
{
    public Image icon;
    public ArtifactData artifact;
    public ArtifactDetailPanel panel; // Class field

    private void Start()
    {
        // Assign the panel from scene
        if (panel == null)
        {

            panel = FindObjectOfType<ArtifactDetailPanel>();
            if (panel == null)
            {
                Debug.LogError("No ArtifactDetailPanel found in the scene!");
            }
        
        }
    }

    public void Setup(ArtifactData data)
    {
        artifact = data;

        if (icon == null)
        {
            Debug.LogError("Icon Image reference is missing!");
            return;
        }

        if (data.icon == null)
        {
            Debug.LogWarning("Artifact has no icon assigned!");
            return;
        }

        icon.sprite = data.icon;
        icon.enabled = true;
    }

    public void OpenArtifactPanel()
    {

        if (panel != null && artifact != null)
        {
            panel.OpenPanel(artifact);
        }
        else
        {
            Debug.LogError("Panel or Artifact is null!");
        }
    }
}
