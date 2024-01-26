using UnityEngine;

public class Bull : MonoBehaviour
{
    [SerializeField] private Rigidbody _projectile;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _speed;

    void Update()
    {
        Rigidbody projectile = Instantiate(
            _projectile, 
            _startPosition.position, 
            _startPosition.rotation);

        projectile.velocity = _startPosition.forward * _speed;
    }
}
