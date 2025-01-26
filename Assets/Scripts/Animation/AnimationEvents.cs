using System.Collections;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{

    private AudioManager _audio;


    private void Awake()
    {
        _audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ShowDefeatPanelAfterDeath()
    {
        Debug.Log("Tentou mostrar o painel");
        GameController.Instance.LoseLife(gameObject.tag);
    }

    public void BubbleDie()
    {
        _audio.PlaySFX(_audio.bubbleDeath);

    }

    public void GumDie()
    {
        _audio.PlaySFX(_audio.gumDeath);
    }

    public void BubbleJump()
    {

        _audio.PlaySFX(_audio.bubbleJump);

    }

    public void GumJump()
    {

        _audio.PlaySFX(_audio.gumJump);
    }



}
