using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility_POI : MonoBehaviour
{
    public float initFood;
    public float initWater;
    public float food;
    public float water;
    public float foodGrowth; // increase per second
    public float waterGrowth; // increase per second
    public float maxFood = 0.0f;
    public float maxWater = 0.0f;

    float timeOfLastConsumption;


    void Start()
    {
        food = initFood;
        water = initWater;
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
    }
    // make methods that other objects can use to eat and drink
    public float drinkFrom(float drinkAmount)
    {
        if (drinkAmount > water) drinkAmount = water;
        water = water - drinkAmount;
        return drinkAmount;
    }
    public float eatFrom(float eatAmount)
    {

        //  float eatAmount = Mathf.Min(food, 1.0f);
        // food = food - eatAmount;
        if (eatAmount > food) eatAmount = food;
        food = food - eatAmount;
        return eatAmount; // need more complete functionality here
    }
}
