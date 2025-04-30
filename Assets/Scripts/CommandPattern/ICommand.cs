
    using UnityEngine;

    namespace CommandPattern
    {
        public interface ICommand
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="direction"> Player position</param>
            void Execute(Vector2 direction);
        }
    }
