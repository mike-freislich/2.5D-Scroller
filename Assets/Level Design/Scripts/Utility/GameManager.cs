using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{

    public GameObject gameOverGroup;
    public GameObject levelCompleteText;
    public GameObject sheepIcon;
    public TextMeshProUGUI sheepEatenText;
    public GameObject playAgainButton;
    public AudioSource gameMusic;
    private AudioSource aud;
    private int totalSheep;
    private Color imageColor;

    void Start()
    {

        levelCompleteText.SetActive(false);
        sheepIcon.SetActive(false);
        playAgainButton.SetActive(false);
        gameOverGroup.SetActive(false);
        aud = GetComponent<AudioSource>();     


    }

    public void StartGameOverSequence()
    {
        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
     
        if(gameMusic !=null)
        gameMusic.Stop();
        
        aud.Play();
        gameOverGroup.SetActive(true);
        totalSheep = GetComponent<FoodCounter>().SheepCount();
        yield return new WaitForSeconds(2f);
        levelCompleteText.SetActive(true);
        yield return new WaitForSeconds(1f);
        sheepIcon.SetActive(true);
        sheepEatenText.text = totalSheep + " sheep eaten";
        yield return new WaitForSeconds(1f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playAgainButton.SetActive(true);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
