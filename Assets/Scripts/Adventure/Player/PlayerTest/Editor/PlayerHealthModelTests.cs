using Adventure.Player.Model;
using NUnit.Framework;

namespace Adventure.Player.PlayerTest.Editor
{
    public class PlayerHealthModelTests
    {
        private PlayerHealthModel model;
        [SetUp]
        public void Setup()
        {
            model = new PlayerHealthModel();
        }
        [Test]
        public void SetInitialConfig()
        {
        
            model.SetPlayerHealthModel(100);
            Assert.AreEqual(100, model.maxLife);
            Assert.AreEqual(100, model.currentLife);
        }
        [Test]
        [TestCase(10)]
        [TestCase(30)]
        [TestCase(50)]
        
        public void ApplyDamage_ReducesLife(float damage)
        {
           
            model.SetPlayerHealthModel(100);
            model.ApplyDamage(damage);
            if(damage == 10)
                Assert.AreEqual(90, model.currentLife);
           if(damage == 30)
               Assert.AreEqual(70, model.currentLife);
           if(damage == 50)
               Assert.AreEqual(50, model.currentLife);
          
           
        }
        [Test]
        public void ApplyDamage_DoesNotGoBelowZero()
        {
           
            model.SetPlayerHealthModel(50);
            model.ApplyDamage(100);
            Assert.AreEqual(0, model.currentLife);
        }
        
        [Test]
        public void IsDead_ReturnsTrueWhenLifeIsZeroOrLess()
        {
          
            model.SetPlayerHealthModel(100);
            model.ApplyDamage(105);
            Assert.IsTrue(model.isDead);
        }
        
    }
}
