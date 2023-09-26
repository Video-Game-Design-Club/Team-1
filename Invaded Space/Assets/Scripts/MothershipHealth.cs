using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
    class for displaying the health of the player's mothership
*/

public class MothershipHealth : MonoBehaviour {
    public int health = 100;
    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    // function to take damage
    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}

