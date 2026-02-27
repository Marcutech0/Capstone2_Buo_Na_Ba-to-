using UnityEngine;

public class CodexManager : MonoBehaviour
{
    public GameObject Artifact;
    public GameObject Character;
    public GameObject House;
    public GameObject Instruction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Character.gameObject.SetActive(false);
        House.gameObject.SetActive(false);
        Instruction.gameObject.SetActive(false);
        Artifact.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Artifacts()
    {
        Character.gameObject.SetActive(false);
        House.gameObject.SetActive(false);
        Instruction.gameObject.SetActive(false);
        Artifact.gameObject.SetActive(true);
    }

    public void Characters()
    {
        Character.gameObject.SetActive(true);
        House.gameObject.SetActive(false);
        Instruction.gameObject.SetActive(false);
        Artifact.gameObject.SetActive(false);
    }

    public void Houses()
    {
        Character.gameObject.SetActive(false);
        House.gameObject.SetActive(true);
        Instruction.gameObject.SetActive(false);
        Artifact.gameObject.SetActive(false);
    }

    public void Instructions()
    {
        Character.gameObject.SetActive(false);
        House.gameObject.SetActive(false);
        Instruction.gameObject.SetActive(true);
        Artifact.gameObject.SetActive(false);
    }
}
