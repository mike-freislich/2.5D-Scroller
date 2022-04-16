using UnityEngine;
using System.Collections;
using TMPro;

public class FPS : MonoBehaviour
{

    string label = "";
    float count;

    Canvas canvas;
    TMP_Text fpsText;
    

    IEnumerator Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        fpsText = canvas.transform.Find("txtFPS").gameObject.GetComponent<TMP_Text>();
        
        GUI.depth = 2;
        while (true)
        {
            if (Time.timeScale == 1)
            {
                yield return new WaitForSeconds(0.1f);
                count = (1 / Time.deltaTime);
                label = "FPS :" + (Mathf.Round(count));
            }
            else
            {
                label = "Pause";
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnGUI()
    {
        fpsText.text = label;        
    }
}