using Adventure.Player.Controller.PlayerHealth;
using Adventure.Player.Model;
using DG.Tweening;
using Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Adventure.Player
{
    public class PlayerHealth: MonoBehaviour
    {
     
       [BoxGroup("Sliders")] [Title("Life bar")] [LabelText("Represents life bar")]
       [SerializeField] private Slider lifeSlider;
       
       [BoxGroup("Sliders")] [Title("Background bar")] [LabelText("Represents life background bar")]
       [SerializeField] private Slider delaySlider;

       [BoxGroup("Object References")] [Title("Sword")] [LabelText("Player's sword")]
       [SerializeField] private GameObject sword;
       
       [BoxGroup("Object References")] [Title("Enemy")] [LabelText("Enemy Reference: automatically get")]
       [SerializeField]private GameObject enemy;
       
       [BoxGroup("Sliders")] [Title("Slider effect Duration")] [LabelText("Duration of life bar effect")]
       [ShowInInspector, ReadOnly]private readonly float _sliderEffectDuration = 0.4f;
       
       [BoxGroup("Sliders")] [Title("Delay Slider Effect Duration")] [LabelText("Duration of background bar effect")]
       [ShowInInspector, ReadOnly] private readonly float _delaySliderEffectDuration = 0.6f;
       
       [BoxGroup("Sliders")] [Title("Delay")] [LabelText("Duration delay before background bar effect")]
       [ShowInInspector, ReadOnly]  private readonly float _delay = 0.1f;
       
       [Title("Health Model")] [LabelText("Script PlayerHealthModel")] [ShowInInspector] private PlayerHealthModel _healthModel;
       
      [Title("Health Controller")] [LabelText("Script PlayerHealthModel")] private PlayerHealthController _healthController;
      
      [ShowInInspector, ReadOnly] [BoxGroup("Testing")]
      public float CurrentLife => _healthModel?.currentLife ?? 0f;

      [ShowInInspector, ReadOnly] [BoxGroup("Testing")]
      public float MaxLife => _healthModel?.maxLife ?? 0f;

      [ShowInInspector, ReadOnly]
      public float LifeSliderValue => lifeSlider?.value ?? 0f;

      [ShowInInspector, ReadOnly]
      public float DelaySliderValue => delaySlider?.value ?? 0f;

  
        private void Start()
        {
           
            _healthModel = gameObject.AddComponent<PlayerHealthModel>();
            _healthModel.SetPlayerHealthModel(100);
            _healthController = new PlayerHealthController(_healthModel);
           
            lifeSlider.minValue = 0;
            lifeSlider.maxValue = _healthModel.maxLife;
            lifeSlider.value = _healthModel.currentLife;
            _healthController.OnLifeChanged += SetCurrentLifeSlider;
            _healthController.OnPlayerDied += HandleDeath;


        }
        private void HandleDeath()=> SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        
        private void SetCurrentLifeSlider(float newLife)
        {
            
            lifeSlider.DOValue(newLife,0.2f).SetEase(Ease.OutCubic);

            
            DOVirtual.DelayedCall(0.1f, () =>
            {
                delaySlider.DOValue(newLife,0.5f).SetEase(Ease.OutQuad).Delay();
            });
            
        }


     
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
           if(other.gameObject.layer == LayerMask.NameToLayer("ENEMY"))
               if (!sword.activeInHierarchy)
               {
                   var enemyPosition = other.gameObject.GetComponent<Slime>().Position;
                   PlayerDamageEffect.Instance.TakeDamage(enemyPosition, Color.red);
                   _healthController.TakeDamage(10f);
               }

        }
     
#if UNITY_EDITOR
      
   

        public void Test_SetCurrentLifeSlider(float newLife)=> SetCurrentLifeSlider(newLife);
        
        public void Test_TakeDamage(float damage)=> _healthController.TakeDamage(damage);
        public void Test_SetSliders(Slider life, Slider delay)
        {
            this.lifeSlider = life;
            this.delaySlider = delay;
        }
        
        public void Test_SetSword(GameObject swordObj)
        {
            this.sword = swordObj;
        }

#endif
        
        
        
        
    }
}