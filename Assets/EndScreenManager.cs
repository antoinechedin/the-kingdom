using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    private float timer;
    public Text surviveTime;
    public TaverneManager tavern;
    private void Awake()
    {
        timer = 0; 
    }
    private void Update()
    {
        timer += Time.deltaTime;
        int seconds = (int)this.timer;
        int minutes = (int)seconds / 60;
        seconds = seconds - (minutes * 60);
        surviveTime.text = "You survived " + minutes + "min " + seconds + "sec";
    }

}
