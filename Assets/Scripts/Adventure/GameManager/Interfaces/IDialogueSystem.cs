using TMPro;
using UnityEngine;

namespace Adventure.GameManager.Interfaces
{
    public interface IDialogueSystem
    {
    
        public bool HasDisableButton { get; set; }
        void ShowMessage(TextMeshProUGUI unityText, string text);
        
        void EnableBox(GameObject box);
        void DisableBox(GameObject box);
      
    }
} 