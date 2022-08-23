using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyTrade : MonoBehaviour
{
    [SerializeField] CollectType givenType;
    [SerializeField] CollectType takenType;
    [SerializeField] int tradeAmount=5;
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "NPCTriggerCollider")
        {
          TradeCurrency(givenType, takenType, tradeAmount);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "NPCTriggerCollider")
        {
        }
    }
    void TradeCurrency(CollectType give,CollectType take, int amount)
    {
        switch (give)
        {
            case CollectType.wood:
                GameDataManager.SpendCoins(1, give);
                GameDataManager.AddCoins(amount, take);
                SoundManager.Instance.Play(SoundManager.Sounds.tradeCoin, true, false);
                break;
            case CollectType.coin:
                if (GameDataManager.CanSpendCoins(amount, CollectType.coin))
                {
                    GameDataManager.SpendCoins(amount, give);
                    GameDataManager.AddCoins(1, take);
                    SoundManager.Instance.Play(SoundManager.Sounds.tradeCoin, true, false);
                }
                
                break;
            case CollectType.gem:
                break;
            default:
                break;
        }


    }
}
