using System;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action OnAttackEvent;
    protected bool IsAttacking { get; set; }
    private float timeSinceLastAttack = float.MaxValue;
    protected CharacterStatsHandler stats { get; private set; }
    protected virtual void Awake()
    {
        stats = GetComponent<CharacterStatsHandler>();
    }
    private void Update()
    {
        HandleAtteckDelay();
    }

    private void HandleAtteckDelay()
    {
        // TODO :: MAGIC NUMBER 수정
        if (timeSinceLastAttack < stats.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        else if (IsAttacking)
        {
            timeSinceLastAttack = 0f;
            CallAttackEvent();
        }
    }

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
    private void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }
}