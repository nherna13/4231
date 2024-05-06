using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
   public static SoundManager Instance { get; set; }

    public AudioSource ShootingChannel;
    

    public AudioClip ScifiPistolsShot;
    public AudioClip ScifiARShot;

    
    
    public AudioSource reloadingSoundScifiAR;
    public AudioSource reloadingSoundScifiPistol;


    public AudioSource emptyMagazineSoundScifiPistol;

    public AudioSource throwablesChannel;
    public AudioClip grenadeSound;


    public AudioClip alienWalking;
    public AudioClip alienChase;
    public AudioClip alienAttack;
    public AudioClip alienHurt;
    public AudioClip alienDeath;

    public AudioSource alienChannel;
    public AudioSource alienChannel2;

    public AudioSource playerChannel;
    public AudioClip playerHurt;
    public AudioClip playerDie;

    public AudioClip gameOverMusic;

    
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.ScifiPistol:
                ShootingChannel.PlayOneShot(ScifiPistolsShot);
                break;
            case WeaponModel.ScifiAR:
                ShootingChannel.PlayOneShot(ScifiARShot);
            //playy ScifiAR
            break;
        }

    }


    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.ScifiPistol:
                reloadingSoundScifiPistol.Play();
                break;
            case WeaponModel.ScifiAR:
                reloadingSoundScifiAR.Play();
            //playy ScifiAR
            break;
        }
    }



}
