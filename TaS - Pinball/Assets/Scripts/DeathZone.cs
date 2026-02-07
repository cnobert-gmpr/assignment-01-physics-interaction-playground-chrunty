using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint01, spawnPoint02, spawnPoint03, spawnPoint04, spawnPoint05;
    [SerializeField] private float _respawnDelay = 2f;

    private Transform randomSpawnPoint;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            GameObject ballCollider = collision.gameObject;
            StartCoroutine(RespawnBall(ballCollider));
        }
    }

    private IEnumerator RespawnBall(GameObject ball)
    {
        int randomIndex = Random.Range(0, 5);
            switch (randomIndex)
            {
                case 0:
                    randomSpawnPoint = spawnPoint01;
                    break;
                case 1:
                    randomSpawnPoint = spawnPoint02;
                    break;
                case 2:
                    randomSpawnPoint = spawnPoint03;
                    break;
                case 3:
                    randomSpawnPoint = spawnPoint04;
                    break;
                case 4:
                    randomSpawnPoint = spawnPoint05;
                    break;
            }

        yield return new WaitForSeconds(_respawnDelay);

        Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();
        if (ballRB != null)
        {
            ballRB.linearVelocity = Vector2.zero;
            ballRB.angularVelocity = 0f;
        }
        ball.transform.position = randomSpawnPoint.position;
    }
}
