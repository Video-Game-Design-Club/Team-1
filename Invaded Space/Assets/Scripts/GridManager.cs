using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/**
This class is responsible for handling all the grid square sprites, click events on the grid,
    and keeping track of all objects currently placed on the grid
**/
public class GridManager : MonoBehaviour
{
    [SerializeField]
    GameObject gridSquareSpritePrefab;
    [SerializeField]
    Vector2Int gridSize;
    [SerializeField]
    float gridSquareSize;
    [SerializeField]
    float gridSquareMargin;
    [SerializeField]
    Color squareColor;
    [SerializeField]
    Color squareColorHighlight;
    [SerializeField]
    Color squareColorClick;

    SpriteRenderer backgroundSprite;
    BoxCollider boxCollider;

    GameObject[,] gridObjects;
    SpriteRenderer[,] gridSprites;

    SpriteRenderer highlightedSquare;

    void Awake(){
        UpdateGridSize();
    }

    /**
    Setup arrays for keeping track of square sprites & any objects on the grid
    **/
    void Inititialize(){
        DestroyOldObjects();

        gridObjects = new GameObject[gridSize.x,gridSize.y];
        gridSprites = new SpriteRenderer[gridSize.x,gridSize.y];

        //initialize grid square sprites
        for (int i = 0; i < gridSize.x; i++){
            for (int j = 0; j < gridSize.y; j++){
                gridSprites[i,j] = Instantiate(gridSquareSpritePrefab).GetComponent<SpriteRenderer>();
                gridSprites[i,j].transform.parent = this.transform;
                Vector3 pos = transform.position + new Vector3(i,j,0) * gridSquareSize;
                pos -= new Vector3(gridSize.x*0.5f-0.5f,gridSize.y*0.5f-0.5f,0f) * gridSquareSize;
                pos -= Vector3.forward * 0.1f;
                gridSprites[i,j].transform.position = pos;
                gridSprites[i,j].name = "Grid Sprite "+i+", "+j;
                gridSprites[i,j].size = new Vector2(gridSquareSize-gridSquareMargin,gridSquareSize-gridSquareMargin);
                gridSprites[i,j].color = squareColor;
            }
        }
    }

    /**
    Remove old objects if the grid is re-initialized at runtime
        (could be useful for testing)
    I'm assuming all relevant objects to the grid will be children of it
    **/
    void DestroyOldObjects(){
        int children = transform.childCount;
        for (int i = children-1; i >= 0; i--){
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }


    /**
    Update background sprite & collider for when the grid size changes
    **/
    public void UpdateGridSize(){
        //these are here so this works in editor
        backgroundSprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();

        Vector3 scale = new Vector3(gridSize.x * gridSquareSize,gridSize.y * gridSquareSize,0.1f);
        backgroundSprite.size = (Vector2)gridSize * gridSquareSize;
        boxCollider.size = scale;

        Inititialize();//update everything else
    }

    /**
    Handle events from the MouseCast class on the main camera
    We can do whatever we want here, I just have basic highlighting right now
    **/
    public void MouseHover(bool hit, Vector3 pos){
        if (highlightedSquare != null){
            highlightedSquare.color = squareColor;
            highlightedSquare = null;
        }

        if(hit){
            Vector3 localPos = transform.InverseTransformPoint(pos);
            Vector2Int coords = LocalPosToSquare(localPos);
            highlightedSquare = gridSprites[coords.x,coords.y];
            highlightedSquare.color = squareColorHighlight;
        }
    }

    /**
    Converts a position in local space to a grid square coordinate
    Right now this is used for figuring out what square the mouse cursor is over
    **/
    Vector2Int LocalPosToSquare(Vector3 pos){
        pos = new Vector3(pos.x,pos.y,0f);
        pos /= gridSquareSize;
        pos += new Vector3(gridSize.x*0.5f,gridSize.y*0.5f,0f);
        //clamping prevents weird & rare bugs where we might get a coord of -1 for some reason
        int x = Mathf.Clamp(Mathf.FloorToInt(pos.x),0,gridSize.x);
        int y = Mathf.Clamp(Mathf.FloorToInt(pos.y),0,gridSize.y);
        Vector2Int res = new Vector2Int(x,y);
        return res;
    }
}

//Boilerplate code for getting the fancy update button in the inspector :>
[CustomEditor(typeof(GridManager))]
public class InspectorGridManager : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridManager grid = (GridManager)target;
        if (GUILayout.Button("Update Grid"))
        {
            grid.UpdateGridSize();
        }
    }
}
