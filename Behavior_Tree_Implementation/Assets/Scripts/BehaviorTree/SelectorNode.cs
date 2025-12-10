using System.Collections.Generic;

public class SelectorNode : BTNode
{
    private List<BTNode> _children;

    public SelectorNode(List<BTNode> children)
    {
        _children = children;
    }

    public override NodeState Evaluate()
    {
        foreach (var child in _children)
        {
            var result = child.Evaluate();

            if (result == NodeState.Success)
            {
                _state = NodeState.Success;
                return _state;
            }

            if (result == NodeState.Running)
            {
                _state = NodeState.Running;
                return _state;
            }
        }

        _state = NodeState.Failure;
        return _state;
    }
}
