using System;
using FMODUnity;
using UnityEngine;
using Random = UnityEngine.Random;

public class levelManager : MonoBehaviour
{
    public int currentLevel = 0;
    public int currentTooth = 0;
    public int correctTooth = 0;

    public SerialRotator wheel;
    public KeyCode confirm = KeyCode.Space;
    public StudioEventEmitter correctClick;

    private void Start()
    {
        StartLevel(currentLevel);
    }

    private void Update()
    {
        currentTooth = wheel.position;
        
        if (currentTooth == correctTooth)
        {
            correctClick.Play();
            Debug.Log("Correct!");
            if(Input.GetKeyDown(confirm))
            {
                currentLevel++;
                StartLevel(currentLevel);
            }
        }
    }

    private void StartLevel(int level)
    {
        correctTooth = Random.Range(0, 40);
    }
    
    
}
