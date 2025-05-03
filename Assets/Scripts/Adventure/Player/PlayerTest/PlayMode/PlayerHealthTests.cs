using System.Collections;
using System.Reflection;
using Adventure.Player.Model;
using NUnit.Framework;
using Player;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Adventure.Player.PlayerTest.PlayMode
{
    public class PlayerHealthTests
    {
        private GameObject _player;
        private PlayerHealth _playerHealth;
        private Slider _lifeSlider;
        private Slider _delaySlider;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            _player = new GameObject("Player");
            _playerHealth = _player.AddComponent<PlayerHealth>();
            
            var sliderGo1 = new GameObject("LifeSlider");
            var sliderGo2 = new GameObject("DelaySlider");
            
            _lifeSlider = sliderGo1.AddComponent<Slider>();
            _delaySlider = sliderGo2.AddComponent<Slider>();

            _playerHealth.Test_SetSliders(_lifeSlider, _delaySlider);
           
            var swordGo = new GameObject("Sword");
            swordGo.SetActive(false);
            _playerHealth.Test_SetSword(swordGo);
            yield return null;
            

            
        }

        
        
        [UnityTest]
        public IEnumerator Damage_UpdatesLifeSlider()
        {
            yield return null;

            float initialValue = _lifeSlider.value;

            var model = _player.GetComponent<PlayerHealthModel>();
            model.ApplyDamage(20);
            yield return null;

            _playerHealth.Test_SetCurrentLifeSlider(10f);

            yield return new WaitForSeconds(0.3f);

            Assert.Less(_lifeSlider.value, initialValue);
        }

        
        
        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.Destroy(_player);
            yield return null;
        }
    }
}
