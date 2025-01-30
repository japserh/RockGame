using System;
using FMODUnity;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public int level = 1;
    public int stage = -1;
    public int stagesPerLevel = 0;
    public int winLevel = 4;
    
    public int currentTooth = 0;
    public int correctTooth = 0;

    public float confirmDelay = 1;
    private float confirmTimer;

    public SerialRotator wheel;
    public KeyCode confirm = KeyCode.Space;
    
    private float catTime = 0;
    public float göpySlöpyTime = 120;
    
    [SerializeField] private StudioEventEmitter latchSound;
    [SerializeField] private StudioEventEmitter passStageSound;
    [SerializeField] private StudioEventEmitter passLevelSound;
    [SerializeField] private StudioEventEmitter loseSound;
    [SerializeField] private StudioEventEmitter winSound;
    
    public BackgroundSoundManager backgroundSound;
    
    private void Start()
    {
        StartLevel();
        catTime = göpySlöpyTime / 2;
    }

    private void Update()
    {
        catTime += Time.deltaTime;
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
        
        backgroundSound.SetThreatParameter(catTime/göpySlöpyTime * 100);
        
        if (catTime > göpySlöpyTime)
        {
            loseGame();
        }
    }

    private void loseGame()
    {
        loseSound.Play();
        Debug.Log("Loooosseeeee");
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
        correctTooth = correctTooth + Random.Range(5, 35);
        correctTooth %= 40;
        
        if (stage == stagesPerLevel)
        {
            level++;
            stage = 0;
            StartLevel();
            if (level == winLevel)
            {
                winGame();
            }
        }
        passStageSound.Play();
    }

    private void winGame()
    {
        winSound.Play();
        Debug.Log("Win!!! yay");
        Invoke(nameof(Application.Quit),5f);
    }

    private void ResetTimer()
    {
        confirmTimer = confirmDelay;
    }

    private void StartLevel()
    {
        passLevelSound.Play();
        latchSound.SetParameter("Level",level);
    }

}
