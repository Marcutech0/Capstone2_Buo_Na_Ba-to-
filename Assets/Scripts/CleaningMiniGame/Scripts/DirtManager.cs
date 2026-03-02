using UnityEngine;

public class DirtManager : MonoBehaviour
{
    public static DirtManager _Instance;

    public int _cleanedDirtCount = 0;

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
        }
    }

}
