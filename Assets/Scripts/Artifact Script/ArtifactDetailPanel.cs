using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class ArtifactDetailPanel : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI artifactNameText;
    public TextMeshProUGUI descriptionText;
    public Image iconImage;
    public Button logButton;

    private CanvasGroup canvasGroup;
    private ArtifactData currentArtifact;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        ClosePanelInstant(); // Hide panel on start
    }

    /// <summary>
    /// Opens the panel and shows the given artifact
    /// </summary>
    public void OpenPanel(ArtifactData artifact)
    {
        currentArtifact = artifact;

        artifactNameText.text = artifact.artifactName;
        descriptionText.text = artifact.description;
        iconImage.sprite = artifact.icon;

        // Enable log button only if artifact not logged
        logButton.interactable = !ArtifactLogManager.Instance.IsArtifactLogged(artifact.artifactID);

        if (!logButton.interactable)
        {
            logButton.gameObject.SetActive(false);
        }
        else
        {
            logButton.gameObject.SetActive(true);
        }

        // Show the panel visually
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Closes the panel (with visibility hidden)
    /// </summary>
    public void ClosePanel()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// Instantly hides the panel on start without animation
    /// </summary>
    private void ClosePanelInstant()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// Called by the log button
    /// </summary>
    public void OnLogButtonPressed()
    {
        if (currentArtifact == null) return;

        ArtifactLogManager.Instance.LogArtifact(currentArtifact.artifactID);
        logButton.interactable = false;
    }
}