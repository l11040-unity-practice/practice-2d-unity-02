using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private TopDownController controller;
    private Rigidbody2D movementRigidbody;
    private Vector2 movementDirection = Vector2.zero;
    private CharacterStatsHandler characterStatsHandler;

    private void Awake()
    {
        //주로 내 컴포넌트에서 끝나는 행위
        controller = GetComponent<TopDownController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
        characterStatsHandler = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        controller.OnMoveEvent += Move;
    }
    private void FixedUpdate()
    {
        //물리 업데이트 관련 작성
        ApplyMovement(movementDirection);
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction;
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * characterStatsHandler.CurrentStat.speed;
        movementRigidbody.velocity = direction;
    }
}
