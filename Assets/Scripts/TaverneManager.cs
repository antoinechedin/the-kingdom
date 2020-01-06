using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaverneManager : MonoBehaviour
{
    public int currentHPTavern;
    public int maxHPTavern;
    private BoxCollider2D frontDoorCollider;

    private void Start()
    {
        currentHPTavern = maxHPTavern;
        frontDoorCollider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D thingThatCollided)
    {
        if (thingThatCollided.tag == "Monster")
        {
            TestifLost();
            currentHPTavern = currentHPTavern - 1; //ou on aurait pu l'écrire currentHPTavern -= 1; currentHPTavern--;  
            Destroy(thingThatCollided.gameObject);
            if (currentHPTavern == 0)
            {
                TestifLost();
            }
        }
    }
    public bool TestifLost ()
    {
        if (currentHPTavern <= 0)
        {
            return true;
        }
        else return false;
    }
}


