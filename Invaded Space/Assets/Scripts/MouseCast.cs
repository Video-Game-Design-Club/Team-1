using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
This is just a simple class to do a raycast from the mouse cursor into the world,
    and letting the grid system know what it finds

(I've had super bad experiences using Unity UI for things it was not specifically designed
    for, which is why I'm making my own mouse click system for this. I think we should
    be OK using Unity UI stuff for the shop and menus and all that)
**/
public class MouseCast : MonoBehaviour
{

    Camera cam;
    [SerializeField]
    LayerMask mask;
    [SerializeField]
    GridManager grid;

    int gridLayer;

    void Awake()
    {
        cam = GetComponent<Camera>();
        gridLayer = LayerMask.NameToLayer("Grid");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hi;
        if (ScreenCast(mask, out hi) && hi.collider.gameObject.layer == gridLayer) {
            grid.MouseHover(true,hi.point);
        }else{
            grid.MouseHover(false,Vector3.zero);
        }
    }

    private bool ScreenCast(LayerMask mask, out RaycastHit hit) { //casts a ray from the camera, through the mouse cursor, into the world
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, cam.farClipPlane, mask);
    }
}
