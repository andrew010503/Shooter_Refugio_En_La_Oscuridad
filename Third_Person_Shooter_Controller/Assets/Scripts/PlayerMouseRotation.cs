using UnityEngine;

public class PlayerMouseRotation : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Oculta y bloquea el cursor
    }

    void Update()
    {
        // Rotación horizontal con el mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotationY += mouseX;

        // Aplicar rotación solo en el eje Y
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }
}
