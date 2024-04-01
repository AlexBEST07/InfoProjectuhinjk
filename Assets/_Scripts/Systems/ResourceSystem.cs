using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioSystem;

public class ResourceSystem : MonoBehaviour {

    public static ResourceSystem Instance;

    private void Awake() {
        Instance = this;
    }

    #region Audio

    [Header("Audio")]

    #region Music

    public MusicClips[] musicClipsArray;

    [System.Serializable]
    public class MusicClips {
        public Music musicName;
        public AudioClip audioClip;
    }

    public AudioClip GetMusicClip(Music music) {
        foreach (ResourceSystem.MusicClips musicClip in ResourceSystem.Instance.musicClipsArray) {
            if (musicClip.musicName == music) {
                return musicClip.audioClip;
            }
        }
        Debug.LogError("Music " + music + " not found");
        return null;
    }

    #endregion

    #region Sound

    public SoundClips[] soundClipsArray;

    [System.Serializable]
    public class SoundClips {
        public Sound soundName;
        public AudioClip audioClip;
    }

    public AudioClip GetSoundClip(Sound sound) {
        foreach (ResourceSystem.SoundClips soundClip in ResourceSystem.Instance.soundClipsArray) {
            if (soundClip.soundName == sound) {
                return soundClip.audioClip;
            }
        }
        //Debug.LogError("Sound " + sound + " not found");
        return null;
    }

    #endregion

    #endregion

    #region Units

    [Header("Units")]

    #region Player

    public Players[] playersArray;

    [System.Serializable]
    public class Players {
        public PlayerTypes playerType;
        public GameObject playerPrefab;
    }

    public GameObject GetPlayerPrefab(PlayerTypes playerType) {
        foreach (ResourceSystem.Players player in ResourceSystem.Instance.playersArray) {
            if (player.playerType == playerType) {
                return player.playerPrefab;
            }
        }
        Debug.LogError("PlayerType" + playerType + "not found");
        return null;
    }

    #endregion

    #region Enemy

    public Enemies[] enemiesArray;

    [SerializeField] GameObject _enemyMarker;


    [System.Serializable]
    public class Enemies {        
        public EnemyTypes enemyType;
        public GameObject enemyPrefab;
    }

    public GameObject GetEnemyMarker() {
        return _enemyMarker;
    }

    #endregion

    #endregion
}

#region Audio
public enum Sound {
    PlayerShoot,
    PlayerHit,
    ButtonClick,
}

public enum Music {
    MainMenuMusic,
    PreparationPhaseMusic,
    WavePhaseMusic,
}

#endregion

#region UnitTypes

public enum PlayerTypes {
    ShooterPlayer,
    SniperPlayer,
    FighterPlayer,
}

public enum EnemyTypes {
    ShooterEnemy,
    SniperEnemy,
    FighterEnemy,
}

#endregion
