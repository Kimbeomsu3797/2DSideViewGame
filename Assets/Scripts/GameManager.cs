using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    //임의 생성 파트 (세이브용)
    int totalScore;
    int stageScore;

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
            updateScore();
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
    }
    void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    //세이브용 임의 생성 함수
    void updateScore()
    {

    }
}
