using UnityEngine;
using Zenject;

public class PlayerMovementSingleplayer : MonoBehaviour
{
    [SerializeField] float speedOfMove = 5;
    [SerializeField] float speedOfTurn = 100;

    [SerializeField] AnimationCurve rotSpeedCurve;

    [Inject]
    Rigidbody _rb;

    [Inject]
    IPlayerInput _input;

    float directionDiff;
    float torqueToApply;

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

        directionDiff = Vector3.Dot(transform.forward, _input.InputDirection.normalized);
        Debug.LogFormat("DirectionDiff value: {0}", directionDiff);
        if (directionDiff < 0.99995f)
        {
            float signAngle = Mathf.Sign(Vector3.SignedAngle(transform.forward, _input.InputDirection.normalized, Vector3.up));
            //_rb.AddTorque(Vector3.up * Mathf.Sign(Vector3.SignedAngle(transform.forward, _input.InputDirection.normalized * speedofTurn, Vector3.up))  * speedofTurn, ForceMode.Force);
            torqueToApply = signAngle * rotSpeedCurve.Evaluate(directionDiff) * speedOfTurn;
            torqueToApply = Mathf.Abs(torqueToApply) < 200 ? 0 : torqueToApply;
            _rb.AddTorque(Vector3.up * torqueToApply, ForceMode.Force);
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 100, 20), directionDiff.ToString());
        GUI.Box(new Rect(0, 30, 100, 20), torqueToApply.ToString());
    }
}
