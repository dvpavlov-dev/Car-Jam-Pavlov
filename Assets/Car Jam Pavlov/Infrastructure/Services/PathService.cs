using System.Collections.Generic;
using UnityEngine;

namespace Car_Jam_Pavlov.Infrastructure.Services
{
    public class PathService : MonoBehaviour
    {
        [SerializeField] private List<Transform> _waypoints;

        public List<Transform> GetPath()
        {
            return _waypoints;
        }
    }
}
