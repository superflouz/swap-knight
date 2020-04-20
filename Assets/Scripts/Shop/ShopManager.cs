﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Party party;

    private int currency;
    public int Currency
    {
        get { return currency; }
        set { currency = value; }
    }

    public void GainEntityReward(Entity killed, Entity killer)
    {
        AddCurrency(killed.GoldYield);
    }

    public void AddCurrency(int amount)
    {
        Currency += amount;
    }

    public bool BuyEntity(Entity entity, Buyable buyable)
    {
        int newCurrency = Currency - buyable.GetPrice();

        if (newCurrency >= 0)
        {
            // Instantiate Entity and Add it to party
            if (party.GetFreeSlotsCount() >= entity.slotCount)
            {
                Entity newEntity = Instantiate(entity, party.transform.position, Quaternion.identity);
                newEntity.transform.position = party.transform.position + Vector3.left * 10;
                newEntity.killEvent += GainEntityReward;
                party.AddToParty(newEntity);
                Currency = newCurrency;

                return true;
            }

            return false;
        }
        else
        {
            Debug.Log("ur poor");
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Currency = 1000;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
