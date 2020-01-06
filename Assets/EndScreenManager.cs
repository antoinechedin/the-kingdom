using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    private float timer;
    public Text menuText;
    public Text surviveTime;
    public GameObject panel;
    public TaverneManager tavern;
    public Player player;
    public GameObject UI;
    private int freezedSurviveTime;
   
    private void Start()
    {
        timer = 0;
        freezedSurviveTime = 0;
        panel.SetActive(false);
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (tavern.TestifLost() && freezedSurviveTime == 0)
        {
            panel.SetActive(true);
            UI.SetActive(false);
            player.playing = false;
            freezedSurviveTime = (int)timer;
            int seconds = (int)this.freezedSurviveTime;
            int minutes = (int)seconds / 60;
            seconds = seconds - (minutes * 60);
            menuText.text = "GAME OVER\n\nThe beer isn't yours anymore.";
            surviveTime.text = "You survived " + minutes + "min " + seconds + "sec";
        }

        if (freezedSurviveTime == 0 && Input.GetButtonDown("Start"))
        {
            if (player.playing)
            {
                panel.SetActive(true);
                UI.SetActive(false);
                player.playing = false;
                menuText.text = "The game is not paused, careful M.Knight";
                surviveTime.text = "";
            }
            else{
                panel.SetActive(false);
                UI.SetActive(true);
                player.playing = true;
            }
        }
    }

}
