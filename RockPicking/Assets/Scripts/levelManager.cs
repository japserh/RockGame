using UnityEngine;

public class levelManager : MonoBehaviour
{
    public int currentLevel = 0;
    public bool correctTooth = false;
    public KeyCode confirm = KeyCode.Space;

    private void Update()
    {
        if (correctTooth)
        {
            if(Input.GetKeyDown(confirm))
            {
                currentLevel++;
            }
        }
    }




}
