using UnityEngine;
using UnityEngine.Animations;

public class EnabledToggler : MonoBehaviour
{

    [SerializeField] private KeyCode _key;
    [SerializeField] private GameObject target;


    void Update()
    {
        bool toggle = Input.GetKeyDown(_key);

        if (toggle)
        {
            target.SetActive(!target.activeInHierarchy);
        }


    }

}