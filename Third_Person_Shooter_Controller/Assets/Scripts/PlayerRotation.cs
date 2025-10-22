using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform cameraTransform;

    void Update()
    {
        Vector3 lookDirection = cameraTransform.forward;
        lookDirection.y = 0f; // Mantener al jugador en el plano horizontal

        if (lookDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}
