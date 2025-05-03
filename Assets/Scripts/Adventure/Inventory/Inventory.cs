using UnityEngine;

public class Inventory : MonoBehaviour
{

    public Item[] items;
    public GameObject mouseItem;

    public void DragItem(GameObject button)
    {
        mouseItem = button;
        mouseItem.transform.position = Input.mousePosition;
    }

    public void DropItem(GameObject button)
    {
        if (mouseItem != null)
        {
            if (button.name.Equals("Drop"))
            {
                DropButton();
            }

            SwitchBetweenItems(button);

        }
    }

    void DropButton()
    {
        int pos = int.Parse(mouseItem.name);
        Instantiate(items[pos].prefab, Vector3.zero, Quaternion.identity);
    }

    void SwitchBetweenItems(GameObject button)
    {
        Transform aux = mouseItem.transform.parent;
        mouseItem.transform.SetParent(button.transform.parent);
        button.transform.SetParent(aux);


    }
}
