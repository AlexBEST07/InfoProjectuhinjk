using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Object : MonoBehaviour
{
    #region Setup

    public virtual void Awake() {
        _health = MAX_HEALTH;
    }

    #endregion


    #region Health

    [Header("Health")]

    [SerializeField] private float MAX_HEALTH;
    
    private float _health;

    [SerializeField] private float _deathScore;

    [HideInInspector] public float multiplicator = 1f;


    public void TakeDamage(float damage) {

        _health = _health - damage;

        if (_health <= 0) {
            Die();
        }
    }

    public void Die() {
        if(gameObject.tag == "Enemy") {
            HUDManager.Instance.AddScore(_deathScore * multiplicator);
            UnitManager.Instance.CheckRemainingEnemies();
        }
        if (gameObject.tag == "Player") Debug.Log("Hallo");// GameManager.Instance.UpdateGameState(GameState.GameOver);
        Destroy(gameObject);
    }

    #endregion
}
