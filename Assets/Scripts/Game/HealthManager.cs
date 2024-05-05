using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100f;
    private float health;

    public Image fillImage;
    public bool isEnemy;

    public ObjectiveManager OM;

    void Awake()
    {
        if (isEnemy) { OM.AddObjective(); }

        //Initially full health
        health = maxHealth;

        //Fill bar according to health
        fillImage.fillAmount = health / maxHealth;
    }

    public float GetHealth() { return health; }
    public void TakeHealth(float take)
    {
        health -= take;
        Refresh();
    }
    public void GiveHealth(float give)
    {
        health += give;
        Refresh();
    }

    private void Refresh()
    {

        //Check if health is empty
        if (health <= 0f)
        {

            IDamageable damageableObject = this.gameObject.GetComponent<IDamageable>();

            //Runs when an enemy dies
            if (isEnemy)
            {
                //Decrement objectives in level
                OM.RemoveObjective();
            }
            else
            //Runs when the player dies
            {
                //Hide health bar
                //althBar.SetActive(false);
            }
            damageableObject.Explode(this.gameObject.transform.position);
        }
        else //Health > 0
        {
            //Check if health overflowed
            if (health > maxHealth) { health = maxHealth; }

            //Fill bar according to health
            fillImage.fillAmount = health / maxHealth;
        }
    }
}
