using System.Collections.Generic;
using UnityEngine;
//Shop Data Holder
[System.Serializable]


//This code is taken from https://github.com/herbou/Unity_ShoppingSystem


public class CharactersShopData
{
	public List<int> purchasedCharactersIndexes = new List<int>();
}
//Player Data Holder
[System.Serializable]
public class PlayerData
{
	public int woods = 0;
	public int coins = 0;
	public int gems = 0;
	public int selectedCharacterIndex = 0;
}
public enum CollectType
{
	wood,
	coin,
	gem
}
public static class GameDataManager
{
	static PlayerData playerData = new PlayerData();
	static CharactersShopData charactersShopData = new CharactersShopData();

	static Blade selectedCharacter;

	static GameDataManager()
	{
        if (Application.isEditor)
        {
			BinarySerializer.DeleteDataFile("characters-shop-data.txt");
			BinarySerializer.DeleteDataFile("player-data.txt");
		}
			
		LoadPlayerData();
		LoadCharactersShopData();
	}

	//Player Data Methods -----------------------------------------------------------------------------
	public static Blade GetSelectedCharacter()
	{
		return selectedCharacter;
	}

	public static void SetSelectedCharacter(Blade character, int index)
	{
		selectedCharacter = character;
		playerData.selectedCharacterIndex = index;
		SavePlayerData();
	}

	public static int GetSelectedCharacterIndex()
	{
		return playerData.selectedCharacterIndex;
	}

	public static int GetCoins()
	{
		return playerData.coins;
	}
	public static int GetWood()
	{
		return playerData.woods;
	}
	public static int GetGem()
	{
		return playerData.gems;
	}

	public static void AddCoins(int amount,CollectType type)
	{
		switch (type)
		{
			case CollectType.wood:
					if (PlayerUnit.Instance.CheckCapacity() >= amount)
						playerData.woods += amount;
					else if (PlayerUnit.Instance.CheckCapacity() - amount < 0) { 
						playerData.woods += PlayerUnit.Instance.CheckCapacity();
					}
					//SoundManager.Instance.Play()
				break;
            case CollectType.coin:
				playerData.coins += amount;
				break;
            case CollectType.gem:
				playerData.gems += amount;
				break;
            default:
                break;
        }
        
		SavePlayerData();
		GameSharedUI.Instance.UpdateCoinsUIText();
	}

	public static bool CanSpendCoins(int amount,CollectType type)
	{
		switch (type)
		{
			case CollectType.wood:
				return (playerData.woods >= amount);
			case CollectType.coin:
				Debug.Log(playerData.coins >= amount);
				return (playerData.coins >= amount);
			case CollectType.gem:
				return (playerData.gems >= amount);
			default:
				return false;
		}
	}

	public static void SpendCoins(int amount,CollectType type)
	{
        switch (type)
        {
            case CollectType.wood:
				playerData.woods -= amount;
				break;
            case CollectType.coin:
				playerData.coins -= amount;
				break;
            case CollectType.gem:
				playerData.gems -= amount;
				break;
            default:
                break;
        }
        
		SavePlayerData();
		GameSharedUI.Instance.UpdateCoinsUIText();
	}

	static void LoadPlayerData()
	{
		playerData = BinarySerializer.Load<PlayerData>("player-data.txt");
		//UnityEngine.Debug.Log("<color=green>[PlayerData] Loaded.</color>");
	}

	static void SavePlayerData()
	{
		BinarySerializer.Save(playerData, "player-data.txt");
		//UnityEngine.Debug.Log("<color=magenta>[PlayerData] Saved.</color>");
	}

	//Characters Shop Data Methods -----------------------------------------------------------------------------
	public static void AddPurchasedCharacter(int characterIndex)
	{
		charactersShopData.purchasedCharactersIndexes.Add(characterIndex);
		SaveCharactersShoprData();
	}

	public static List<int> GetAllPurchasedCharacter()
	{
		return charactersShopData.purchasedCharactersIndexes;
	}

	public static int GetPurchasedCharacter(int index)
	{
		return charactersShopData.purchasedCharactersIndexes[index];
	}

	static void LoadCharactersShopData()
	{
		charactersShopData = BinarySerializer.Load<CharactersShopData>("characters-shop-data.txt");
		UnityEngine.Debug.Log("<color=green>[CharactersShopData] Loaded.</color>");
	}

	static void SaveCharactersShoprData()
	{
		BinarySerializer.Save(charactersShopData, "characters-shop-data.txt");
		UnityEngine.Debug.Log("<color=magenta>[CharactersShopData] Saved.</color>");
	}
	public static float WoodFillAmount()
    {
		return (float)GameDataManager.GetWood() / (float)PlayerUnit.Instance.chopper.capacity;
	}
}