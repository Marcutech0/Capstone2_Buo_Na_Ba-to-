using UnityEngine;

public class Artifact_Interactable : MonoBehaviour
{
    [SerializeField] private GameObject artifactObject; //Object is artifacts around the map
    [SerializeField] public ArtifactData artifact;
    [SerializeField] public ArtifactDetailPanel panel;
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

    private void OnMouseDown()
    { 
        artifactObject.SetActive(false); //Artifact on map becomes invisible
        panel.OpenPanel(artifact);
    }

    
}
