using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class Entity : Object
{
    [HideInInspector] public Rigidbody2D rb;

    public override void Awake() {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
    }

    #region Rotation

    public void Rotate(Vector2 lookDir) {
        transform.up = new Vector2(lookDir.x - transform.position.x, lookDir.y - transform.position.y);
    }

    #endregion

    #region Movement

    [SerializeField] private float _moveSpeed;

    [HideInInspector] public float speedBoost;

    [HideInInspector] public Vector2 playerMoveDirection;

    public virtual void Move(Vector3 moveDirection) {
        transform.position += moveDirection.normalized * (_moveSpeed + speedBoost) * Time.deltaTime;
    }

    public IEnumerator ResetVelocity() {
        yield return new WaitForSeconds(0.01f);
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().isKinematic = false;
    }

    #endregion

    private void OnCollisionEnter2D(Collision2D collision) {
        StartCoroutine(ResetVelocity());
    }
}
