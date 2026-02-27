using UnityEngine;

public class DebugReset : MonoBehaviour
{
    [ContextMenu("Clear PlayerPrefs")]
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs Cleared!");
    }
}
