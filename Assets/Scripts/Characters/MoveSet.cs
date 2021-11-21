using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSet : MonoBehaviour
{
    [SerializeField] List<ListOfAttacks> attacks;

    List<ListOfAttacks> movesetList;
    int sumOfWeights;

    private void Start()
    {
        movesetList = new List<ListOfAttacks>();
        BuildWeightedListOfAttacks();
        PickARandomAttack();
        
    }

    void BuildWeightedListOfAttacks()
    {
        foreach (var attack in attacks)
        {
            sumOfWeights += attack.maxChance;
            for (int i = 0; i < attack.maxChance; i++)
            {
                movesetList.Add(attack);
            }
        }
    }

    void PickARandomAttack()
    {
        int pick = UnityEngine.Random.Range(0, movesetList.Count);
        RemovePickedAttack(movesetList[pick]);
    }

    void RemovePickedAttack(ListOfAttacks picked)
    {
        List<ListOfAttacks> tempMovesetList = new List<ListOfAttacks>();
        foreach (var item in movesetList)
        {
            if (item.name != picked.name)
            {
                tempMovesetList.Add(item);
            }
        }

        movesetList = tempMovesetList;
    }
}
