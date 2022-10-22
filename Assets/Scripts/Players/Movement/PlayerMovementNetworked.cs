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

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out PlayerInputData input))
        {
            Debug.LogFormat("About to move: {0}", input.directionMove);
            //transform.position += input.directionMove * speedOfMove * Runner.DeltaTime;
            rb.velocity = input.directionMove * speedOfMove;
            rb.AddTorque(Vector3.up * input.directionMove.z * speedofTurn);
        }
    }
}
