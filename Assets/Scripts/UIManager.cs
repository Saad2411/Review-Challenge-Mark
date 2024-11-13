using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Observer
{
    private PlayerMovement player;
    public GameObject scoreTextObject;
    private int _score;

    private void Awake()
    {
        if (scoreTextObject == null)
        {
            
            scoreTextObject.SetActive(true);
            scoreTextObject = GameObject.Find("score");
        }

    }

    private void Start()
    {
        scoreTextObject = GameObject.Find("score");
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreTextObject != null)
        {
            scoreTextObject.GetComponent<Text>().text = ("Score:" + _score.ToString());
        }
    }

    public override void Notify(Subject subject)
    {
        player = subject.GetComponent<PlayerMovement>();
        _score = player.score;
    }
}
