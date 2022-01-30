
using UnityEngine;
using System.Collections.Generic;

namespace RafaEslava {
    
[System.Serializable]
public class Audio
{
    public string Name;
    public AudioClip Clip;

    [Range (0f, 1f)]
    public float Volume = 0.7f;
    [Range (0.5f, 1.5f)]
    public float Pitch = 1f;
    [Range (0f, 0.5f)]
    public float RandomVolume = 0.1f;
    [Range (0f, 0.5f)]
    public float RandomPitch = 0.1f;
    
    public void Play(AudioSource audioSource)
    {
        audioSource.clip = Clip;
        audioSource.volume = Volume * (1 +Random.Range(-RandomVolume / 2, RandomVolume / 2));
        audioSource.pitch = Pitch * (1 +Random.Range(-RandomPitch / 2, RandomPitch / 2));;
        audioSource.Play();
    }
}

public class AudioManager : MonoBehaviour
{
    public List<Audio> Audios;
    
    private AudioSource audioSource;

    private void Awake() 
    {
        audioSource = gameObject.AddComponent<AudioSource>();    
    }

    public void PlaySound(string name)
    {
        var sound = Audios.Find(x =>  x.Name == name);
        sound.Play(audioSource);
    } 

    public void PlaySoundIndex(string name, int i)    
    {
        var sound = Audios.Find(x =>  x.Name == name + i);;
        sound.Play(audioSource);
    } 

    private void OnEnable() 
    {
        Events.OnGameStart.AddListener(OnGameStart);    
        Events.OnBlockHelperDroppingExit.AddListener(OnBlockGrounded);
        Events.OnBlockHelperRotatingEnter.AddListener(OnBlockRotate);
        Events.OnBlockHelperMove.AddListener(OnBlockMove);
        Events.OnBlockHelperOnStep.AddListener(OnBlockMove);

        Events.OnPlanesDoneAtOnce.AddListener(OnPlanesDoneAtOnce);
    }

    private void OnDisable() 
    {
        Events.OnGameStart.RemoveListener(OnGameStart);
        Events.OnBlockHelperDroppingExit.RemoveListener(OnBlockGrounded);
        Events.OnBlockHelperRotatingEnter.RemoveListener(OnBlockRotate);
        Events.OnBlockHelperMove.RemoveListener(OnBlockMove);
        Events.OnBlockHelperOnStep.RemoveListener(OnBlockMove);

        Events.OnPlanesDoneAtOnce.RemoveListener(OnPlanesDoneAtOnce);
    }

    private void OnGameStart() => PlaySound("GameStart");    
    private void OnGameOver() => PlaySound("GameOver");    
    private void OnLevelUp() => PlaySound("LevelUp");    
    private void OnBlockMove() => PlaySound("BlockMove");
    private void OnBlockRotate() => PlaySound("BlockRotate");
    private void OnBlockGrounded() => PlaySound("BlockGrounded");

    private void OnPlanesDoneAtOnce(int i) => PlaySound("PlaneDone" + i.ToString());
}

}