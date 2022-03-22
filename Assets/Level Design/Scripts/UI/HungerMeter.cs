using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerMeter : MonoBehaviour
{

    public Image hungerMeterFill;
    [Range(0, 100)]
    public float hungerReplenish = 80;
    [Range(0, 300)]
    public float totalHunger = 200;
    [Range(0,10)]
    public float hungerDepleteRate = 5f;
    private bool hungerDepleted = false;
    private WaitForSeconds hungerDelayTimer = new WaitForSeconds(1f);
    private float currentHunger = 100f;

    public delegate void HungerDeath();
    public static event HungerDeath OnHungerDeath;

    void OnEnable()
    {
        currentHunger = totalHunger;
        hungerMeterFill.fillAmount = currentHunger / totalHunger;
        StartCoroutine(HungerIncrease());

        CharacterMovement.OnEatSheep += AddFood;
    }

    void OnDisable ()
    {
        CharacterMovement.OnEatSheep -= AddFood;
    }

    IEnumerator HungerIncrease()
    {
        yield return new WaitForSeconds(2f);

        while (currentHunger > 0)
        {
            currentHunger -= Time.deltaTime * hungerDepleteRate;
            hungerMeterFill.fillAmount = currentHunger / totalHunger;
            yield return new WaitForEndOfFrame();
        }
        hungerDepleted = true;

        if (OnHungerDeath != null)
            OnHungerDeath();

    }

    public void AddFood()
    {
        if (hungerDepleted)
            return;

        currentHunger += hungerReplenish;

        if (currentHunger > totalHunger)
        {
            currentHunger = totalHunger;
        }
        hungerMeterFill.fillAmount = currentHunger / totalHunger;

    }

    public void ResetHunger()
    {
         StopCoroutine("HungerIncrease");
        currentHunger = totalHunger;
        hungerMeterFill.fillAmount = currentHunger / totalHunger;
        StartCoroutine(HungerIncrease());
    }


}
