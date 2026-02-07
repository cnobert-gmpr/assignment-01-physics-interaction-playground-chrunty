using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Color _hitColor = Color.lemonChiffon;
    [SerializeField] private float _hideDelay = 0.1f, _resetDelay = 6f;

    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;
    
    private bool _isHit = false;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball") && !_isHit)
        {
            _isHit = true;
            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = _hitColor;
            }
            Invoke(nameof(HideTarget), _hideDelay);
        }
    }

    void HideTarget()
    {
        gameObject.SetActive(false);
        Invoke(nameof(ResetTarget), _resetDelay);
    }

    void ResetTarget()
    {
        _isHit = false;
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = _defaultColor;
        }
        gameObject.SetActive(true);
    }
}
