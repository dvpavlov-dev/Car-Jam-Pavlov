using System.Collections.Generic;
using UnityEngine;

namespace Car_Jam_Pavlov.Create_level.Scripts
{
    public class GenerateParking : MonoBehaviour
    {
        [SerializeField] private GameObject _platformView;
    
        [SerializeField, Range(1,4)] private int _widthCar = 4;
        [SerializeField, Range(1,3)] private int _heightCar = 1;
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
                    float width = isHorizontal ? _widthCar : _heightCar;
                    float height = isHorizontal ? _heightCar : _widthCar;
                
                    if (CanPlaceRectangle(x, z, width, height))
                    {
                        PlaceRectangle(x, z, width, height);
                    }
                    else if(CanPlaceRectangle(x, z, height,width))
                    {
                        PlaceRectangle(x, z, height,width);
                    }
                }
            }
        }

        private bool CanPlaceRectangle(int startX, int startZ, float width, float height)
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

        private void PlaceRectangle(int startX, int startZ, float width, float height)
        {
            for (int x = startX; x < startX + width; x++)
            {
                for (int z = startZ; z < startZ + height; z++)
                {
                    _occupied[x, z] = true;
                }
            }
        
            GameObject rectangle = Instantiate(_platformView);
            _cars.Add(rectangle);

            var platform = rectangle.GetComponent<PlatformView>();
            bool isHorizontal = width > height;
            platform.GenerateModel(isHorizontal, _spacing);
            platform.SetScaleForFrame(width, height);

            rectangle.transform.position = new Vector3(startX + width / 2, 0, startZ + height / 2);
        }
    }
}

