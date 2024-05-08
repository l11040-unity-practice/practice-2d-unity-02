using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float Speed = 15;
    private Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rigid.velocity = transform.right * Speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "WallCollision")
        {
            Destroy(gameObject);
        }
    }
}
