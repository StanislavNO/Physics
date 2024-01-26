using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCC : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 playerInput = new(Input.GetAxis("Horizontal") * _speed, _rigidBody.position.y, Input.GetAxis("Vertical") * _speed);

        _rigidBody.velocity = playerInput;
        _rigidBody.velocity += Physics.gravity;
    }
}
