using UnityEngine;

namespace Player.Singletons
    {
        public class PlayerSingleton: MonoBehaviour
        { 
            public static PlayerSingleton Instance;
         /* Verify if player can use attack  */  public static bool CanAttack;
        /* looking at player attack  */ public static bool IsAttacking;
        /* Get last player direction */ public static Vector2 LastDirection;
       /* Get Current Player Movement */ public static Vector2 Movement;
       /* Get Player Physics */ public static Rigidbody2D Rigidbody;
        public static Animator Animator;
        
            private void Awake()
            {
                if(Instance == null) Instance = this;
                else Destroy(gameObject);
                DontDestroyOnLoad(gameObject);
            }
            
        }
    }
