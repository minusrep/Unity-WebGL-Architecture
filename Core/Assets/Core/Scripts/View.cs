using UnityEngine;

namespace Core
{
    public abstract class View<T, S> : MonoBehaviour where T : Controller where S : Model
    {
        [SerializeField] private S model;
        protected T controller;


        private void OnEnable()
        {
            GameManager.OnControllersCreatedEvent += this.OnControllersCreated;
            GameManager.OnGameLoadedEvent += this.OnGameLoaded;
        }

        private void Update()
        {
            if (this.controller == null) return;
                this.controller.Update();
        }

        private void OnDisable()
        {
            GameManager.OnControllersCreatedEvent -= OnControllersCreated;
            GameManager.OnGameLoadedEvent -= OnGameLoaded;
        }

        private void OnControllersCreated()
        {
            this.controller = GameManager.GetController<T>();
            this.controller.Initialize<S>(this.model);
        }

        protected abstract void OnGameLoaded();
    }
}