using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlaerMovementSingleplayer : MonoBehaviour
{
    [SerializeField] float speedOfMove = 5;
    [SerializeField] float speedOfTurn = 100;

    [Inject]
    Rigidbody _rb;
    [Inject]
    IPlayerInput _input;

    //[Inject]
    //public PlaerMovementSingleplayer(Rigidbody rb, IPlayerInput input)
    //{
    //    _rb = rb;
    //    _input = input;
    //}

    private void FixedUpdate()
    {
        _rb.velocity = _input.InputDirection.normalized * speedOfMove;

        if (_input.InputDirection == Vector3.zero)
        {
            return;
        }

        float directionDiff = Vector3.Dot(transform.forward, _input.InputDirection.normalized);
        if (directionDiff < 0.95f)
        {            
            //_rb.AddTorque(Vector3.up * Mathf.Sign(Vector3.SignedAngle(transform.forward, _input.InputDirection.normalized * speedofTurn, Vector3.up))  * speedofTurn, ForceMode.Force);
            _rb.AddTorque(Vector3.up *  (1 - directionDiff) * speedOfTurn, ForceMode.Force);
        }
    }
}
