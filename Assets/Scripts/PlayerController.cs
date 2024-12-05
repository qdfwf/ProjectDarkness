using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform;

    // Параметры передвижения
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float crouchSpeed = 2.5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    // Параметры вращения камеры
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    private Vector3 velocity;
    private bool isGrounded;
    private float originalHeight;
    private float CameraOriginalHeight;
    public float crouchHeight = 1f;

    void Start()
    {
        CameraOriginalHeight = cameraTransform.localPosition.y;
        originalHeight = controller.height;
        Cursor.lockState = CursorLockMode.Locked;  // Блокируем курсор в центре экрана
    }

    void Update()
    {
        Move();
        Crouch();
        RotateCamera();
    }

public float jumpForce = 5f; // Новая переменная для прыжковой силы

void Move()
{
    isGrounded = controller.isGrounded;
    if (isGrounded && velocity.y < 0)
    {
        velocity.y = -2f;
    }

    float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    Vector3 move = transform.right * x + transform.forward * z;
    controller.Move(move * speed * Time.deltaTime);

    if (Input.GetButtonDown("Jump") && isGrounded)
    {
            // Используем jumpForce с учётом текущего масштаба времени
            //velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity) * Time.timeScale;
            velocity.y = jumpForce * -gravity;
    }

    velocity.y += gravity * Time.deltaTime;
    controller.Move(velocity * Time.deltaTime);
}





    void Crouch()
{
    if (Input.GetKeyDown(KeyCode.C))
    {
        controller.height = crouchHeight;
        cameraTransform.localPosition = new Vector3(0, crouchHeight / 2, 0);
    }
    else if (Input.GetKeyUp(KeyCode.C))
    {
        controller.height = originalHeight;
        cameraTransform.localPosition = new Vector3(0, CameraOriginalHeight, 0);
    }
}


   void RotateCamera()
{
    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime / Time.timeScale;
    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime / Time.timeScale;

    // Поворот по оси Y (вверх-вниз)
    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    // Применяем поворот к камере по оси X
    cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    // Поворот по оси X (влево-вправо) для персонажа
    transform.Rotate(Vector3.up * mouseX);
}

}
