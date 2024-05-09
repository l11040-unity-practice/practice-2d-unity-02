using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static GameManger Instance;
    [SerializeField] private string playerTag;

    public ObjectPool ObjectPool { get; private set; }
    public Transform Player { get; private set; }

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        Player = GameObject.FindGameObjectWithTag(playerTag).transform;
        ObjectPool = GetComponent<ObjectPool>();
    }

    private void Update()
    {

    }
}
