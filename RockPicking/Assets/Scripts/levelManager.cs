using System;
using FMODUnity;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class levelManager : MonoBehaviour
{
    public int level = 0;
    public int stage = 0;
    public int stagesPerLevel = 0;
    
    public int currentTooth = 0;
    public int correctTooth = 0;

    public float confirmDelay = 1;
    private float confirmTimer;

    public SerialRotator wheel;
    public KeyCode confirm = KeyCode.Space;
    
    [SerializeField] private StudioEventEmitter latchSound;
    [SerializeField] private StudioEventEmitter passStageSound;
    [SerializeField] private StudioEventEmitter passLevelSound;
    
    private void Start()
    {
        StartLevel();
    }

    private void Update()
    {
        currentTooth = wheel.position;
        confirmTimer -= Time.deltaTime;
        
        if (currentTooth == correctTooth)
        {
            latchSound.Play();
        }
        
        if ((confirmTimer <= 0f) | Input.GetKeyDown(confirm))
        {
            ConfirmPosition();
        }
    }

    public void onWheelPositionChanged()
    {
        ResetTimer();
    }

    private void ConfirmPosition()
    {
        if (currentTooth == correctTooth)
        {
            Success();
        }
        ResetTimer();
    }

    private void Success()
    {
        stage++;
        correctTooth = Random.Range(0, 40);
        if (stage == stagesPerLevel)
        {
            level++;
            stage = 0;
            StartLevel();
        }
        passStageSound.Play();
    }

    private void ResetTimer()
    {
        confirmTimer = confirmDelay;
    }

    private void StartLevel()
    {
        passLevelSound.Play();
        latchSound.SetParameter("level",level);
    }

}
