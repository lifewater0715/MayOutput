using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEditor;

using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ReadOnlyAttribute : PropertyAttribute
{
}
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
public class Item_Junk : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler//, IPointerClickHandler
{
    public GameObject Inventory;
    public Inventory_Controller InvCon;

    private float Min = 999; // 최솟값

    public GameObject Item_Junk_Image_OBJ;
    public Image Item_Junk_Image;

    [Header("아이템 이미지 피봇")]
    public float PivotX; //이미지 피봇
    public float PivotY;

    [Header("아이템 위치")]
    public GameObject CloseSlot; // 가까운 슬롯
    public GameObject StartSlot; // 시작 슬롯

    [Header("아이템 크기")]
    public int[] num; //아이템 크기
    public int[] SlotSizeY; //아이템 크기
    public int[] SlotSizeX; //아이템 크기

    public GameObject[] SlotSizeObj;
    public GameObject[] SlotStart;

    [Header("아이템 코드")]
    public int Item_Code = 0; //아이템 코드

    [Header("아이템 정보")]
    [ReadOnly] public string Item_Name; //아이템 이름
    [ReadOnly] public string Item_Explanation; //아이템 설명

    [Header("아이템 이미지")]
    [ReadOnly] public Sprite Item_Texture; //아이템 텍스쳐

    [Header("아이템 크기")]
    [ReadOnly] public int Horizontal_Size = 1; //가로 크기
    [ReadOnly] public int Vertical_Size = 1; //세로 크기

    void Start()
    {
        Inventory = GameObject.FindWithTag("Inventory");
        InvCon = Inventory.GetComponent<Inventory_Controller>();

        Item_Junk_Image_OBJ = transform.GetChild(0).gameObject;
        Item_Junk_Image = transform.GetChild(0).GetComponent<Image>();
    }
    void LateUpdate()
    {
        Item_Junk_Image.GetComponent<Image>().sprite = Item_Texture; // 이미지

        RectTransform rectTran = gameObject.GetComponent<RectTransform>(); // RectTransform 가져오기
        Vector2 Pivot = gameObject.GetComponent<RectTransform>().pivot; // 피봇 가져오기

        rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 26 * Horizontal_Size); //가로,세로 크기 조절
        rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 26 * Vertical_Size);

        PivotX = 0.5f - ((Horizontal_Size - 1) * 0.25f);
        PivotY = 0.5f + ((Vertical_Size - 1) * 0.25f);

        rectTran.pivot = new Vector2(PivotX, PivotY);

        RectTransform rectTran_image = Item_Junk_Image_OBJ.GetComponent<RectTransform>(); // 이미지 가로/세로 크기 조절

        rectTran_image.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 26 * Horizontal_Size);
        rectTran_image.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 26 * Vertical_Size);

        if (Item_Code == 0)
        {
            Item_Name = "고체 연료";
            Item_Explanation = "MEHO사의 MRE나 뎁힐법한 고체연료다.";

            string Path = @"Item\Junk\Dryfuel";
            Item_Texture = Resources.Load(Path, typeof(Sprite)) as Sprite;

            Horizontal_Size = 1;
            Vertical_Size = 1;
        }
        else if (Item_Code == 1)
        {
            Item_Name = "식용유";
            Item_Explanation = "조리용 기름";

            string Path = @"Item\Junk\CookingOil";
            Item_Texture = Resources.Load(Path, typeof(Sprite)) as Sprite;

            Horizontal_Size = 1;
            Vertical_Size = 2;
        }
        else if (Item_Code == 2)
        {
            Item_Name = "휘발유";
            Item_Explanation = "MEHO사의 보급형 가정용 기름이다.";

            string Path = @"Item\Junk\Expeditionary_fuel_tank";
            Item_Texture = Resources.Load(Path, typeof(Sprite)) as Sprite;

            Horizontal_Size = 2;
            Vertical_Size = 2;
        }
        else if (Item_Code == 3)
        {
            Item_Name = "휘발유";
            Item_Explanation = "MEHO사의 보급형 가정용 기름이다.";

            string Path = @"Item\Junk\Expeditionary_fuel_tank";
            Item_Texture = Resources.Load(Path, typeof(Sprite)) as Sprite;

            Horizontal_Size = 3;
            Vertical_Size = 2;
        }
    }
    public void OnPointerDown(PointerEventData eventData) // 드레그 시작
    {
        Array.Resize<GameObject>(ref SlotSizeObj, Horizontal_Size * Vertical_Size);
        Array.Resize<GameObject>(ref SlotStart, Horizontal_Size * Vertical_Size);

        Array.Resize<int>(ref num, Horizontal_Size * Vertical_Size);

        Array.Resize<int>(ref SlotSizeX, Horizontal_Size);
        Array.Resize<int>(ref SlotSizeY, Vertical_Size);

        for (int i = 1; i < InvCon.Slots.Count; i++)
        {
            GameObject slot = InvCon.Slots[i];
            float Length = Vector3.Distance(slot.transform.position, transform.position);

            if (Min >= Length)
            {
                Min = Length;
                StartSlot = InvCon.Slots[i];
                CloseSlot = InvCon.Slots[i];

                SlotSizeY[0] = CloseSlot.GetComponent<Slot>().PosY;
                SlotSizeX[0] = CloseSlot.GetComponent<Slot>().PosX;
                SlotStart[0] = CloseSlot;

                int y = SlotSizeY[0];

                for (int i0 = 1; i0 < Vertical_Size; i0++)
                {
                    int x = SlotSizeX[0];

                    for (int i1 = 1; i1 < Horizontal_Size; i1++)
                    {
                        SlotStart[(i0 + i1) - 1] = InvCon.Slots[i];
                        SlotSizeX[(i0 + i1) - 1] = x + 1;
                        x++;
                    }
                    SlotStart[i0] = InvCon.Slots[i + InvCon.GetComponent<Inventory_Controller>().Inventory_Horizontal];

                    y++;
                    SlotSizeY[i0] = y;
                }
            }
            StartSlot.GetComponent<Slot>().use = false;

            for (int i0 = 0; i0 < SlotSizeObj.Length; i0++)
            {
                SlotStart[i0].GetComponent<Slot>().use = false;
            }
        }
    }
    public void OnDrag(PointerEventData eventData) //드레그 중
    {
        Min = 999;
        for (int i = 1; i < InvCon.Slots.Count; i++)
        {
            GameObject slot = InvCon.Slots[i];
            Vector3 slotpos = new Vector2(slot.transform.position.x, slot.transform.position.y);
            float Length = Vector3.Distance(slotpos, transform.position);

            if (Min >= Length)
            {
                Min = Length;
                CloseSlot = InvCon.Slots[i];

                SlotSizeY[0] = CloseSlot.GetComponent<Slot>().PosY;
                SlotSizeX[0] = CloseSlot.GetComponent<Slot>().PosX;
                SlotSizeObj[0] = CloseSlot;

                int y = SlotSizeY[0];

                for (int i0 = 1; i0 < Vertical_Size; i0++)
                {
                    int x = SlotSizeX[0];

                    for (int i1 = 1; i1 < Horizontal_Size; i1++)
                    {

                        SlotSizeObj[(i0 + i1) - 1] = InvCon.Slots[i];


                        SlotSizeX[(i0 + i1) - 1] = x + 1;
                        x++;
                    }

                    SlotSizeObj[i0] = InvCon.Slots[i + InvCon.GetComponent<Inventory_Controller>().Inventory_Horizontal];

                    y++;
                    SlotSizeY[i0] = y;
                }
            }
        }
        gameObject.transform.position = eventData.position;
    }
    public void OnPointerUp(PointerEventData eventData) // 드레그 놓음
    {
        int SlotNumber = 0;
        for (int y = 0; y < Vertical_Size; y++)
        {
            bool BREAK = false;
            for (int x = 0; x < Horizontal_Size; x++)
            {
                if (SlotSizeObj.Length > 1)
                {
                    Debug.Log("튀어나감 방지");
                    for (int i = 1; i < SlotSizeObj.Length; i++)
                    {
                        if (SlotSizeObj[0] == SlotSizeObj[i])
                        {
                            Debug.Log("튀어나감");
                            gameObject.transform.position = StartSlot.GetComponent<Transform>().position;

                            Debug.Log("불가능");
                            for (int i1 = 0; i1 < SlotSizeObj.Length; i1++)
                            {
                                SlotStart[i1].GetComponent<Slot>().use = true;
                            }

                            Vector2 Slotpos = StartSlot.transform.position;
                        }
                        else
                        {
                            Debug.Log("튀어나가지 않음");

                            if (SlotSizeObj[SlotNumber].GetComponent<Slot>().use == true)
                            {
                                Debug.Log("이동 불가능");
                                gameObject.transform.position = StartSlot.GetComponent<Transform>().position;
                                for (int i1 = 0; i1 < SlotSizeObj.Length; i1++)
                                {
                                    SlotStart[i1].GetComponent<Slot>().use = true;
                                }
                                Vector2 Slotpos = StartSlot.transform.position;

                                BREAK = true;
                                break;
                            }
                            else
                            {
                                Debug.Log("이동 가능");
                                gameObject.transform.position = CloseSlot.GetComponent<Transform>().position;

                                SlotStart[SlotNumber] = SlotSizeObj[SlotNumber];
                                SlotStart[SlotNumber].GetComponent<Slot>().use = false;

                                SlotSizeObj[SlotNumber].GetComponent<Slot>().use = true;
                            }
                        }
                    }
                }
                else
                {
                    if (SlotSizeObj[SlotNumber].GetComponent<Slot>().use == false)
                    {
                        // Vector2 Slotpos = CloseSlot.transform.position;
                        Debug.Log("이동 가능");
                        gameObject.transform.position = CloseSlot.GetComponent<Transform>().position;

                        for (int i0 = 0; i0 < SlotSizeObj.Length; i0++)
                        {
                            SlotStart[i0] = SlotSizeObj[i0];
                            SlotStart[i0].GetComponent<Slot>().use = false;

                            SlotSizeObj[i0].GetComponent<Slot>().use = true;
                        }

                    }
                    else
                    {
                        gameObject.transform.position = StartSlot.GetComponent<Transform>().position;

                        Debug.Log("이동 불가능");
                        for (int i1 = 0; i1 < SlotSizeObj.Length; i1++)
                        {
                            SlotStart[i1].GetComponent<Slot>().use = true;
                            SlotSizeObj[i1].GetComponent<Slot>().use = true;
                        }

                        Vector2 Slotpos = StartSlot.transform.position;
                        break;
                    }
                }

                SlotNumber++;
            }
            if (BREAK == true)
            {
                Debug.Log("탈출");
                break;
            }
        }

        //Debug.Log(SlotNumber);
    }
}