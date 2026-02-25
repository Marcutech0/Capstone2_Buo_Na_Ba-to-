using UnityEngine;

public class Artifact_Interactable : MonoBehaviour
{
    [SerializeField] private GameObject artifactObject; //Object is artifacts around the map
    [SerializeField] private GameObject artifactUI; //Ui is on the artifacts panel

 
    private void OnMouseDown()
    { 
        artifactObject.SetActive(false); //Artifact on map becomes invisible
        artifactUI.SetActive(true); //Artifact becomes visible on the artifact panel
    }

    
}
