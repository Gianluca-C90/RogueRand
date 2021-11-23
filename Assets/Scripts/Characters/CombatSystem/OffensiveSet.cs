using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class OffensiveSet : MonoBehaviour
{
    public List<ListOfAttacks> attacks;

    public List<ListOfAttacks> movesetList;
    public int sumOfWeights = 0;

    ListOfAttacks lastPickedAttack;

    public void Start()
    {
        sumOfWeights = 0;
    }

    public void BuildWeightedListOfAttacks()
    {
        movesetList = new List<ListOfAttacks>();
        sumOfWeights = 0;
        foreach (var attack in attacks)
        {
            sumOfWeights += attack.maxChance;
            for (int i = 0; i < attack.maxChance; i++)
            {
                movesetList.Add(attack);
            }
        }
    }

    public ListOfAttacks PickARandomAttack()
    {
        if (sumOfWeights < 0)
            BuildWeightedListOfAttacks();
        int pick = UnityEngine.Random.Range(0, movesetList.Count);
        lastPickedAttack = movesetList[pick];
        return movesetList[pick];
    }

    public void RemovePickedAttack()
    {
        List<ListOfAttacks> tempMovesetList = new List<ListOfAttacks>();
        if (movesetList.Count > 1)
        {
            foreach (var item in movesetList)
            {
                if (item.name != lastPickedAttack.name)
                {
                    tempMovesetList.Add(item);
                }
            }
            movesetList = tempMovesetList;
        }
        else
        {
            BuildWeightedListOfAttacks();
        }

        sumOfWeights = movesetList.Count;
    }
}
