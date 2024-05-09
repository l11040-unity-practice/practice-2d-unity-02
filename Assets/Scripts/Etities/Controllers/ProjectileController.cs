using UnityEngine;

public class ProjectileController : TopDownController
{
  [SerializeField] private LayerMask levelCollisionLayer;
  private bool isReady;
  private float currentDuration;
  private bool fxOnDestroy = true;
  private RangedAttackSO attackData;
  private Rigidbody2D rigidbody;
  private SpriteRenderer spriteRenderer;
  private TrailRenderer trailRenderer;
  private Vector2 direction;

  protected override void Awake()
  {
    base.Awake();
    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    rigidbody = GetComponent<Rigidbody2D>();
    trailRenderer = GetComponentInChildren<TrailRenderer>();
  }

  private void Update()
  {
    if (!isReady)
    {
      return;
    }

    currentDuration += Time.deltaTime;

    if (currentDuration > attackData.duration)
    {
      DestroyProjectile(transform.position, false);
    }

    rigidbody.velocity = transform.right * attackData.speed;
  }


  public void InitializeAttack(Vector2 direction, RangedAttackSO attackData)
  {
    this.attackData = attackData;
    this.direction = direction;

    UpdateProjectileSprite();
    trailRenderer.Clear();
    currentDuration = 0;
    spriteRenderer.color = attackData.projectileColor;

    transform.right = this.direction;

    isReady = true;
  }

  private void UpdateProjectileSprite()
  {
    transform.localScale = Vector2.one * attackData.size;
  }

  private void DestroyProjectile(Vector3 position, bool createFx)
  {
    if (createFx)
    {
      // TODO : ParticleSystem에 대해서 배우고, 무기 NameTag에 해당하는 FX 가져오기
    }
    gameObject.SetActive(false);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer))
    {
      Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * 0.2f;
      DestroyProjectile(destroyPosition, fxOnDestroy);
    }
    else if (IsLayerMatched(attackData.target.value, collision.gameObject.layer))
    {
      // TODO : 데미지 주기
      DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
    }
  }

  private bool IsLayerMatched(int value, int layer)
  {
    return value == (value | 1 << layer);
  }
}