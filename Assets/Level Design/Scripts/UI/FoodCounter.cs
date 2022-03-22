using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodCounter : MonoBehaviour
{
    public TextMeshProUGUI sheepText;
    private int currentSheep;


    // Start is called before the first frame update
    void Start()
    {
        CharacterMovement.OnEatSheep += AddEatenSheep;
        sheepText.text = currentSheep.ToString();
    }

    public int SheepCount()
    {
        return currentSheep;
    }

    public void AddEatenSheep()
    {
        currentSheep++;
        sheepText.text = currentSheep.ToString();

    }


}
