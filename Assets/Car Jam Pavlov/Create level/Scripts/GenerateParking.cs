using System.Collections.Generic;
using UnityEngine;

namespace Car_Jam_Pavlov.Create_level.Scripts
{
    public class GenerateParking : MonoBehaviour
    {
        [SerializeField] private GameObject _parkingSpace;

        private const int WIDTH_CAR = 3;
        private const int HEIGHT_CAR = 1;
        [SerializeField] private float _spacing = 0.3f;

        [SerializeField] private int _gridWidth = 10;
        [SerializeField] private int _gridHeight = 10;

        private bool[,] _occupied;
        private readonly List<GameObject> _cars = new();
    
        private void Start()
        {
            _occupied = new bool[_gridWidth, _gridHeight];
            FillGrid();
        }

        [ContextMenu("Refresh Grid")]
        public void RegenerateGrid()
        {
            foreach (GameObject car in _cars)
            {
                Destroy(car);
            }
        
            _cars.Clear();
            _occupied = new bool[_gridWidth, _gridHeight];
        
            FillGrid();
        }

        private void FillGrid()
        {
            for (int z = 0; z < _gridHeight; z++)
            {
                for (int x = 0; x < _gridWidth; x++)
                {
                    bool isHorizontal = Random.Range(0, 2) == 0;
                    float width = isHorizontal ? WIDTH_CAR : HEIGHT_CAR;
                    float height = isHorizontal ? HEIGHT_CAR : WIDTH_CAR;
                
                    if (CanPlaceParkingSpace(x, z, width, height))
                    {
                        PlaceParkingSpace(x, z, width, height);
                    }
                    else if(CanPlaceParkingSpace(x, z, height,width))
                    {
                        PlaceParkingSpace(x, z, height,width);
                    }
                }
            }
        }

        private bool CanPlaceParkingSpace(int startX, int startZ, float width, float height)
        {
            if (startX + width > _gridWidth || startZ + height > _gridHeight)
            {
                return false;
            }

            for (int x = startX; x < startX + width; x++)
            {
                for (int z = startZ; z < startZ + height; z++)
                {
                    if (_occupied[x, z])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void PlaceParkingSpace(int startX, int startZ, float width, float height)
        {
            for (int x = startX; x < startX + width; x++)
            {
                for (int z = startZ; z < startZ + height; z++)
                {
                    _occupied[x, z] = true;
                }
            }
        
            GameObject parkingSpaceObj = Instantiate(_parkingSpace);
            _cars.Add(parkingSpaceObj);

            ParkingSpace parkingSpace = parkingSpaceObj.GetComponent<ParkingSpace>();
            bool isHorizontal = width > height;
            parkingSpaceObj.transform.position = new Vector3(startX + width / 2, 0, startZ + height / 2);
            parkingSpace.GenerateCar(isHorizontal, _spacing);
        }
    }
}

