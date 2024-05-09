using System;
using UnityEngine;

public class TopDownRangeEnemyController : TopDownEnemyController
{
    [SerializeField][Range(0f, 100f)] private float followRange = 15f;
    [SerializeField][Range(0f, 100f)] private float shootRange = 10f;
    [SerializeField] private SpriteRenderer characterRenderer;
    private int layerMaskTarget;

    protected override void Start()
    {
        base.Start();
        layerMaskTarget = stats.CurrentStat.attackSO.target;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        float distanceTotarget = DistanceToTarget();
        Vector2 directionToTarget = DirectionToTarget();

        UpdateEnemyState(distanceTotarget, directionToTarget);
    }
    private void UpdateEnemyState(float distanceTotarget, Vector2 directionToTarget)
    {
        IsAttacking = false;
        if (distanceTotarget < followRange)
        {
            CheckIfNear(distanceTotarget, directionToTarget);
        }
    }

    private void CheckIfNear(float distanceTotarget, Vector2 directionToTarget)
    {
        if (distanceTotarget <= shootRange)
        {
            TryShootAtTarget(directionToTarget);
        }
        else
        {
            CallMoveEvent(directionToTarget);
        }
    }

    private void TryShootAtTarget(Vector2 directionToTarget)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, shootRange, layerMaskTarget);

        if (hit.collider != null)
        {
            PerformAttackAction(directionToTarget);
        }
        else
        {
            CallMoveEvent(directionToTarget);
        }
    }

    private void PerformAttackAction(Vector2 directionToTarget)
    {
        CallLookEvent(directionToTarget);
        CallMoveEvent(Vector2.zero);
        IsAttacking = true;
    }
}
