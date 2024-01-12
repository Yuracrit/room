using UnityEngine;

public class RaycastController : MonoBehaviour
{
    public Camera playerCamera;

    public bool PerformRaycast(out RaycastHit hit, float raycastDistance)
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, raycastDistance);
    }

    public Ray CreateRay()
    {
        return new Ray(playerCamera.transform.position, playerCamera.transform.forward);
    }
}

