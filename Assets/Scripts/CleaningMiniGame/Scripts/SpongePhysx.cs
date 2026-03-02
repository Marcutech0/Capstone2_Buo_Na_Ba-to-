using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class SpongePhysx : MonoBehaviour
{
    public float followStrength = 25f;
    public float damping = 6f;
    public float maxSpeed = 15f;

    public float maxTiltAngle = 15f;
    public float tiltSmoothness = 10f;
    public float velocityInfluence = 1f;

    private Rigidbody2D _rb;
    private Camera _cam;
    private bool _dragging;

    private float _currentTilt;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cam = Camera.main;

        _rb.gravityScale = 0;
        _rb.linearDamping = damping;
        _rb.angularDamping = 8f;
        _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void OnMouseDown()
    {
        _dragging = true;
    }

    void OnMouseUp()
    {
        _dragging = false;
    }

    void FixedUpdate()
    {
        if (!_dragging) return;

        Vector3 mouseWorld = _cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector2 direction = (Vector2)mouseWorld - _rb.position;
        _rb.AddForce(direction * followStrength, ForceMode2D.Force);

        _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, maxSpeed);
    }

    void Update()
    {
        ApplySway();
    }

    void ApplySway()
    {
        Vector2 velocity = _rb.linearVelocity;

        float targetTilt = -velocity.x * velocityInfluence;
        targetTilt = Mathf.Clamp(targetTilt, -maxTiltAngle, maxTiltAngle);

        _currentTilt = Mathf.Lerp(
            _currentTilt,
            targetTilt,
            Time.deltaTime * tiltSmoothness
        );

        transform.rotation = Quaternion.Euler(0f, 0f, _currentTilt);
    }
}
