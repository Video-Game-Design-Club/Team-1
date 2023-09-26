using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    GameObject testBoi;

    public void TestMe(){
        if (MouseHold.instance.GetHeldObject() == null){
            Transform test = Instantiate(testBoi).transform;
            MouseHold.instance.SetHeldObject(test);
        }
    }
}
