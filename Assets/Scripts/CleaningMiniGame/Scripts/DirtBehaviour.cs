using UnityEngine;

public class DirtBehaviour : MonoBehaviour
{
    public float _cleanTime = 1.5f;

    private float _cleanProgress = 0f;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Sponge"))
        {
            _cleanProgress += Time.deltaTime;
            Debug.Log("Sponge Detected!");

            if (_cleanProgress >= _cleanTime)
            {
                Destroy(gameObject);
                Debug.Log("Dirt Cleaned!");
            }
        }
    }
}
