using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : Observer
{
    private PlayerMovement player;
    public GameObject checkpointTextObject;
    private int score;

    private void Awake()
    {
        if (checkpointTextObject == null)
        {
            checkpointTextObject.SetActive(true);
            checkpointTextObject = GameObject.Find("score");
        }

    }

    private void Start()
    {
        checkpointTextObject = GameObject.Find("score");
    }

    // Update is called once per frame
    void Update()
    {
        if (checkpointTextObject != null)
        {
            checkpointTextObject.GetComponent<Text>().text = ("Score:" + score);
        }
    }

    public override void Notify(Subject subject)
    {
        player = subject.GetComponent<PlayerMovement>();
        score = player.score;
    }
}
