


    using UnityEngine;

    public interface IPlayerAnimations
    {
        /// <summary>
        ///  Set Player Animation
        /// </summary>
        /// <param name="state"> State of Player </param>
        /// <param name="direction"> Input Direction</param>
        /// <returns></returns>
        public void SetAnimationMovement(PLAYER_STATE state, Vector2 direction );
        

        /// <summary>
        ///   /// <param name="state"> State of Player </param>
        /// <param name="direction"> Input Direction</param>
        /// </summary>
        public void SetAnimationIdle(PLAYER_STATE state, Vector2 direction);

    
    }
