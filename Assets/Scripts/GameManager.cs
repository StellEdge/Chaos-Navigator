using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int gameState;
    public GameObject SpawnManager;
    public GameObject MapManager;

    public float gameTime;
    public float timeScaleRate;
    public int stateTimeClip;

    public float maxTime;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime < maxTime)
            MapManager.GetComponent<MapManger>().setTargetScale(1 + timeScaleRate * ((int)gameTime) / stateTimeClip);
    }
}
