using UnityEngine;

public class FixedTimeScale : MonoBehaviour
{


    [SerializeField] private GameObject _bubbleObject;
    [SerializeField] private GameObject _gumObject;







    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            CharacterActionScriptsIsEnabled(false);


        }
        else
        {
            CharacterActionScriptsIsEnabled(true);
        }


    }


    public void CharacterActionScriptsIsEnabled(bool scriptIsEnabled)
    {
        _gumObject.GetComponent<SplitInHalf>().enabled = scriptIsEnabled;
        _bubbleObject.GetComponent<DoubleJump>().enabled = scriptIsEnabled;
        _gumObject.GetComponent<PlayerController>().enabled = scriptIsEnabled;
        _bubbleObject.GetComponent<PlayerController>().enabled = scriptIsEnabled;
    }


}
