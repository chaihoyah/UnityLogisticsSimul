using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//개별 오브젝트의 정보만 (Move XX)
public class ObjectScript : MonoBehaviour {

    private int target;
    private int type;

    public void SetInfo(int target_, int type_)
    {
        target = target_;
        type = type_;
    }

    public int Get_Type()
    {
        return type;
    }

    public int GetTarget()
    {
        return target;
    }
}
