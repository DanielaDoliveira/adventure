using Adventure.GameManager.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Adventure.GameManager
{
    public class DialogueSystem : IDialogueSystem
    {
      

        public string message { get; set; }
     

        public TextMeshProUGUI UnityText { get; set; }
        public void SetText(TextMeshProUGUI unityText, string text)=>unityText.text = text;
            
        

        public void EnableBox(GameObject box)=> box.SetActive(true);
        

        public void DisableBox(GameObject box)=>box.SetActive(false);
        
    }
}
