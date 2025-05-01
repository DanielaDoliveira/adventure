
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngine.UI;

namespace Player
{
    public class PlayerLife: MonoBehaviour
    {
       private float _life, _maxLife;
       [SerializeField] private Slider lifeSlider;
       [SerializeField] private Slider delaySlider;
      [SerializeField] private GameObject sword, enemy;
      private readonly float _sliderEffectDuration = 0.4f;
      private readonly float _delaySliderEffectDuration = 0.6f;
      private readonly float _delay = 0.1f;
  
        private void Start()
        {
            _maxLife = 100;
            _life = _maxLife;
            VerifyReferences();
            lifeSlider.minValue = 0;
            lifeSlider.maxValue = _maxLife; 
            lifeSlider.value = _life;
           
        }

        private void SetCurrentLifeSlider()
        {
            lifeSlider.DOValue(_life,0.2f).SetEase(Ease.OutCubic);

            
            DOVirtual.DelayedCall(0.1f, () =>
            {
                delaySlider.DOValue(_life,0.5f).SetEase(Ease.OutQuad).Delay();
            });
        }



        private void OnTriggerEnter2D(Collider2D other)
        {
           if(other.gameObject.layer == LayerMask.NameToLayer("ENEMY"))
               if (!sword.activeInHierarchy)
               {
                   var enemyPosition = other.gameObject.GetComponent<Slime>().Position;
                   PlayerDamageEffect.Instance.TakeDamage(enemyPosition, Color.red);
                   SetLife(10);
               }

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