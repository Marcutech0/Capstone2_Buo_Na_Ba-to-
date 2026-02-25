using UnityEngine;

public class ShelfUI : MonoBehaviour
{
    public ArtifactData[] allArtifacts;
    public GameObject artifactSlotPrefab;
    public Transform shelfContainer;

    private void OnEnable()
    {
        RefreshShelf();
    }

    public void RefreshShelf()
    {
        if (ArtifactLogManager.Instance == null)
        {
            Debug.LogError("ArtifactLogManager is NULL!");
            return;
        }

        foreach (Transform child in shelfContainer)
            Destroy(child.gameObject);

        foreach (var artifact in allArtifacts)
        {
            if (artifact == null) continue;

            if (ArtifactLogManager.Instance.IsArtifactLogged(artifact.artifactID))
            {
                GameObject slot = Instantiate(artifactSlotPrefab, shelfContainer);
                slot.GetComponent<ArtifactSlotUI>().Setup(artifact);
            }
        }
    }
}
