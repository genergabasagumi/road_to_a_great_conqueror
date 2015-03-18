//Gener: added Soomla store manager
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla;
using Soomla.Store;

public class StoreManager : MonoBehaviour {
	public static StoreManager instance = null;

	void Awake() {
				instance = this;
	}

	// Use this for initialization
	void Start () {


		StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialize;
		StoreEvents.OnMarketPurchase += onMarketPurchase;

		StoreEvents.OnItemPurchased += onItemPurchased;

		IStoreAssets storeAssets = new StoreAssetsImpl ();
		SoomlaStore.Initialize (storeAssets);
		SoomlaStore.StartIabServiceInBg();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void onSoomlaStoreInitialize() {


	if (!PlayerPrefs.HasKey("First Time")) {
			PlayerPrefs.SetInt("First Time", 1);
			PlayerPrefs.Save();
			StoreInventory.GiveItem (StoreAssetsImpl.COIN_CURRENCY_ITEM_ID, 1000); //Coin
			//StoreInventory.GiveItem (StoreAssetsImpl.TICKET_CURRENCY_ITEM_ID, 10); //Ticket
			//DataManager.instance.setTotalCoins (GetCoins ());
		//	GUIManager.instance.showGUI(GUIState.MainMenu); //awkward call here, just to refresh GUI on start
		}
	}

	public void onMarketPurchase(PurchasableVirtualItem pvi, string payload,
	                             Dictionary<string, string> extra) {
		// pvi - the PurchasableVirtualItem that was just purchased
		// payload - a text that you can give when you initiate the purchase operation and
		//    you want to receive back upon completion
		// extra - contains platform specific information about the market purchase
		//    Android: The "extra" dictionary will contain "orderId" and "purchaseToken"
		//    iOS: The "extra" dictionary will contain "receipt" and "token"
		
		// ... your game specific implementation here ...
		Debug.Log ("MARKET PURCHASED " + pvi.Name);
		//Update DataManager
	//	DataManager.instance.setTotalCoins (GetCoins ());
	}
	
	public void onItemPurchased(PurchasableVirtualItem pvi, string payload) {
		// pvi - the PurchasableVirtualItem that was just purchased
		// payload - a text that you can give when you initiate the purchase operation
		//    and you want to receive back upon completion
		
		// ... your game specific implementation here ...
		Debug.Log ("ITEM PURCHASED " + pvi.Name);
	}

	public void BuyItem(string productId) {
		//SoomlaStore.BuyMarketItem (productId,"");
		StoreInventory.BuyItem (productId);
	}

	public int GetCoins() {
		StoreInventory.RefreshLocalInventory();
		return StoreInventory.GetItemBalance (StoreAssetsImpl.COIN_CURRENCY_ITEM_ID);
	}

	public void GiveCoins(int amount) {
		StoreInventory.GiveItem (StoreAssetsImpl.COIN_CURRENCY_ITEM_ID, amount);
	}
}
