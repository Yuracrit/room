using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    public float throwForce = 1f;

    private Rigidbody itemRigidbody;
    private bool isHoldingItem = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isHoldingItem)
        {
            ThrowItemAway();
        }
    }

    void ThrowItemAway()
    {
        if (itemRigidbody != null)
        {
            // Включаем физику объекта, чтобы он мог быть выброшен
            itemRigidbody.isKinematic = false;
            itemRigidbody.useGravity = true;

            // Даем объекту силу в направлении взгляда игрока
            itemRigidbody.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);

            // Отсоединяем объект от руки
            itemRigidbody.transform.SetParent(null);

            // Сбрасываем переменные
            itemRigidbody = null;
            isHoldingItem = false;
        }
    }

    // Этот метод может быть вызван из вашего существующего скрипта для установки объекта, как держащегося в руке
    public void HoldItem(Transform itemTransform)
    {
        itemRigidbody = itemTransform.GetComponent<Rigidbody>();

        if (itemRigidbody != null)
        {
            // Отключаем физику объекта в руке
            itemRigidbody.isKinematic = true;
            itemRigidbody.useGravity = false;

            // Устанавливаем родителя в руку (или в другое место, где должен держаться)
            //itemRigidbody.transform.SetParent(transform);


            isHoldingItem = true;
        }
    }
}