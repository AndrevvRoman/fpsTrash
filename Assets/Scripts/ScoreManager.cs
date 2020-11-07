using System.Collections;
using System.Collections.Generic;
using TMPro;
using Mirror;
using UnityEngine;

public class ScoreManager : NetworkBehaviour
{

    public GameObject redScoreObj;
    public GameObject blueScoreObj;
    TextMeshPro redScoreText;
    TextMeshPro blueScoreText;
    float m_lastTimeAdded = 0;
    [SyncVar]int redScore = 0;
    [SyncVar]int blueScore = 0;

    void Start()
    {
        redScoreText = redScoreObj.GetComponent<TextMeshPro>();
        blueScoreText = blueScoreObj.GetComponent<TextMeshPro>();
    }

    void Update()
    {
        redScoreText.SetText(redScore.ToString());
        blueScoreText.SetText(blueScore.ToString());
    }

    public void AddScore(Team team)
    {
        if (Time.time - m_lastTimeAdded > 1f) //задержка следующего прибавления, тк одно исполнение cmd может занимать несколько кадров 
        {
            m_lastTimeAdded = Time.time;
            if (team == Team.Red) //вызывает объект у которого 0 хп, прибавляем очки противоположной команде
            {
                blueScore++;
            }
            else if (team == Team.Blue)
            {
                redScore++;
            }
        }
    }
}
