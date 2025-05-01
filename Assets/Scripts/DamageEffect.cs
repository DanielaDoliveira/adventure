using DG.Tweening;
using UnityEngine;


    public abstract class DamageEffect: MonoBehaviour
    {
        private Rigidbody2D _rb;
        [SerializeField] protected SpriteRenderer spriteRenderer;
        private bool _isTakingDamage = false;
        private readonly float _flashDuration = 0.1f;
        private readonly float _knockbackDuration = 0.5f;
        private readonly float _knockbackForce = 2.5f;
        protected virtual void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            if(spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public  void TakeDamage(Vector2 enemyPosition, Color color)
        {
            if (_isTakingDamage) return;
            _isTakingDamage = true;
            FlashColor(color);
            ApplyKnockback(enemyPosition);
            
        }

        private void FlashColor(Color color)
        {
            spriteRenderer.DOColor(Color.red, _flashDuration)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.Linear)
                .OnComplete(() => _isTakingDamage = false);
        }

        private void ApplyKnockback(Vector2 enemyPosition)
        {
            Vector2 direction = (Vector2)(transform.position) - enemyPosition;
            direction.Normalize();
            Vector2 knockbackTarget = (Vector2)transform.position + direction * _knockbackForce;
            _rb.DOMove(knockbackTarget,_knockbackDuration).SetEase(Ease.OutQuad);

        }
    }
