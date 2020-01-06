using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Player player;
    public TaverneManager tavern;

    public Text beerText;
    public Text civilianText;
    public Text tavernText;

    private void Update()
    {
        beerText.text = "Beer : " + player.ressources.beer;
        civilianText.text = "Civilian : " + player.ressources.civilian;
        tavernText.text = "Tavern : " + tavern.currentHPTavern;
    }
}
