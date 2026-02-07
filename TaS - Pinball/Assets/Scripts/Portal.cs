using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Color _teleportColor = Color.magenta;
    [SerializeField] private Transform otherPortal;
    [SerializeField] private float _respawnDelay = 0.5f, _teleportCooldown = 3f, _rotationSpeed = 100f;

    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;
    
    private bool _isTeleporting = false;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer != null)
        {
            _defaultColor = _spriteRenderer.color;
        }
    }

    void Update()
    {
        transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            GameObject ball = collision.gameObject;
            if (!_isTeleporting)
            {
                _isTeleporting = true;
                otherPortal.GetComponent<Portal>()._isTeleporting = true;

                if (_spriteRenderer != null)
                {
                    _spriteRenderer.color = _teleportColor;
                }
                if (otherPortal != null)
                {
                    SpriteRenderer otherSpriteRenderer = otherPortal.GetComponent<SpriteRenderer>();
                    if (otherSpriteRenderer != null)
                    {
                        otherSpriteRenderer.color = _teleportColor;
                    }
                }
                StartCoroutine(TeleportBall(ball));
            }
        }
    }

    private IEnumerator TeleportBall(GameObject ball)
    {
        yield return new WaitForSeconds(_respawnDelay);
        ball.transform.position = otherPortal.position;
        yield return new WaitForSeconds(_teleportCooldown);
        _isTeleporting = false;
        otherPortal.GetComponent<Portal>()._isTeleporting = false;

        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = _defaultColor;
        }
        if (otherPortal != null)
        {
            SpriteRenderer otherSpriteRenderer = otherPortal.GetComponent<SpriteRenderer>();
            if (otherSpriteRenderer != null)
            {
                otherSpriteRenderer.color = _defaultColor;
            }
        }
    }
}
