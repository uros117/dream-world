
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [Header("############## Audio Clips ##############")]
    public AudioClip backgroundMusic;
    public AudioClip death;
    public AudioClip hera;
    public AudioClip hercules;
    public AudioClip hydra;
    public AudioClip goon;
    public AudioClip henchman;
    public AudioClip sheild;
    public AudioClip projectile;
    public AudioClip footstep;
    
    [Header("##############  Extras  ##############")]

    public AudioClip piano;
    public AudioClip quietTense;
    public AudioClip boom;
    public AudioClip end;
    public AudioClip punch;
    public AudioClip reveal;
    public AudioClip slash;
    public AudioClip takeDamage;
    public AudioClip slideWhistle;


    //etras


    public void Start(){
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlayerSFX(AudioClip clip){
        sfxSource.PlayOneShot(clip);
    }


}
