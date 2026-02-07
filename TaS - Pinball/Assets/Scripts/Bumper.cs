using System.Collections;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField] private Color _litColor = Color.lightGoldenRodYellow;
    [SerializeField, Range(0f, 100f)] private float _bumperForce = 15f;
    [SerializeField, Range(0f, 1f)] private float _litDuration = 0.2f;

    private bool _isLit = false;
    private Color _defaultColor;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer != null)
        {
            _defaultColor = _spriteRenderer.color; 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            Vector2 normal = Vector2.zero;
            Rigidbody2D ballRB = collision.collider.GetComponent<Rigidbody2D>();
            if (ballRB != null)
            {
                if (collision.contacts.Length > 0)
                {
                    normal = collision.contacts[0].normal;
                }
                else if (normal == Vector2.zero)
                {
                    normal = (ballRB.position - (Vector2)transform.position).normalized;
                }
                Vector2 bumpVelocity = normal * _bumperForce;
                collision.rigidbody.AddForce(bumpVelocity, ForceMode2D.Impulse);
            }
            if (!_isLit)
            {
                StartCoroutine(LightBumper());
            }
        }
    }

    private IEnumerator LightBumper()
    {
        _isLit = true;
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = _litColor;
        }
        yield return new WaitForSeconds(_litDuration);
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = _defaultColor;
        }
        _isLit = false;
    }
}
