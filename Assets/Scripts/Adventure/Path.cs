using System;
using Adventure.GameManager.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Adventure
{
    public class Path : MonoBehaviour
    {
        [BoxGroup("Box features")] [LabelText("Determine if path be locked")]
        [SerializeField]private bool isLocked =  true;
        
        [BoxGroup("Box features")] [LabelText("Does the box have a disable button?")]
        [SerializeField]private bool containsButton =  true;
        
    
        [BoxGroup("Messages")] [LabelText("Text of lock dialogue: ")]
        [SerializeField]  private string lockedText;
        
       
        [BoxGroup("Messages")] [LabelText("Text of unlock dialogue: ")] 
        [SerializeField] private string unlockedText;
        
        private string _message;
        [BoxGroup("Box")] [LabelText("Dialogue box")] [Required]
       [SerializeField]  private GameObject box;
       
       [BoxGroup("Box")][Required] [LabelText("Button of dialogue box")]
       [SerializeField] private Button disableButton;
   
       [BoxGroup("Box")] [Required] 
       [SerializeField] private TextMeshProUGUI unityText;
        
       
        
       [Inject] private IDialogueSystem _dialogueSystem;
       private PathModel _model;

       
    
        void Start()
        {
            _model = new PathModel(_dialogueSystem,lockedText,unlockedText,isLocked,containsButton);
            _model.Init(box);
            
            if (_model.ShouldSetupButton)
            {
                disableButton.onClick.AddListener(() =>
                {
                    _model.SuppressDialogue();
                    _dialogueSystem.DisableBox(box);
                });
            }
            else disableButton.gameObject.SetActive(false);
          
        }


      
        
        private void OnTriggerStay2D(Collider2D other)
        {
          if (other.CompareTag("Player"))   _model.ShowDialogue(unityText, box);
        }

        
        private void OnTriggerExit2D(Collider2D other)
        {
           

            if (other.CompareTag("Player"))
            {
                _model.VerifyEnabledMessage();
                _model.ResetDialogue(box);
            }
           

        }
    }
}
