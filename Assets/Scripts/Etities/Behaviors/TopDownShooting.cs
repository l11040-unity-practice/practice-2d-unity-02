using UnityEngine;
using Random = UnityEngine.Random;

public class TopDownShooting : MonoBehaviour
{
    private TopDownController controller;
    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 aimDirection = Vector2.right;

    public GameObject TestPrefab;

    private ObjectPool pool;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
        pool = GameObject.FindObjectOfType<ObjectPool>();
    }
    void Start()
    {
        controller.OnAttackEvent += OnShoot;
        controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        aimDirection = direction;
    }

    private void OnShoot(AttackSO attackSO)
    {
        RangedAttackSO rangedAttackSO = attackSO as RangedAttackSO;
        if (rangedAttackSO == null) return;

        float projectilesAngleSpace = rangedAttackSO.MultipleProjectilesAngle;
        int numberOfProjectilesPerShot = rangedAttackSO.numberOfProjectilesPerShot;

        float minAngle = -((numberOfProjectilesPerShot - 1) / 2f * projectilesAngleSpace);
        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + i * projectilesAngleSpace;
            float randomSpread = Random.Range(-rangedAttackSO.spread, rangedAttackSO.spread);
            angle += randomSpread;
            CreateProjectile(rangedAttackSO, angle);
        }

    }

    private void CreateProjectile(RangedAttackSO rangedAttackSO, float angle)
    {
        GameObject obj = pool.SpawnFromPool(rangedAttackSO.bulletNameTag);
        obj.transform.position = projectileSpawnPosition.position;
        ProjectileController attackController = obj.GetComponent<ProjectileController>();

        attackController.InitializeAttack(RotateVector2(aimDirection, angle), rangedAttackSO);
    }

    private static Vector2 RotateVector2(Vector2 v, float angle)
    {
        return Quaternion.Euler(0f, 0f, angle) * v;
    }
}
