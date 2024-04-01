using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    [HideInInspector] public Transform _playerPosition;
    
    public override void Awake() {
        base.Awake();
        GetComponent<Rigidbody2D>().isKinematic = false;
        _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    [Header("Movement")]
    
    [SerializeField]
    float MAX_DISTANCE;

    public override void Move(Vector3 playerPosition) {
        Vector3 direction = (playerPosition - transform.position);
        float distance = Mathf.Sqrt((direction.x * direction.x) + (direction.y * direction.y));
        if (distance > MAX_DISTANCE) {
            base.Move(direction);
        }
    }
}
