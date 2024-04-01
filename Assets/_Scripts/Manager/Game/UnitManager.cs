using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitManager : MonoBehaviour {

    #region Setup

    public static UnitManager Instance;

    private void Awake() {
        Instance = this;    
        _markers = new List<GameObject>();
    }

    #endregion

    #region Player

    private GameObject _selectedPlayer;
    private GameObject _currentPlayer;

    public void ChangePlayer(PlayerTypes playerType) {
        _selectedPlayer = ResourceSystem.Instance.GetPlayerPrefab(playerType);
    }

    public void RemovePlayer() {
        
        Destroy(_currentPlayer);
    }

    public void SpawnPlayer() {
        _currentPlayer = Instantiate(_selectedPlayer, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void DeactivatePlayer() {
        _selectedPlayer.GetComponent<InputHandler>().enabled = false;
    }

    public void ActivatePlayer() {
        _selectedPlayer.GetComponent<InputHandler>().enabled = true;
    }

    #endregion


    #region Enemy

    [SerializeField] private Tilemap _playGrid;

    private List<GameObject> _markers;

    private HashSet<Vector3Int> spawningPositions = new HashSet<Vector3Int>();

    private int _enemySpawnCount = 5;

    public void SpawnEnemy() {        
        foreach(GameObject marker in _markers) {
            Instantiate(ResourceSystem.Instance.enemiesArray[Random.Range(0, ResourceSystem.Instance.enemiesArray.Length)].enemyPrefab, marker.transform.position, Quaternion.identity);
            Destroy(marker);
        }
    }

    public void MarkEnemiesSpawn() {       
        _markers.Clear();
        for (int i = 0; i <= _enemySpawnCount - 1; i++) {
            Vector3Int cellPosition = new Vector3Int(Random.Range(-13, 14), Random.Range(-8, 8));
            if (spawningPositions.Contains(cellPosition) && cellPosition != new Vector3Int(0, 0, 0)) {
                i--;
            }
            else {
                _markers.Add(Instantiate(ResourceSystem.Instance.GetEnemyMarker(), new Vector3(_playGrid.CellToWorld(cellPosition).x, _playGrid.CellToWorld(cellPosition).y, 0f), Quaternion.identity));
                spawningPositions.Add(cellPosition);
            } 
        }
    }

    public void CheckRemainingEnemies() {
        _markers.RemoveAt(_markers.Count-1);
        if(_markers.Count <= 0) {
            GameManager.Instance.UpdateGameState(GameState.Preparation);
        }
    }

    public void DestroyAllEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            Debug.Log("Hallo");
            Destroy(enemy);
        }
    }

    #endregion
}