using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using AdBrixRmAOS;


//AdBrixRm.cs (브릿지 클래스)들의 함수를 호출하여 사용합니다.
//아래의 로직은 기본적 샘플이며, 실 개발환경에 맞추어 해당하는 값으로 변경하여 사용하시면 됩니다
public class AdBrixRmSample_AOS : MonoBehaviour
{
    void Start()
    {
#if UNITY_ANDROID
AdBrixRm.setEventUploadCountInterval(AdBrixRm.AdBrixEventUploadCountInterval.NORMAL);
AdBrixRm.setEventUploadTimeInterval(AdBrixRm.AdBrixEventUploadTimeInterval.NORMAL);
#endif
	}

	void OnGUI()
    {
#if UNITY_ANDROID
		Screen.SetResolution( 1080, 1920, true ); //420dpi, Nexus 5X

		GUIStyle labelStyle = new GUIStyle();
		labelStyle.alignment = TextAnchor.MiddleLeft;
		labelStyle.normal.textColor = Color.white;
		labelStyle.wordWrap = true;
		labelStyle.fontSize = 40;

		float buttonWidth = 270;
		float buttonHeight = 100;
		int lowCnt = 0;
		int calCnt = 0;

		//Button style
		GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
		myButtonStyle.fontSize = 33;

		// Quit app on BACK key.
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }


		////////////////////////////////// USING OF FUNCTION SAMPLE START ////////////////////////////////
		GUI.Label(new Rect(10, 20, 800, 20), "AdBrix Remaster Sample App", labelStyle);

		//1. custom event
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "event", myButtonStyle))	{

			//custom event - just name
			AdBrixRm.eventWithName ("unityEvent");
		}
		lowCnt++;
		calCnt++;

		//2. custom event with dictionary
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "event sub", myButtonStyle))	{
			Dictionary<string, string> dict = new Dictionary<string, string>();
			dict.Add("detailInfo", "success");

			//event - event name, event's detail dictionary
			AdBrixRm.eventWithEventNameAndValue ("unityEventSub", dict);
		}
		lowCnt++;
		calCnt++;

		//3. set Age
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "setAge-20", myButtonStyle))	{
			//set age - 1~99
			AdBrixRm.setAge (20);
		}
		lowCnt++;
		calCnt++;

		//4. set Gender
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "setGender-Male", myButtonStyle)) {
			//set Gender (AdBrixRm.Gender)
			AdBrixRm.setGender (AdBrixRm.Gender.FEMALE);
		}
		lowCnt++;
		calCnt++;

		//5. login event
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "Login-igaworks", myButtonStyle)) {
            //login - userid
            AdBrixRm.login ("igaworks");

		}
		lowCnt++;
		calCnt++;

		//6. set userproperties
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "setUserProp", myButtonStyle))		{
			Dictionary<string, string> dict = new Dictionary<string, string>();
			dict.Add("nickname", "adbrixRM");

			//set user properties - dictionary
			AdBrixRm.saveUserProperties (dict);
		}
		lowCnt++;
		calCnt++;

		//7. commerce - viewhome
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-ViewHome", myButtonStyle))		{

			//view home
			AdBrixRm.commerceViewHome ();

			//view home - commerce extra attributes
			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");
			commerceExtraAttrs.Add ("Att4", "Value4");
			commerceExtraAttrs.Add ("Att5", "Value5");

			AdBrixRm.commerceViewHome(commerceExtraAttrs);
		}
		lowCnt++;
		calCnt++;

		//8. commerce - category view
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-Category", myButtonStyle))		{

			Dictionary<string, string> extraAttrs = new Dictionary<string, string>();
			extraAttrs.Add ("Att1", "Value1");
			extraAttrs.Add ("Att2", "Value2");
			extraAttrs.Add ("Att3", "Value3");
			extraAttrs.Add ("Att4", "Value4");
			extraAttrs.Add ("Att5", "Value5");

			List<string> categoryList = new List<string>();
			categoryList.Add("sale");

			AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000,
				AdBrixRmAOS.AdBrixRm.Currency.KR_KRW.ToString().ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (extraAttrs)
			);

			List<AdBrixRm.AdBrixRmCommerceProductModel> productList = new List<AdBrixRm.AdBrixRmCommerceProductModel>();
			productList.Add(productModel);

			//CategoryView - category, products
			AdBrixRm.commerceCategoryView (categoryList, productList);


			//CategoryView - category, products, commerce extra attributes
			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");
			commerceExtraAttrs.Add ("Att4", "Value4");
			commerceExtraAttrs.Add ("Att5", "Value5");
			AdBrixRm.commerceCategoryView (categoryList, productList, commerceExtraAttrs);

		}
		lowCnt++;
		calCnt++;

		//9. commerce - product view
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-Product", myButtonStyle))		{

			Dictionary<string, string> ExtraAttrs = new Dictionary<string, string>();
			ExtraAttrs.Add ("Att1", "Value1");
			ExtraAttrs.Add ("Att2", "Value2");
			ExtraAttrs.Add ("Att3", "Value3");
			ExtraAttrs.Add ("Att4", "Value4");
			ExtraAttrs.Add ("Att5", "Value5");

			AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);

			//product view - product
			AdBrixRm.commerceProductView (productModel);

			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");
			commerceExtraAttrs.Add ("Att4", "Value4");
			commerceExtraAttrs.Add ("Att5", "Value5");

			//product view - product, commerce extra attributes
			AdBrixRm.commerceProductView (productModel, commerceExtraAttrs);

		}
		lowCnt++;
		calCnt++;

		//10. commerce - addToCart-single
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-AddCart-S", myButtonStyle))		{

			List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List<AdBrixRm.AdBrixRmCommerceProductModel> ();
			Dictionary<string, string>ExtraAttrs = new Dictionary<string, string>();
			ExtraAttrs.Add ("Att1", "Value1");
			ExtraAttrs.Add ("Att2", "Value2");
			ExtraAttrs.Add ("Att3", "Value3");
			ExtraAttrs.Add ("Att4", "Value4");
			ExtraAttrs.Add ("Att5", "Value5");


			AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);

			items.Add (productModel);

			//addToCart - products
			AdBrixRm.commerceAddToCart (items);

			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");
			commerceExtraAttrs.Add ("Att4", "Value4");
			commerceExtraAttrs.Add ("Att5", "Value5");

			//addToCart - product, commerce extra attributes
			AdBrixRm.commerceAddToCart (items, commerceExtraAttrs);
		}
		lowCnt++;
		calCnt++;

		//11. commerce - addToCart-bulk
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-AddCart-B", myButtonStyle))		{
			List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List<AdBrixRm.AdBrixRmCommerceProductModel> ();
			Dictionary<string, string>ExtraAttrs = new Dictionary<string, string>();
			ExtraAttrs.Add ("att1", "Value1");
			ExtraAttrs.Add ("att2", "Value2");
			ExtraAttrs.Add ("att3", "Value3");
			ExtraAttrs.Add ("att4", "Value4");
			ExtraAttrs.Add ("att5", "Value5");


			AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);

			AdBrixRm.AdBrixRmCommerceProductModel productModel2 = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);

			items.Add (productModel);
			items.Add (productModel2);

			//addToCart - products
			AdBrixRm.commerceAddToCart (items);

			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("attr1", "Value1");
			commerceExtraAttrs.Add ("attr2", "Value2");
			commerceExtraAttrs.Add ("attr3", "Value3");
			commerceExtraAttrs.Add ("attr4", "Value4");
			commerceExtraAttrs.Add ("attr5", "Value5");
			//addToCart - products, commerce extra attributes
			AdBrixRm.commerceAddToCart (items, commerceExtraAttrs);

		}
		lowCnt++;
		calCnt++;

		//12. commerce - addToWish
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-AddWish-S", myButtonStyle))		{
			Dictionary<string, string>ExtraAttrs = new Dictionary<string, string>();
			ExtraAttrs.Add ("Att1", "Value1");
			ExtraAttrs.Add ("Att2", "Value2");
			ExtraAttrs.Add ("Att3", "Value3");
			ExtraAttrs.Add ("Att4", "Value4");
			ExtraAttrs.Add ("Att5", "Value5");


			AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);

			//addToWishList - products
			AdBrixRm.commerceAddToWishList (productModel);

			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");
			commerceExtraAttrs.Add ("Att4", "Value4");
			commerceExtraAttrs.Add ("Att5", "Value5");

			//addToWishList - products, commerce extra attributes
			AdBrixRm.commerceAddToWishList (productModel, commerceExtraAttrs);
		}
		lowCnt++;
		calCnt++;



		//13. commerce - ReviewOrder-single
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-ReviewOrder-S", myButtonStyle))		{
			List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List<AdBrixRm.AdBrixRmCommerceProductModel> ();
			Dictionary<string, string>ExtraAttrs = new Dictionary<string, string>();
			ExtraAttrs.Add ("Att1", "Value1");
			ExtraAttrs.Add ("Att2", "Value2");
			ExtraAttrs.Add ("Att3", "Value3");
			ExtraAttrs.Add ("Att4", "Value4");
			ExtraAttrs.Add ("Att5", "Value5");


			AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);

			items.Add (productModel);

			//ReviewOrder - order id, product, discount, delivery charge
			AdBrixRm.commerceReviewOrder("30290121", items, 1000.00, 3500.00);

			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");
			commerceExtraAttrs.Add ("Att4", "Value4");
			commerceExtraAttrs.Add ("Att5", "Value5");

			//ReviewOrder - order id, product, discount, delivery charge, commerce extra attributes
			AdBrixRm.commerceReviewOrder ("30290121", items, 1000.00, 3500.00, commerceExtraAttrs);

		}
		lowCnt++;
		calCnt++;

		//14. commerce - ReviewOrder-bulk
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-ReviewOrder-B", myButtonStyle))		{
			List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List<AdBrixRm.AdBrixRmCommerceProductModel> ();
			Dictionary<string, string>ExtraAttrs = new Dictionary<string, string>();
			ExtraAttrs.Add ("Att1", "Value1");
			ExtraAttrs.Add ("Att2", "Value2");
			ExtraAttrs.Add ("Att3", "Value3");
			ExtraAttrs.Add ("Att4", "Value4");
			ExtraAttrs.Add ("Att5", "Value5");


			AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);
			AdBrixRm.AdBrixRmCommerceProductModel productModel2 = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);

			items.Add (productModel);
			items.Add (productModel2);

			//ReviewOrder - order id, products, discount, delivery charge
			AdBrixRm.commerceReviewOrder("30290121", items, 1000.00, 3500.00);

			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");
			commerceExtraAttrs.Add ("Att4", "Value4");
			commerceExtraAttrs.Add ("Att5", "Value5");

			//ReviewOrder - order id, products, discount, delivery charge, commerce extra attributes
			AdBrixRm.commerceReviewOrder ("30290121", items, 1000.00, 3500.00, commerceExtraAttrs);
		}
		lowCnt++;
		calCnt++;



		//17. commerce - Refund-single
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-Refund-S", myButtonStyle))		{

			List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List<AdBrixRm.AdBrixRmCommerceProductModel> ();
			Dictionary<string, string>ExtraAttrs = new Dictionary<string, string>();
			ExtraAttrs.Add ("Att1", "Value1");
			ExtraAttrs.Add ("Att2", "Value2");
			ExtraAttrs.Add ("Att3", "Value3");
			ExtraAttrs.Add ("Att4", "Value4");
			ExtraAttrs.Add ("Att5", "Value5");


			AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);

			items.Add (productModel);

			//Refund - order id, product, penalty charge
			AdBrixRm.commerceRefund("30290121", items, 3500.00);


			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");
			commerceExtraAttrs.Add ("Att4", "Value4");
			commerceExtraAttrs.Add ("Att5", "Value5");

			//Refund - order id, product, penalty charge, commerce extra attributes
			AdBrixRm.commerceRefund ("30290121", items, 3500.00, commerceExtraAttrs);

		}
		lowCnt++;
		calCnt++;

		//18. commerce - Refund-bulk
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-Refund-B", myButtonStyle))		{
			List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List<AdBrixRm.AdBrixRmCommerceProductModel> ();
			Dictionary<string, string>ExtraAttrs = new Dictionary<string, string>();
			ExtraAttrs.Add ("Att1", "Value1");
			ExtraAttrs.Add ("Att2", "Value2");
			ExtraAttrs.Add ("Att3", "Value3");
			ExtraAttrs.Add ("Att4", "Value4");
			ExtraAttrs.Add ("Att5", "Value5");


			AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);
			AdBrixRm.AdBrixRmCommerceProductModel productModel2 = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);


			items.Add (productModel);
			items.Add (productModel2);

			//Refund - order id, product, penalty charge
			AdBrixRm.commerceRefund("30290121", items, 3500.00);

			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");
			commerceExtraAttrs.Add ("Att4", "Value4");
			commerceExtraAttrs.Add ("Att5", "Value5");

			//Refund - order id, products, penalty charge, commerce extra attributes
			AdBrixRm.commerceRefund ("30290121", items, 3500.00, commerceExtraAttrs);

		}
		lowCnt++;
		calCnt++;

		//19. commerce - Search-single,bulk
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-Search", myButtonStyle))		{
			List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List<AdBrixRm.AdBrixRmCommerceProductModel> ();
			Dictionary<string, string>ExtraAttrs = new Dictionary<string, string>();
			ExtraAttrs.Add ("Att1", "Value1");
			ExtraAttrs.Add ("Att2", "Value2");
			ExtraAttrs.Add ("Att3", "Value3");
			ExtraAttrs.Add ("Att4", "Value4");
			ExtraAttrs.Add ("Att5", "Value5");


			AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);
			AdBrixRm.AdBrixRmCommerceProductModel productModel2 = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);


			items.Add (productModel);
			items.Add (productModel2);

			//Refund - keyword, products
			AdBrixRm.commerceSearch("nike", items);

			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");
			commerceExtraAttrs.Add ("Att4", "Value4");
			commerceExtraAttrs.Add ("Att5", "Value5");

			//Refund - keyword, products, commerce extra attributes
			AdBrixRm.commerceSearch ("nike", items, commerceExtraAttrs);
		}
		lowCnt++;
		calCnt++;

		//20. commerce - Share
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-Share", myButtonStyle))		{
			Dictionary<string, string>ExtraAttrs = new Dictionary<string, string>();
			ExtraAttrs.Add ("Att1", "Value1");
			ExtraAttrs.Add ("Att2", "Value2");
			ExtraAttrs.Add ("Att3", "Value3");
			ExtraAttrs.Add ("Att4", "Value4");
			ExtraAttrs.Add ("Att5", "Value5");


			AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);


			//Refund - AdBrixRm.SharingChannel, product
			AdBrixRm.commerceShare(AdBrixRm.SharingChannel.KAKAOTALK, productModel);

			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");
			commerceExtraAttrs.Add ("Att4", "Value4");
			commerceExtraAttrs.Add ("Att5", "Value5");

			//Refund - AdBrixRm.SharingChannel, product, commerce extra attributes
			AdBrixRm.commerceShare (AdBrixRm.SharingChannel.KAKAOTALK, productModel, commerceExtraAttrs);
		}
		lowCnt++;
		calCnt++;


		//commerce - list view -single,bulk
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-ListView", myButtonStyle))		{
			List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List<AdBrixRm.AdBrixRmCommerceProductModel> ();
			Dictionary<string, string>ExtraAttrs = new Dictionary<string, string>();
			ExtraAttrs.Add ("Att1", "Value1");
			ExtraAttrs.Add ("Att2", "Value2");
			ExtraAttrs.Add ("Att3", "Value3");


			AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);
			AdBrixRm.AdBrixRmCommerceProductModel productModel2 = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);


			items.Add (productModel);
			items.Add (productModel2);

			//commerceListView - keyword, products
			AdBrixRm.commerceListView(items);

			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");

			//commerceListView - keyword, products, commerce extra attributes
			AdBrixRm.commerceListView (items, commerceExtraAttrs);

		}
		lowCnt++;
		calCnt++;

		//commerce - cart view -single,bulk
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-CartView", myButtonStyle))		{
			List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List<AdBrixRm.AdBrixRmCommerceProductModel> ();
			Dictionary<string, string>ExtraAttrs = new Dictionary<string, string>();
			ExtraAttrs.Add ("Att1", "Value1");
			ExtraAttrs.Add ("Att2", "Value2");
			ExtraAttrs.Add ("Att3", "Value3");


			AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);
			AdBrixRm.AdBrixRmCommerceProductModel productModel2 = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);


			items.Add (productModel);
			items.Add (productModel2);

			//commerceCartView - products
			AdBrixRm.commerceCartView(items);

			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");

			//commerceCartView - products, commerce extra attributes
			AdBrixRm.commerceCartView (items, commerceExtraAttrs);

		}
		lowCnt++;
		calCnt++;


		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-PaymentAdded", myButtonStyle))		{

			//commercePaymentInfoAdded
			AdBrixRm.commercePaymentInfoAdded ();

			//commercePaymentInfoAdded - commerce extra attributes
			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");

			AdBrixRm.commercePaymentInfoAdded(commerceExtraAttrs);

		}
		lowCnt++;
		calCnt++;


		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "G-TutorialCompleted", myButtonStyle))		{

			bool is_skip = false;

			//commercePaymentInfoAdded - extra attributes
			Dictionary<string, string> extraAttrs = new Dictionary<string, string>();
			extraAttrs.Add ("Att1", "Value1");
			extraAttrs.Add ("Att2", "Value2");
			extraAttrs.Add ("Att3", "Value3");

			AdBrixRm.gameTutorialComplete(is_skip, extraAttrs);
		}
		lowCnt++;
		calCnt++;


		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "G-LevelAchieved", myButtonStyle))		{

			int level = 15;

			//gameLevelAchieved -  extra attributes
			Dictionary<string, string> extraAttrs = new Dictionary<string, string>();
			extraAttrs.Add ("Att1", "Value1");
			extraAttrs.Add ("Att2", "Value2");
			extraAttrs.Add ("Att3", "Value3");

			AdBrixRm.gameLevelAchieved(level, extraAttrs);

		}
		lowCnt++;
		calCnt++;

		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "G-CharacterCreated", myButtonStyle))		{


			//gameCharacterCreated
			AdBrixRm.gameCharacterCreated ();

			//gameLevelAchieved -  extra attributes
			Dictionary<string, string> extraAttrs = new Dictionary<string, string>();
			extraAttrs.Add ("Att1", "Value1");
			extraAttrs.Add ("Att2", "Value2");
			extraAttrs.Add ("Att3", "Value3");

			AdBrixRm.gameCharacterCreated(extraAttrs);

		}
		lowCnt++;
		calCnt++;

		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "G-StageCleared", myButtonStyle))		{

			string stageName = "1-1";
			//gameLevelAchieved -  extra attributes
			Dictionary<string, string> extraAttrs = new Dictionary<string, string>();
			extraAttrs.Add ("Att1", "Value1");
			extraAttrs.Add ("Att2", "Value2");
			extraAttrs.Add ("Att3", "Value3");

			AdBrixRm.gameStageCleared(stageName, extraAttrs);

		}
		lowCnt++;
		calCnt++;



		//common - Purchase-single
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-Purchase-S", myButtonStyle))		{


			List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List<AdBrixRm.AdBrixRmCommerceProductModel> ();
			Dictionary<string, string>ExtraAttrs = new Dictionary<string, string>();
			ExtraAttrs.Add ("Att1", "Value1");
			ExtraAttrs.Add ("Att2", "Value2");
			ExtraAttrs.Add ("Att3", "Value3");


			AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);


			items.Add (productModel);

			//Purchase - order id, product, discount, delivery charge, AdBrixRm.PaymentMethod
			AdBrixRm.commonPurchase("30290121", items, 1000.00, 3500.00, AdBrixRm.PaymentMethod.CreditCard);

			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");

			//Purchase - order id, product, discount, delivery charge, AdBrixRm.PaymentMethod, commerce extra attributes
			AdBrixRm.commonPurchase ("30290121", items, 1000.00, 3500.00, AdBrixRm.PaymentMethod.CreditCard, commerceExtraAttrs);

		}
		lowCnt++;
		calCnt++;

		//common - Purchase-bulk
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-Purchase-B", myButtonStyle))		{
			List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List<AdBrixRm.AdBrixRmCommerceProductModel> ();
			Dictionary<string, string>ExtraAttrs = new Dictionary<string, string>();
			ExtraAttrs.Add ("Att1", "Value1");
			ExtraAttrs.Add ("Att2", "Value2");
			ExtraAttrs.Add ("Att3", "Value3");


			AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);
			AdBrixRm.AdBrixRmCommerceProductModel productModel2 = AdBrixRm.AdBrixRmCommerceProductModel.create (
				"productId01",
				"productName01",
				10000.00,
				1,
				5000.00,
				AdBrixRm.Currency.KR_KRW.ToString(),
				AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
				AdBrixRm.AdBrixRmCommerceProductAttrModel.create (ExtraAttrs)
			);


			items.Add (productModel);
			items.Add (productModel2);

			//Purchase - order id, products, discount, delivery charge, AdBrixRm.PaymentMethod
			AdBrixRm.commonPurchase("30290121", items, 1000.00, 3500.00, AdBrixRm.PaymentMethod.CreditCard);


			Dictionary<string, string> commerceExtraAttrs = new Dictionary<string, string>();
			commerceExtraAttrs.Add ("Att1", "Value1");
			commerceExtraAttrs.Add ("Att2", "Value2");
			commerceExtraAttrs.Add ("Att3", "Value3");

			//Purchase - order id, products, discount, delivery charge, AdBrixRm.PaymentMethod, commerce extra attributes
			AdBrixRm.commonPurchase ("30290121", items, 1000.00, 3500.00, AdBrixRm.PaymentMethod.CreditCard, commerceExtraAttrs);

		}
		lowCnt++;
		calCnt++;

		//common - signup
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-signup", myButtonStyle))		{
			AdBrixRm.commonSignUp(AdBrixRm.SignUpChannel.Kakao);
		}
		lowCnt++;
		calCnt++;
		//common - useCredit
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-useCredit", myButtonStyle))		{
			AdBrixRm.commonUseCredit();
		}
		lowCnt++;
		calCnt++;
		//common - appUpdate
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-appUpdate", myButtonStyle))		{
			Dictionary<string, string> extraAttrs = new Dictionary<string, string>();
			extraAttrs.Add ("Att1", "Value1");
			extraAttrs.Add ("Att2", "Value2");
			extraAttrs.Add ("Att3", "Value3");
			AdBrixRm.commonAppUpdate("2.0","2.1",extraAttrs);
		}
		lowCnt++;
		calCnt++;
		//common - invite
		if (GUI.Button(new Rect(10 + (buttonWidth * (lowCnt%3)), 50 + (buttonHeight * (calCnt/3)), buttonWidth, buttonHeight), "C-invite", myButtonStyle))		{
			AdBrixRm.commonInvite(AdBrixRm.InviteChannel.Line);
		}
		lowCnt++;
		calCnt++;

#endif
	}
	public void setLogListenerCallback(string message)
	{
#if UNITY_ANDROID
        Debug.Log("setLogListenerCallback : " + message);
#endif


	}
}
