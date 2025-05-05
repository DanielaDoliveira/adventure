using TMPro;
using UnityEngine;

namespace Adventure.GameManager.Interfaces
{
    public interface IDialogueSystem
    {
        public TextMeshProUGUI UnityText { get; set; }
        public string message { get; set; }
        void SetText(TextMeshProUGUI unityText, string text);
        
        void EnableBox(GameObject box);
        void DisableBox(GameObject box);
    }
} 