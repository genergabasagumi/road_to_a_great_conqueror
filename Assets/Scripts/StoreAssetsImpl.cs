//Gener: added, Soomla store assets implementation
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla;
using Soomla.Store;

public class StoreAssetsImpl : IStoreAssets {
	public int GetVersion() {
		return 0;
	}
	
	/// <summary>
	/// Retrieves the array of your game's virtual currencies.
	/// </summary>
	/// <returns>All virtual currencies in your game.</returns>
	public VirtualCurrency[] GetCurrencies() {
		return new VirtualCurrency[]{COIN_CURRENCY};
	}
	
	/// <summary>
	/// Retrieves the array of all virtual goods served by your store (all kinds in one array).
	/// </summary>
	/// <returns>All virtual goods in your game.</returns>
	public VirtualGood[] GetGoods() {
		return new VirtualGood[]{
			REVIVE_GOOD, INITIAL_HP_UPGRADE_GOOD,HP_UPGRADE_LEVEL1_GOOD
		};
	}
	
	/// <summary>
	/// Retrieves the array of all virtual currency packs served by your store.
	/// </summary>
	/// <returns>All virtual currency packs in your game.</returns>
	public VirtualCurrencyPack[] GetCurrencyPacks() {
		return new VirtualCurrencyPack[]{

			THREEHUNDCOIN_PACK, SIXHUNDCOIN_PACK, ONETHOUFOURHUNDCOIN_PACK, THIRTEENTHOUSANDCOIN_PACK
		};
	}
	
	/// <summary>
	/// Retrieves the array of all virtual categories handled in your store.
	/// </summary>
	/// <returns>All virtual categories in your game.</returns>
	public VirtualCategory[] GetCategories() {
		return new VirtualCategory[]{GENERAL_CATEGORY};
	}



	/** Static Final Members **/

	public const string COIN_CURRENCY_ITEM_ID      = "currency_coin";
	public const string THREEHUNDCOIN_PACK_PRODUCT_ID      = "coins_1";	
	public const string SIXHUNDCOIN_PACK_PRODUCT_ID    = "coins_2";	
	public const string ONETHOUFOURHUNDCOIN_PACK_PRODUCT_ID = "coins_3";	
	public const string THIRTEENTHOUCOIN_PACK_PRODUCT_ID = "coins_6";

	public const string INITIAL_HP_UPGRADE_ITEM_ID = "hp_upgrade";
	public const string REVIVE_ITEM_ID = "revive";
	public const string HP_UPGRADE_LEVEL1_ITEM_ID = "hp_upgrade_level1";
	public const string NO_ADS_LIFETIME_PRODUCT_ID = "no_ads";

	/** Virtual Currencies **/
	
	public static VirtualCurrency COIN_CURRENCY = new VirtualCurrency(
		"Coins",										// name
		"",												// description
		COIN_CURRENCY_ITEM_ID							// item id
		);

	

	public static VirtualCurrencyPack THREEHUNDCOIN_PACK = new VirtualCurrencyPack(
		"300 Coins",                                   // name
		"",                       // description
		"coins_1",                                   // item id
		300,												// number of currencies in the pack
		COIN_CURRENCY_ITEM_ID,                        	// the currency associated with this pack
		new PurchaseWithMarket(THREEHUNDCOIN_PACK_PRODUCT_ID, 0.99)
		);
	
	public static VirtualCurrencyPack SIXHUNDCOIN_PACK = new VirtualCurrencyPack(
		"600 Coins",                                   // name
		"",                 // description
		"coins_2",                                   // item id
		600,                                             // number of currencies in the pack
		COIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithMarket(SIXHUNDCOIN_PACK_PRODUCT_ID, 1.99)
		);
	
	public static VirtualCurrencyPack ONETHOUFOURHUNDCOIN_PACK = new VirtualCurrencyPack(
		"1400 Coins",                                  // name
		"",                 	// description
		"coins_3",                                  // item id
		1400,                                            // number of currencies in the pack
		COIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithMarket(ONETHOUFOURHUNDCOIN_PACK_PRODUCT_ID, 2.99)
		);
	
	public static VirtualCurrencyPack THIRTEENTHOUSANDCOIN_PACK = new VirtualCurrencyPack(
		"13000 Coins",                                 // name
		"",                 		// description
		"coins_6",                                 // item id
		13000,                                           // number of currencies in the pack
		COIN_CURRENCY_ITEM_ID,                        // the currency associated with this pack
		new PurchaseWithMarket(THIRTEENTHOUCOIN_PACK_PRODUCT_ID, 5.99)
		);

	/** Virtual Goods **/

	public static VirtualGood REVIVE_GOOD = new SingleUseVG(
		"Revive",                                       		// name
		"", // description
		REVIVE_ITEM_ID,                                       		// item id
		new PurchaseWithVirtualItem(COIN_CURRENCY_ITEM_ID, 15)); // the way this virtual good is purchased

	public static VirtualGood INITIAL_HP_UPGRADE_GOOD = new SingleUseVG(
		"HP Upgrade",                                       		// name
		"", // description
		INITIAL_HP_UPGRADE_ITEM_ID,                                       		// item id
		new PurchaseWithVirtualItem(COIN_CURRENCY_ITEM_ID, 15)); // the way this virtual good is purchased


	public static VirtualGood HP_UPGRADE_LEVEL1_GOOD = new UpgradeVG(
		INITIAL_HP_UPGRADE_ITEM_ID,											//associated item id
		null,																	//next
		null,																//prev
		"HP Upgrade Level 1",                                       		// name
		"", // description
		HP_UPGRADE_LEVEL1_ITEM_ID,                                       		// item id
		new PurchaseWithVirtualItem(COIN_CURRENCY_ITEM_ID, 15)); // the way this virtual good is purchased
		

	/** Virtual Categories **/
	public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
		"General", new List<string>(new string[] { REVIVE_ITEM_ID, INITIAL_HP_UPGRADE_ITEM_ID, HP_UPGRADE_LEVEL1_ITEM_ID })
		);

	/** LifeTimeVGs **/
	// Note: create non-consumable items using LifeTimeVG with PuchaseType of PurchaseWithMarket
	/*public static VirtualGood NO_ADS_LTVG = new LifetimeVG(
		"No Ads", 														// name
		"No More Ads!",				 									// description
		"no_ads",														// item id
		new PurchaseWithMarket(NO_ADS_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased
		*/
}
