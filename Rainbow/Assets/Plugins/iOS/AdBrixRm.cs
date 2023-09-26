using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;

//AdBrxRmSample.cs 클래스에서 본 브릿지의 static 함수를 호출하여 사용하며, 
//각 함수별로 AdBrix API를 사용하는 AdBrixRmBridge.mm 클래스 파일내의 함수들을 호출합니다.
//본 iOS 브릿지 함수는 특별한 경우를 제외하고는 개발간 수정하지 않도록 유의해주십시오.
namespace AdBrixRmIOS {
	public class AdBrixRm : MonoBehaviour {

		public const string AdBrixDateFormat = "yyyy-MM-dd'T'HH:mm:ss.000'Z'";

		public enum AdBrixLogLevel {
			NONE = 0,
			TRACE = 1,
			DEBUG = 2,
			INFO = 3,
			WARNING = 4,
			ERROR = 5
		}

		public enum AdBrixEventUploadCountInterval {
			MIN = 10,
			NORMAL = 30,
			MAX = 60
		}

		public enum AdBrixEventUploadTimeInterval {
			MIN = 30,
			NORMAL = 60,
			MAX = 120
		}

		public enum Gender {
			UNKNOWN = 0,
			FEMALE = 1,
			MALE = 2
		}

		public enum Currency {
				KR_KRW = 1,
				US_USD = 2,
				JP_JPY = 3,
				EU_EUR = 4,
				UK_GBP = 5,
				CH_CNY = 6,
				TW_TWD = 7,
				HK_HKD = 8,
				ID_IDR = 9,//Indonesia
				IN_INR = 10,//India
				RU_RUB = 11,//Russia
				TH_THB = 12,//Thailand
				VN_VND = 13,//Vietnam
				MY_MYR = 14//Malaysia
		}

		public enum PaymentMethod {
					CREDIT_CARD = 1,
					BANK_TRANSFER = 2,
					MOBILE_PAYMENT = 3,
					ETC            = 4
		}

		public enum SharingChannel {
					FACEBOOK = 1,
					KAKAOTALK = 2,
					KAKAOSTORY = 3,
					LINE = 4,
					WHATSAPP = 5,
					QQ = 6,
					WECHAT = 7,
					SMS = 8,
					EMAIL = 9,
					COPYURL = 10,
					ETC = 11
		}

		public enum SignUpChannel {
			Kakao = 1,
			Naver = 2,
			Line = 3,
			Google = 4,
			Facebook = 5,
			Twitter = 6,
			WhatsApp = 7,
			QQ = 8,
			WeChat = 9,
			UserId = 10,
			ETC = 11,
			SkTid = 12,
			AppleId = 13
		}

		public enum InviteChannel {
			Kakao = 1,
			Naver = 2,
			Line = 3,
			Google = 4,
			Facebook = 5,
			Twitter = 6,
			WhatsApp = 7,
			QQ = 8,
			WeChat = 9,
			ETC = 10
		}


#if UNITY_IOS && !UNITY_EDITOR
		[DllImport("__Internal")]
		extern public static void _SetAdBrixDeeplinkDelegate(string _object, string function);

        [DllImport("__Internal")]
		extern public static void _SetAdBrixDeferredDeeplinkDelegate(string _object, string function);

		[DllImport("__Internal")]
		extern public static void _SetAdBrixRmPushLocalDelegate(string _object, string function);

		[DllImport("__Internal")]
		extern public static void _SetAdBrixRmPushRemoteDelegate(string _object, string function);

		[DllImport("__Internal")]
		extern public static void _SetLogDelegate(string _object, string function);

		[DllImport("__Internal")]
		extern public static void _SetInAppMessageClickDelegate(string _object, string function);

		[DllImport("__Internal")]
		extern public static void _SetInAppMessageAutoFetchDelegate(string _object, string function);

		[DllImport("__Internal")]
		extern public static void _setRegistrationId(string deviceToken);

		[DllImport("__Internal")]
		extern public static void _setPushEnable(bool toEnable);

		[DllImport("__Internal")]
		private static extern void _initAdBrix(string appKey, string secretKey);

		[DllImport("__Internal")]
		private static extern  void _gdprForgetMe ();

		[DllImport("__Internal")]
		private static extern void _setEventUploadCountInterval(int countInterval);

		[DllImport("__Internal")]
		private static extern void _setEventUploadTimeInterval(int timeInterval);

		[DllImport("__Internal")]
		private static extern void _deepLinkOpenWithUrl(string url);

		[DllImport("__Internal")]
		private static extern void _eventWithName(string eventName);

		[DllImport("__Internal")]
		private static extern void _eventWithEventNameAndValue(string eventName, string[] listKey, string[] listValue, int count);

		[DllImport("__Internal")]
		private static extern void _loginWithUserId(string userId);

        [DllImport("__Internal")]
		private static extern void _logout();

		[DllImport("__Internal")]
		private static extern void _commerceViewHome();

		[DllImport("__Internal")]
		private static extern void _commerceViewHomeWithExtraAttr(string jsonCommerceExtraAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceCategoryViewWithCategoryAndProduct(string categoryString, string jsonDataString);

		[DllImport("__Internal")]
		private static extern void _commerceCategoryViewWithCategoryAndProductAndExtraAttr(string categoryString, string jsonDataString, string jsonCommerceExtraAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceCategoryViewBulkWithCategoryAndProduct(string categoryString, string jsonDataString);

		[DllImport("__Internal")]
		private static extern void _commerceCategoryViewBulkWithCategoryAndProductAndExtraAttr(string categoryString, string jsonDataString, string jsonCommerceExtraAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceProductViewWithProduct(string jsonDataString);

		[DllImport("__Internal")]
		private static extern void _commerceProductViewWithProductAndExtraAttr(string jsonDataString, string jsonCommerceExtraAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceAddToCartWithProduct(string jsonDataString);

		[DllImport("__Internal")]
		private static extern void _commerceAddToCartWithProductAndExtraAttr(string jsonDataString, string jsonCommerceExtraAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceAddToCartBulkWithProduct(string jsonDataString);

		[DllImport("__Internal")]
		private static extern void _commerceAddToCartBulkWithProductAndExtraAttr(string jsonDataString, string jsonCommerceExtraAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceAddToWishListWithProduct(string jsonDataString);

		[DllImport("__Internal")]
		private static extern void _commerceAddToWishListWithProductAndExtraAttr(string jsonDataString, string jsonCommerceExtraAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceReviewOrderWithOrderId(string orderId, string jsonDataString, double discount, double deliveryCharge);

		[DllImport("__Internal")]
		private static extern void _commerceReviewOrderBulkWithOrderId(string orderId, string jsonDataString, double discount, double deliveryCharge) ;

		[DllImport("__Internal")]
		private static extern void _commerceReviewOrderWithOrderIdAndExtraAttr(string orderId, string jsonDataString, double discount, double deliveryCharge, string jsonCommerceExtraAttrString) ;

		[DllImport("__Internal")]
		private static extern void _commerceReviewOrderBulkWithOrderIdAndExtraAttr(string orderId, string jsonDataString, double discount, double deliveryCharge, string jsonCommerceExtraAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceRefundWithOrderId(string orderId, string jsonDataString, double penaltyCharge);

		[DllImport("__Internal")]
		private static extern void _commerceRefundBulkWithOrderId(string orderId, string jsonDataString, double penaltyCharge);

		[DllImport("__Internal")]
		private static extern void _commerceRefundWithOrderIdAndExtraAttr(string orderId, string jsonDataString, double penaltyCharge, string jsonCommerceExtraAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceRefundBulkWithOrderIdAndExtraAttr(string orderId, string jsonDataString, double penaltyCharge, string jsonCommerceExtraAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceSearchWithProduct(string jsonDataString, string keyword);

		[DllImport("__Internal")]
		private static extern void _commerceSearchBulkWithProduct(string jsonDataString, string keyword);

		[DllImport("__Internal")]
		private static extern void _commerceSearchWithProductAndExtraAttr(string jsonDataString, string keyword, string jsonCommerceExtraAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceSearchBulkWithProductAndExtraAttr(string jsonDataString, string keyword, string jsonCommerceExtraAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceShareWithChannel(int channel, string jsonDataString);

		[DllImport("__Internal")]
		private static extern void _commerceShareWithChannelAndExtraAttr(int channel, string jsonDataString, string jsonCommerceExtraAttrString);

		[DllImport("__Internal")]
		private static extern void _commonPurchaseWithOrderId(string orderId, string jsonDataString, double discount, double deliveryCharge, int paymentMethod) ;

		[DllImport("__Internal")]
		private static extern void _commonPurchaseBulkWithOrderId(string orderId, string jsonDataString, double discount, double deliveryCharge, int paymentMethod);

		[DllImport("__Internal")]
		private static extern void _commonPurchaseWithOrderIdAndExtraAttr(string orderId, string jsonDataString, double discount, double deliveryCharge, int paymentMethod, string jsonCommerceExtraAttrString) ;

		[DllImport("__Internal")]
		private static extern void _commonPurchaseBulkWithOrderIdAndExtraAttr(string orderId, string jsonDataString, double discount, double deliveryCharge, int paymentMethod, string jsonCommerceExtraAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceListViewWithProduct(string jsonDataString);

		[DllImport("__Internal")]
		private static extern void _commerceListViewBulkWithProduct(string jsonDataString);

		[DllImport("__Internal")]
		private static extern void _commerceListViewProductAndOrderAttr(string jsonDataString, string jsonOrderAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceListViewBulkWithProductAndOrderAttr(string jsonDataString, string jsonOrderAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceCartViewWithProduct(string jsonDataString);

		[DllImport("__Internal")]
		private static extern void _commerceCartViewBulkWithProduct(string jsonDataString);

		[DllImport("__Internal")]
		private static extern void _commerceCartViewProductAndOrderAttr(string jsonDataString, string jsonOrderAttrString);

		[DllImport("__Internal")]
		private static extern void _commerceCartViewBulkWithProductAndOrderAttr(string jsonDataString, string jsonOrderAttrString);

		[DllImport("__Internal")]
		private static extern void _commercePaymentInfoAddedWithExtraAttr(string jsonOrderAttrString);

		[DllImport("__Internal")]
		private static extern void _gameLevelAchievedWithLevel(int level);

		[DllImport("__Internal")]
		private static extern void _gameLevelAchievedWithLevelWithGameAttr(int level, string jsonGameAttrString);

		[DllImport("__Internal")]
		private static extern void _gameTutorialCompletedWithIsSkip(bool isSkip);

		[DllImport("__Internal")]
		private static extern void _gameTutorialCompletedWithIsSkipAndGameAttr(bool isSkip, string jsonGameAttrString);

		[DllImport("__Internal")]
		private static extern void _gameCharacterCreated();

		[DllImport("__Internal")]
		private static extern void _gameCharacterCreatedWithGameAttr(string jsonGameAttrString);

		[DllImport("__Internal")]
		private static extern void _gameStageClearedWithStageName(string stageName);

		[DllImport("__Internal")]
		private static extern void _gameStageClearedWithStageNameAndGameAttr(string stageName, string jsonGameAttrString);

		[DllImport("__Internal")]
		private static extern void _setLocationWithLatitude(double latitude, double longitude);

		[DllImport("__Internal")]
		private static extern void _setAgeWithInt(int age);

		[DllImport("__Internal")]
		private static extern void _setGenderWithAdBrixGenderType(int adBrixGenderType);

		[DllImport("__Internal")]
		private static extern void _setUserPropertiesWithDictionary(string[] listKey, string[] listValue, int count);

		[DllImport("__Internal")]
		private static extern void _setCiPropertiesWithDictionary(string[] listKey, string[] listValue, int count);

		[DllImport("__Internal")]
		private static extern void _setKakaoId(string kakaoId);

        [DllImport("__Internal")]
		private static extern void _clearUserProperties();

		[DllImport("__Internal")]
		private static extern void _commonSignUp(int channel);

		[DllImport("__Internal")]
		private static extern void _commonSignUpWithAttr(int channel, string extraAttrJsonString);

		[DllImport("__Internal")]
		private static extern void _commonUseCredit();

		[DllImport("__Internal")]
		private static extern void _commonUseCreditWithAttr(string extraAttrJsonString);

		[DllImport("__Internal")]
		private static extern void _commonAppUpdate(string prev_ver, string curr_ver);

		[DllImport("__Internal")]
		private static extern void _commonAppUpdateWithAttr(string prev_ver, string curr_ver, string extraAttrJsonString);

		[DllImport("__Internal")]
		private static extern void _commonInvite(int channel);

		[DllImport("__Internal")]
		private static extern void _commonInviteWithAttr(int channel, string extraAttrJsonString);

		[DllImport("__Internal")]
		private static extern void _registerLocalPushNotification(int delaySecond, string title, string subTitle, string body, string categoryId, string threadId, int badgeNumber, string imageUrlString, string attrDicJsonString, string _object, string _function);

		[DllImport("__Internal")]
		private static extern void _getRegisteredLocalPushNotification(string _object, string _function);

		[DllImport("__Internal")]
		private static extern void _cancelLocalPushNotification(string pushId);

		[DllImport("__Internal")]
		private static extern void _cancelLocalPushNotificationAll();

		[DllImport("__Internal")]
		public static extern void _restartSDK(string userId, string _object, string function);

		[DllImport("__Internal")]
		public static extern void _deleteUserDataAndStopSDK(string userId, string _object, string function);

		[DllImport("__Internal")]
		public static extern void _flushAllEvents(string _object, string function);

		[DllImport("__Internal")]
		public static extern string _getSdkVersion();

		[DllImport("__Internal")]
		public static extern void _fetchActionHistoryByUserId(string token, string[] actionType, int actionTypeCount, string _object, string function);

		[DllImport("__Internal")]
		public static extern void _fetchActionHistoryByAdid(string token, string[] actionType, int actionTypeCount, string _object, string function);

		[DllImport("__Internal")]
		public static extern void _getActionHistory(int skip, int limit, string[] actionType, int actionTypeCount, string _object, string function);

		[DllImport("__Internal")]
		public static extern void _getAllActionHistory(string[] actionType, int actionTypeCount, string _object, string function);

		[DllImport("__Internal")]
		public static extern void _deleteActionHistory(string token, string historyId, long timestamp, string _object, string function);

		[DllImport("__Internal")]
   		public static extern void _deleteAllActionHistoryByUserId(string token, string _object, string function);
		
		[DllImport("__Internal")]
		public static extern void _deleteAllActionHistoryByAdid(string token, string _object, string function);

		[DllImport("__Internal")]
		public static extern void _getAllInAppMessage(string _object, string function);

		[DllImport("__Internal")]
		public static extern void _openInAppMessage(string campaignId, string _object, string function);

		[DllImport("__Internal")]
		public static extern void _fetchInAppMessage(string _object, string function);

		[DllImport("__Internal")]
		public static extern void _clearSyncedActionHistoryInLocalDB(string _object, string function);

		[DllImport("__Internal")]
		public static extern void _setInAppMessageFetchMode(int mode);

		[DllImport("__Internal")]
		public static extern void _setInAppMessageToken(string token);

		[DllImport("__Internal")]
		public static extern  string _AdBrixCurrencyNameRm (int currency);

		[DllImport("__Internal")]
		public static extern  string _AdBrixPaymentMethodNameRm (int method);

		[DllImport("__Internal")]
		public static extern  string _AdBrixSharingChannelNameRm (int channel);
#endif

		public static void setDeeplinkListener(string _object, string function)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			if (Application.platform == RuntimePlatform.OSXEditor)
				return;

			_SetAdBrixDeeplinkDelegate(_object, function);
			#endif
		}

		public static void setDeferredDeeplinkListener(string _object, string function)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			if (Application.platform == RuntimePlatform.OSXEditor)
			return;

			_SetAdBrixDeferredDeeplinkDelegate(_object, function);
			#endif
		}

		public static void setLocalPushMessageListener(string _object, string function)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			if (Application.platform == RuntimePlatform.OSXEditor)
			return;

			_SetAdBrixRmPushLocalDelegate(_object, function);
			#endif
		}

		public static void setRemotePushMessageListener(string _object, string function)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			if (Application.platform == RuntimePlatform.OSXEditor)
			return;

			_SetAdBrixRmPushRemoteDelegate(_object, function);
			#endif
		}

		public static void setLogListener(string _object, string function)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			if (Application.platform == RuntimePlatform.OSXEditor)
			return;

			_SetLogDelegate(_object, function);
			#endif
		}

		public static void setInAppMessageClickListener(string _object, string function)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			if (Application.platform == RuntimePlatform.OSXEditor)
			return;

			_SetInAppMessageClickDelegate(_object, function);
			#endif
		}

		public static void setDfnInAppMessageAutoFetchListener(string _object, string function)
		{
			#if UNITY_IPHONE && !UNITY_EDITOR
			if (Application.platform == RuntimePlatform.OSXEditor)
			return;

			_SetInAppMessageAutoFetchDelegate(_object, function);
			#endif
		}

        public static void initAdBrix(string appKey, string secretKey) {

			#if UNITY_IOS && !UNITY_EDITOR
				_initAdBrix(appKey, secretKey);
			#endif
		}

		public static void setPushEnable(bool toEnable) {

			#if UNITY_IOS && !UNITY_EDITOR
				_setPushEnable(toEnable);
			#endif
		}

		public static void setRegistrationId(string deviceToken) {
			#if UNITY_IOS && !UNITY_EDITOR
				_setRegistrationId(deviceToken);
			#endif
		}

		public static void gdprForgetMe() {

			#if UNITY_IOS && !UNITY_EDITOR
			_gdprForgetMe();
			#endif

		}

		public static void setEventUploadCountInterval(AdBrixEventUploadCountInterval countInterval) {
			#if UNITY_IOS && !UNITY_EDITOR
				_setEventUploadCountInterval((int)countInterval);
			#endif
		}

		public static void setEventUploadTimeInterval(AdBrixEventUploadTimeInterval timeInterval) {
			
			#if UNITY_IOS && !UNITY_EDITOR
				_setEventUploadTimeInterval((int)timeInterval);
			#endif
		}


		public static void deepLinkOpenWithUrl(string url) {
			#if UNITY_IOS && !UNITY_EDITOR
			_deepLinkOpenWithUrl(url);
			#endif
		}


		public static void eventWithName(string eventName) {
			#if UNITY_IOS && !UNITY_EDITOR
			_eventWithName(eventName);
			#endif

		}

		public static void eventWithEventNameAndValue(string eventName, Dictionary<string, string> dictionary) {

			#if UNITY_IOS && !UNITY_EDITOR
				try {
					var listKey = dictionary.Keys.ToArray();
					var listValue =  dictionary.Values.ToArray();
					if (listKey != null && listValue != null && listKey.Length > 0 && listValue.Length > 0 && listKey.Length == listValue.Length) {
						_eventWithEventNameAndValue(eventName, listKey, listValue, listKey.Length);
					}
				}
				catch (Exception ex) {
					Console.WriteLine(ex.ToString());
				}

			#endif


		}

		public static void login(string userId) {
			#if UNITY_IOS && !UNITY_EDITOR
				_loginWithUserId(userId);
			#endif
		}

        public static void logout() {
            #if UNITY_IOS && !UNITY_EDITOR
			 _logout();
            #endif
        }


        public static void commerceViewHome() {
			#if UNITY_IOS && !UNITY_EDITOR
				_commerceViewHome();
			#endif
		}

		public static void commerceViewHome(Dictionary<string,object> dicCommerceExtraAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			_commerceViewHomeWithExtraAttr(MiniJson_iOS.Serialize(dicCommerceExtraAttr));
			#endif
		}

		public static void commerceCategoryView(AdBrixRmCommerceProductCategoryModel category, List<AdBrixRmCommerceProductModel> productInfo) {
			#if UNITY_IOS && !UNITY_EDITOR
			if(productInfo != null) {
				if (productInfo.Count == 1) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
					_commerceCategoryViewWithCategoryAndProduct(category.CategoryFullString, jsonDataString);
				} 
			}
			else if(productInfo.Count >= 2){
				List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
				for(int i  = 0, j= productInfo.Count; i < j; i++) {
					if (productInfo [i] != null) {
						filterList.Add (productInfo[i]);
					}
				}

				if(filterList != null && filterList.Count > 0) {
					string jsonDataString = "[";
					for(int i = 0; i < filterList.Count; i++) {
						AdBrixRmCommerceProductModel item = filterList[i];
						if(i == (filterList.Count-1)) {
							jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
						}
						else {
							jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
						}
					}
					_commerceCategoryViewBulkWithCategoryAndProduct(category.CategoryFullString, jsonDataString);

				}
			}
			#endif
		}

		public static void commerceCategoryView(AdBrixRmCommerceProductCategoryModel category, List<AdBrixRmCommerceProductModel> productInfo, Dictionary<string,object> dicCommerceExtraAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			if(productInfo != null) {
				if (productInfo.Count == 1) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
					_commerceCategoryViewWithCategoryAndProductAndExtraAttr(category.CategoryFullString, jsonDataString, MiniJson_iOS.Serialize(dicCommerceExtraAttr));
				} 
			}
			else if(productInfo.Count >= 2){
				List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
				for(int i  = 0, j= productInfo.Count; i < j; i++) {
					if (productInfo [i] != null) {
						filterList.Add (productInfo[i]);
					}
				}

				if(filterList != null && filterList.Count > 0) {
					string jsonDataString = "[";
					for(int i = 0; i < filterList.Count; i++) {
						AdBrixRmCommerceProductModel item = filterList[i];
						if(i == (filterList.Count-1)) {
							jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
						}
						else {
							jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
						}
					}
					_commerceCategoryViewBulkWithCategoryAndProductAndExtraAttr(category.CategoryFullString, jsonDataString, MiniJson_iOS.Serialize(dicCommerceExtraAttr));

				}
			}
			#endif
		}

		public static void commerceProductView(AdBrixRmCommerceProductModel productInfo) {
			#if UNITY_IOS && !UNITY_EDITOR
				if (productInfo != null) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem (productInfo) + "]";
					_commerceProductViewWithProduct(jsonDataString);

				}
			#endif
		}

		public static void commerceProductView(AdBrixRmCommerceProductModel productInfo, Dictionary<string,object> dicCommerceExtraAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
				if (productInfo != null) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem (productInfo) + "]";
					_commerceProductViewWithProductAndExtraAttr(jsonDataString, MiniJson_iOS.Serialize(dicCommerceExtraAttr));
				}
			#endif
		}

		public static void commerceAddToCart(List<AdBrixRmCommerceProductModel> productInfo) {

			#if UNITY_IOS && !UNITY_EDITOR
				if(productInfo != null) {
					if (productInfo.Count == 1) {
						string jsonDataString = "[";
						jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
						_commerceAddToCartWithProduct(jsonDataString);
					} 
					else if(productInfo.Count >= 2){
						List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
						for(int i  = 0, j= productInfo.Count; i < j; i++) {
							if (productInfo [i] != null) {
								filterList.Add (productInfo[i]);
							}
						}

						if(filterList != null && filterList.Count > 0) {
							string jsonDataString = "[";
							for(int i = 0; i < filterList.Count; i++) {
								AdBrixRmCommerceProductModel item = filterList[i];
								if(i == (filterList.Count-1)) {
									jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
								}
								else {
									jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
								}
							}
							_commerceAddToCartBulkWithProduct(jsonDataString);

						}
					}
				}
			#endif

		}

		public static void commerceAddToCart(List<AdBrixRmCommerceProductModel> productInfo, Dictionary<string,object> dicCommerceExtraAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			if(productInfo != null) {
				if (productInfo.Count == 1) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
					_commerceAddToCartWithProductAndExtraAttr(jsonDataString, MiniJson_iOS.Serialize(dicCommerceExtraAttr));
				} 
				else if(productInfo.Count >= 2){
					List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
					for(int i  = 0, j= productInfo.Count; i < j; i++) {
						if (productInfo [i] != null) {
							filterList.Add (productInfo[i]);
						}
					}

					if(filterList != null && filterList.Count > 0) {
						string jsonDataString = "[";
						for(int i = 0; i < filterList.Count; i++) {
							AdBrixRmCommerceProductModel item = filterList[i];
							if(i == (filterList.Count-1)) {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
							}
							else {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
							}
						}
						_commerceAddToCartBulkWithProductAndExtraAttr(jsonDataString, MiniJson_iOS.Serialize(dicCommerceExtraAttr));

					}
				}
			}
			#endif
		}

		public static void commerceAddToWishList(AdBrixRmCommerceProductModel productInfo) {
			#if UNITY_IOS && !UNITY_EDITOR
				if (productInfo != null) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem (productInfo) + "]";
					_commerceAddToWishListWithProduct(jsonDataString);
				}
			#endif
		}

		public static void commerceAddToWishList(AdBrixRmCommerceProductModel productInfo, Dictionary<string,object> dicCommerceExtraAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
				if (productInfo != null) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem (productInfo) + "]";
					_commerceAddToWishListWithProductAndExtraAttr(jsonDataString, MiniJson_iOS.Serialize(dicCommerceExtraAttr));
				}
			#endif
		}

		public static void commerceReviewOrder(string orderId, List<AdBrixRmCommerceProductModel> productInfo, double discount, double deliveryCharge) {
			#if UNITY_IOS && !UNITY_EDITOR
			if(productInfo != null) {
				if (productInfo.Count == 1) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
					_commerceReviewOrderWithOrderId(orderId, jsonDataString, discount, deliveryCharge);
				} 
				else if(productInfo.Count >= 2){
					List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
					for(int i  = 0, j= productInfo.Count; i < j; i++) {
						if (productInfo [i] != null) {
							filterList.Add (productInfo[i]);
						}
					}

					if(filterList != null && filterList.Count > 0) {
						string jsonDataString = "[";
						for(int i = 0; i < filterList.Count; i++) {
							AdBrixRmCommerceProductModel item = filterList[i];
							if(i == (filterList.Count-1)) {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
							}
							else {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
							}
						}
						_commerceReviewOrderBulkWithOrderId(orderId, jsonDataString, discount, deliveryCharge);

					}
				}
			}
			#endif

		}

		public static void commerceReviewOrder(string orderId, List<AdBrixRmCommerceProductModel> productInfo, double discount, double deliveryCharge, Dictionary<string,object> dicCommerceExtraAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			if(productInfo != null) {
				if (productInfo.Count == 1) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
					_commerceReviewOrderWithOrderIdAndExtraAttr (orderId, jsonDataString, discount, deliveryCharge, MiniJson_iOS.Serialize (dicCommerceExtraAttr));
				} 

			}
			else if(productInfo.Count >= 2){
				List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
				for(int i  = 0, j= productInfo.Count; i < j; i++) {
					if (productInfo [i] != null) {
						filterList.Add (productInfo[i]);
					}
				}

				if(filterList != null && filterList.Count > 0) {
					string jsonDataString = "[";
					for(int i = 0; i < filterList.Count; i++) {
						AdBrixRmCommerceProductModel item = filterList[i];
						if(i == (filterList.Count-1)) {
							jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
						}
						else {
							jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
						}
					}
					_commerceReviewOrderBulkWithOrderIdAndExtraAttr(orderId, jsonDataString, discount, deliveryCharge, MiniJson_iOS.Serialize (dicCommerceExtraAttr));

				}
			}
			#endif
		}

		public static void commerceRefund(string orderId, List<AdBrixRmCommerceProductModel> productInfo, double penaltyCharge) {
			#if UNITY_IOS && !UNITY_EDITOR
			if(productInfo != null) {
				if (productInfo.Count == 1) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
					_commerceRefundWithOrderId(orderId, jsonDataString, penaltyCharge);
				} 
				else if(productInfo.Count >= 2){
					List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
					for(int i  = 0, j= productInfo.Count; i < j; i++) {
						if (productInfo [i] != null) {
							filterList.Add (productInfo[i]);
						}
					}

					if(filterList != null && filterList.Count > 0) {
						string jsonDataString = "[";
						for(int i = 0; i < filterList.Count; i++) {
							AdBrixRmCommerceProductModel item = filterList[i];
							if(i == (filterList.Count-1)) {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
							}
							else {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
							}
						}
						_commerceRefundBulkWithOrderId(orderId, jsonDataString, penaltyCharge);

					}
				}
			}
			#endif

		}

		public static void commerceRefund(string orderId, List<AdBrixRmCommerceProductModel> productInfo, double penaltyCharge, Dictionary<string,object> dicCommerceExtraAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			if(productInfo != null) {
				if (productInfo.Count == 1) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
					_commerceRefundWithOrderIdAndExtraAttr(orderId, jsonDataString, penaltyCharge, MiniJson_iOS.Serialize (dicCommerceExtraAttr));
				} 
				else if(productInfo.Count >= 2){
					List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
					for(int i  = 0, j= productInfo.Count; i < j; i++) {
						if (productInfo [i] != null) {
							filterList.Add (productInfo[i]);
						}
					}

					if(filterList != null && filterList.Count > 0) {
						string jsonDataString = "[";
						for(int i = 0; i < filterList.Count; i++) {
							AdBrixRmCommerceProductModel item = filterList[i];
							if(i == (filterList.Count-1)) {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
							}
							else {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
							}
						}
						_commerceRefundBulkWithOrderIdAndExtraAttr(orderId, jsonDataString, penaltyCharge, MiniJson_iOS.Serialize (dicCommerceExtraAttr));

					}
				}
			}
			#endif
		}

		public static void commerceSearch(List<AdBrixRmCommerceProductModel> productInfo, string keyword) {
			#if UNITY_IOS && !UNITY_EDITOR
			if(productInfo != null) {
				if (productInfo.Count == 1) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
					_commerceSearchWithProduct(jsonDataString, keyword);
				} 
				else if(productInfo.Count >= 2){
					List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
					for(int i  = 0, j= productInfo.Count; i < j; i++) {
						if (productInfo [i] != null) {
							filterList.Add (productInfo[i]);
						}
					}

					if(filterList != null && filterList.Count > 0) {
						string jsonDataString = "[";
						for(int i = 0; i < filterList.Count; i++) {
							AdBrixRmCommerceProductModel item = filterList[i];
							if(i == (filterList.Count-1)) {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
							}
							else {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
							}
						}
						_commerceSearchBulkWithProduct(jsonDataString, keyword);

					}
				}
			}
			#endif
		}

		public static void commerceSearch(List<AdBrixRmCommerceProductModel> productInfo, string keyword, Dictionary<string,object> dicCommerceExtraAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			if(productInfo != null) {
				if (productInfo.Count == 1) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
					_commerceSearchWithProductAndExtraAttr(jsonDataString, keyword, MiniJson_iOS.Serialize (dicCommerceExtraAttr));
				} 
				else if(productInfo.Count >= 2){
					List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
					for(int i  = 0, j= productInfo.Count; i < j; i++) {
						if (productInfo [i] != null) {
							filterList.Add (productInfo[i]);
						}
					}

					if(filterList != null && filterList.Count > 0) {
						string jsonDataString = "[";
						for(int i = 0; i < filterList.Count; i++) {
							AdBrixRmCommerceProductModel item = filterList[i];
							if(i == (filterList.Count-1)) {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
							}
							else {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
							}
						}
						_commerceSearchBulkWithProductAndExtraAttr(jsonDataString, keyword, MiniJson_iOS.Serialize (dicCommerceExtraAttr));

					}
				}
			}
			#endif
		}

		public static void commerceShare(AdBrixRm.SharingChannel channel, AdBrixRmCommerceProductModel productInfo) {
			#if UNITY_IOS && !UNITY_EDITOR
				if (productInfo != null) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo) + "]";
					_commerceShareWithChannel((int) channel, jsonDataString);
				} 
			#endif
		}

		public static void commerceShare(AdBrixRm.SharingChannel channel, AdBrixRmCommerceProductModel productInfo, Dictionary<string,object> dicCommerceExtraAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
				if (productInfo != null) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo) + "]";
					_commerceShareWithChannelAndExtraAttr((int) channel, jsonDataString, MiniJson_iOS.Serialize (dicCommerceExtraAttr));
				} 
			#endif
		}

		public static void commerceListView(List<AdBrixRmCommerceProductModel> productInfo) {
			#if UNITY_IOS && !UNITY_EDITOR
			if(productInfo != null) {
				if (productInfo.Count == 1) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
					_commerceListViewWithProduct(jsonDataString);
				} 
				else if(productInfo.Count >= 2){
					List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
					for(int i  = 0, j= productInfo.Count; i < j; i++) {
						if (productInfo [i] != null) {
							filterList.Add (productInfo[i]);
						}
					}

					if(filterList != null && filterList.Count > 0) {
						string jsonDataString = "[";
						for(int i = 0; i < filterList.Count; i++) {
							AdBrixRmCommerceProductModel item = filterList[i];
							if(i == (filterList.Count-1)) {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
							}
							else {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
							}
						}
						_commerceListViewBulkWithProduct(jsonDataString);

					}
				}
			}
			#endif
		}

		public static void commerceListView(List<AdBrixRmCommerceProductModel> productInfo, Dictionary<string,object> dicOrderAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			if(productInfo != null) {
				if (productInfo.Count == 1) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
					_commerceListViewProductAndOrderAttr(jsonDataString, MiniJson_iOS.Serialize (dicOrderAttr));
				} 
				else if(productInfo.Count >= 2){
					List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
					for(int i  = 0, j= productInfo.Count; i < j; i++) {
						if (productInfo [i] != null) {
							filterList.Add (productInfo[i]);
						}
					}

					if(filterList != null && filterList.Count > 0) {
						string jsonDataString = "[";
						for(int i = 0; i < filterList.Count; i++) {
							AdBrixRmCommerceProductModel item = filterList[i];
							if(i == (filterList.Count-1)) {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
							}
							else {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
							}
						}
						_commerceListViewBulkWithProductAndOrderAttr(jsonDataString, MiniJson_iOS.Serialize (dicOrderAttr));

					}
				}
			}
			#endif
		}


		public static void commerceCartView(List<AdBrixRmCommerceProductModel> productInfo) {
			#if UNITY_IOS && !UNITY_EDITOR
			if(productInfo != null) {
				if (productInfo.Count == 1) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
					_commerceCartViewWithProduct(jsonDataString);
				} 
				else if(productInfo.Count >= 2){
					List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
					for(int i  = 0, j= productInfo.Count; i < j; i++) {
						if (productInfo [i] != null) {
							filterList.Add (productInfo[i]);
						}
					}

					if(filterList != null && filterList.Count > 0) {
						string jsonDataString = "[";
						for(int i = 0; i < filterList.Count; i++) {
							AdBrixRmCommerceProductModel item = filterList[i];
							if(i == (filterList.Count-1)) {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
							}
							else {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
							}
						}
						_commerceCartViewBulkWithProduct(jsonDataString);

					}
				}
			}
			#endif
		}

		public static void commerceCartView(List<AdBrixRmCommerceProductModel> productInfo, Dictionary<string,object> dicOrderAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			if(productInfo != null) {
				if (productInfo.Count == 1) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
					_commerceCartViewProductAndOrderAttr(jsonDataString, MiniJson_iOS.Serialize (dicOrderAttr));
				} 
				else if(productInfo.Count >= 2){
					List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
					for(int i  = 0, j= productInfo.Count; i < j; i++) {
						if (productInfo [i] != null) {
							filterList.Add (productInfo[i]);
						}
					}

					if(filterList != null && filterList.Count > 0) {
						string jsonDataString = "[";
						for(int i = 0; i < filterList.Count; i++) {
							AdBrixRmCommerceProductModel item = filterList[i];
							if(i == (filterList.Count-1)) {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
							}
							else {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
							}
						}
						_commerceCartViewBulkWithProductAndOrderAttr(jsonDataString, MiniJson_iOS.Serialize (dicOrderAttr));

					}
				}
			}
			#endif
		}


		public static void commercePaymentInfoAdded(Dictionary<string,string> dicExtraAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			_commercePaymentInfoAddedWithExtraAttr(MiniJson_iOS.Serialize(dicExtraAttr));
			#endif
		}

		/* GAME */
		public static void gameLevelAchieved(int level) {
			#if UNITY_IOS && !UNITY_EDITOR
			_gameLevelAchievedWithLevel (level);
			#endif
		}

		public static void gameLevelAchieved(int level, Dictionary<string,string> dicGameAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			_gameLevelAchievedWithLevelWithGameAttr (level, MiniJson_iOS.Serialize (dicGameAttr));
			#endif
		}

		public static void gameTutorialComplete(bool isSkip) {
			#if UNITY_IOS && !UNITY_EDITOR
			_gameTutorialCompletedWithIsSkip (isSkip);
			#endif
		}

		public static void gameTutorialComplete(bool isSkip, Dictionary<string,string> dicGameAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			_gameTutorialCompletedWithIsSkipAndGameAttr (isSkip, MiniJson_iOS.Serialize (dicGameAttr));
			#endif
		}

		public static void gameCharacterCreated() {
			#if UNITY_IOS && !UNITY_EDITOR
			_gameCharacterCreated ();
			#endif
		}

		public static void gameCharacterCreated(Dictionary<string,string> dicGameAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			_gameCharacterCreatedWithGameAttr (MiniJson_iOS.Serialize (dicGameAttr));
			#endif
		}

		public static void gameStageCleared(string stageName) {
			#if UNITY_IOS && !UNITY_EDITOR
			_gameStageClearedWithStageName (stageName);
			#endif
		}

		public static void gameStageCleared(string stageName, Dictionary<string,string> dicGameAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			_gameStageClearedWithStageNameAndGameAttr (stageName, MiniJson_iOS.Serialize(dicGameAttr));
			#endif
		}

		public static void commonPurchase(string orderId, List<AdBrixRmCommerceProductModel> productInfo, double discount, double deliveryCharge, AdBrixRm.PaymentMethod paymentMethod) {
			#if UNITY_IOS && !UNITY_EDITOR
			if(productInfo != null) {
				if (productInfo.Count == 1) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
					_commonPurchaseWithOrderId(orderId, jsonDataString, discount, deliveryCharge, (int)paymentMethod);
				} 
				else if(productInfo.Count >= 2){
					List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
					for(int i  = 0, j= productInfo.Count; i < j; i++) {
						if (productInfo [i] != null) {
							filterList.Add (productInfo[i]);
						}
					}

					if(filterList != null && filterList.Count > 0) {
						string jsonDataString = "[";
						for(int i = 0; i < filterList.Count; i++) {
							AdBrixRmCommerceProductModel item = filterList[i];
							if(i == (filterList.Count-1)) {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
							}
							else {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
							}
						}
						_commonPurchaseBulkWithOrderId(orderId, jsonDataString, discount, deliveryCharge, (int)paymentMethod);

					}
				}
			}
			#endif
		}

		public static void commonPurchase(string orderId, List<AdBrixRmCommerceProductModel> productInfo, double discount, double deliveryCharge, AdBrixRm.PaymentMethod paymentMethod, Dictionary<string,object> dicCommerceExtraAttr) {

			#if UNITY_IOS && !UNITY_EDITOR
			if(productInfo != null) {
				if (productInfo.Count == 1) {
					string jsonDataString = "[";
					jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
					_commonPurchaseWithOrderIdAndExtraAttr(orderId, jsonDataString, discount, deliveryCharge, (int)paymentMethod, MiniJson_iOS.Serialize (dicCommerceExtraAttr));
				} 
				else if(productInfo.Count >= 2){
					List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
					for(int i  = 0, j= productInfo.Count; i < j; i++) {
						if (productInfo [i] != null) {
							filterList.Add (productInfo[i]);
						}
					}

					if(filterList != null && filterList.Count > 0) {
						string jsonDataString = "[";
						for(int i = 0; i < filterList.Count; i++) {
							AdBrixRmCommerceProductModel item = filterList[i];
							if(i == (filterList.Count-1)) {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
							}
							else {
								jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";		
							}
						}
						_commonPurchaseBulkWithOrderIdAndExtraAttr(orderId, jsonDataString, discount, deliveryCharge, (int)paymentMethod, MiniJson_iOS.Serialize (dicCommerceExtraAttr));

					}
				}
			}
			#endif
		}

		public static void commonSignUp(AdBrixRm.SignUpChannel channel) {
			#if UNITY_IOS && !UNITY_EDITOR
				_commonSignUp((int)channel);
			#endif
		}

		public static void commonSignUp(AdBrixRm.SignUpChannel channel, Dictionary<string, string> extraAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			_commonSignUpWithAttr((int)channel, MiniJson_iOS.Serialize (extraAttr));
			#endif
		}

		public static void commonUseCredit() {
			#if UNITY_IOS && !UNITY_EDITOR
			_commonUseCredit();
			#endif
		}

		public static void commonUseCredit(Dictionary<string, string> extraAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			_commonUseCreditWithAttr(MiniJson_iOS.Serialize (extraAttr));
			#endif
		}

		public static void commonAppUpdate(string prev_ver, string curr_ver) {
			#if UNITY_IOS && !UNITY_EDITOR
			_commonAppUpdate(prev_ver, curr_ver);
			#endif
		}

		public static void commonAppUpdate(string prev_ver, string curr_ver, Dictionary<string, string> extraAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			_commonAppUpdateWithAttr(prev_ver, curr_ver, MiniJson_iOS.Serialize (extraAttr));
			#endif
		}

		public static void commonInvite(AdBrixRm.InviteChannel channel) {
			#if UNITY_IOS && !UNITY_EDITOR
			_commonInvite((int)channel);
			#endif
		}

		public static void commonInvite(AdBrixRm.InviteChannel channel, Dictionary<string, string> extraAttr) {
			#if UNITY_IOS && !UNITY_EDITOR
			_commonInviteWithAttr((int)channel, MiniJson_iOS.Serialize (extraAttr));
			#endif
		}

		public static void registerLocalPushNotification(int delaySecond, string title, string subTitle, string body, string categoryId, string threadId, int badgeNumber, string imageUrlString, Dictionary<string, string> attrDic, string _object, string _function) {
			#if UNITY_IOS && !UNITY_EDITOR
			_registerLocalPushNotification(delaySecond, title, subTitle, body, categoryId, threadId, badgeNumber, imageUrlString, MiniJson_iOS.Serialize(attrDic), _object, _function);
			#endif
		}

		public static void getPushEventList(string _object, string _function) {
			#if UNITY_IOS && !UNITY_EDITOR
			_getRegisteredLocalPushNotification(_object, _function);
			#endif
		}
		
		public static void cancelClientPushEvent (string pushId) {
			#if UNITY_IOS && !UNITY_EDITOR
			_cancelLocalPushNotification(pushId);
			#endif
		}

		public static void cancelClientPushEventAll () {
			#if UNITY_IOS && !UNITY_EDITOR
			_cancelLocalPushNotificationAll();
			#endif
		}

		public static void restartSDK(string userId, string _object = "", string function = "") {
			#if UNITY_IOS && !UNITY_EDITOR
            _restartSDK(userId, _object, function);
			#endif
		}

		public static void deleteUserDataAndStopSDK(string userId, string _object = "", string function = "") {
			#if UNITY_IOS && !UNITY_EDITOR
            _deleteUserDataAndStopSDK(userId, _object, function);
			#endif
		}

		public static void flushAllEvents(string _object = "", string function = "") {
			#if UNITY_IOS && !UNITY_EDITOR
			_flushAllEvents(_object, function);
			#endif
		}

		public static string getSdkVersion() {
			#if UNITY_IOS && !UNITY_EDITOR
			return _getSdkVersion();
			#endif
			return "";
		}

		public static void fetchActionHistoryByUserId(string token, string[] actionType, int actionTypeCount, string _object, string function) {
			#if UNITY_IOS && !UNITY_EDITOR
            _fetchActionHistoryByUserId(token, actionType, actionTypeCount, _object, function);
			#endif
		}

		public static void fetchActionHistoryByAdid(string token, string[] actionType, int actionTypeCount, string _object, string function) {
			#if UNITY_IOS && !UNITY_EDITOR
            _fetchActionHistoryByAdid(token, actionType, actionTypeCount, _object, function);
			#endif
		}

		public static void getActionHistory(int skip, int limit, string[] actionType, int actionTypeCount, string _object, string function) {
			#if UNITY_IOS && !UNITY_EDITOR
            _getActionHistory(skip, limit, actionType, actionTypeCount, _object, function);
			#endif
		}

		public static void getAllActionHistory(string[] actionType, int actionTypeCount, string _object, string function) {
			#if UNITY_IOS && !UNITY_EDITOR
            _getAllActionHistory(actionType, actionTypeCount, _object, function);
			#endif
		}

		public static void deleteActionHistory(string token, string historyId, long timestamp, string _object, string function) {
			#if UNITY_IOS && !UNITY_EDITOR
            _deleteActionHistory(token, historyId, timestamp, _object, function);
			#endif
		}

   		public static void deleteAllActionHistoryByUserId(string token, string _object, string function) {
			#if UNITY_IOS && !UNITY_EDITOR
            _deleteAllActionHistoryByUserId(token, _object, function);
			#endif
		}
		
		public static void deleteAllActionHistoryByAdid(string token, string _object, string function) {
			#if UNITY_IOS && !UNITY_EDITOR
            _deleteAllActionHistoryByAdid(token, _object, function);
			#endif
		}

		public static void clearSyncedActionHistoryInLocalDB(string _object, string function) {
			#if UNITY_IOS && !UNITY_EDITOR
            _clearSyncedActionHistoryInLocalDB(_object, function);
			#endif
		}

		public static void getAllInAppMessage(string _object, string function) {
			#if UNITY_IOS && !UNITY_EDITOR
			_getAllInAppMessage(_object, function);
			#endif
		}

		public static void openInAppMessage(string campaignId, string _object, string function) {
			#if UNITY_IOS && !UNITY_EDITOR
			_openInAppMessage(campaignId, _object, function);
			#endif
		}

		public static void fetchInAppMessage(string _object, string function) {
			#if UNITY_IOS && !UNITY_EDITOR
			_fetchInAppMessage(_object, function);
			#endif
		}

		public static void setInAppMessageFetchMode(int mode) {
			#if UNITY_IOS && !UNITY_EDITOR
            _setInAppMessageFetchMode(mode);
			#endif
		}

		public static void setInAppMessageToken(string token) {
			#if UNITY_IOS && !UNITY_EDITOR
            _setInAppMessageToken(token);
			#endif
		}

		/* UTIL */
		public static string AdBrixCurrencyName(int currency) {
			string res = null;
			#if UNITY_IPHONE && !UNITY_EDITOR
			res = _AdBrixCurrencyNameRm((int)currency);
			#endif
			return res;
		}

		public static string AdBrixPaymentMethodName(int method) {
			string res = null;
			#if UNITY_IPHONE && !UNITY_EDITOR
			res = _AdBrixPaymentMethodNameRm((int)method);
			#endif
			return res;
		}



		/* UTIL */
		public static string AdBrixCurrencyName(AdBrixRm.Currency currency) {
			string res = null;
			#if UNITY_IPHONE && !UNITY_EDITOR
			res = _AdBrixCurrencyNameRm((int)currency);
			#endif
			return res;
		}

		public static string AdBrixPaymentMethodName(AdBrixRm.PaymentMethod method) {
			string res = null;
			#if UNITY_IPHONE && !UNITY_EDITOR
			res = _AdBrixPaymentMethodNameRm((int)method);
			#endif
			return res;
		}


		public static string AdBrixSharingChannelName(AdBrixRm.SharingChannel channel) 
		{
			string res = null;
			#if UNITY_IPHONE && !UNITY_EDITOR
			res = _AdBrixSharingChannelNameRm((int)channel);
			#endif
			return res;
		}



		public static void setLocation(double latitude, double longitude) {
			#if UNITY_IOS && !UNITY_EDITOR
				_setLocationWithLatitude(latitude, longitude);
			#endif
		}

		public static void setAge(int age) {
			#if UNITY_IOS && !UNITY_EDITOR
				_setAgeWithInt(age);
			#endif
		}

		public static void setGender(AdBrixRm.Gender adBrixGenderType) {
			#if UNITY_IOS && !UNITY_EDITOR
			_setGenderWithAdBrixGenderType((int)adBrixGenderType);
			#endif
		}

		public static void saveUserProperties(Dictionary<string, string> dictionary) {

			#if UNITY_IOS && !UNITY_EDITOR

				try {
					var listKey = dictionary.Keys.ToArray();
					var listValue =  dictionary.Values.ToArray();
					if (listKey != null && listValue != null && listKey.Length > 0 && listValue.Length > 0 && listKey.Length == listValue.Length) {
						_setUserPropertiesWithDictionary(listKey, listValue, listKey.Length);
					}
				}
				catch (Exception ex) {
					Console.WriteLine(ex.ToString());
				}

			#endif
		}

		public static void saveCiProperties(Dictionary<string, string> dictionary) {

			#if UNITY_IOS && !UNITY_EDITOR

				try {
					var listKey = dictionary.Keys.ToArray();
					var listValue =  dictionary.Values.ToArray();
					if (listKey != null && listValue != null && listKey.Length > 0 && listValue.Length > 0 && listKey.Length == listValue.Length) {
						_setCiPropertiesWithDictionary(listKey, listValue, listKey.Length);
					}
				}
				catch (Exception ex) {
					Console.WriteLine(ex.ToString());
				}

			#endif
		}


		public static void setKakaoId(string kakaoId) {

			#if UNITY_IOS && !UNITY_EDITOR
				_setKakaoId(kakaoId);
			#endif
		}

        public static void clearUserProperties() {
            #if UNITY_IOS && !UNITY_EDITOR
				_clearUserProperties();

            #endif
        }

        public static string stringifyCommerceItem(AdBrixRmCommerceProductModel item) {

			string jsonString = "";
			string jsonAttrString = "\"extra_attrs\":{";
			if(item != null)
			{

				string productId = item.productId;
				string productName = item.productName;
				double price = item.price;
				string currency = item.currencyString;
				double discount = item.discount;
				int quantity = item.quantity;
				string category = item.categories;
				Dictionary<string, string> extraAttrsDic = item.extraAttr;

				jsonString  = "{ \"productId\": " + "\"" + productId + "\"" + ", " +
					"\"productName\": " + "\"" + productName + "\"" + ", " +
					"\"price\": " + price + ", " +
					"\"currency\": " + "\"" + currency + "\"" + ", " +
					"\"discount\": " + discount + ", " +
					"\"quantity\": " + "\"" + quantity + "\"" + ", " +
					"\"category\": " + "\"" + category + "\"" + ", ";

				if (extraAttrsDic != null) {
					int pCnt = 0;
					foreach (KeyValuePair<string, string> pair in extraAttrsDic) {
						if (pCnt == (extraAttrsDic.Count - 1)) {
							jsonAttrString = jsonAttrString + stringifyCommerceItemAttr (pair) + "}";
						} else {
							jsonAttrString = jsonAttrString + stringifyCommerceItemAttr (pair) + ",";
						}
						pCnt++;
					}
				} 
				else
				{
					jsonAttrString = jsonAttrString + "}";
				}
			}
			return jsonString + jsonAttrString + "}";
		}

		public static string stringifyCommerceItemAttr(KeyValuePair<string, string> extraAttr) {
			string jsonstring;

			jsonstring = "\"" + extraAttr.Key.ToString () + "\":" + "\"" + extraAttr.Value.ToString () + "\"";

			return jsonstring;
		}

	}

	public class convertCommerModel {

		public static string stringifyCommerceItem(AdBrixRmCommerceProductModel item) {

			string jsonString = "";
			string jsonAttrString = "\"extra_attrs\":{";
			if(item != null)
			{
				string productId = item.productId;
				string productName = item.productName;
				double price = item.price;
				string currency = item.currencyString;
				double discount = item.discount;
				int quantity = item.quantity;
				string category = item.categories;
				Dictionary<string, string> extraAttrsDic = item.extraAttr;

				jsonString  = "{ \"productId\": " + "\"" + productId + "\"" + ", " +
					"\"productName\": " + "\"" + productName + "\"" + ", " +
					"\"price\": " + price + ", " +
					"\"currency\": " + "\"" + currency + "\"" + ", " +
					"\"discount\": " + discount + ", " +
					"\"quantity\": " + "\"" + quantity + "\"" + ", " +
					"\"category\": " + "\"" + category + "\"" + ", ";

				if (extraAttrsDic != null) {
					int pCnt = 0;
					foreach (KeyValuePair<string, string> pair in extraAttrsDic) {
						if (pCnt == (extraAttrsDic.Count - 1)) {
							jsonAttrString = jsonAttrString + stringifyCommerceItemAttr (pair) + "}";
						} else {
							jsonAttrString = jsonAttrString + stringifyCommerceItemAttr (pair) + ",";
						}
						pCnt++;
					}
				} 
				else
				{
					jsonAttrString = jsonAttrString + "}";
				}
			}
			return jsonString + jsonAttrString + "}";
		}

		public static string stringifyCommerceItemAttr(KeyValuePair<string, string> extraAttr) {
			string jsonstring;

			jsonstring = "\"" + extraAttr.Key.ToString () + "\":" + "\"" + extraAttr.Value.ToString () + "\"";

			return jsonstring;
		}

	}

	public class AdBrixRmCommerceProductModel {


		public string prefixExtAttr = "ex_";

		public string productId {get; set;}
		public string productName {get; set;}
		public int quantity {get; set;}
		public double price {get; set;}
		public double discount {get; set;}
		public string currencyString {get; set;}
		public string categories {get; set;}
		public Dictionary<string, string> extraAttr {get; set;}

		private void initialize() {
			this.productId = "";
			this.productName = "";
			this.quantity = 0;
			this.price = 0.0;
			this.discount = 0.0;
			this.currencyString = "";
			this.categories = "";
			this.extraAttr = new Dictionary<string, string>();
		}

		public AdBrixRmCommerceProductModel(string productId, string productName, double price, int quantity, double discount, string currencyString, AdBrixRmCommerceProductCategoryModel categories, AdBrixRmCommerceProductAttrModel extraAttrs) {

			this.initialize ();

			this.productId = productId;
			this.productName = productName;
			this.quantity = quantity;
			this.price = price;
			this.discount = discount;
			this.currencyString = currencyString;

			this.extraAttr = new Dictionary<string, string>();

			if(extraAttrs != null) {
				for(int i = 0 ; i < 5;  i ++) {
					if (!String.IsNullOrEmpty(extraAttrs.getKey(i)))
					{
						if (!extraAttrs.getKey(i).Equals(""))
						{
							this.extraAttr.Add (extraAttrs.getKey (i), extraAttrs.getValue (i));
						}
					}
				}
			}
			else {
				this.extraAttr = null;
			}
		}

		public static AdBrixRmCommerceProductModel create(string productId, string productName, double price, int quantity, double discount, string currencyString, AdBrixRmCommerceProductCategoryModel category, AdBrixRmCommerceProductAttrModel extraAttrs) {
			return new AdBrixRmCommerceProductModel(productId, productName, price, quantity, discount, currencyString, category, extraAttrs);
		}
	}

	public class AdBrixRmCommerceProductCategoryModel {
		public string[] Key{ get; set; }
		public string[] Value{ get; set; }
		public string[] Categories{ get; set; }
		public string CategoryFullString{ get; set; }
		List<string> CategoryArr;

		private string setCategoryFullString(List<string> categories)
		{
			string fullString = @"";
			int count = 0;
			categories.ForEach(delegate(String pCat)
				{
					if (!String.IsNullOrEmpty(pCat) && (count != 0))
					{
						if(!pCat.Equals(""))
						{
							fullString = fullString + "."+pCat;
						}
					}
					else if(!String.IsNullOrEmpty(pCat) && (count == 0))
					{
						if(!pCat.Equals(""))
						{
							fullString = pCat;
						}
					}

					count++;
				});

			return fullString;
		}

		public static AdBrixRmCommerceProductCategoryModel create(string category1)
		{
			AdBrixRmCommerceProductCategoryModel pObject = new AdBrixRmCommerceProductCategoryModel();
			List<string> pArr = new List<string>();
			pArr.Add (category1);
			pObject.CategoryFullString =  pObject.setCategoryFullString(pArr);
			return pObject;
		}

		public static AdBrixRmCommerceProductCategoryModel create(string category1, string category2)
		{
			AdBrixRmCommerceProductCategoryModel pObject = new AdBrixRmCommerceProductCategoryModel();
			List<string> pArr = new List<string>();
			pArr.Add (category1);
			pArr.Add (category2);
			pObject.CategoryFullString =  pObject.setCategoryFullString(pArr);
			return pObject;
		}

		public static AdBrixRmCommerceProductCategoryModel create(string category1, string category2, string category3)
		{
			AdBrixRmCommerceProductCategoryModel pObject = new AdBrixRmCommerceProductCategoryModel();
			List<string> pArr = new List<string>();
			pArr.Add (category1);
			pArr.Add (category2);
			pArr.Add (category3);
			pObject.CategoryFullString =  pObject.setCategoryFullString(pArr);
			return pObject;
		}

		public static AdBrixRmCommerceProductCategoryModel create(string category1, string category2, string category3, string category4)
		{
			AdBrixRmCommerceProductCategoryModel pObject = new AdBrixRmCommerceProductCategoryModel();
			List<string> pArr = new List<string>();
			pArr.Add (category1);
			pArr.Add (category2);
			pArr.Add (category3);
			pArr.Add (category4);

			pObject.CategoryFullString =  pObject.setCategoryFullString(pArr);
			return pObject;
		}

		public static AdBrixRmCommerceProductCategoryModel create(string category1, string category2, string category3, string category4, string category5)
		{
			AdBrixRmCommerceProductCategoryModel pObject = new AdBrixRmCommerceProductCategoryModel();
			List<string> pArr = new List<string>();
			pArr.Add (category1);
			pArr.Add (category2);
			pArr.Add (category3);
			pArr.Add (category4);
			pArr.Add (category5);

			pObject.CategoryFullString =  pObject.setCategoryFullString(pArr);
			return pObject;
		}

	}

	public class AdBrixRmCommerceProductAttrModel
	{
		public string[] Key{ get; set; }
		public string[] Value{ get; set; }

		public static AdBrixRmCommerceProductAttrModel create(Dictionary<string, string> attrData) {
			AdBrixRmCommerceProductAttrModel pObject = new AdBrixRmCommerceProductAttrModel();

			pObject.Key = new string[5];
			pObject.Value = new string[5];
			int count = 0;
			foreach(KeyValuePair<string, string> entry in attrData)
			{
				pObject.setKeyAndVal (count, entry.Key, entry.Value);
				if (count == 4)
				{
					//parameter counts must set less then 5, data from the 6th to the end gonna be missed!!");
					break;
				}
				count++;
			} 

			return pObject;
		}

		private void setKeyAndVal(int pIndex, string key, string value) {
			Key[pIndex]		= key;
			Value[pIndex] 	= value;
		}

		public string getKey (int pIndex) {
			return Key[pIndex];
		}

		public string getValue (int pIndex) {
			return Value[pIndex];
		}
	}

	public class DfnIOSLogger : MonoBehaviour {
 
     #if UNITY_IPHONE
     [DllImport ("__Internal")]
     private static extern void _logToiOS(string debugMessage);
     #endif
 
     public static void LogToiOS(string logString, string stackTrace, LogType type) {
         // We check for UNITY_IPHONE again so we don't try this if it isn't iOS platform.
         #if UNITY_IPHONE
         // Now we check that it's actually an iOS device/simulator, not the Unity Player. You only get plugins on the actual device or iOS Simulator.
         if (Application.platform == RuntimePlatform.IPhonePlayer) {
             _logToiOS(logString);
         }
         #endif
     }
 }


}

