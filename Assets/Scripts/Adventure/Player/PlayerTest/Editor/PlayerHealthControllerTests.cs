using Adventure.Player.Controller.PlayerHealth;
using Adventure.Player.Model;
using NUnit.Framework;

namespace Adventure.Player.PlayerTest.Editor
{
    public class PlayerHealthControllerTests
    {
        private PlayerHealthModel _model;
        private PlayerHealthController _controller;


        [SetUp]
        public void Setup()
        {
            _model = new PlayerHealthModel();
            _model.SetPlayerHealthModel(100);
            _controller = new PlayerHealthController(_model);
        }

        // A Test behaves as an ordinary method
        [Test]

        public void TakeDamage_ReducesLifeAndTriggersEvents()
        {

            var controller = new PlayerHealthController(_model);

            float? reportedLife = null;
            bool died = false;

            controller.OnLifeChanged += life => reportedLife = life;
            controller.OnPlayerDied += () => died = true;

            controller.TakeDamage(30f);

            Assert.AreEqual(70f, reportedLife);
            Assert.IsFalse(died);

            controller.TakeDamage(70f);
            Assert.IsTrue(died);
        }


        [Test]
        public void TakeDamage_TriggersOnLifeChanged()
        {
            float? notifiedLife = null;
            _controller.OnLifeChanged += newLife => notifiedLife = newLife;

            _controller.TakeDamage(25);

            Assert.AreEqual(75, notifiedLife);
        }
        
        [Test]
        public void TakeDamage_TriggersOnPlayerDied_WhenHealthZero()
        {
            bool died = false;
            _controller.OnPlayerDied += () => died = true;

            _controller.TakeDamage(100);

            Assert.IsTrue(died);
        }
        
        [Test]
        public void CurrentLife_ReflectsModel()
        {
            _controller.TakeDamage(40);
            Assert.AreEqual(60, _controller.currentLife);
        }
        [Test]
        public void MaxLife_ReflectsModel()
        {
            Assert.AreEqual(100, _controller.maxLife);
        }
        
        
    }

}
