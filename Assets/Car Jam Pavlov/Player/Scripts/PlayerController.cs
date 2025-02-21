using UnityEngine;

namespace Car_Jam_Pavlov.Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
            {
                if (Camera.main is {} playerCamera)
                {
                    Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit[] hits = new RaycastHit[10];

                    int raycastHitsCount = Physics.RaycastNonAlloc(ray, hits);
                    if (raycastHitsCount > 0)
                    {
                        for(int i = 0; i < raycastHitsCount; i++)
                        {
                            if (hits[i].collider.GetComponent<IInteractable>() is {} interactable)
                            {
                                interactable.Interact();
                            }
                        }
                    } 
                }
            }
        }
    }
}
