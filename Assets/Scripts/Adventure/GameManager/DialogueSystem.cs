using Adventure.GameManager.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Adventure.GameManager
{
    public class DialogueSystem : IDialogueSystem
    {
     
        public bool HasDisableButton { get; set; } = false;
        
       
        public void ShowMessage(TextMeshProUGUI unityText, string message)=>unityText.text = message;
            
        

        public void EnableBox(GameObject box)=> box.SetActive(true);
        

        public void DisableBox(GameObject box)=>box.SetActive(false);
     
    }
}
