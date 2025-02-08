using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//This class just moves the clouds according to differents speeds for now,
//But it could be extended to manage clouds generation with Unity built-in ObjectPool system
//And check distance from the player to adjust spawning and despawning of clouds instances.
public class Clouds : MonoBehaviour
{
    [SerializeField]
    private GameObject[] clouds;


    [SerializeField]
    private float[] cloudsSpeedFactors;

    [SerializeField]
    private Vector2 windDirection;

    /*
    private Vector3[] startPositions;

    private void Start()
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            startPositions[i] = clouds[i].transform.position;
        }
    }
    */
    void Update()
    {
        Vector3 pos;

        for (int i = 0; i < clouds.Length ; i++)
        {
            GameObject cloud = clouds[i];
            pos = cloud.transform.position;
            pos.x += windDirection.x * Time.deltaTime * cloudsSpeedFactors[i];
            pos.z += windDirection.y * Time.deltaTime * cloudsSpeedFactors[i];
            cloud.transform.position = pos;
        }
    }
}
