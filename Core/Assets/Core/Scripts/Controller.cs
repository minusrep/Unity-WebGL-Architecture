namespace Core
{
    public abstract class Controller 
    {
        public abstract bool isInitialized { get; set; }

        public abstract void Initialize<T>(T settings) where T : Model;
        public abstract void Update();
    }
}