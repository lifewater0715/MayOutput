using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Controller : MonoBehaviour
{
    [Range(1, 10)]
    public int Inventory_Horizontal;
    [Range(1, 10)]
    public int Inventory_Vertical;

    public int num= 1;
    public int SlotPosX = 1;
    public int SlotPosY = 1;
    public List<GameObject> Slots = new List<GameObject>();

    public float Inventory_HorizontalPos = 0f;
    private float Hpos;
    public float Inventory_VerticalPos = 0f;

    public float Inventory_HorizontalPos_increase = 0f;
    public float Inventory_VerticalPos_increase = 0f;

    public GameObject slot;
    public bool OnSlot;

    public Item_Junk Item;

    void Start()
    {
        Hpos = Inventory_HorizontalPos;
        Slots.Add(null); //리스트에 추가(1부터 보기위함)
    }
    void Update()
    {
        Inventory_HorizontalPos_increase = slot.GetComponent<RectTransform>().sizeDelta.x * slot.GetComponent<Transform>().localScale.x;
        Inventory_VerticalPos_increase = slot.GetComponent<RectTransform>().sizeDelta.y * slot.GetComponent<Transform>().localScale.y;

        if(OnSlot == false)
        {
            CreateSlot();
            OnSlot = true;
        }
    }
    public void CreateSlot()
    {
        var H = 0f;
        var V = 0f;
        for (V = 0; V < Inventory_Vertical; V++)
        {
            Inventory_VerticalPos = Inventory_VerticalPos - Inventory_VerticalPos_increase;
            SlotPosX = 0;

            for (H = 0; H < Inventory_Horizontal; H++)
            {
                
                SlotPosX++; // 인벤토리 번호
                GameObject Slot = GameObject.Instantiate(slot);

                Slot.transform.parent = gameObject.transform;
                Slot.transform.localScale = new Vector3(2f, 2f, 1f);
                Slot.transform.localPosition = new Vector3(Inventory_HorizontalPos, Inventory_VerticalPos, 0f);
                //Slot Pos X,Y
                Slot.GetComponent<Slot>().num = num;
                Slot.GetComponent<Slot>().PosX = SlotPosX;
                Slot.GetComponent<Slot>().PosY = SlotPosY;

                num++; // 인벤토리 번호

                Slots.Add(Slot); //리스트에 추가

                Inventory_HorizontalPos = Inventory_HorizontalPos + Inventory_HorizontalPos_increase;
            }
            Inventory_HorizontalPos = Hpos;
            SlotPosY++; // 인벤토리 번호
        }
    }
    public void GetItem()
    {

    }
    public void MoveItem()
    {

    }
}
