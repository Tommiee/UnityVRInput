using UnityEngine;
using UnityEngine.Animations;

public class RestartGame : MonoBehaviour
{
    void Update()
    {
        bool restart = Input.GetKeyDown(KeyCode.R);
        
        if (restart)
        {
            SceneUtils.Reset();
        }
       
       
    }

}