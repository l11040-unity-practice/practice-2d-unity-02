using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;
    private CharacterStatsHandler statsHandler;
    private float tiemSinceLastChange = float.MaxValue;
    private bool isAttacked = false;
    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public float MaxHealth => statsHandler.CurrentStat.maxHealth;
    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        statsHandler = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if (isAttacked && tiemSinceLastChange < healthChangeDelay)
        {
            tiemSinceLastChange += Time.deltaTime;
            if (tiemSinceLastChange >= healthChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();
                isAttacked = false;
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (tiemSinceLastChange < healthChangeDelay)
        {
            // 공격하지 않고 끝나는 상황
            return false;
        }

        tiemSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (CurrentHealth <= 0f)
        {
            CallDeath();
            return true;
        }
        if (change > 0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            OnDamage?.Invoke();
            isAttacked = true;
        }
        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}
