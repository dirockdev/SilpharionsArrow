using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    ItemData itemData;

    public int onGridPositionX;
    public int onGridPositionY;
    public ItemData ItemData { get => itemData; set => itemData = value; }

    internal void Set(ItemData itemData)
    {
        this.ItemData=itemData;

        GetComponent<Image>().sprite = itemData.texture;
        Vector2 size = new Vector2();
        size.x = itemData.width*ItemGrid.tileSizeWidth;
        size.y = itemData.height*ItemGrid.tileSizeHeight;

        GetComponent<RectTransform>().sizeDelta = size;

    }

}
