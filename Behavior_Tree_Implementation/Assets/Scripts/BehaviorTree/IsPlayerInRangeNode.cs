using UnityEngine;

public class IsPlayerInRangeNode : BTNode
{
    private Transform _enemy;
    private Transform _player;
    private float _range;

    public IsPlayerInRangeNode(Transform enemy, Transform player, float range)
    {
        _enemy = enemy;
        _player = player;
        _range = range;
    }

    public override NodeState Evaluate()
    {
        if (_enemy == null || _player == null)
        {
            _state = NodeState.Failure;
            return _state;
        }

        float dist = Vector3.Distance(_enemy.position, _player.position);
        _state = dist <= _range ? NodeState.Success : NodeState.Failure;
        return _state;
    }
}
