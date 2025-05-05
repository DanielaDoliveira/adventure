using System;
using Adventure.GameManager;
using Adventure.GameManager.Interfaces;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Adventure
{
    public class Path : MonoBehaviour
    {
        [LabelText("Determine if path be locked")]
        [SerializeField]private bool isLocked =  true;
        
        [BoxGroup("Messages")] [LabelText("Text of lock dialogue: ")]
     [SerializeField]  private string _lockedText;
        [BoxGroup("Messages")] [LabelText("Text of unlock dialogue: ")] 
      [SerializeField] private string _unlockedText;

        private string _message;
       [Title("Box")] [LabelText("Dialogue box")] [Required]
       [SerializeField]  private GameObject box;
       
   
       private bool _canEnableMessage = true;
       
       [Required] [LabelText("Button of dialogue box")]
       [SerializeField] private Button disableButton;

       [Inject] private IDialogueSystem _dialogueSystem;

       [Required] [SerializeField] private TextMeshProUGUI unityText;

       
    
        void Start()
        {
            
            if(_lockedText == String.Empty) _lockedText = "You need a key to proceed this path";
            if(_unlockedText == String.Empty)  _unlockedText = "The key unlock the path...";
          disableButton.onClick.AddListener(DisableBox);
          DisableBox();
        }

        void PathLocked()
        {
            _dialogueSystem.SetText(unityText, _lockedText);
            _dialogueSystem.EnableBox(box);
        }

        void DisableBox()=>_dialogueSystem.DisableBox(box);
        

        void PathUnlocked()
        {
            _dialogueSystem.SetText(unityText, _unlockedText);
            _dialogueSystem.EnableBox(box);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (_canEnableMessage)
                {
                    if(isLocked)
                    {
                        PathLocked();
                        _canEnableMessage = false;
                    }
                    else
                    {
                        _canEnableMessage = false;
                        PathUnlocked();
                    }
                }
                
            }
                
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!isLocked) _canEnableMessage = false;
            else _canEnableMessage = true;
        }
    }
}
