using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public float value;
    Image valueImage;

    void Start()
    {
        GameObject image = transform.Find("pbValueImage").gameObject;
        valueImage = image.GetComponent<Image>();
    }

    void Update()
    {

    }

    public void SetValue(float newValue)
    {
        if (value != newValue)
        {
            value = newValue;
            
            if (valueImage != null)
                valueImage.rectTransform.localScale = new Vector3(value, 1, 1);
        }
    }

    public void Hide()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    public void Show()
    {
        if (!gameObject.activeSelf)
            gameObject.SetActive(true);
    }
}
