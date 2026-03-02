using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Artifact_Interactable : MonoBehaviour
{
    [Header("Artifact Setup")]
    [SerializeField] private GameObject artifactObject;
    [SerializeField] public ArtifactData artifact;
    [SerializeField] public ArtifactDetailPanel panel;
    [SerializeField] public GameObject artifactName;
    [SerializeField] public bool isTutorialArtifact;
    [SerializeField] public enum ArtifactType
    {
       FirstArtifact,
         SecondArtifact,
            ThirdArtifact,
    }
    [SerializeField] public string[] ArtifactDialogueLines;

    [SerializeField] public string[] DialogueSpeakers;
    [SerializeField] public ArtifactType artifactType;
    [SerializeField] public Exploration1_1 explorationScript;
    [SerializeField] public DialogueManager dialogueScript;
    [SerializeField] public System.Action OnDialogueFinished;

    [Header("Player Movement")]
    [SerializeField] private NavMeshAgent playerAgent;
    [SerializeField] private float interactionDistance = 1f;
    [SerializeField] private PlayerMovement playerMovement;



    private bool isInteracting = false;

    private void Start()
    {
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
        if (isTutorialArtifact)
        {
            StartCoroutine(HandleTutorialArtifact());
        } 
        if (!isInteracting && !isTutorialArtifact)
        {
            StartCoroutine(MoveAndOpenArtifact());
        }
    }

    private IEnumerator HandleTutorialArtifact()
    {
        isInteracting = true;

        playerAgent.SetDestination(transform.position);

        while (Vector3.Distance(playerAgent.transform.position, transform.position) > interactionDistance)
        {
            yield return null;
        }

        playerAgent.ResetPath();

        // Start the dialogue
        dialogueScript.StartDialogueSet(ArtifactDialogueLines, DialogueSpeakers, false, new int[] { -1 });

        // Wait until the dialogue finishes
        yield return new WaitUntil(() => dialogueScript.IsDialogueRunning == false);

        // Now do the switch logic AFTER dialogue is finished
        string i = artifactType.ToString();
        switch (i)
        {
            case "FirstArtifact":
                explorationScript.hasInteractedArtifact1 = true;
                break;
            case "SecondArtifact":
                explorationScript.hasInteractedArtifact2 = true;
                break;
            case "ThirdArtifact":
                explorationScript.hasInteractedArtifact3 = true;
                break;
        }
    }
    private IEnumerator MoveAndOpenArtifact()
    {
        isInteracting = true;

        // Move player
        playerAgent.isStopped = false;
        playerAgent.SetDestination(transform.position);

        while (playerAgent.pathPending)
            yield return null;

        while (playerAgent.remainingDistance > playerAgent.stoppingDistance)
            yield return null;

        while (playerAgent.velocity.magnitude > 0.05f)
            yield return null;

        // Full stop
        playerAgent.isStopped = true;
        playerAgent.ResetPath();
        playerAgent.velocity = Vector3.zero;

        // Lock movement
        playerMovement.LockMovement();

        // Hide artifact
        artifactObject.SetActive(false);

        // Open panel
        panel.OpenPanel(artifact);

        isInteracting = false;
    }

    public void OnMouseOver()
    {
        if (artifactName != null)
            artifactName.SetActive(true);
    }

    public void OnMouseExit()
    {
        if (artifactName != null)
            artifactName.SetActive(false);
    }
}
