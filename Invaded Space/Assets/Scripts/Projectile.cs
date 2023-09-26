using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    base class for all projectiles, has basic functionality like movement, damage, etc
*/

public class Projectile : MonoBehaviour
{
    public Transform target;            // projectile target
    public float speed = 1f;            // projectile speed
    public int damage = 10;             // projectile damage
    public float lifetime = 5f;         // destroy proj after x amount of seconds to save resources
    public bool isAllied = false;       // check if the projectile from an ally or enemy (idk a cooler name)
    private void Start() {
        Destroy(gameObject, lifetime);  // destroy proj after lifetime seconds
    }

    // Update is called once per frame
    void Update() {
        // move the projectile towards the target (same formula as enemy cause lazy)
        float step = speed * Time.deltaTime;                                                 // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step); // move the enemy
    }

    // function for target to take damage 
    // TODO: this ain't work yet idk why projectiles just go through. idk bruh sorry im tired
    private void OnCollisionEnter(Collision collision){
        // if the projectile is from an enemy and collides with the player, call player script TakeDamage method
        if (!isAllied && collision.gameObject.name == "Player Mothership") {
            // call player script TakeDamage method
            MothershipHealth mothershipHealth = collision.gameObject.GetComponent<MothershipHealth>();
            if (mothershipHealth != null) {
                mothershipHealth.TakeDamage(damage);
            }
        }
        Destroy(gameObject); // destroy the projectile upon collision
    }
}
