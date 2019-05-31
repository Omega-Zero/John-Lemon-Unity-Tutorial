using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{

    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCavasGroup;
    bool m_IsPlayerAtExit;
    float m_Timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel();
        }

       
    }

    void EndLevel()
    {
        //Setting timer
        m_Timer += Time.deltaTime;
        //Image fades when exit reached 
        exitBackgroundImageCavasGroup.alpha = m_Timer / fadeDuration;
        //if timer is greater than desired duration of fade quit out the game
        if (m_Timer > fadeDuration + displayImageDuration)
        {
            Application.Quit();
        }
    }


}
