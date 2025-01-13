using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BombScript : MonoBehaviour
{
    [SerializeField]
    private GameObject bombMeshesParent;

    [SerializeField]
    private StylizedExplosionParticles bombParticlesPrefab;

    [SerializeField]
    private StylizedExplosionParticles bombParticles;

    [SerializeField]
    private List<VisualEffect> bombPreExplosionParticles;

    [SerializeField]
    private float scaleCoefAnim;

    [SerializeField] private Ease easingScale;
    [SerializeField] private Ease easingColor;

    public Color bombRedColor;

    [SerializeField]
    private MeshRenderer bombMainMesh;

    public float durationFirstPingPong;
    public float timeDecayPingPong = 0.5f;
    public float timeStopEpsilon = 0.1f;
    public float scaleAnimFraction = 1 / 3;
    public float colorAnimDelay = 0f;

    private Material _bombMainMat;
    private Color _bombBlueColor;

    void Start()
    {
        _bombMainMat = bombMainMesh.material;
        _bombBlueColor = _bombMainMat.color;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _bombMainMat.color = _bombBlueColor;
            foreach (var par in bombPreExplosionParticles)
            {
                par.Play();
            }
            //color tween
            float colorAnimDuration = PlayColorAnim();

            
            
            //scale tween and instanciate explosion particles
            float dur = colorAnimDuration * scaleAnimFraction;
            float delay = colorAnimDuration* (1- scaleAnimFraction);
            bombMeshesParent.transform.DOScale(scaleCoefAnim, dur)
                .SetDelay(delay)
                .SetEase(easingScale)
                .OnComplete(
                    () => {
                        foreach (var par in bombPreExplosionParticles)
                        {
                            par.Stop();
                        }
                        bombMeshesParent.transform.localScale = Vector3.one;
                        //Instantiate(bombParticlesPrefab, bombMeshesParent.transform.position, Quaternion.identity, null);
                        bombParticles.Play();
                    }
                    ).Play();
            
        }
    }

    private int _maxLoopColorAnim = 50;
    private float PlayColorAnim()
    {
        Sequence tweenSequence = DOTween.Sequence();

        tweenSequence.SetDelay(colorAnimDelay);
        float curDuration = durationFirstPingPong;

        int i = 1;
        do
        {
            curDuration = curDuration * timeDecayPingPong;

            tweenSequence.Append(_bombMainMat.DOColor(bombRedColor, curDuration).SetEase(easingColor));
            tweenSequence.Append(_bombMainMat.DOColor(_bombBlueColor, curDuration).SetEase(easingColor));


            i++;
        } while (curDuration > timeStopEpsilon && i< _maxLoopColorAnim);

        tweenSequence.Append(_bombMainMat.DOColor(bombRedColor, curDuration).SetEase(easingColor));


        print("i = " + i);
        tweenSequence.Play();

        return tweenSequence.Duration();
    }

}
