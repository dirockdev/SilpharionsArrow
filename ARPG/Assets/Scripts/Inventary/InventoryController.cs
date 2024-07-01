using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    private ItemGrid selectedItemGrid;

    public ItemGrid SelectedItemGrid { get => selectedItemGrid; set => selectedItemGrid = value; }
    [SerializeField]
    private InputReader inputReader;


    InventoryItem selectedItem;
    InventoryItem overlapItem;

    RectTransform rectTransform;

    [SerializeField]List<ItemData> items = new List<ItemData>();
    [SerializeField]GameObject itemPrefab;
    [SerializeField]Transform canvasTransform;

    private void OnEnable()
    {
        inputReader.OnInputLeftMouse += LeftMouseInventory;

    }
    private void OnDisable()
    {
        inputReader.OnInputLeftMouse -= LeftMouseInventory;

    }


    void Update()
    {
        ItemIconDrag();
        if (SelectedItemGrid == null) return;


    }

    public void CreateRandomItem()
    {
        InventoryItem inventoryItem=Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem=inventoryItem;

        rectTransform=inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);

        int selectedItemID=Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }
    private void ItemIconDrag()
    {
        if (selectedItem != null) { rectTransform.position = MouseInput.mouseInputPosition; }
    }

    void LeftMouseInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SelectItemGrid();
        }

    }

    private void SelectItemGrid()
    {
        if (SelectedItemGrid != null)
        {
            Vector2Int tileGridPos = SelectedItemGrid.GetTileGridPosition(MouseInput.mouseInputPosition);

            if (selectedItem == null)
            {
                PickUpItem(tileGridPos);
            }
            else
            {
                PlaceItem(tileGridPos);
            }
        }
    }

    private void PlaceItem(Vector2Int tileGridPos)
    {
        bool complete=selectedItemGrid.PlaceItem(selectedItem, tileGridPos.x, tileGridPos.y, ref overlapItem);
        if (complete)
        {
            selectedItem = null;
            if (overlapItem != null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                rectTransform=selectedItem.GetComponent<RectTransform>();

            }
        
        }
    }

    private void PickUpItem(Vector2Int tileGridPos)
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPos.x, tileGridPos.y);
        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
    }
}
