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
}
