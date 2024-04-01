using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour {
    
    private Rigidbody2D _rb;

    public float bulletSpeed = 5f, bulletSpread = 0, bulletDamage = 10f;
    
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = false;
        _rb.velocity = transform.up * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Enemy") {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);
        }
        if(collision.collider.tag == "Player") {            
            collision.gameObject.GetComponent<Player>().TakeDamage(bulletDamage);
        }
        
        Destroy(gameObject);
    }
}
