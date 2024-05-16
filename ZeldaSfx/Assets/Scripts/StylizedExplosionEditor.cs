using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(StylizedExplosionParticles))]

public class StylizedExplosionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Reset streak particles placement"))
        {
            StylizedExplosionParticles particlesScript = (StylizedExplosionParticles)target;
            particlesScript.ResetStreakParticles();
        }

        if (GUILayout.Button("Play"))
        {
            StylizedExplosionParticles particlesScript = (StylizedExplosionParticles)target;
            particlesScript.Play();
        }
    }
}
