
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Estadísticas")]
    [SerializeField] private PlayerStatsSO playerStats;

    private Rigidbody2D rb;
    private Camera mainCamera;

    private Vector2 movement;
    private Vector2 aimDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        ReadMovement();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void ReadMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontal, vertical).normalized;
    }


    private void MovePlayer()
    {
        if (playerStats == null)
            return;

        rb.MovePosition(
            rb.position + movement * playerStats.moveSpeed * Time.fixedDeltaTime
        );
    }
}
