using UnityEngine;

public enum Emotion
{
    Neutral,
    Worried,
    Confused,
    Happy,
    Sad,
}

public class EmotionController : MonoBehaviour
{
    [Header("Emotion GameObjects")]
    public GameObject Neutral;
    public GameObject Worried;
    public GameObject Confused;
    public GameObject Happy;
    public GameObject Sad;

    public void SetEmotion(Emotion emotion)
    {
        Neutral.SetActive(false);
        Worried.SetActive(false);
        Confused.SetActive(false);
        Happy.SetActive(false);
        Sad.SetActive(false);

        switch (emotion)
        {
            case Emotion.Neutral:
                Neutral.SetActive(true);
                break;

            case Emotion.Worried:
                Worried.SetActive(true);
                break;

            case Emotion.Confused:
                Confused.SetActive(true);
                break;

            case Emotion.Happy:
                Happy.SetActive(true);
                break;

            case Emotion.Sad:
                Sad.SetActive(true);
                break;
        }
    }
}