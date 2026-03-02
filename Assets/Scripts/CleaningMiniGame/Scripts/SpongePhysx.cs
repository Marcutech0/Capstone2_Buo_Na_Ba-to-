using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class SpongePhysx : MonoBehaviour
{
    [Header("Drag Physics")]
    public float followStrength = 25f;
    public float damping = 6f;
    public float maxSpeed = 15f;

    [Header("Sway (Balatro Style)")]
    public float maxTiltAngle = 15f;
    public float tiltSmoothness = 10f;
    public float velocityInfluence = 1f;

    private Rigidbody2D rb;
    private Camera cam;
    private bool dragging;

    private float currentTilt;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        rb.gravityScale = 0;
        rb.linearDamping = damping;
        rb.angularDamping = 8f;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void OnMouseDown()
    {
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    void FixedUpdate()
    {
        if (!dragging) return;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector2 direction = (Vector2)mouseWorld - rb.position;
        rb.AddForce(direction * followStrength, ForceMode2D.Force);

        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    void Update()
    {
        ApplySway();
    }

    void ApplySway()
    {
        Vector2 velocity = rb.linearVelocity;

        // Balatro-style opposite tilt based on movement direction
        float targetTilt = -velocity.x * velocityInfluence;
        targetTilt = Mathf.Clamp(targetTilt, -maxTiltAngle, maxTiltAngle);

        currentTilt = Mathf.Lerp(
            currentTilt,
            targetTilt,
            Time.deltaTime * tiltSmoothness
        );

        transform.rotation = Quaternion.Euler(0f, 0f, currentTilt);
    }
}
