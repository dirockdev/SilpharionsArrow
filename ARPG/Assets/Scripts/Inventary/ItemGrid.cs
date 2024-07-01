
using System;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public const float tileSizeWidth = 22.4f;
    public const float tileSizeHeight = 22.175f;
    RectTransform rectTransform;
    Canvas rootCanvas;

    InventoryItem[,] inventoryItemSlot;

    const int gridSizeWidth = 17;
    const int gridSizeHeight= 8;

    private void Start()
    {
        rootCanvas = GetComponentInParent<Canvas>();
        rectTransform=GetComponent<RectTransform>();

        InitSlot(gridSizeWidth, gridSizeHeight);

        
    }

    private void InitSlot(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size=new Vector2(width*tileSizeWidth, height*tileSizeHeight);
        rectTransform.sizeDelta = size;


    }
    Vector2 positionOnTheGrid= new Vector2 ();
    Vector2Int tileGridPosition= new Vector2Int();
    public Vector2Int GetTileGridPosition(Vector2 mousepos)
    {
        positionOnTheGrid.x = mousepos.x-rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousepos.y;


        tileGridPosition.x = (int)(positionOnTheGrid.x / (tileSizeWidth * rootCanvas.scaleFactor));
        tileGridPosition.y = (int)(positionOnTheGrid.y / (tileSizeHeight * rootCanvas.scaleFactor));
        
        return tileGridPosition;
    }

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {
        if(BoundryCheck(posX,posY,inventoryItem.ItemData.width, inventoryItem.ItemData.height)==false) { return false; }
        if(OverlapCheck(posX,posY,inventoryItem.ItemData.width, inventoryItem.ItemData.height, ref overlapItem) == false)
        {
            overlapItem = null;
            return false;
        }

        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform> ();
        rectTransform.SetParent(this.rectTransform);

        for (int x = 0; x < inventoryItem.ItemData.width; x++)
        {
            for (int y = 0; y < inventoryItem.ItemData.height; y++)
            {
                inventoryItemSlot[posX+x, posY+y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;

        Vector2 position=new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth*inventoryItem.ItemData.width / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight*inventoryItem.ItemData.height / 2);

        rectTransform.localPosition=position;

        return true;
    }

    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventoryItemSlot[posX + x, posY + y] != null)
                {
                    if(overlapItem==null)overlapItem = inventoryItemSlot[posX + x, posY + y];
                    else
                    {
                        if(overlapItem!= inventoryItemSlot[posX + x, posY + y])
                        {
                            return false;

                        }
                    }
                }
            }
        }
        return true;
    }

    internal InventoryItem PickUpItem(int x, int y)
    {


        InventoryItem toReturn = inventoryItemSlot[x, y];

        if (toReturn == null) { return null; }
        CleanGridReference(toReturn);
        return toReturn;
    }

    private void CleanGridReference(InventoryItem item)
    {
        for (int ix = 0; ix < item.ItemData.width; ix++)
        {
            for (int iy = 0; iy < item.ItemData.height; iy++)
            {
                inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;
            }
        }
    }

    bool PositionCheck(int posX,int posY)
    {
        if(posX<0|| posY<0) return false;
        if(posX>=gridSizeWidth|| posY>=gridSizeHeight) return false;
        return true;
    }

    bool BoundryCheck(int posX, int posY, int width, int height)
    {
        if(PositionCheck(posX,posY)==false) return false;
        posX += width-1;
        posY += height-1;
            
        if (PositionCheck(posX, posY) == false) {  return false; }
        
        return true;
    }
}
