using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _strafeSpeed = 3f;
    [SerializeField] private float _jumpSpeed = 7f;
    [SerializeField] private float _gravityFactor = 2;

    [SerializeField] private Transform _camera;
    [SerializeField] private float _horizontalTurnSensitivity;
    [SerializeField] private float _verticalTurnSensitivity = 10;
    [SerializeField] private float _verticalMinAngle = -89;
    [SerializeField] private float _verticalMaxAngle = 89;

    private Transform _transform;
    private float _cameraAngle = 0;
    private Vector3 _verticalVelocity;
    private CharacterController _characterController;

    private void Awake()
    {
        _transform = transform;
        _characterController = GetComponent<CharacterController>();
        _cameraAngle = _camera.localEulerAngles.x;
    }

    private void Update()
    {
        Vector3 forward = Vector3.ProjectOnPlane(_camera.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(_camera.right, Vector3.up).normalized;

        _cameraAngle -= Input.GetAxis("Mouse Y") * _verticalTurnSensitivity;
        _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinAngle, _verticalMaxAngle);
        _camera.localEulerAngles = Vector3.right * _cameraAngle;

        _transform.Rotate(Vector3.up * _horizontalTurnSensitivity * Input.GetAxis("Mouse X"));

        if (_characterController != null)
        {
            Vector3 playerInput = forward * Input.GetAxis("Vertical") * _speed + right * Input.GetAxis("Horizontal") * _strafeSpeed;
            //playerInput *= Time.deltaTime;

            if (_characterController.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _verticalVelocity = Vector3.up * _jumpSpeed;
                }
                else
                {
                    _verticalVelocity = Vector3.down;
                }

                _characterController.Move((playerInput + _verticalVelocity) * Time.deltaTime);
            }
            else
            {
                Vector3 horizontalVelocity = _characterController.velocity;
                horizontalVelocity.y = 0;
                _verticalVelocity += Physics.gravity * Time.deltaTime * _gravityFactor;

                _characterController.Move((horizontalVelocity + _verticalVelocity) * Time.deltaTime);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.TryGetComponent(out Enemy _))
            hit.rigidbody.velocity = Vector3.up * 10;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        var character = GetComponent<CharacterController>();

        Gizmos.DrawCube(transform.position, Vector3.right + Vector3.forward + Vector3.up * character.height);
    }
}
