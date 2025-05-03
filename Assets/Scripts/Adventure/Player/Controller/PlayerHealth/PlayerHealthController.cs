using System;
using Adventure.Player.Model;

namespace Adventure.Player.Controller.PlayerHealth
{
    public class PlayerHealthController
    {
        private readonly PlayerHealthModel _model;
        public event Action<float> OnLifeChanged;
        public event Action OnPlayerDied;
        public float currentLife => _model.currentLife;
        public float maxLife => _model.maxLife;

        public PlayerHealthController(PlayerHealthModel model)
        {
            _model = model;
        }
        
        public void TakeDamage(float damage)
        {
            _model.ApplyDamage(damage);
            OnLifeChanged?.Invoke(_model.currentLife);
            if (_model.isDead)
                OnPlayerDied?.Invoke();
        }
      
    }
}