using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class Countdown : MonoBehaviour
{

    [SerializeField]
    Text countdownText;
    public float timeRemaining = 6f;
    public GameObject player;
    public GameObject ai;

    // Start is called before the first frame update
    void Start()
    {
        BeginCountdown();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if (timeRemaining <= 0)
        {
            player.SetActive(true);
            ai.SetActive(true);
            countdownText.enabled = false;
        }

        int displayValue = (int)timeRemaining;
        countdownText.text = displayValue.ToString();

    }

    public void BeginCountdown()
    {

        player.SetActive(false);
        ai.SetActive(false);
        countdownText.enabled = true;
        timeRemaining = 6f;

    }

}