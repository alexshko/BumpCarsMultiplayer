using UnityEngine;

public class GamePlayerInput : IPlayerInput
{
    public Vector3 InputDirection
    {
        get
        {
            return GetDirection();
        }
    }

    private Vector3 GetDirection()
    {
        //check the correct order of params:
        return new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical"));
        //return new Vector3(1, 0, 1);
    }
}
