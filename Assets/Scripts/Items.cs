using UnityEngine;

public class Items : MonoBehaviour
{
    public RaycastController raycastController;
    public float raycastDistance = 2f;
    public LineRenderer lineRenderer;

    private InteractableInventory lastInteractable; // Последний объект с фокусом

    void Update()
    {
        RaycastHit hit;


        if (raycastController.PerformRaycast(out hit, raycastDistance))
        {
            InteractableInventory interactable = hit.collider.GetComponent<InteractableInventory>();

            if (interactable != null)
            {
                if (interactable != lastInteractable)
                {
                    // Убираем подсветку с предыдущего объекта
                    if (lastInteractable != null)
                    {
                        lastInteractable.RemoveFocus();
                    }

                    // Устанавливаем фокус на новый объект
                    interactable.SetFocus();
                    lastInteractable = interactable;
                }
            }
            else
            {
                // Убираем подсветку, так как луч уже не попадает на объект
                if (lastInteractable != null)
                {
                    lastInteractable.RemoveFocus();
                    lastInteractable = null;
                }
            }

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // Убираем подсветку, так как луч уже не попадает на объект
            if (lastInteractable != null)
            {
                lastInteractable.RemoveFocus();
                lastInteractable = null;
            }
        }
    }
}