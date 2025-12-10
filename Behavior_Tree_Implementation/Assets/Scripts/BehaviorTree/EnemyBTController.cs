using UnityEngine;
using System.Collections.Generic;

public class EnemyBTController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform[] waypoints;

    [Header("Settings")]
    public float detectRange = 5f;
    public float moveSpeed = 2f;

    private BTNode _rootNode;

    void Start()
    {
        // 1. 追击子树：如果玩家在范围内 → 朝玩家移动
        var isPlayerInRange = new IsPlayerInRangeNode(transform, player, detectRange);
        var moveToPlayer    = new MoveToPlayerNode(transform, player, moveSpeed);
        var chaseSequence   = new SequenceNode(new List<BTNode>
        {
            isPlayerInRange,
            moveToPlayer
        });

        // 2. 巡逻子树：在路点之间来回走
        var patrolNode      = new PatrolNode(transform, waypoints, moveSpeed);
        var patrolSequence  = new SequenceNode(new List<BTNode>
        {
            patrolNode
        });

        // 3. 根：先尝试追击，不行就巡逻
        _rootNode = new SelectorNode(new List<BTNode>
        {
            chaseSequence,
            patrolSequence
        });
    }

    void Update()
    {
        if (_rootNode != null)
        {
            _rootNode.Evaluate();
        }
    }

    // 可选：在 Scene 视图里画出巡逻路径和侦测范围，方便录视频时讲解
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (waypoints != null && waypoints.Length > 1)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                Transform a = waypoints[i];
                Transform b = waypoints[(i + 1) % waypoints.Length];
                if (a != null && b != null)
                {
                    Gizmos.DrawSphere(a.position, 0.15f);
                    Gizmos.DrawLine(a.position, b.position);
                }
            }
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
