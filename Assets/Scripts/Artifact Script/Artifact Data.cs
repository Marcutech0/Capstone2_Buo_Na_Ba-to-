using UnityEngine;

[CreateAssetMenu(fileName = "NewArtifact", menuName = "Game/Artifact")]
public class ArtifactData : ScriptableObject
{
    public string artifactID;
    public string artifactName;
    [TextArea] public string description;
    public Sprite icon;
}
