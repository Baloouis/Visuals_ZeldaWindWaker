using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.VFX;

public class StylizedExplosionParticles : MonoBehaviour
{
    [Header("Main Particles")]
    [SerializeField]
    private VisualEffect mainVfx;
    [Header("Streak Particles")]
    [SerializeField]
    private GameObject streakParticleParent;
    [SerializeField]
    private VisualEffect streakParticlePrefab;
    [SerializeField]
    private float streakParticleRadius;
    [SerializeField]
    private int nbStreakParticles;
    [Header("Sparks Particles")]
    [SerializeField]
    private List<VisualEffect> sparksParticles;

    [SerializeField]
    private List<VisualEffect> streakParticles;
    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Play()
    {
        mainVfx.Play();
        
        for (int i = 0; i < streakParticles.Count; i++)
        {
            streakParticles[i].Play();
        }
        

        for (int i = 0; i < sparksParticles.Count; i++)
        {
            sparksParticles[i].Play();
        }

    }
    public void ResetStreakParticles()
    {
        DestroyChildrenInEditor(streakParticleParent);

        streakParticles = new List<VisualEffect>();
        for (int i = 0; i < nbStreakParticles; i++) 
        {
            VisualEffect partSyst = Instantiate(streakParticlePrefab,streakParticleParent.transform);
            float theta = i*(360 / nbStreakParticles);
            float thetaRad = theta*Mathf.Deg2Rad;
            Vector3 particleLocalPos = new Vector3(Mathf.Cos(thetaRad), 0,Mathf.Sin(thetaRad));
            partSyst.transform.localPosition = particleLocalPos* streakParticleRadius;
            partSyst.transform.localEulerAngles = new Vector3(0, -theta, 0);

            streakParticles.Add(partSyst);
        }
    }

    private void DestroyChildrenInEditor(GameObject childrenParent)
    {
        for (int i = childrenParent.transform.childCount; i > 0; i--)
        {
            DestroyImmediate(childrenParent.transform.GetChild(i-1).gameObject);
        }
    }
}
