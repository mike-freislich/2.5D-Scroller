using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PowerUp : MonoBehaviour
{
    public Color color;
    public Vector3 spin;

    void Start()
    {
        GameObject light = transform.Find("Light").gameObject;
        GameObject halo = transform.Find("Halo").gameObject;
        light.GetComponent<Light>().color = color;
        halo.GetComponent<Light>().color = color;

        UnityEditor.SerializedObject debugLight = new UnityEditor.SerializedObject(light);
        debugLight.FindProperty("color").colorValue = color;
        UnityEditor.SerializedObject debugHalo = new UnityEditor.SerializedObject(halo);
        debugLight.FindProperty("color").colorValue = color;

    }
        
    void Update()
    {
        transform.Rotate(spin * Time.deltaTime, Space.Self);
    }
}
