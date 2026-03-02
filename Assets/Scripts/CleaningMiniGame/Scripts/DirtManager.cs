using UnityEngine;

public class DirtManager : MonoBehaviour
{
    public static DirtManager _Instance;

    public int _cleanedDirtCount = 0;

    [SerializeField] private GameObject _winScreen;

    private void Start()
    {
        _winScreen.SetActive(false);
    }
    void Awake()
    {
        if (_Instance == null)
            _Instance = this;
        else
            Destroy(gameObject);
    }

    public void DirtCleaned()
    {
        _cleanedDirtCount++;
        Debug.Log("Dirt cleaned: " + _cleanedDirtCount);

        if (_cleanedDirtCount >= 4)
        {
            Debug.Log("Level Complete!"); // Can Change this to anything (ex. Scene Transition, or Winning Panel)
            _winScreen.SetActive(true);
        }
    }

}
