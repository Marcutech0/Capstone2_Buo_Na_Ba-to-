using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // This script handles player movement using NavMeshAgent and updates the walking animation based on the player's velocity.
    public Camera _Cam;
    public NavMeshAgent _Player;
    public Animator _PlayerAnimator;
    public GameObject _TargetDest;

    public void Update()
    {
        // Check for mouse input to set the destination for the NavMeshAgent
        if (Input.GetMouseButtonDown(0)) 
        {
          Ray _Ray = _Cam.ScreenPointToRay(Input.mousePosition);
          RaycastHit _HitPoint;

            if (Physics.Raycast(_Ray, out _HitPoint)) 
            {
                _TargetDest.transform.position = _HitPoint.point;
                _Player.SetDestination(_HitPoint.point);
            }
        }

        // Update the walking animation based on the player's velocity
        if (_Player.velocity != Vector3.zero) 
        {
            _PlayerAnimator.SetBool("isWalking", true);
        }
        else if (_Player.velocity == Vector3.zero) 
        {
            _PlayerAnimator.SetBool("isWalking", false);
        }
    }
}
