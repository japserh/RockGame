using System;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using FMODUnity;


public class SerialRotator : SerialReader
{
    public int rotationSteps = 40;
    public float lerpFactor = 0.1f;
    
    private readonly string rotation_prefix = "pos:";
    private readonly string direction_prefix = "dir:";
    
    private int rotation;
    private int direction;

    private StudioEventEmitter emitter;

   private void Awake()
    {
        emitter = GetComponent<StudioEventEmitter>();
  
    }


    public override void HandleMessage(string message)
    {
        int pos = 0;
        int dir = 0;
        string[] words = message.Split(" ");
        
        if (words[0] == rotation_prefix)
        {
            int.TryParse(words[1], out pos);
        }
        if (words[2] == direction_prefix)
        {
            int.TryParse(words[1], out dir);
        }
        
        // 20 step Rotary Encoder
        rotation = pos * (360 / rotationSteps);
        direction = dir;

        emitter.Play();
    }

    private void LateUpdate()
    {
        SetRotation(rotation);  
    }

    private void SetRotation(int rotation)
    {
        var target = Quaternion.Euler(0, rotation, 0);
        gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.localRotation, target, lerpFactor);
    }
}
