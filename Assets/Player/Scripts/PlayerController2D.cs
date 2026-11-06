
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
        AimAtMouse();
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

    private void AimAtMouse()
    {
        if (mainCamera == null)
            return;

        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(
            Input.mousePosition
        );

        Vector2 direction = (Vector2)(mousePosition - transform.position);

        if (direction.sqrMagnitude > 0.001f)
        {
            aimDirection = direction.normalized;
            transform.up = aimDirection;
        }
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
