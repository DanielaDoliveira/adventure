


    using UnityEngine;

    public interface IPlayerAnimations
    {
        /// <summary>
        ///  Set Player Animation
        /// </summary>
        /// <param name="layer"> Animation Layer </param>
        /// <param name="direction"> Input Direction</param>
        /// <returns></returns>
        public void SetAnimationMovement(LAYER_TYPE layer, Vector2 direction );
        

        /// <summary>
        ///   /// <param name="layer">Animation Layer </param>
        /// <param name="direction"> Input Direction</param>
        /// </summary>
        public void SetAnimationIdle(LAYER_TYPE layer , Vector2 direction);

    
    }
