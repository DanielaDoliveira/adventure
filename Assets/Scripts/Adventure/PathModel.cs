using Adventure.GameManager.Interfaces;
using TMPro;
using UnityEngine;

namespace Adventure
{
    public class PathModel
    {
        private readonly IDialogueSystem _dialogueSystem;
        private readonly string _lockedText;
        private readonly string _unlockedText;
        private readonly bool _isLocked;
        private readonly bool _containsButton;

        private bool _showContent;
        private bool _dialogueSuppressed;
        private bool _canEnableMessage  = true;

        public PathModel(IDialogueSystem dialogueSystem, string lockedText, string unlockedText, bool isLocked, bool containsButton)
        {
            _dialogueSystem = dialogueSystem;
            _lockedText = string.IsNullOrWhiteSpace(lockedText) ? "You need a key to proceed this path" : lockedText;
            _unlockedText = string.IsNullOrWhiteSpace(unlockedText) ? "The key unlock the path..." : unlockedText;
            _isLocked = isLocked;
            _containsButton = containsButton;
        }

        public bool ShouldSetupButton => _containsButton;
        public void SuppressDialogue() => (_dialogueSuppressed, _showContent) = (true, false);

        public void ShowDialogue(TextMeshProUGUI unityText, GameObject box)
        {
            if (_dialogueSuppressed || _showContent) return;
            if (!_canEnableMessage) return;
            _showContent = true;
            string msg = _isLocked ? _lockedText : _unlockedText;
            _dialogueSystem.ShowMessage(unityText, msg);
            _dialogueSystem.EnableBox(box);
        }

        public void ResetDialogue(GameObject box)
        {
            _showContent = false;
            _dialogueSuppressed = false;
            _dialogueSystem.DisableBox(box);
        }

        public void VerifyEnabledMessage()
        {
            if (!_isLocked) _canEnableMessage = false;
            
        }
        public void Init(GameObject box)
        {
            _dialogueSystem.HasDisableButton = _containsButton;
            _dialogueSystem.DisableBox(box);
        }
    }
}
