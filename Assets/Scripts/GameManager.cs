using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;
    public Image[] UIHealth;
    public Text UIPoint;
    public Text UIStage;
    public GameObject RestartBtn;
   
    



    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();        
    }

    public void NextStage()
    {
        //Change Stage
        if(stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "Stage : " + (stageIndex + 1);
        }
        else
        {
            //Game Clear
            //Player Controll Lock
            Time.timeScale = 0;

            //Result UI
            Debug.Log("Game Clear!");
            //Restart Button UI
            Text btnText = RestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Game Clear!";
            RestartBtn.SetActive(true);
            
        }
        

        //calculate Point
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if(health > 1)
        {
            health--;
            UIHealth[health].color = new Color(1, 0, 0, 0.4f); 
        }
        else
        {
            //All Health UI Off
            UIHealth[0].color = new Color(1, 0, 0, 0.4f);
            //Player Die Effect
            player.OnDie();

            //Result UI
            Debug.Log("You Die");
            //Retry Button UI
            RestartBtn.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Player Reposition
            if(health > 0)
            {
                PlayerReposition();
            }

            //Health Down
            HealthDown();
        }    
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
