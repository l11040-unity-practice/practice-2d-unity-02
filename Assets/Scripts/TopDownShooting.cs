using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private TopDownController controller;
    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 aimDirection = Vector2.right;

    public GameObject TestPrefab;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
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

    private void OnShoot()
    {
        CreateProjectile();
    }

    private void CreateProjectile()
    {
        float rotZ = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        Instantiate(TestPrefab, projectileSpawnPosition.position, Quaternion.Euler(0, 0, rotZ));
    }
}
