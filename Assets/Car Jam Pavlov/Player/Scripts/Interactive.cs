using System;
using UnityEngine;

namespace Car_Jam_Pavlov.Player.Scripts
{
    public class Interactable : MonoBehaviour, IInteractable
    {
        public Action OnInteract { get; set; }

        public void Interact()
        {
            OnInteract?.Invoke();
        }
    }
    
    public interface IInteractable
    {
        Action OnInteract { get; set; }
        public void Interact();
    }
}
