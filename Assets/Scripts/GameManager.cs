using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject mainImage;
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;
    public GameObject panel;
    public GameObject restartButton;
    public GameObject nextButton;
    Image titleImage;

    public GameObject timeBar;
    public GameObject timeText;
    TimeController timeCnt;
    //
    public GameObject scoreText;
    public static int totalScore;
    public int stageScore = 0;
    PlayerController playerCnt;

    public GameObject inputUI;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("InactiveImage", 1.0f);
        panel.SetActive(false);

        timeCnt = GetComponent<TimeController>();
        if(timeCnt != null)
        {
            if(timeCnt.gameTime == 0.0f)
            {
                timeBar.SetActive(false);
            }
        }
        UpdateScore();
        playerCnt = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.gameState == "gameclear")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);

            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            PlayerController.gameState = "gameend";
            if(timeCnt != null)
            {
                timeCnt.isTimeOver = true;

                int time = (int)timeCnt.displayTime;
                totalScore += time * 10;
            }
            totalScore += stageScore;
            stageScore = 0;
            UpdateScore();
            inputUI.SetActive(false);
        }
        else if(PlayerController.gameState == "gameover")
        {
            mainImage.SetActive(true);
            panel.SetActive(true);

            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            PlayerController.gameState = "gameend";
            if(timeCnt != null)
            {
                timeCnt.isTimeOver = true;
            }
            inputUI.SetActive(false);
        }
        else if(PlayerController.gameState == "playing")
        {
            GameObject player = GameObject.FindGameObjectWithTag("player");
            PlayerController playerCnt = player.GetComponent<PlayerController>();
            if(timeCnt != null)
            {
                if(timeCnt.gameTime > 0.0f)
                {

                    int time = (int)timeCnt.displayTime;

                    timeText.GetComponent<Text>().text = time.ToString();

                    if(time == 0)
                    {
                        playerCnt.GameOver();
                    }
                }
            }
        }
        if(playerCnt.score != 0)
        {
            stageScore += playerCnt.score;
            playerCnt.score = 0;
            UpdateScore();
        }
        
    }
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    //세이브용 임의 생성 함수
    void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<Text>().text = score.ToString();
    }
    public void Jump()
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        PlayerController playerCnt = player.GetComponent<PlayerController>();
        playerCnt.Jump();
    }
}
