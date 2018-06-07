using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrail : MonoBehaviour {
    [SerializeField] Camera trailCamera;
    [SerializeField] TrailRenderer trail;
    [SerializeField] ParticleSystem particle;

    [SerializeField] float fadeIterations;

    //Vector3[] trailVertex;
    //Material trailMaterial;
    //Mesh trailMesh;
    Color originalColor;
    /// <summary>
    /// Quando true permite a chamada do método FollowMouse() no update;
    /// </summary>
    bool followingMouse;
    bool fading;
    /// <summary>
    /// Encapsulamento do campo followingMouse;
    /// </summary>
    public bool FollowingMouse
    {
        set
        {
            if (!fading)
            {
                followingMouse = value;
                if (value)
                {
                    trail.Clear();
                    trail.enabled = true;
                    StartCoroutine(StartParticle());
                }
                else
                {
                    trail.enabled = false;
                    StartCoroutine(StopParticle());
                }

            }
        }
    }

    void Awake()
    {
        //trailMaterial = trail.material;
        originalColor = trail.material.color;
    }
    void Start() {
        followingMouse = false;
        ParticleSystem.EmissionModule emission = particle.emission;
    }

    void Update() {
        if (followingMouse) FollowMouse();
    }

    void ToggleParticles(bool condition)
    {
        ParticleSystem.EmissionModule emission = particle.emission;
        emission.enabled = condition;

    }

    /// <summary>
    /// Teleporta o transform para a posição do mouse;
    /// </summary>
    void FollowMouse()
    {
        transform.position = trailCamera.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10);
    }

    void GenerateTrail()
    {   //
        //trail.GetPositions(trailVertex);
        //Vector3[] normals = new Vector3[];
        //
        //trailMesh.vertices = trailVertex;
    }

    public void FadeTrail(float fadeDuration)
    {
        //print(trail.GetPositions(trailVertex));
        //foreach(Vector3 vec in trailVertex) print(vec);
        fading = true;
        followingMouse = false;
        StartCoroutine(StopParticle());
        StartCoroutine(Fade(fadeDuration));
    }

    IEnumerator Fade(float fadeDuration)
    {
        float decreaseValue = 1 / fadeIterations;
        float secondsToWait = fadeDuration / fadeIterations;

        for (int i = 0; i < fadeIterations; i++)
        {
            originalColor.a -= (decreaseValue);
            trail.material.color = originalColor;
            yield return new WaitForSeconds(secondsToWait);
        }
        originalColor.a = 1;
        fading = false;
        trail.enabled = false;
        trail.material.color = originalColor;
        FollowingMouse = false;
    }
    IEnumerator StartParticle()
    {
        yield return new WaitForEndOfFrame();
        particle.Play();
    }
    IEnumerator StopParticle()
    {
        yield return new WaitForEndOfFrame();
        particle.Stop();
    }
}
