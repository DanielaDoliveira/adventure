


    public interface IPlayerAnimations
    {
        /// <summary>
        ///  Set Player Animation
        /// </summary>
        /// <param name="state"> State of Player </param>
        /// <returns></returns>
        public void SetAnimation(PLAYER_STATE state);

        /// <summary>
        /// Set all Animation Layers to zero 
        /// </summary>
        public void SetAllLayerWeights(LAYER_TYPE layerType);

        /// <summary>
        /// Identify Layer Animation by PlayerState
        /// </summary>
        public LAYER_TYPE IdentifyLayerName(PLAYER_STATE state);
    }
