using UnityEngine;

public class SystemInitializer : MonoBehaviour
{
   
    void Awake()
    {
        Application.targetFrameRate = 60;
    }

}
