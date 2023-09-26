using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    base class for all enemies, has basic functionality like movement, attacking, taking damage, dying, etc
*/  

public class Enemy : MonoBehaviour
{
    public float speed = 0.5f; // enemy movespeed
    public Transform target; // enemy target moves towards
    public int health = 100; // enemy health
    public int damage = 10; // enemy damage dealt to target

    protected virtual void Start() {
        // TODO: initialization for stuff that applies to all enemies
    }

    // Update is called once per frame
    void Update()  {
        // move the enemy towards the target
        float step = speed * Time.deltaTime;                                                 // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step); // move the enemy

        // if the enemy is close enough to the target, attack (and then die)
        if (Vector3.Distance(transform.position, target.position) < 0.1f) {
            Attack();
            Die();
        }
    }

    // function for enemy to attack target
    void Attack() {
        // TODO: calculate dmg based on enemy stats and target stats, etc
        Debug.Log("enemy attacks for " + damage + " damage! oof owie"); 
    }

    // function for target to take damage
    public void TakeDamage(int damage) {
        // TODO: add roblox OOF + vine boom
        health -= damage;
        Debug.Log("Enemy took damage. Current health: " + health);
        StartCoroutine(FlashRed()); // coroutine flash red when hit
        if (health <= 0) {
            Die();  // lol
        }
    }

    // function for enemy to die
    void Die() {
        // TODO: add death animation, sound, etc here
        Destroy(gameObject);
    }

    IEnumerator FlashRed() {
        Renderer renderer = GetComponent<Renderer>();   // get the renderer component of enemy
        Color originalColor = renderer.material.color;  // get the original color of enemy
        renderer.material.color = Color.red;            // change the color to red
        yield return new WaitForSeconds(0.1f);          // wait 0.1 seconds
        renderer.material.color = originalColor;        // change the color back to original
    }
}