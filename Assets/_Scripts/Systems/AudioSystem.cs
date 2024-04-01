using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioSystem : MonoBehaviour {

    public static AudioSystem Instance;

    [SerializeField] private AudioMixerGroup _masterMixerGroup;
    [SerializeField] private AudioMixerGroup _musicMixerGroup;

    private float _lastPreparationMusicPosition = 0;
    
    private void Awake() {
        Instance = this;
        if(SceneManager.GetActiveScene().name == "MainMenu") {
            PlayMusic(Music.MainMenuMusic);
        }
    }

    public void PlaySound(Sound sound) {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = _masterMixerGroup;
        audioSource.volume = 0.7f;
        audioSource.PlayOneShot(ResourceSystem.Instance.GetSoundClip(sound));
        if(ResourceSystem.Instance.GetSoundClip(sound) != null) StartCoroutine(DestroyAfterSoundFinished(soundGameObject, ResourceSystem.Instance.GetSoundClip(sound).length));
    }

    private IEnumerator DestroyAfterSoundFinished(GameObject soundGameObject, float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(soundGameObject);
    }

    public void PlayMusic(Music music) {
        StopAllMusic();
        GameObject musicGameObject = new GameObject("Music");
        AudioSource audioSource = musicGameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = _musicMixerGroup;
        audioSource.volume = 0.1f;
        audioSource.loop = true;
        audioSource.clip = ResourceSystem.Instance.GetMusicClip(music);
        if (music == Music.PreparationPhaseMusic) audioSource.time = _lastPreparationMusicPosition;
        audioSource.Play();
    }

    private void StopAllMusic() {
        foreach (AudioSource currentMusicPlaying in FindObjectsOfType<AudioSource>()) Destroy(currentMusicPlaying);    

    }
}
