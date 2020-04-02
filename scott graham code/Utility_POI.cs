using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility_POI : MonoBehaviour
{
    public float status = 1;
    public GameObject medkit;

    public float initPatrol;
    public float patrol;
    public float patrolGrowth;
    public float maxPatrol = 0.0f;

    public float initFood;
    public float initWater;
    public float food;
    public float water;
    public float foodGrowth; // increase per second
    public float waterGrowth; // increase per second
    public float maxFood = 0.0f;
    public float maxWater = 0.0f;

    //----------------------------------------------
    
    public float inithealth;
    public float initcombat;
    public float health;
    public float combat;
    public float healthGrowth; // increase per second
    public float combatGrowth; // increase per second
    public float maxhealth = 0.0f;
    public float maxcombat = 0.0f;
    
    //--------------------------------------

    float timeOfLastConsumption;

   
      
        
    
    void Start()
    {
        food = initFood;
        water = initWater;
        health = inithealth;
        combat = initcombat;
        patrol = initPatrol;
    }
    // Update is called once per frame


    void Update()
    {
        // if something is eating decrease food 
        // if something is drinking decrease water
        // always sloooooowly increase food & water, if less than max...
        if (food < maxFood)
        {
            food = food + foodGrowth * Time.deltaTime;
            //make sure we never go over max...
            if (food > maxFood) food = maxFood;
        }
        if (water < maxWater)
        {
            water = water + waterGrowth * Time.deltaTime;
            //never go over max water
            if (water > maxWater) water = maxWater;
        }

        if (patrol < maxPatrol)
        {
            patrol = patrol + patrolGrowth * Time.deltaTime;
            //never go over max water
            if (patrol > maxPatrol) patrol = maxPatrol;
        }

        //------------------------------------

        if (health < maxhealth)
        {

            health = health + healthGrowth * Time.deltaTime;
            //make sure we never go over max...
            //if (health > maxhealth) health = maxhealth;
        }
        if (combat < maxcombat)
        {
            combat = combat + combatGrowth * Time.deltaTime;
            //never go over max water
            if (combat > maxcombat) combat = maxcombat;
        }
        
        if(healthGrowth > 0 && health <= 20)
        {
            Destroy(medkit);
        }
        //----------------------------------
    }
    // make methods that other objects can use to eat and drink
    public float drinkFrom(float drinkAmount)
    {
        if (drinkAmount > water) drinkAmount = water;
        water = water - drinkAmount;
        return drinkAmount;
    }
    public float patrolFrom(float patrolAmount)
    {
        if (patrolAmount > patrol) patrolAmount = patrol;
        water = water - patrolAmount;
        return patrolAmount;
    }
    public float eatFrom(float eatAmount)
    {

        //  float eatAmount = Mathf.Min(food, 1.0f);
        // food = food - eatAmount;
        if (eatAmount > food) eatAmount = food;
        food = food - eatAmount;
        return eatAmount; // need more complete functionality here
    }

    //---------------------------------
    
    public float fightFrom(float fightAmount)
    {
        if (fightAmount > combat) fightAmount = combat;
        combat = combat - fightAmount;
        return fightAmount;
    }
    public float healFrom(float healthAmount)
    {

        //  float eatAmount = Mathf.Min(food, 1.0f);
        // food = food - eatAmount;
        if (healthAmount > health) healthAmount = health;
        health = health - healthAmount;
        return healthAmount; // need more complete functionality here
    }


   
    //----------------------------------
}
