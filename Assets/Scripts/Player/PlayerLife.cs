
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Player
{
    public class PlayerLife: MonoBehaviour
    {
       private float _life, _maxLife;
       [SerializeField] private Slider lifeSlider;
      [SerializeField] private GameObject sword, enemy;
        private void Start()
        {
            _maxLife = 100;
            _life = _maxLife;
            VerifyReferences();
            lifeSlider.minValue = 0;
            lifeSlider.maxValue = _maxLife;
            SetCurrentLifeSlider();
           
        }

        private void SetCurrentLifeSlider() => lifeSlider.value = _life;


        private void OnCollisionEnter2D(Collision2D other)
        {
           if(other.gameObject.layer == LayerMask.NameToLayer("ENEMY"))
               if (!sword.activeInHierarchy)
                   SetLife(10);
        }

        private void SetLife(float enemyDamage)
        {
            _life -= enemyDamage;
            SetCurrentLifeSlider();
            ValidatePlayerDeath();
        }
       
        private void ValidatePlayerDeath()
        {
            if (_life <= 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
       
        private void VerifyReferences()
        {
            if (lifeSlider == null)
                Debug.LogError("LIFE SLIDER IS NULL(class PlayerLife)");
            if (_life == 0)
                Debug.LogError("LIFE IS ZERO. Set a value greater than zero. (Class PlayerLife) ");
            
            if (_maxLife == 0)
                Debug.LogError("MAX LIFE IS ZERO. Set a value greater than zero. (Class PlayerLife) ");
            
        }
        
        
        
    }
}