using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHold : MonoBehaviour
{
    public static MouseHold instance;

    [SerializeField]
    Vector3 heldObjectOffset;

    Transform heldObject;
    Camera cam;

    void Awake(){
        cam = GetComponent<Camera>();
        if (instance != null){
            DestroyImmediate(this.gameObject);
        }
        instance = this;
    }

    //move with mouse cursor
    void Update(){
        if (heldObject != null){
            Vector3 worldPos = GetMouseWorldPosition(Input.mousePosition,5.0f);
            heldObject.transform.position = worldPos + heldObjectOffset;
        }
    }

    public Transform GetHeldObject(){
        return heldObject;
    }

    public void SetHeldObject(Transform heldO){
        heldObject = heldO;
    }

    private Vector3 GetMouseWorldPosition(Vector2 position, float depth) { //converts screen space to world space
        Vector3 pos3D = new Vector3(position.x, position.y, depth);
        return cam.ScreenToWorldPoint(pos3D);
    }
}
