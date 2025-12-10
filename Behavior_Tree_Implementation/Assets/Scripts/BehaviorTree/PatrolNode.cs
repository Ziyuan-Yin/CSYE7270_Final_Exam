using UnityEngine;

public class PatrolNode : BTNode
{
    private Transform _enemy;
    private Transform[] _waypoints;
    private float _speed;
    private int _currentIndex = 0;

    private const float ARRIVE_DISTANCE = 0.2f;

    public PatrolNode(Transform enemy, Transform[] waypoints, float speed)
    {
        _enemy = enemy;
        _waypoints = waypoints;
        _speed = speed;
    }

    public override NodeState Evaluate()
    {
        if (_waypoints == null || _waypoints.Length == 0 || _enemy == null)
        {
            _state = NodeState.Failure;
            return _state;
        }

        Transform target = _waypoints[_currentIndex];
        Vector3 dir = target.position - _enemy.position;
        dir.y = 0f;

        if (dir.magnitude <= ARRIVE_DISTANCE)
        {
            _currentIndex = (_currentIndex + 1) % _waypoints.Length;
            _state = NodeState.Success;   // 本次“到达”成功
            return _state;
        }

        dir.Normalize();
        _enemy.position += dir * _speed * Time.deltaTime;

        _state = NodeState.Running;
        return _state;
    }
}
