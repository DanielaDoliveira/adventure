using UnityEngine;

namespace Adventure.Player.Model
{
    public class PlayerHealthModel : MonoBehaviour
    {
       
        public float currentLife { get; set; }

        public float maxLife { get; private set; }
        public bool isDead => currentLife <= 0;
        public PlayerHealthModel()
        {
        }

        public void SetPlayerHealthModel(float maxLife)
        {
            this.maxLife = maxLife;
            currentLife = maxLife;
        }
        public void ApplyDamage(float damage)
        {
            currentLife -= damage;
            if (currentLife < 0) currentLife = 0;
        }
    
       
    }
}


