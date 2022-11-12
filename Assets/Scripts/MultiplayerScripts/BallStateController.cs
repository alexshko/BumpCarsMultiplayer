using Fusion;
using UnityEngine;
using Zenject;

namespace alexshkorp.bumpcars.Multiplayer
{
    public class BallStateController : IBallController
    {
        /// <summary>
        /// Prefab of the ball to be instatnitated when the game starts to run
        /// </summary>
        [Inject(Id = "ballPref")]
        NetworkObject ballPref;

        [Inject]
        NetworkRunner Runner;


        NetworkObject ballRef;

        public void RecenterBall()
        {
            Rigidbody ballRB = ballRef.GetComponent<Rigidbody>();
            ballRB.position = new Vector3(0, 1, 0);
            ballRB.velocity = Vector3.zero;
            ballRB.angularVelocity = Vector3.zero;
        }

        public void SetBallByGameState(GameState state)
        {
            if (state == GameState.running && ballRef == null)
            {
                ballRef = Runner.Spawn(ballPref, new Vector3(0, 1, 0), ballPref.transform.rotation, null);
            }
            else if (ballRef != null && state != GameState.running)
            {
                Runner.Despawn(ballRef);
            }
        }
    }
}