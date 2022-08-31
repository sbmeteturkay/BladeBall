using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenuUI : MonoBehaviour
{
	[Header("UI elements")]
	[SerializeField] GameObject itemPrefab;
	[Header("Layout Settings")]
	[SerializeField] float itemSpacing = .5f;
	float itemHeight;
	[SerializeField] Transform UpgradeItemsContainer;
	[SerializeField] UpgradeMaterial[] upgradeMaterials;
	List<UpgradeMenuItem> upgradeMenuItems;
    private void Start()
    {
		upgradeMenuItems = new List<UpgradeMenuItem>();
		GenerateShopItemsUI();
    }
    void GenerateShopItemsUI()
	{
		//Delete itemTemplate after calculating item's Height :
		itemHeight = UpgradeItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
		Destroy(UpgradeItemsContainer.GetChild(0).gameObject);
		//DetachChildren () will make sure to delete it from the hierarchy, otherwise if you 
		//write UpgradeItemsContainer.ChildCount you w'll get "1"
		UpgradeItemsContainer.DetachChildren();

		//Generate Items
		for (int i = 0; i < upgradeMaterials.Length; i++)
		{
			//Create a Character and its corresponding UI element (uiItem)
			UpgradeMenuItem uiItem = Instantiate(itemPrefab, UpgradeItemsContainer).GetComponent<UpgradeMenuItem>();
			upgradeMaterials[i].GetPlayerPref();
			upgradeMenuItems.Add(uiItem);
			//Move item to its position
			uiItem.SetItemPosition(Vector2.right * i * (itemHeight + itemSpacing));

			//Set Item name in Hierarchy (Not required)
			uiItem.gameObject.name = "Item" + i + "-" + upgradeMaterials[i].name;

			//Add information to the UI (one item)
			UpdateUIValues(i);
			uiItem.OnButtonUpgrade(i,UpdateMaterial);
			if (!upgradeMaterials[i].CanUpdateLevel())
			{
				//Character is Purchased
				uiItem.SetCharacterAsMaxLevel();
				//uiItem.OnItemSelect(i, OnItemSelected);
			}
			else
			{
				//Character is not Purchased yet
				//uiItem.OnItemPurchase(i, OnItemPurchased);
			}
			
			//Resize Items Container
			/*UpgradeItemsContainer.GetComponent<RectTransform>().sizeDelta =
				Vector2.zero * ((itemHeight + itemSpacing) * upgradeMaterials.Length + itemSpacing);*/

			//you can use VerticalLayoutGroup with ContentSizeFitter to skip all of this :
			//(moving items & resizing the container)
		}

	}
	void UpdateUIValues(int i)
    {
		upgradeMenuItems[i].SetCharacterName(upgradeMaterials[i].upgradeName);
		upgradeMenuItems[i].SetCharacterPrice(upgradeMaterials[i].price);
		upgradeMenuItems[i].SetCharacterLevel(upgradeMaterials[i].level);
		upgradeMenuItems[i].SetImage(upgradeMaterials[i].referenceImage);
		if (!upgradeMaterials[i].CanUpdateLevel())
		{
			//Character is Purchased
			upgradeMenuItems[i].SetCharacterAsMaxLevel();
			//uiItem.OnItemSelect(i, OnItemSelected);
		}
	}
	void UpdateMaterial(int index)
    {
        if (upgradeMaterials[index].UpdateAction())
        {
			SoundManager.Instance.Play(SoundManager.Sounds.upgrade,true);
			UpdateUIValues(index);
        }
        else {
			SoundManager.Instance.Play(SoundManager.Sounds.notEnoughMoney,true);
			upgradeMenuItems[index].AnimateShakeItem();
		}
		
    }

}
