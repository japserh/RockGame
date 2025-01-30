using System;
using FMODUnity;
using UnityEngine;

public class BackgroundSoundManager : MonoBehaviour
{
    public LevelManager levelManager;
    
    [SerializeField] private StudioEventEmitter threatSound;
    [SerializeField] private StudioEventEmitter ambienceSound;
    [SerializeField] private StudioEventEmitter goalSound;

    private float level;
    public float lerpFactor;
    
    private void Update()
    {
        level = Mathf.Lerp(level,levelManager.level,Time.deltaTime * lerpFactor);

        ambienceSound.SetParameter("Depth", level * 20);
        goalSound.SetParameter("Goal_Distance", 100 - level * 20);
        
    }

    public void SetThreatParameter(float percentage)
    {
        threatSound.SetParameter("Threat_Distance", percentage);
    }
}
