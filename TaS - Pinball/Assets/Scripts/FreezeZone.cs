using System.Collections;
using UnityEngine;

public class FreezeZone : MonoBehaviour
{
    [SerializeField] private Color _freezeColor = Color.cyan;
    [SerializeField] private float _freezeDuration = 2f, _freezeCooldown = 3f, _initialDelay = 0.2f;

    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;
    
    private bool _isFreezing = false;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer != null)
        {
            _defaultColor = _spriteRenderer.color;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            GameObject ballCollider = collision.gameObject;
            if (!_isFreezing)
            {
                _isFreezing = true;
                StartCoroutine(FreezeBall(ballCollider));
            }
        }
    }

    private IEnumerator FreezeBall(GameObject ball)
    {
        Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();
        if (ballRB != null)
        {
            yield return new WaitForSeconds(_initialDelay); 

            Vector2 originalVelocity = ballRB.linearVelocity;
            float originalAngularVelocity = ballRB.angularVelocity;

            ballRB.linearVelocity = Vector2.zero;
            ballRB.angularVelocity = 0f;
            ballRB.gravityScale = 0f;

            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = _freezeColor;
            }

            yield return new WaitForSeconds(_freezeDuration);

            ballRB.gravityScale = 1f;
            ballRB.linearVelocity = originalVelocity;
            ballRB.angularVelocity = originalAngularVelocity;

            yield return new WaitForSeconds(_freezeCooldown);
            _isFreezing = false;

            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = _defaultColor;
            }
        }
    }
}
