using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;

public class UpgradeMenuItem : MonoBehaviour
{

	[Space(20f)]
	[SerializeField] TMP_Text characterNameText;
	[SerializeField] TMP_Text characterPriceText;
	[SerializeField] TMP_Text characterLevelText;
	[SerializeField] Button characterPurchaseButton;

	[Space(20f)]
	//[SerializeField] Button itemButton;
	[SerializeField] Image itemImage;
	[SerializeField] GameObject selected;

	//--------------------------------------------------------------
	public void SetItemPosition(Vector2 pos)
	{
		GetComponent<RectTransform>().anchoredPosition += pos;
	}

	public void SetImage(Sprite img)
    {
		itemImage.sprite=img;
    }
	public void SetCharacterName(string name)
	{
		characterNameText.text = name;
	}
	public void SetCharacterLevel(int level)
	{
		characterLevelText.text = level.ToString();
	}
	public void SetCharacterPrice(float price)
	{
		characterPriceText.text = price.ToString();
	}

	public void SetCharacterAsMaxLevel()
	{
		characterPurchaseButton.gameObject.SetActive(false);
		//itemButton.interactable = false;
		characterPriceText.text = "MAXED";
		//itemImage.color = itemNotSelectedColor;
	}
	public void OnItemPurchase(int itemIndex, UnityAction<int> action)
	{
		characterPurchaseButton.onClick.RemoveAllListeners();
		characterPurchaseButton.onClick.AddListener(() => action.Invoke(itemIndex));
	}
	public void OnButtonUpgrade(int itemIndex, UnityAction<int> action)
	{
		characterPurchaseButton.onClick.RemoveAllListeners();
		characterPurchaseButton.onClick.AddListener(() => action.Invoke(itemIndex));
	}

	public void OnItemSelect(int itemIndex, UnityAction<int> action)
	{
		//itemButton.interactable = true;

		//itemButton.onClick.RemoveAllListeners();
		//itemButton.onClick.AddListener(() => action.Invoke(itemIndex));
	}

	public void SelectItem()
	{
		//itemOutline.effectColor = new Color(255, 255, 255, 255);
		//itemImage.color = itemSelectedColor;
		//itemButton.interactable = false;
		selected.SetActive(true);
	}

	public void DeselectItem()
	{
		//itemOutline.enabled = false;
		//itemOutline.effectColor = new Color(255, 255, 255, 100);
		//itemImage.color = itemNotSelectedColor;
		//itemButton.interactable = true;
		selected.SetActive(false);
	}

	public void AnimateShakeItem()
	{
		//End all animations first
		transform.DOComplete();

		transform.DOShakePosition(1f, new Vector3(8f, 0, 0), 10, 0).SetEase(Ease.Linear);
	}
}