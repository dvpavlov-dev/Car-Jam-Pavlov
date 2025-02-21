using UnityEngine;

namespace Car_Jam_Pavlov.Create_level.Scripts
{
    public class ParkingSpace : MonoBehaviour
    {
        [SerializeField] private GameObject _carPrefab;
        [SerializeField] private GameObject _smallBusPrefab;
        [SerializeField] private GameObject _bigBusPrefab;

        public void GenerateCar(bool isHorizontal, float spacing)
        {
            TypeCar typeCar = (TypeCar)Random.Range(0, 3);

            GameObject car = typeCar switch
            {
                TypeCar.CAR => Instantiate(_carPrefab, transform.position, Quaternion.identity),
                TypeCar.SMALL_BUS => Instantiate(_smallBusPrefab, transform.position, Quaternion.identity),
                TypeCar.BIG_BUS => Instantiate(_bigBusPrefab, transform.position, Quaternion.identity),
                _ => Instantiate(_carPrefab, transform.position, Quaternion.identity)
            };
        
            bool reverseDirection = Random.Range(0, 2) == 0;
            car.transform.rotation = Quaternion.Euler(0, isHorizontal ? 
                reverseDirection ? 90 : -90 : 
                reverseDirection ? 0 : 180, 0);
            car.transform.localScale = new Vector3(car.transform.localScale.x - spacing, 1, car.transform.localScale.z - spacing);
        }

        private enum TypeCar
        {
            CAR = 0,
            SMALL_BUS = 1,
            BIG_BUS = 2,
        }
    }
}
