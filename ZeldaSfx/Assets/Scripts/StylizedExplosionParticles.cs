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


    private List<VisualEffect> _streakParticles;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            Play();
        }
    }

    public void Play()
    {
        mainVfx.Play();
        
        if(_streakParticles!=null)
        {
            for (int i = 0; i < streakParticleParent.transform.childCount; i++)
            {
                _streakParticles[i].Play();
            }
        }
        
    }
    public void ResetStreakParticles()
    {
        DestroyChildrenInEditor(streakParticleParent);

        _streakParticles = new List<VisualEffect>();
        for (int i = 0; i < nbStreakParticles; i++) 
        {
            VisualEffect partSyst = Instantiate(streakParticlePrefab,streakParticleParent.transform);
            float theta = i*(360 / nbStreakParticles);
            float thetaRad = theta*Mathf.Deg2Rad;
            Vector3 particleLocalPos = new Vector3(Mathf.Cos(thetaRad), 0,Mathf.Sin(thetaRad));
            partSyst.transform.localPosition = particleLocalPos* streakParticleRadius;
            partSyst.transform.localEulerAngles = new Vector3(0, -theta, 0);

            _streakParticles.Add(partSyst);
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
