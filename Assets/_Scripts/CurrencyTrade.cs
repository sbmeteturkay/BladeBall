using TMPro;
using UnityEngine;

public class CurrencyTrade : MonoBehaviour
{
    [SerializeField] CollectType givenType;
    [SerializeField] CollectType takenType;
    [SerializeField] int givenAmount=5;
    [SerializeField] int takenAmount = 1;
    [SerializeField] TMP_Text giveText, takeText;
    private void Start()
    {
        giveText.text = givenAmount.ToString();
        takeText.text = takenAmount.ToString();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "NPCTriggerCollider")
        {
          TradeCurrency(givenType, takenType, givenAmount,takenAmount);
        }
    }
    void TradeCurrency(CollectType give,CollectType take, int _givenAmount,int _takenAmount)
    {
        switch (give)
        {
            case CollectType.wood:
                if (GameDataManager.CanSpendCoins(_givenAmount, CollectType.wood))
                {
                    GameDataManager.SpendCoins(_givenAmount, give);
                    GameDataManager.AddCoins(_takenAmount, take);
                    SoundManager.Instance.Play(SoundManager.Sounds.tradeCoin, true, false);
                }
                break;
            case CollectType.coin:
                if (GameDataManager.CanSpendCoins(_givenAmount, CollectType.coin))
                {
                    GameDataManager.SpendCoins(_givenAmount, give);
                    GameDataManager.AddCoins(_takenAmount, take);
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
