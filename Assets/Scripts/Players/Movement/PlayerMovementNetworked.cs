using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMovementNetworked : NetworkBehaviour
{
    [SerializeField] float speedOfMove = 5;
    [SerializeField] float speedofTurn = 100;

    [Inject]
    Rigidbody rb;

    private float minSpeedForRotation = 10;
    private float currentSpeed;

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out PlayerInputData input))
        {
            currentSpeed = input.directionMove.z * speedOfMove;
            Debug.LogFormat("About to move: {0}", input.directionMove);
            //transform.position += input.directionMove * speedOfMove * Runner.DeltaTime;
            rb.AddForce(transform.forward * currentSpeed);
            if (currentSpeed > minSpeedForRotation)
            {
                rb.AddTorque(Vector3.up * input.directionMove.x * speedofTurn);
            }

            //rb.velocity = input.directionMove * speedOfMove;
            //rb.AddTorque(Vector3.up * input.directionMove.z * speedofTurn);
        }
    }
}
