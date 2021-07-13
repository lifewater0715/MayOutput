using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int Hard; 

    public List<int> Wallinfo = new List<int>(); //추후 데이터 로더로 
    public List<string> Wallname = new List<string>();
}
