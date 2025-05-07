using Adventure.GameManager.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TMPro;
using UnityEngine;

namespace Adventure.AdventureTests.Editor
{
    [TestFixture]
    public class PathModelTests
    {
        private IDialogueSystem _dialogueSystem;
        private TextMeshProUGUI _text;
        private GameObject _box;

        [SetUp]
        public void Setup()
        {
            _dialogueSystem = Substitute.For<IDialogueSystem>();
            _text = new GameObject().AddComponent<TextMeshProUGUI>();
            _box = new GameObject();
        }

        [Test]
        public void Init_SetsHasDisableButton_AndDisablesBox()
        {
            var model = new PathModel(_dialogueSystem, "", "", true, true);
            model.Init(_box);

            _dialogueSystem.Received().HasDisableButton = true;
            _dialogueSystem.Received().DisableBox(_box);
        }

        [Test]
        public void ShowDialogue_WhenLocked_ShowsLockedMessage()
        {
            var model = new PathModel(_dialogueSystem, "locked", "unlocked", true, true);
            model.ShowDialogue(_text, _box);

            _dialogueSystem.Received().ShowMessage(_text, "locked");
            _dialogueSystem.Received().EnableBox(_box);
        }

        [Test]
        public void ShowDialogue_WhenUnlocked_ShowsUnlockedMessage()
        {
            var model = new PathModel(_dialogueSystem, "locked", "unlocked", false, true);
            model.ShowDialogue(_text, _box);

            _dialogueSystem.Received().ShowMessage(_text, "unlocked");
            _dialogueSystem.Received().EnableBox(_box);
        }

        [Test]
        public void ShowDialogue_WhenSuppressed_DoesNotShowAnything()
        {
            var model = new PathModel(_dialogueSystem, "x", "y", true, true);
            model.SuppressDialogue();
            model.ShowDialogue(_text, _box);

            _dialogueSystem.DidNotReceiveWithAnyArgs().ShowMessage(default, default);
            _dialogueSystem.DidNotReceiveWithAnyArgs().EnableBox(default);
        }

        [Test]
        public void VerifyEnabledMessage_WhenUnlocked_DisablesMessage()
        {
            var model = new PathModel(_dialogueSystem, "x", "y", false, true);
            model.VerifyEnabledMessage();
            model.ShowDialogue(_text, _box);

            _dialogueSystem.DidNotReceiveWithAnyArgs().ShowMessage(default, default);
        }

        [Test]
        public void ResetDialogue_ClearsState_AndDisablesBox()
        {
            var model = new PathModel(_dialogueSystem, "x", "y", true, true);
            model.ResetDialogue(_box);

            _dialogueSystem.Received().DisableBox(_box);
        }
    }
}
