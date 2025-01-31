using System;
using FMODUnity;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public int level = 1;
    public int stage = -1;
    public int stagesPerLevel = 0;
    public int winLevel = 4;
    
    public int currentTooth = 0;
    public int correctTooth = 0;
    public bool justMoved = false;

    public float confirmDelay = 1;
    private float confirmTimer;

    public SerialRotator wheel;
    public KeyCode confirm = KeyCode.Space;
    
    public float threatDistance = 0;
    public float threatStartingDistance = 50;
    public float threatSpeed = 1;
    public float threatDecreasePerLevel = 20;

    public int nextSceneIndex = 0;
    
    [SerializeField] private StudioEventEmitter latchSound;
    [SerializeField] private StudioEventEmitter passStageSound;
    [SerializeField] private StudioEventEmitter passLevelSound;
    [SerializeField] private StudioEventEmitter wrongPinSound;
    [SerializeField] private StudioEventEmitter loseSound;
    [SerializeField] private StudioEventEmitter winSound;
    
    public BackgroundSoundManager backgroundSound;
    
    private void Start()
    {
        StartLevel();
        threatDistance = threatStartingDistance;
    }

    private void Update()
    {
        threatDistance -= Time.deltaTime * threatSpeed;
        justMoved = (currentTooth != wheel.position);
        currentTooth = wheel.position;
        confirmTimer -= Time.deltaTime;
        
        if ((currentTooth == correctTooth) && justMoved)
        {
            latchSound.Play();
        }
        
        if ((confirmTimer <= 0f) | Input.GetKeyDown(confirm))
        {
            ConfirmPosition();
        }
        
        backgroundSound.SetThreatParameter((1 - (threatDistance/threatStartingDistance)) * 100);
        Debug.Log("threat percentage = " + ((1 - (threatDistance / threatStartingDistance)) * 100));
        
        if (threatDistance <= 0f)
        {
            loseGame();
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
        else
        {
            wrongPinSound.Play();
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
            threatDistance += threatDecreasePerLevel;
            if (level == winLevel)
            {
                winGame();
                Invoke(nameof(Restart),5);
            }
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
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Level", level);
    }
    
    private void winGame()
    {
        winSound.Play();
        Debug.Log("Win!!! yay");
        Invoke(nameof(Application.Quit),5f);
    }
    
    private void loseGame()
    {
        loseSound.Play();
        Debug.Log("Loooosseeeee");
        Invoke(nameof(Restart),5);
    }

    private void Restart()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }

}
