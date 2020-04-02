using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility_NPC : MonoBehaviour
{
    //----------------------------------

    public GameObject me;

    public float patrol; // food preference
    public float goalPatrol;
    public float patirst; // water preference
    public float patrolation;
    public float patrolationRate = 0.1f;

    public float damage; // food preference
    public float bloodlust; // water preference
    
    // functional variables

    public float decombatRate = 0.1f; // per second
    public float healthiness = 0.01f; // per second

    // status variables
    public float healthiety; // how full of food? 1 to 0
    public float battlewant; // how full of water? 1 to 0

    // goal variables (for now public so we can set directly to test behavior...
    public float goalhealth;
    public float goalcombat;
    


    
    //--------------------------------------
    // personality variables..
    public float appetite; // food preference
    public float thirst; // water preference
    public float wanderlust; // wandering preference
    // functional variables
    public float size = 1.0f; // will affect the utility of food & water (bigger need more)
    public float dehydrationRate = 0.1f; // per second
    public float metabolism = 0.01f; // per second

    // status variables
    public float satiety; // how full of food? 1 to 0
    public float hydration; // how full of water? 1 to 0

    // goal variables (for now public so we can set directly to test behavior...
    public float goalFood;
    public float goalWater;
    public float goalWander;

    public float speed = 1.0f;
    // public float distanceToDesire;

    // the thing we ultimately need the AI to determine: where are we going now?
    public GameObject desiredPOI = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        updateStatusVariables();

        // should have code here so we don't do this every frame... (???)
        updateGoals();

        // choose what to do and where to go
        setDestination();

        // set velocity toward desired destination...
        doMovement();

        doActions();

       if(healthiety <= -5)
        {
            Destroy(me);
        }
    }

   

    void updateStatusVariables()
    {

        //-------------------------------
        
        healthiety = healthiety - Time.deltaTime * healthiness;
        battlewant = battlewant - Time.deltaTime * decombatRate;
        
        //--------------------------------
        // satiety & hydration always go down slowly
        satiety = satiety - Time.deltaTime * metabolism;
        hydration = hydration - Time.deltaTime * dehydrationRate;

        patrolation = hydration - Time.deltaTime * patrolationRate;
        // if eating, large decrement to hunger
        // ditto for water
    }
    void updateGoals()
    {
        setGoalEat();
        setGoalDrink();
        setGoalWander();


        setGoalPatrol();
        //-------------------------

        setGoalEats();
        setGoalDrinks();

        
        //------------------------------------
    }
    //-----------------------------------------
    
    void setGoalEats()
    {
        goalhealth = damage * (0.2f - healthiety);
    }
    void setGoalDrinks()
    {
        goalcombat = bloodlust * (0.4f - battlewant);
    }
  
    
    //-------------------------------------
    void setGoalEat()
    {
        goalFood = appetite * (1 - satiety);
    }
    void setGoalDrink()
    {
        goalWater = thirst * (1 - hydration);
    }
    void setGoalWander()
    {
        goalWander = wanderlust;
    }

    void setGoalPatrol()
    {
        goalPatrol = patirst * (1 - patrolation);
    }

    // select the POI object to move towards, or wander to random point
    void setDestination()
    {
        //get list of POI's...
        GameObject[] poiList = GameObject.FindGameObjectsWithTag("POI");
        float highestDesire = 0.0f;
        foreach (GameObject poi in poiList)
        {
            float desire = calculateDestinationDesirability(poi);
            if (desire > highestDesire)
            {
                highestDesire = desire;
                desiredPOI = poi;
            }
        }
    }
    float calculateDestinationDesirability(GameObject poi)
    {
        //-----------------------------------
        
        float combatdesire = goalcombat * poi.GetComponent<Utility_POI>().combat / Vector3.Distance(poi.transform.position, this.transform.position);
        float healthdesire = goalhealth * poi.GetComponent<Utility_POI>().health / Vector3.Distance(poi.transform.position, this.transform.position);
        float patroldesire = goalPatrol * poi.GetComponent<Utility_POI>().patrol / Vector3.Distance(poi.transform.position, this.transform.position);
        //-----------------------------------
        float Waterdesire = goalWater * poi.GetComponent<Utility_POI>().water / Vector3.Distance(poi.transform.position, this.transform.position);
        float Fooddesire = goalFood * poi.GetComponent<Utility_POI>().food / Vector3.Distance(poi.transform.position, this.transform.position);
        float desire = Waterdesire + Fooddesire + combatdesire + healthdesire;
        /* calculate something based on 
         *     goal priorities, 
         *     distance, 
         *     food/water quantity, etc.
         */
        return desire; // just a stub for now...
    }
    void doMovement()
    {
        if (desiredPOI != null)
        {
            float distance = Vector3.Distance(desiredPOI.transform.position, this.transform.position);
            if (distance > 2.5)
            {
                var direction = (desiredPOI.transform.position - this.transform.position).normalized;
                Vector3 step = Time.deltaTime * speed * direction;

                this.gameObject.transform.position = this.gameObject.transform.position + step;
            }
        }
    }

    void doActions()
    {
        float distance = Vector3.Distance(desiredPOI.transform.position, this.transform.position);
        if (distance <= 2.5)
        {
            float patrolNow = goalPatrol * desiredPOI.GetComponent<Utility_POI>().patrol;

            float drinkNow = goalWater * desiredPOI.GetComponent<Utility_POI>().water;
            float eatNow = goalFood * desiredPOI.GetComponent<Utility_POI>().food;
            //----------------------------------------------
            float fightNow = goalcombat * desiredPOI.GetComponent<Utility_POI>().combat;
            float healNow = goalhealth * desiredPOI.GetComponent<Utility_POI>().health;
            //--------------------------------------------

            if (drinkNow > eatNow && drinkNow > healNow && drinkNow > fightNow && drinkNow > patrolNow)
            {
                float drinkAmount = desiredPOI.GetComponent<Utility_POI>().drinkFrom(3.0f);
                hydration = hydration + drinkAmount / size;
                if (hydration > 1.0f)
                {
                    hydration = 1.0f;
                }
            }
           else if (fightNow > healNow && fightNow > drinkNow && fightNow > eatNow && fightNow > patrolNow)
            {
                float fightAmount = desiredPOI.GetComponent<Utility_POI>().fightFrom(3.0f);
                battlewant = battlewant + fightAmount / (size + 4);
                if (battlewant > 1.0f)
                {
                    battlewant = 1.0f;
                }
            }
           else if (healNow > fightNow && healNow > eatNow && healNow > drinkNow && healNow > patrolNow)
            {
                float healthAmount = desiredPOI.GetComponent<Utility_POI>().healFrom(3.0f);
                healthiety = healthiety + healthAmount / size;
                if (battlewant > 1.0f)
                {
                    healthiety = 1.0f;
                }
            }

            else if (patrolNow > fightNow && patrolNow > eatNow && patrolNow > drinkNow && patrolNow > healNow)
            {
                float patrolAmount = desiredPOI.GetComponent<Utility_POI>().patrolFrom(3.0f);
                patrolation = patrolation + patrolAmount / size;
                if (patrolAmount > 1.0f)
                {
                    patrolation = 1.0f;
                }
            }

            else 
            {
                float eatAmount = desiredPOI.GetComponent<Utility_POI>().eatFrom(3.0f);
                satiety = satiety + eatAmount / size;
                if (satiety > 1.0f)
                {
                    satiety = 1.0f;
                }
            }
        }
    }

  
}
