using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* 
    class for displaying the health of an enemy
*/

public class HealthBar : MonoBehaviour
{
    public Enemy enemy;                // enemy this health bar is attached to
    public Slider healthSlider;        // health bar slider
    public TextMeshProUGUI healthText; // health bar text

    // Update is called once per frame
    void Update()
    {
        // update the health bar and text to reflect the enemy's health
        healthSlider.value = (float)enemy.health / 100;   // assuming the enemy's max health is 100
        healthText.text = enemy.health.ToString();      // convert enemy's health to string and display it
        transform.position = enemy.transform.position + new Vector3(0, 0.6f, 0);    // position the health bar above the enemy
    }
}