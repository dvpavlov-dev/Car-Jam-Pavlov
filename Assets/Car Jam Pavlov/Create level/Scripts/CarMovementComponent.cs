using System.Collections.Generic;
using Car_Jam_Pavlov.Infrastructure.Services;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Car_Jam_Pavlov.Create_level.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CarMovementComponent : MonoBehaviour
    {
        private const float RADIUS_AREA_DESTINATION_POINT = 2;
        
        private NavMeshAgent _agent;
        private int _currentWaypointIndex;
        private PathService _pathService;
        private List<Transform> _path = new();
        private Sequence _wrongPathSequence;
        private bool _animationInProgress;

        [Inject]
        private void Constructor(PathService pathService)
        {
            _pathService = pathService;

            _path = _pathService.GetPath();
        }
        
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.enabled = false;
        }

        private void FixedUpdate()
        {
            if (_agent.enabled && _agent.remainingDistance <= RADIUS_AREA_DESTINATION_POINT && !_agent.pathPending)
            {
                MoveToNextWaypoint();
            }
        }

        public void Move()
        {
            if (!_animationInProgress && Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Frame"))
                {
                    GetComponent<Collider>().enabled = false;
                    GetComponent<NavMeshObstacle>().enabled = false;
                    _agent.enabled = true;
                    _agent.SetDestination(hit.point);
                }
                else
                {
                    _wrongPathSequence = DOTween.Sequence();
                    
                    Vector3 direction = hit.point - transform.position;
                    Vector3 normalizedDirection = direction.normalized;
                    Vector3 point = transform.position + normalizedDirection * Mathf.Max(0, direction.magnitude - transform.localScale.z / 2);
                    Vector3 currentPos = transform.position;

                    _animationInProgress = true;

                    _wrongPathSequence
                        .Append(transform.DOMove(point, 0.1f))
                        .Append(transform.DOMove(currentPos, 0.3f))
                        .OnComplete(() => _animationInProgress = false);
                }
            }
        }
        
        private void MoveToNextWaypoint()
        {
            if (_currentWaypointIndex < _path.Count)
            {
                _agent.SetDestination(_path[_currentWaypointIndex].position);
                _currentWaypointIndex++;
            }
            else
            {
                _agent.enabled = false;
            }
        }
    }
}
