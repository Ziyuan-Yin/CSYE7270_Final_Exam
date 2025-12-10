using System.Collections.Generic;

public class SequenceNode : BTNode
{
    private List<BTNode> _children;

    public SequenceNode(List<BTNode> children)
    {
        _children = children;
    }

    public override NodeState Evaluate()
    {
        bool anyRunning = false;

        foreach (var child in _children)
        {
            var result = child.Evaluate();

            if (result == NodeState.Failure)
            {
                _state = NodeState.Failure;
                return _state;
            }

            if (result == NodeState.Running)
            {
                anyRunning = true;
            }
        }

        _state = anyRunning ? NodeState.Running : NodeState.Success;
        return _state;
    }
}
