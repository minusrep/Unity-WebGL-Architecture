using System;
using System.Collections.Generic;

namespace Core
{
    public sealed class Game 
    {
        public bool isInitialized
        {
            get
            {
                var value = true;
                foreach (var controller in this.controllersMap)
                    if (!controller.Value.isInitialized) value = false;
                return value;
            }
        }
        
        private Dictionary<Type, Controller> controllersMap;


        public Game()
        {
            this.controllersMap = new Dictionary<Type, Controller>();
        }

        public T GetController<T>() where T : Controller
        {
            this.controllersMap.TryGetValue(typeof(T), out var founded);
            return (T) founded;
        }
    }
}