using Car_Jam_Pavlov.Player.Scripts;
using UnityEngine;

namespace Car_Jam_Pavlov.Create_level.Scripts
{
    [RequireComponent(typeof(CarMovementComponent), typeof(Interactable))]
    public class CarController : MonoBehaviour
    {
        private CarMovementComponent _carMovementComponent;
        private IInteractable _interactable;
    
        void Start()
        {
            _carMovementComponent = GetComponent<CarMovementComponent>();
            _interactable = GetComponent<IInteractable>();

            _interactable.OnInteract += MoveCar;
        }
        
        private void MoveCar()
        {
            _carMovementComponent.Move();
        }

        private void OnDestroy()
        {
            _interactable.OnInteract -= MoveCar;
        }
    }
}
