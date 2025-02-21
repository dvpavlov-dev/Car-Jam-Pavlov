using UnityEngine;

namespace Car_Jam_Pavlov.Create_level.Scripts
{
    public class CarMovementComponent : MonoBehaviour
    {
        public bool TryMove()
        {
            Debug.Log("Car Movement Component");
            return false;
        }

        private bool CheckPath()
        {
            return false;
        }
    }
}
