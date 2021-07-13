using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public bool use = false;
    public int tag;

    public int num;
    public int PosX;
    public int PosY;

    void Start()
    {

    }
    void Update()
    {
        CheckSlot();
    }
    public void CheckSlot()
    {
    }
}