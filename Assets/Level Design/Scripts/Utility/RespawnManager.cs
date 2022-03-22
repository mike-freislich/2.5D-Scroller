using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RespawnManager : MonoBehaviour
{
    public Transform player;
    public Transform playerCamera;
    public Transform respawnPoint;   
    public Animator anim;

    public UnityEvent OnRespawnComplete;
    public delegate void RespawningPlayer();
    public static event RespawningPlayer OnRespawningPlayer;

    void Start ()
    {
        MelvinGhost.OnPlayerGhost += RespawnPlayer;
    }

    void OnDisable ()
    {
       MelvinGhost.OnPlayerGhost -= RespawnPlayer; 
    }

    public void RespawnPlayer()
    {
        if(anim !=null)
        anim.SetBool("FadeOut", true);        
        Invoke("Respawn", 2f);
    }

    void Respawn()
    {
        if(OnRespawningPlayer != null)
            OnRespawningPlayer();

        player.gameObject.SetActive(false);
        player.transform.position = respawnPoint.position;
        player.gameObject.SetActive(true);
        playerCamera.position = new Vector3(player.position.x, playerCamera.position.y,playerCamera.position.z);

        if(anim !=null)
        anim.SetBool("FadeOut", false);

        OnRespawnComplete.Invoke();

    }


}
