using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum EnemyState { idle, active, deathSequence, destroy };
    public int health;
    public GameObject explosion;
    public int score = 50;
    public bool offScreenCulling = true;    

    TheGame theGame;

    private EnemyState state;

    void Start()
    {
        state = EnemyState.active;

        theGame = TheGame.Instance;
        StartCoroutine(MyTimer.Start(2.0f, true, () =>
        {
            if (offScreenCulling && didScrollOffScreen())
                RemoveObject();
        }));
    }

    void Update()
    {
    }

    void RemoveObject()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        if (offScreenCulling)
            RemoveObject();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0 && state == EnemyState.active)
            DeathSequence();
        else
            StartCoroutine(HitFade());            
    }

    private IEnumerator HitFade()
    {        
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        Color hitColor = Color.red;
        float intensity = 1;
        do
        {
            intensity -= Time.deltaTime * 5.0f;
            if (intensity < 0) intensity = 0;
            foreach (var renderer in renderers)
                SetMaterialEmissiveColor(renderer, hitColor, intensity);            
            yield return null;
        } while (intensity > 0);            
    }

    private void SetMaterialEmissiveColor(Renderer renderer, Color emissiveColor, float emissiveIntensity)
    {
        List<Material> materials = new List<Material>();
        renderer.GetMaterials(materials);
        foreach (Material material in materials)        
            material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);        
    }

    private void DeathSequence()
    {
        state = EnemyState.deathSequence;
        if (theGame != null) theGame.AddScore(score);

        state = EnemyState.destroy;
        Explode();
    }


    void Explode()
    {
        GameObject explodeObject = Instantiate<GameObject>(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(explodeObject, 2);
    }

    bool didScrollOffScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        return (screenPoint.x < -2f);
    }

    public bool isOnCamera
    {
        get { return ScreenUtility.isOnCamera(transform); }
    }
}
