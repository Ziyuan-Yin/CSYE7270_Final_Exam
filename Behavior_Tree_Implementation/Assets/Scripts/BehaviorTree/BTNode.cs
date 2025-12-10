using UnityEngine;

public enum NodeState
{
    Success,
    Failure,
    Running
}

public abstract class BTNode
{
    protected NodeState _state;
    public NodeState State => _state;

    // 核心：每帧调用一次，返回当前节点状态
    public abstract NodeState Evaluate();
}
