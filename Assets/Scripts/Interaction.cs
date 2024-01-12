using System;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public RaycastController raycastController;
    public float raycastDistance = 3f; // длина луча
    public LineRenderer lineRenderer; // компонент LineRenderer для визуализации луча
    public Door[] doors; // Массив дверей

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (raycastController.PerformRaycast(out hit, raycastDistance))
            {
                Debug.Log("Пересеченный объект: " + hit.collider.gameObject.name);

                // Проверяем, является ли объект дверью
                Door door = hit.collider.GetComponent<Door>();

                // Если это дверь и она содержится в массиве дверей, то открываем/закрываем её
                if (door != null && Array.Exists(doors, d => d == door))
                {
                    door.SwitchDoorState();
                }

                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.point);
            }
        }
    }
}