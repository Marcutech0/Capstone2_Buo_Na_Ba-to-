using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    public Camera _Cam;
    public NavMeshAgent _Player;
    public Animator _PlayerAnimator;

    private bool _movementLocked = false;

    private void Awake()
    {
        // IMPORTANT for 2.5D
        _Player.updateRotation = false;
        _Player.updateUpAxis = false;
    }

    private void Update()
    {
        if (_movementLocked) return;

        // Mouse click movement
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _Cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                _Player.SetDestination(hit.point);
            }
        }

        HandleAnimationAndFlip();
    }

    private void LateUpdate()
    {
        // Fully lock rotation to prevent snapping
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void HandleAnimationAndFlip()
    {
        bool isMoving = _Player.velocity.magnitude > 0.1f;
        _PlayerAnimator.SetBool("isWalking", isMoving);

        if (!isMoving) return;

        if (_Player.velocity.x > 0.05f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (_Player.velocity.x < -0.05f)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    // =========================
    // CLEAN LOCK SYSTEM
    // =========================

    public void LockMovement()
    {
        _movementLocked = true;

        _Player.isStopped = true;
        _Player.ResetPath();
        _Player.velocity = Vector3.zero;

        _PlayerAnimator.SetBool("isWalking", false);
    }

    public void UnlockMovement()
    {
        _movementLocked = false;
        _Player.isStopped = false;
    }
}