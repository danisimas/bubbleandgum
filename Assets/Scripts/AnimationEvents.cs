using System.Collections;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ShowDefeatPanelAfterDeath()
    {
        Debug.Log("Tentou mostrar o painel");
        GameController.Instance.LoseLife(gameObject.tag);
    }
}
