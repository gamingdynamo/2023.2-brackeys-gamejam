using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform _camera;
    CharacterController controller;
    [field: SerializeField] private float Gravity { get; set; } = 9.81f;
    [field: SerializeField] private LayerMask GroundMask { get; set; }
    [field: SerializeField] private bool IsGrounded { get; set; } = false;
    [field: SerializeField] private float JumpForce { get; set; } = 4f;
    [field: SerializeField] private float Sensitivity { get; set; } = 2f;
    [field: SerializeField] private float Speed { get; set; } = 1.5f;
    [field: SerializeField] private Vector3 Velocity { get; set; } = Vector3.zero;
    float camX, camY;

    private void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("PlayerCamera").transform;
        controller = GetComponent<CharacterController>();
    }

    void Look()
    {
        float mx = Input.GetAxisRaw("Mouse X");
        float my = Input.GetAxisRaw("Mouse Y");
        camX += -my * Sensitivity;
        camY += mx * Sensitivity;
        _camera.localRotation = Quaternion.Euler(camX, camY, 0f);
    }

    void Move()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        bool jmp = Input.GetAxisRaw("Jump") > 0f;
        Vector3 rht = _camera.right;
        Vector3 fwd = Vector3.Cross(rht, transform.up);
        Vector3 mov = (rht * hor + fwd * ver).normalized;
        mov *= Speed;
        mov *= Time.deltaTime;

        IsGrounded = Physics.Raycast(transform.position, -transform.up, controller.height * 0.6f, GroundMask, QueryTriggerInteraction.Ignore);

        if (!IsGrounded)
        {
            Velocity += -transform.up * Gravity * Time.deltaTime;
        }
        else if (jmp) Velocity = new Vector3(Velocity.x, JumpForce, Velocity.z);

        Vector3 mot = Velocity * Time.deltaTime;
        mot += mov;

        CollisionFlags cf = controller.Move(mot);

        if (cf == CollisionFlags.Above && Velocity.y > 0f)
        {
            Velocity = new Vector3(Velocity.x, 0f, Velocity.z);
        }
        if (cf == CollisionFlags.Below && Velocity.y < 0f)
        {
            Velocity = new Vector3(Velocity.x, 0f, Velocity.z);
        }
    }

    private void Update()
    {
        Look();
        Move();
    }
}
