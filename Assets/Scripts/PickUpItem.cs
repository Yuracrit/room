using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Transform inventoryHolder;
    public RaycastController raycastController; // Ссылка на точку инвентаря
    public ThrowItem throwItem;

    private InteractableInventory currentHeldItem; // Ссылка на текущий поднятый предмет
    private Rigidbody itemRigidbody;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (raycastController.PerformRaycast(out hit, 2f))
            {
                InteractableInventory item = hit.collider.GetComponent<InteractableInventory>();
                if (item != null)
                {
                    itemRigidbody = item.GetComponent<Rigidbody>();
                    if (itemRigidbody != null)
                    {
                        // Отключаем физику объекта
                        itemRigidbody.isKinematic = true;
                        itemRigidbody.useGravity = false;
                    }

                    RemoveItemFromInventoryHolder(inventoryHolder);

                    // Если уже был предмет в руке, убираем его
                    if (currentHeldItem != null)
                    {
                        DropCurrentItem();
                    }

                    // Устанавливаем позицию нового предмета в руку
                    item.transform.position = inventoryHolder.position;
                    item.transform.rotation = inventoryHolder.rotation;
                    item.transform.parent = inventoryHolder;

                    // Сохраняем ссылку на новый предмет
                    currentHeldItem = item;


                    throwItem.HoldItem(currentHeldItem.transform);
                }
            }
        }
    }

    void RemoveItemFromInventoryHolder(Transform holder)
    {
        if (holder.childCount > 0)
        {
            Transform item = holder.GetChild(0);
            AddPhysicsToItem(item);
            item.SetParent(null);
        }
    }

    void AddPhysicsToItem(Transform item)
    {
        Rigidbody itemRigidbody = item.GetComponent<Rigidbody>();
        if (itemRigidbody != null)
        {
            itemRigidbody.isKinematic = false;
            itemRigidbody.useGravity = true;
        }
    }

    void DropCurrentItem()
    {
        // Отделяем текущий предмет от руки
        currentHeldItem.transform.parent = null;
        AddPhysicsToItem(currentHeldItem.transform);
        currentHeldItem = null;
    }
}