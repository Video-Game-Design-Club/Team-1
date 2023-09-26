using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
pssst sorry i had to change some stuff to playest the enemy script
1. added damage logic
2. added raycast debugging w/ nested 'if' statements at the bottom of update, print statements 
 are commented out rn
    - kevin :)))
 */

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

    public int playerDamage = 20;   // added for playtesting enemy.cs
    int gridLayer;

    void Awake()
    {
        cam = GetComponent<Camera>();
        gridLayer = LayerMask.NameToLayer("Grid");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hi;
        if (ScreenCast(mask, out hi, ray) && hi.collider.gameObject.layer == gridLayer) {
            grid.MouseHover(true,hi.point);
        }else{
            grid.MouseHover(false,Vector3.zero);
        }

        // raycast debugging stuff 
        if (Input.GetMouseButtonDown(0)) {  
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.green, 2f);
            //Debug.Log("Mouse clicked.");            
            if (hi.collider != null) {      
                //Debug.Log("Raycast hit an object.");
                if (hi.collider.gameObject != null) {
                   // Debug.Log("Raycast hit a GameObject.");
                    if (hi.collider.gameObject.GetComponent<Enemy>() != null) {
                        //Debug.Log("Raycast hit an enemy.");
                        hi.collider.gameObject.GetComponent<Enemy>().TakeDamage(playerDamage);
                        //Debug.Log("Raycast hit: " + hi.collider.gameObject.name);
                    }
                }
            }
        }

    }

    //added ray as a 3rd parameter  
    private bool ScreenCast(LayerMask mask, out RaycastHit hit, Ray ray) { //casts a ray from the camera, through the mouse cursor, into the world
        //return Physics.Raycast(ray, out hit, cam.farClipPlane, mask);
        return Physics.Raycast(ray, out hit, Mathf.Infinity, mask);
    }

    private Vector3 GetMouseWorldPosition(Vector2 position) { //converts screen space to world space
        Vector3 pos3D = new Vector3(position.x, position.y, 5.0f);
        return cam.ScreenToWorldPoint(pos3D);
    }
}
