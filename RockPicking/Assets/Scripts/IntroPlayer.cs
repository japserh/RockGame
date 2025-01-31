using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroPlayer : MonoBehaviour
{

    public StudioEventEmitter intro;
    public int nextSceneIndex;
    
    void Start()
    {
        intro.Play();
    }
    
    void Update()
    {
        if (!intro.IsPlaying())
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
