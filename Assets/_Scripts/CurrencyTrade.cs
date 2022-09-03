using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CurrencyTrade : MonoBehaviour
{
    [SerializeField] CollectType givenType;
    [SerializeField] CollectType takenType;
    [SerializeField] int givenAmount=5;
    [SerializeField] int takenAmount = 1;
    [SerializeField] TMP_Text giveText, takeText;
    [SerializeField] AudioSource tradeSound;
    private void Start()
    {
        giveText.text = givenAmount.ToString();
        takeText.text = takenAmount.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "NPCTriggerCollider")
        {
           StartCoroutine(TradeCurrency(givenType, takenType, givenAmount, takenAmount, 0.1f));
        }
            
    }
    private void OnTriggerExit(Collider other)
        {
            if (other.name == "NPCTriggerCollider")
            {
            StopAllCoroutines();
            }
    }
    IEnumerator TradeCurrency(CollectType give, CollectType take, int _givenAmount, int _takenAmount, float repeatTime = 1f)
    {
        while (true)
        {
            switch (give)
            {
                case CollectType.wood:
                    if (GameDataManager.CanSpendCoins(PlayerUnit.Instance.chopper.capacity / 10, CollectType.wood))
                    {
                        GameDataManager.SpendCoins(PlayerUnit.Instance.chopper.capacity / 10, give);
                        GameDataManager.AddCoins((PlayerUnit.Instance.chopper.capacity / 10) * _takenAmount, take);
                        tradeSound.PlayOneShot(tradeSound.clip);
                    }
                    else if (GameDataManager.CanSpendCoins(_givenAmount, CollectType.wood))
                    {
                        GameDataManager.SpendCoins(_givenAmount, give);
                        GameDataManager.AddCoins(_takenAmount, take);
                        //SoundManager.Instance.Play(SoundManager.Sounds.tradeCoin, true, false);
                        tradeSound.PlayOneShot(tradeSound.clip);
                    }

                    else
                        StopAllCoroutines();
                    break;
                case CollectType.coin:
                    if (GameDataManager.CanSpendCoins(_givenAmount, CollectType.coin))
                    {
                        GameDataManager.SpendCoins(_givenAmount, give);
                        GameDataManager.AddCoins(_takenAmount, take);
                        //SoundManager.Instance.Play(SoundManager.Sounds.tradeCoin, true, false);
                        tradeSound.PlayOneShot(tradeSound.clip);
                    }
                    else
                        StopAllCoroutines();
                    break;
                case CollectType.gem:
                    break;
                default:
                    break;
            }
        
        yield return new WaitForSeconds(repeatTime);
            
        }

    }
}
