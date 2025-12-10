using UnityEngine;

public class MoveToPlayerNode : BTNode
{
    private Transform _enemy;
    private Transform _player;
    private float _speed;

    public MoveToPlayerNode(Transform enemy, Transform player, float speed)
    {
        _enemy = enemy;
        _player = player;
        _speed = speed;
    }

    public override NodeState Evaluate()
    {
        if (_enemy == null || _player == null)
        {
            _state = NodeState.Failure;
            return _state;
        }

        Vector3 dir = (_player.position - _enemy.position);
        dir.y = 0f;

        if (dir.magnitude < 0.1f)
        {
            _state = NodeState.Success;
            return _state;
        }

        dir.Normalize();
        _enemy.position += dir * _speed * Time.deltaTime;

        _state = NodeState.Running;
        return _state;
    }
}
