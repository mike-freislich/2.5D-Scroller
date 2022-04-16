using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour
{

    string label = "";
    float count;

    IEnumerator Start()
    {
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
        GUIStyle style = new GUIStyle();        
        style.fontSize = 48;
        style.fontStyle = FontStyle.Bold;         
        GUI.Label(new Rect(5, 15, 400, 100), label, style);
    }
}