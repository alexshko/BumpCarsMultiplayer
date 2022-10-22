using UnityEngine;
using Zenject;

public class PlayerMoveSimpleSinglePlayer : MonoBehaviour
{
    [SerializeField] float speedOfMove = 5;
    [SerializeField] float speedOfTurn = 100;


    [Inject]
    Rigidbody _rb;

    [Inject]
    IPlayerInput _input;

    private float minSpeedForRotation = 10;

    private float currentSpeed;

    private void FixedUpdate()
    {
        currentSpeed = _input.InputDirection.z * speedOfMove;
        _rb.AddForce(transform.forward * currentSpeed);

        if (currentSpeed > minSpeedForRotation)
        {
            _rb.AddTorque(Vector3.up * _input.InputDirection.x * speedOfTurn);
        }
    }
}
