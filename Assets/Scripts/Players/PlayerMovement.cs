using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] float speedOfMove = 5;

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out PlayerInputData input))
        {
            transform.position += input.directionMove * speedOfMove * Runner.DeltaTime;
        }
    }

    public override void Spawned()
    {
        base.Spawned();
    }
}
