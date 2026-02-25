using System.Collections.Generic;
using UnityEngine;

public class ArtifactLogManager : MonoBehaviour
{
    public static ArtifactLogManager Instance;

    private HashSet<string> loggedArtifacts = new HashSet<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLoggedArtifacts();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LogArtifact(string artifactID)
    {
        if (!loggedArtifacts.Contains(artifactID))
        {
            loggedArtifacts.Add(artifactID);
            SaveLoggedArtifacts();
        }
    }

    public bool IsArtifactLogged(string artifactID)
    {
        return loggedArtifacts.Contains(artifactID);
    }

    void SaveLoggedArtifacts()
    {
        string data = string.Join(",", loggedArtifacts);
        PlayerPrefs.SetString("LoggedArtifacts", data);
        PlayerPrefs.Save();
    }

    public void LoadLoggedArtifacts()
    {
        string data = PlayerPrefs.GetString("LoggedArtifacts", "");
        if (!string.IsNullOrEmpty(data))
        {
            string[] ids = data.Split(",");
            foreach (var id in ids)
                loggedArtifacts.Add(id);
        }
    }
}
