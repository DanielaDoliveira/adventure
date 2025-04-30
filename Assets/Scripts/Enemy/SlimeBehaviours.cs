using System.Collections;
using Player;
using UnityEngine;

namespace Enemy
{
    public class SlimeBehaviours: MonoBehaviour,IEnemyBehaviour
    {
       
        private readonly float _flashDuration = 0.1f;
        private readonly float _flashCounter = 3;
        private Rigidbody2D _rigidbody;
        private Material _material;
        private readonly Color _flashColor = new Color(255,255,255,0);
        private Knockback _knockback;
        private readonly float _knockbackThrust = 2f;
        
        void Start()
        {
            _material = GetComponent<SpriteRenderer>().material;
            _material.SetColor("_FlashColor", _flashColor);
            _knockback = GetComponent<Knockback>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        public void BehaviourTakingDamage()
        {
            StartCoroutine(DamageBehaviour());
        }
        public IEnumerator DamageBehaviour()
        {
            
            for (int i = 0; i < _flashCounter; i++)
            {
                _material.SetFloat("_FlashAmount",1f);
            
                yield return new WaitForSeconds(_flashDuration);
                _material.SetFloat("_FlashAmount",0f);

         
                yield return new WaitForSeconds(_flashDuration);
            }
     
        }

        public void PirsuitPlayer()
        {
            throw new System.NotImplementedException();
        }

        public void StopPirsuitPlayer()
        {
            throw new System.NotImplementedException();
        }


        public void KnockbackBehaviour()
        {
            StartCoroutine(KnockTime());
        }
        public IEnumerator KnockTime()
        {
            _knockback.GetKnockedBack(PlayerControl.Instance.transform,_knockbackThrust);
            yield return new WaitForSeconds(0.2f);
            _rigidbody.linearVelocity = Vector2.zero;
        }
      
    }
}