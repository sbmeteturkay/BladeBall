using UnityEngine;
using TMPro;
using UnityEngine.UI;
//This code is taken from https://github.com/herbou/Unity_ShoppingSystem
public class GameSharedUI : MonoBehaviour
{
	#region Singleton class: GameSharedUI

	public static GameSharedUI Instance;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	#endregion

	[SerializeField] TMP_Text[] coinsUIText;
	[SerializeField] TMP_Text[] gemUIText;
	[SerializeField] TMP_Text[] woodUIText;
	[SerializeField] Image woodSlider;

	void Start()
	{
		UpdateCoinsUIText();
	}

	public void UpdateCoinsUIText()
	{
		for (int i = 0; i < coinsUIText.Length; i++)
		{
			SetCoinsText(coinsUIText[i], GameDataManager.GetCoins());
			SetCoinsText(gemUIText[i], GameDataManager.GetGem());
			SetCoinsText(woodUIText[i], GameDataManager.GetWood());
		}
		UpdateWoodFillAmount();
	}

	void SetCoinsText(TMP_Text textMesh, int value)
	{
		// if (value >= 1000000)...
		// .....

		if (value >= 1000)
			textMesh.text = string.Format("{0}K.{1}", (value / 1000), GetFirstDigitFromNumber(value % 1000));
		else
			textMesh.text = value.ToString();
	}

	int GetFirstDigitFromNumber(int num)
	{
		return int.Parse(num.ToString()[0].ToString());
	}
	void UpdateWoodFillAmount()
    {
		woodSlider.fillAmount = GameDataManager.WoodFillAmount();
		PlayerUnit.Instance.UpdateCapacityAnimation();
	}
}