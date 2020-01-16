using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpot : MonoBehaviour
{
    public int state;
    public Sprite barrel;
    public GameObject towerPrefab;

    private void Awake()
    {
        state = 0;
    }
    private void OnTriggerEnter2D(Collider2D thingThatCollided)
    {
        if (thingThatCollided.tag == "Player")
        {
            thingThatCollided.GetComponent<Player>().currentTurret = this;
        }
    }
    private void OnTriggerExit2D(Collider2D thingThatCollided)
    {
        if (thingThatCollided.tag == "Player")
        {
            thingThatCollided.GetComponent<Player>().currentTurret = null;

        }
    }
    public bool UpgradeTurret(ref PlayerRessources ressources)
    {
        if (this.state < 2)
        {
            if (this.state == 0 && ressources.beer > 0) 
            {
                this.state++;
                ressources.beer--;
                this.GetComponent<SpriteRenderer>().sprite = this.barrel;
                return true;
            }
            if (this.state == 1 && ressources.civilian > 0)
            {
                // Deux façons d'écrire un nombre à virgules
                //double d = 0.5;
                //float f = 0.5f;
                this.state++;
                ressources.civilian--;
                Instantiate(towerPrefab, this.transform.position + new Vector3(0.05f, 0.25f, 0), Quaternion.identity, this.transform);
                return true;
            }
            return false;
        } 
        return false;        
    }
}
