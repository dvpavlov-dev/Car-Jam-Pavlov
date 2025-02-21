using UnityEngine;

namespace Car_Jam_Pavlov.Create_level.Scripts
{
    public class PlatformView : MonoBehaviour
    {
        [SerializeField] private Transform _frame;

        [SerializeField] private GameObject _carPrefab;
        [SerializeField] private GameObject _smallBusPrefab;
        [SerializeField] private GameObject _bigBusPrefab;

        public void SetScaleForFrame(float width, float height)
        {
            _frame.localScale = new Vector3(width, 0.1f, height);
        }

        public void GenerateModel(bool isHorizontal, float spacing)
        {
            TypeCar typeCar = (TypeCar)Random.Range(0, 3);

            GameObject car = typeCar switch
            {
                TypeCar.CAR => Instantiate(_carPrefab, transform),
                TypeCar.SMALL_BUS => Instantiate(_smallBusPrefab, transform),
                TypeCar.BIG_BUS => Instantiate(_bigBusPrefab, transform),
                _ => Instantiate(_carPrefab, transform)
            };
        
            car.transform.rotation = Quaternion.Euler(0, isHorizontal ? 0 : 90, 0);
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
