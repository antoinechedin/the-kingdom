using UnityEngine;

public class Player : Actor
{
    public float moveSpeed;
    public PathfindingManager pathfinding;
    public TurretSpot currentTurret = null;
    public BoxCollider2D actionCollider;
    public PlayerRessources ressources;

    protected override void Awake()
    {
        base.Awake();
        ressources = new PlayerRessources();
    }

    private void Update()
    {
        Vector2 moveDir;
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        float moveMag = Mathf.Clamp(moveDir.magnitude, 0f, 1f);
        moveDir = moveDir.normalized * moveMag;
        moveVec = moveDir * moveSpeed;

        // Move action object
        if (actionCollider != null && moveDir.magnitude > 0)
            actionCollider.transform.localPosition = new Vector2(0, 0.5f) + moveDir.normalized / 2f;

        if (moveVec.x > 0) sr.flipX = false;
        if (moveVec.x < 0) sr.flipX = true;
        animator.SetFloat("Speed", moveDir.magnitude);

        if (currentTurret != null)
        {
            if (Input.GetButtonDown("Action"))
            {
                bool upgradeSuccess = currentTurret.UpgradeTurret(ref ressources);
                if (upgradeSuccess) //ou dans ce cas if (upgradeSuccess == true)
                {
                    Debug.Log("Amelioration effectuee");
                }
                else
                {
                    Debug.Log("Echec de l'amelioration");
                }
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D thingThatCollide)
    {
        Debug.Log("Touch");
        if (thingThatCollide.tag == "Beer")
        {
            this.ressources.beer++;
            Destroy(thingThatCollide.gameObject);
        }
        else if (thingThatCollide.tag == "Civilian")
        {
            this.ressources.civilian++;
            Destroy(thingThatCollide.gameObject);
        }
    }
}

