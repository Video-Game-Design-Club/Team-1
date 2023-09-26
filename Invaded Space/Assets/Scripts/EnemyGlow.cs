
using UnityEngine;

/*
    class for making enemies glow when the mouse hovers over them
*/

public class EnemyGlow : MonoBehaviour
{
    private Renderer rend;  // reference to Renderer component
    private Color originalColor;    // original color of the enemy
    public Color glowColor = Color.white; // color to change to when mouse hovers over enemy
    
    // Start() is called before the first frame update
    private void Start()        
    {
        rend = GetComponent<Renderer>();        // get the Renderer component
        originalColor = rend.material.color;    // get the original color of the enemy
    }

    // OnMouseEnter() is called when the mouse enters the collider
    private void OnMouseEnter()     {
        rend.material.color = glowColor;    // change the color to glowColor
    }

    // OnMouseExit() is called when the mouse exits the collider
    private void OnMouseExit()     {
        rend.material.color = originalColor;    // change the color back to original
    }
}
