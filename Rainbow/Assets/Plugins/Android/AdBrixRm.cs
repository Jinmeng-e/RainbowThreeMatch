using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;

namespace AdBrixRmAOS
{
    public class AdBrixRm : MonoBehaviour
    {
        #region AdbrixRM enum
        public enum AdBrixLogLevel
        {
            NONE = 0,
            TRACE = 1,
            DEBUG = 2,
            INFO = 3,
            WARNING = 4,
            ERROR = 5
        }

        public enum AdBrixEventUploadCountInterval
        {
            MIN = 10,
            NORMAL = 30,
            MAX = 60
        }

        public enum AdBrixEventUploadTimeInterval
        {
            MIN = 30,
            NORMAL = 60,
            MAX = 120
        }

        public enum Gender
        {
            FEMALE = 1,
            MALE = 2
        }

        public enum Currency
        {
            KR_KRW,
            US_USD,
            JP_JPY,
            EU_EUR,
            UK_GBP,
            CH_CNY,
            TW_TWD,
            HK_HKD,
            ID_IDR,//Indonesia
            IN_INR,//India
            RU_RUB,//Russia
            TH_THB,//Thailand
            VN_VND,//Vietnam
            MY_MYR//Malaysia
        }

        public enum PaymentMethod
        {
            CreditCard = 1,
            BankTransfer = 2,
            MobilePayment = 3,
            ETC = 4
        }

        public enum SharingChannel
        {
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

        public enum SignUpChannel
        {
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

        public enum InviteChannel
        {
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

        public enum DfnInAppMessageFetchMode
        {
            USER_ID = 0,
            ADID = 1
        }
        #endregion

        #region Unity Event
        public static AndroidJavaObject sdkAosInstance;
        void Awake()
        {
            initPlugin();
        }

        public static void initPlugin()
        {
#if UNITY_ANDROID
            if(sdkAosInstance == null)
            {
                sdkAosInstance = new AndroidJavaClass("com.igaworks.v2.unity.AbxUnityActivity");
                if(sdkAosInstance != null)
                {
			        Debug.Log ("AdbrixRM AOS Connected!!!");
		        }
		        else{
			        Debug.Log ("AdbrixRM AOS Fail!!!");
		        }
            }
#endif
        }
        #endregion

        #region AdbrixRM API
        public static void sendGameObjectNameToAbxUnityApplication(string gameObjectName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm sendGameObjectNameToAbxUnityApplication() is called");
initPlugin();
sdkAosInstance.CallStatic("sendGameObjectNameToAbxUnityApplication", gameObjectName);
#endif
        }
        public static void init(string appKey, string secretKey)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm init() is called");
initPlugin();
sdkAosInstance.CallStatic("init", appKey, secretKey);
#endif
        }
        public static void login(string userId)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm login() is called");
initPlugin();
sdkAosInstance.CallStatic("login", userId);
#endif
        }
        public static void logout()
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm logout() is called");
initPlugin();
sdkAosInstance.CallStatic("logout");
#endif
        }
        public static void gdprForgetMe()
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm gdprForgetMe() is called");
initPlugin();
sdkAosInstance.CallStatic("gdprForgetMe");
#endif
        }
        public static void setAge(int age)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setAge() is called");
initPlugin();
sdkAosInstance.CallStatic("setAge", age);
#endif
        }
        public static void setGender(Gender gender)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setGender() is called");
initPlugin();
sdkAosInstance.CallStatic("setGender", (int)gender);
#endif
        }
        public static void saveUserProperties(Dictionary<string, string> properties)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm saveUserProperties() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("saveUserProperties", map);
#endif
        }
        public static void clearUserProperties()
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm clearUserProperties() is called");
initPlugin();
sdkAosInstance.CallStatic("clearUserProperties");
#endif
        }
        public static void setLocation(double latitude, double longitude)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setLocation() is called");
initPlugin();
sdkAosInstance.CallStatic("setLocation",  latitude,  longitude);
#endif
        }
        public static void eventWithName (string eventName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm event() is called");
initPlugin();
sdkAosInstance.CallStatic("event", eventName);
#endif
        }
        public static void eventWithEventNameAndValue (string eventName, Dictionary<string, string> properties)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm event() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("event", eventName, map);
#endif
        }
        public static void flushAllEvents(string callbackObjectName, string callbackMethodName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm flushAllEvents() is called");
initPlugin();
sdkAosInstance.CallStatic("flushAllEvents", callbackObjectName, callbackMethodName);
#endif
        }
        public static void setEventUploadCountInterval(AdBrixEventUploadCountInterval eventUploadCountInterval)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setEventUploadCountInterval() is called");
initPlugin();
sdkAosInstance.CallStatic("setEventUploadCountInterval", (int)eventUploadCountInterval);
#endif
        }
        public static void setEventUploadTimeInterval(AdBrixEventUploadTimeInterval eventUploadTimeInterval)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setEventUploadTimeInterval() is called");
initPlugin();
sdkAosInstance.CallStatic("setEventUploadTimeInterval", (int)eventUploadTimeInterval);
#endif
        }
        public static void setPushEnable(bool enable)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setPushEnable() is called");
initPlugin();
sdkAosInstance.CallStatic("setPushEnable", enable);
#endif
        }
        public static void setRegistrationId(string token)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setRegistrationId() is called");
initPlugin();
sdkAosInstance.CallStatic("setRegistrationId", token);
#endif
        }
        public static void setBigTextClientPushEvent(BigTextPushProperties bigTextPushProperties, bool alwaysIsShown)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setBigTextClientPushEvent() is called");
initPlugin();
sdkAosInstance.CallStatic("setBigTextClientPushEvent", bigTextPushProperties.ToJsonString(), alwaysIsShown);
#endif
        }
        public static void setBigPictureClientPushEvent(BigPicturePushProperties bigPicturePushProperties, bool alwaysIsShown)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setBigPictureClientPushEvent() is called");
initPlugin();
sdkAosInstance.CallStatic("setBigPictureClientPushEvent", bigPicturePushProperties.ToJsonString(), alwaysIsShown);
#endif
        }
        public static void cancelClientPushEvent(int eventId)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm cancelClientPushEvent() is called");
initPlugin();
sdkAosInstance.CallStatic("cancelClientPushEvent", eventId);
#endif
        }
        public static string getPushEventList()
        {
            string result = "";
#if UNITY_ANDROID
Debug.Log("AdBrixRm getPushEventList() is called");
initPlugin();
result = sdkAosInstance.CallStatic<string>("getPushEventList");
#endif
            return result;
        }
        public static void setPushIconStyle(string smallIconName, string largeIconName, int argb)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setPushIconStyle() is called");
initPlugin();
sdkAosInstance.CallStatic("setPushIconStyle", smallIconName, largeIconName, argb);
#endif
        }
        public static void setPushIconStyle(string smallIconName, string largeIconName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setPushIconStyle() is called");
initPlugin();
sdkAosInstance.CallStatic("setPushIconStyle", smallIconName, largeIconName);
#endif
        }
        public static void setNotificationOption(int priority, int visibility)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setNotificationOption() is called");
initPlugin();
sdkAosInstance.CallStatic("setNotificationOption", priority, visibility);
#endif
        }
        public static void setNotificationChannel(string channelName, string channelDescription, int importance, bool vibrateEnable)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setNotificationChannel() is called");
initPlugin();
sdkAosInstance.CallStatic("setNotificationChannel", channelName, channelDescription, importance, vibrateEnable);
#endif
        }
        public static void setKakaoId(string kakaoId)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setKakaoId() is called");
initPlugin();
sdkAosInstance.CallStatic("setKakaoId", kakaoId);
#endif
        }
        public static void saveCiProperties(string key, string value)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm saveCiProperties() is called");
initPlugin();
sdkAosInstance.CallStatic("saveCiProperties", key, value);
#endif
        }
        public static void openPush(AbxRemotePushModel abxRemotePushModel)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm openPush() is called");
initPlugin();
sdkAosInstance.CallStatic("openPush", abxRemotePushModel.toOpenPushEventParamJsonstring());
#endif
        }
        public static void deleteUserDataAndStopSDK(string userId, string callbackObjectName, string callbackMethodName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm deleteUserDataAndStopSDK() is called");
initPlugin();
sdkAosInstance.CallStatic("deleteUserDataAndStopSDK", userId, callbackObjectName, callbackMethodName);
#endif
        }
        public static void restartSDK(string userId, string callbackObjectName, string callbackMethodName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm restartSDK() is called");
initPlugin();
sdkAosInstance.CallStatic("restartSDK", userId, callbackObjectName, callbackMethodName);
#endif
        }
        public static string getSdkVersion()
        {
            string result = "";
#if UNITY_ANDROID
Debug.Log("AdBrixRm getSdkVersion() is called");
initPlugin();
result = sdkAosInstance.CallStatic<string>("getSdkVersion");
#endif
            return result;
        }
        public static void fetchActionHistoryByUserId(string token, List<string> actionType, string callbackObjectName, string callbackMethodName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm fetchActionHistoryByUserId() is called");
initPlugin();
string list = getJsonArrayFromList(actionType);
sdkAosInstance.CallStatic("fetchActionHistoryByUserId", token, list, callbackObjectName, callbackMethodName);
#endif
        }
        public static void fetchActionHistoryByAdid(string token, List<string> actionType, string callbackObjectName, string callbackMethodName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm fetchActionHistoryByAdid() is called");
initPlugin();
string list = getJsonArrayFromList(actionType);
sdkAosInstance.CallStatic("fetchActionHistoryByAdid", token, list, callbackObjectName, callbackMethodName);
#endif
        }
        public static void insertPushData(Dictionary<string, string> data)
        {

#if UNITY_ANDROID
Debug.Log("AdBrixRm insertPushData() is called");
initPlugin();
string map = getJsonObjectFromDictionary(data);
sdkAosInstance.CallStatic("insertPushData", map);
#endif
        }
        public static void getActionHistory(int skip, int limit, List<string> actionType, string callbackObjectName, string callbackMethodName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm getActionHistory() is called");
initPlugin();
string list = getJsonArrayFromList(actionType);
sdkAosInstance.CallStatic("getActionHistory", skip, limit, list, callbackObjectName, callbackMethodName);
#endif
        }
        public static void getAllActionHistory(List<string> actionType, string callbackObjectName, string callbackMethodName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm getAllActionHistory() is called");
initPlugin();
string list = getJsonArrayFromList(actionType);
sdkAosInstance.CallStatic("getAllActionHistory", list, callbackObjectName, callbackMethodName);
#endif
        }
        public static void deleteActionHistory(
            string token,
            string historyId,
            long timestamp,
            string callbackObjectName,
            string callbackMethodName
    )
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm deleteActionHistory() is called");
initPlugin();
sdkAosInstance.CallStatic("deleteActionHistory",
            token,
            historyId,
            timestamp,
            callbackObjectName,
            callbackMethodName
    );
#endif
        }
        public static void deleteAllActionHistoryByUserId(string token, string callbackObjectName, string callbackMethodName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm deleteAllActionHistoryByUserId() is called");
initPlugin();
sdkAosInstance.CallStatic("deleteAllActionHistoryByUserId", token, callbackObjectName, callbackMethodName);
#endif
        }

        public static void deleteAllActionHistoryByAdid(string token, string callbackObjectName, string callbackMethodName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm deleteAllActionHistoryByAdid() is called");
initPlugin();
sdkAosInstance.CallStatic("deleteAllActionHistoryByAdid", token, callbackObjectName, callbackMethodName);
#endif
        }

        public static void clearSyncedActionHistoryInLocalDB(string callbackObjectName, string callbackMethodName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm clearSyncedActionHistoryInLocalDB() is called");
initPlugin();
sdkAosInstance.CallStatic("clearSyncedActionHistoryInLocalDB", callbackObjectName, callbackMethodName);
#endif
        }

        public static void setInAppMessageFetchMode(DfnInAppMessageFetchMode mode)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setInAppMessageFetchMode() is called");
initPlugin();
sdkAosInstance.CallStatic("setInAppMessageFetchMode", (int)mode);
#endif
        }

        public static void setInAppMessageToken(string token)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm setInAppMessageToken() is called");
initPlugin();
sdkAosInstance.CallStatic("setInAppMessageToken", token);
#endif
        }

        public static void fetchInAppMessage(string callbackObjectName, string callbackMethodName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm fetchInAppMessage() is called");
initPlugin();
sdkAosInstance.CallStatic("fetchInAppMessage", callbackObjectName, callbackMethodName);
#endif
        }

        public static void getAllInAppMessage(string callbackObjectName, string callbackMethodName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm getAllInAppMessage() is called");
initPlugin();
sdkAosInstance.CallStatic("getAllInAppMessage", callbackObjectName, callbackMethodName);
#endif
        }

        public static void openInAppMessage(string campaignId, string callbackObjectName, string callbackMethodName)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm openInAppMessage() is called");
initPlugin();
sdkAosInstance.CallStatic("openInAppMessage", campaignId, callbackObjectName, callbackMethodName);
#endif
        }

        public static void commonPurchase(string orderId, List<AdBrixRmCommerceProductModel> models, double discount, double deliveryCharge, PaymentMethod paymentMethod)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commonPurchase() is called");
initPlugin();
sdkAosInstance.CallStatic("commonPurchase", orderId, commerceProductModelListJson,  discount,  deliveryCharge, (int)paymentMethod);
#endif
        }

        public static void commonPurchase(string orderId, List<AdBrixRmCommerceProductModel> models, double discount, double deliveryCharge, PaymentMethod paymentMethod, Dictionary<string, string> properties)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commonPurchase() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commonPurchase", orderId, commerceProductModelListJson,  discount,  deliveryCharge, (int)paymentMethod, map);
#endif
        }

        public static void commonPurchase(string orderId, List<AdBrixRmCommerceProductModel> models, double orderSales, double discount, double deliveryCharge, PaymentMethod paymentMethod)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commonPurchase() is called");
initPlugin();
sdkAosInstance.CallStatic("commonPurchase", orderId, commerceProductModelListJson,  orderSales,  discount,  deliveryCharge, (int)paymentMethod);
#endif
        }

        public static void commonPurchase(string orderId, List<AdBrixRmCommerceProductModel> models, double orderSales, double discount, double deliveryCharge, PaymentMethod paymentMethod, Dictionary<string, string> properties)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commonPurchase() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commonPurchase", orderId, commerceProductModelListJson,  orderSales,  discount,  deliveryCharge, (int)paymentMethod, map);
#endif
        }

        public static void commonSignUp(SignUpChannel signUpChannel)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm commonSignUp() is called");
initPlugin();
sdkAosInstance.CallStatic("commonSignUp", (int)signUpChannel);
#endif
        }

        public static void commonSignUp(SignUpChannel signUpChannel, Dictionary<string, string> properties)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm commonSignUp() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commonSignUp", (int)signUpChannel, map);
#endif
        }

        public static void commonUseCredit()
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm commonUseCredit() is called");
initPlugin();
sdkAosInstance.CallStatic("commonUseCredit");
#endif
        }

        public static void commonUseCredit(Dictionary<string, string> properties)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm commonUseCredit() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commonUseCredit", map);
#endif
        }

        public static void commonAppUpdate(string previousVersion, string currentVersion, Dictionary<string, string> properties)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm commonAppUpdate() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commonAppUpdate", previousVersion, currentVersion, map);
#endif
        }

        public static void commonInvite(InviteChannel channel)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm commonInvite() is called");
initPlugin();
sdkAosInstance.CallStatic("commonInvite", (int)channel);
#endif
        }

        public static void commonInvite(InviteChannel channel, Dictionary<string, string> properties)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm commonInvite() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commonInvite", (int)channel, map);
#endif
        }

        public static void commerceViewHome()
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceViewHome() is called");
initPlugin();
sdkAosInstance.CallStatic("commerceViewHome");
#endif
        }

        public static void commerceViewHome(Dictionary<string, string> properties)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceViewHome() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commerceViewHome", map);
#endif
        }

        public static void commerceCategoryView(List<string> categories, List<AdBrixRmCommerceProductModel> models)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceCategoryView() is called");
initPlugin();
string list = getJsonArrayFromList(categories);
sdkAosInstance.CallStatic("commerceCategoryView", list, commerceProductModelListJson);
#endif
        }

        public static void commerceCategoryView(List<string> categories, List<AdBrixRmCommerceProductModel> models, Dictionary<string, string> properties)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceCategoryView() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
string list = getJsonArrayFromList(categories);
sdkAosInstance.CallStatic("commerceCategoryView", list, commerceProductModelListJson, map);
#endif
        }

        public static void commerceCategoryView(List<string> categories)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceCategoryView() is called");
initPlugin();
string list = getJsonArrayFromList(categories);
sdkAosInstance.CallStatic("commerceCategoryView", list);
#endif
        }

        public static void commerceProductView(AdBrixRmCommerceProductModel model)
        {
            string commerceProductModelJson = getAdBrixRmCommerceProductModelJsonString(model);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceProductView() is called");
initPlugin();
sdkAosInstance.CallStatic("commerceProductView", commerceProductModelJson);
#endif
        }

        public static void commerceProductView(AdBrixRmCommerceProductModel model, Dictionary<string, string> properties)
        {
            string commerceProductModelJson = getAdBrixRmCommerceProductModelJsonString(model);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceProductView() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commerceProductView", commerceProductModelJson, map);
#endif
        }

        public static void commerceAddToCart(List<AdBrixRmCommerceProductModel> models)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceAddToCart() is called");
initPlugin();
sdkAosInstance.CallStatic("commerceAddToCart", commerceProductModelListJson);
#endif
        }

        public static void commerceAddToCart(List<AdBrixRmCommerceProductModel> models, Dictionary<string, string> properties)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceAddToCart() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commerceAddToCart", commerceProductModelListJson, map);
#endif
        }

        public static void commerceAddToWishList(AdBrixRmCommerceProductModel model)
        {
            string commerceProductModelJson = getAdBrixRmCommerceProductModelJsonString(model);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceAddToWishList() is called");
initPlugin();
sdkAosInstance.CallStatic("commerceAddToWishList", commerceProductModelJson);
#endif
        }

        public static void commerceAddToWishList(AdBrixRmCommerceProductModel model, Dictionary<string, string> properties)
        {
            string commerceProductModelJson = getAdBrixRmCommerceProductModelJsonString(model);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceAddToWishList() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commerceAddToWishList", commerceProductModelJson, map);
#endif
        }

        public static void commerceReviewOrder(string orderId, List<AdBrixRmCommerceProductModel> models, double discount, double deliveryCharge)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceReviewOrder() is called");
initPlugin();
sdkAosInstance.CallStatic("commerceReviewOrder", orderId, commerceProductModelListJson,  discount,  deliveryCharge);
#endif
        }

        public static void commerceReviewOrder(string orderId, List<AdBrixRmCommerceProductModel> models, double discount, double deliveryCharge, Dictionary<string, string> properties)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceReviewOrder() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commerceReviewOrder", orderId, commerceProductModelListJson,  discount,  deliveryCharge, map);
#endif
        }

        public static void commerceRefund(string orderId, List<AdBrixRmCommerceProductModel> models, double penaltyCharge)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceRefund() is called");
initPlugin();
sdkAosInstance.CallStatic("commerceRefund", orderId, commerceProductModelListJson,  penaltyCharge);
#endif
        }

        public static void commerceRefund(string orderId, List<AdBrixRmCommerceProductModel> models, double penaltyCharge, Dictionary<string, string> properties)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceRefund() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commerceRefund", orderId, commerceProductModelListJson,  penaltyCharge, map);
#endif
        }

        public static void commerceSearch(string keyword, List<AdBrixRmCommerceProductModel> models)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceSearch() is called");
initPlugin();
sdkAosInstance.CallStatic("commerceSearch", keyword, commerceProductModelListJson);
#endif
        }

        public static void commerceSearch(string keyword, List<AdBrixRmCommerceProductModel> models, Dictionary<string, string> properties)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceSearch() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commerceSearch", keyword, commerceProductModelListJson, map);
#endif
        }

        public static void commerceShare(SharingChannel sharingChannel, AdBrixRmCommerceProductModel model)
        {
            string commerceProductModelJson = getAdBrixRmCommerceProductModelJsonString(model);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceShare() is called");
initPlugin();
sdkAosInstance.CallStatic("commerceShare", sharingChannel.ToString(), commerceProductModelJson);
#endif
        }

        public static void commerceShare(SharingChannel sharingChannel, AdBrixRmCommerceProductModel model, Dictionary<string, string> properties)
        {
            string commerceProductModelJson = getAdBrixRmCommerceProductModelJsonString(model);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceShare() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commerceShare", sharingChannel.ToString(), commerceProductModelJson, map);
#endif
        }

        public static void commerceListView(List<AdBrixRmCommerceProductModel> models)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceListView() is called");
initPlugin();
sdkAosInstance.CallStatic("commerceListView", commerceProductModelListJson);
#endif
        }

        public static void commerceListView(List<AdBrixRmCommerceProductModel> models, Dictionary<string, string> properties)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceListView() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commerceListView", commerceProductModelListJson, map);
#endif
        }

        public static void commerceCartView(List<AdBrixRmCommerceProductModel> models)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceCartView() is called");
initPlugin();
sdkAosInstance.CallStatic("commerceCartView", commerceProductModelListJson);
#endif
        }

        public static void commerceCartView(List<AdBrixRmCommerceProductModel> models, Dictionary<string, string> properties)
        {
            string commerceProductModelListJson = getAdBrixRmCommerceProductModelListJsonString(models);
#if UNITY_ANDROID
Debug.Log("AdBrixRm commerceCartView() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commerceCartView", commerceProductModelListJson, map);
#endif
        }

        public static void commercePaymentInfoAdded()
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm commercePaymentInfoAdded() is called");
initPlugin();
sdkAosInstance.CallStatic("commercePaymentInfoAdded");
#endif
        }

        public static void commercePaymentInfoAdded(Dictionary<string, string> properties)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm commercePaymentInfoAdded() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("commercePaymentInfoAdded", map);
#endif
        }

        public static void gameTutorialComplete()
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm gameTutorialComplete() is called");
initPlugin();
sdkAosInstance.CallStatic("gameTutorialComplete");
#endif
        }

        public static void gameTutorialComplete(bool isSkip, Dictionary<string, string> properties)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm gameTutorialComplete() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("gameTutorialComplete", isSkip, map);
#endif
        }

        public static void gameLevelAchieved()
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm gameLevelAchieved() is called");
initPlugin();
sdkAosInstance.CallStatic("gameLevelAchieved");
#endif
        }

        public static void gameLevelAchieved(int level, Dictionary<string, string> properties)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm gameLevelAchieved() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("gameLevelAchieved", level, map);
#endif
        }

        public static void gameCharacterCreated()
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm gameCharacterCreated() is called");
initPlugin();
sdkAosInstance.CallStatic("gameCharacterCreated");
#endif
        }

        public static void gameCharacterCreated(Dictionary<string, string> properties)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm gameCharacterCreated() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("gameCharacterCreated", map);
#endif
        }

        public static void gameStageCleared()
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm gameStageCleared() is called");
initPlugin();
sdkAosInstance.CallStatic("gameStageCleared");
#endif
        }

        public static void gameStageCleared(string stageName, Dictionary<string, string> properties)
        {
#if UNITY_ANDROID
Debug.Log("AdBrixRm gameStageCleared() is called");
initPlugin();
string map = getJsonObjectFromDictionary(properties);
sdkAosInstance.CallStatic("gameStageCleared", stageName, map);
#endif
        }

        #endregion

        #region Utils
        private static string getJsonObjectFromDictionary(IDictionary<string, string> parameters)
        {
            return MiniJson_aos.Serialize(parameters);
        }

        private static string getJsonArrayFromList(List<string> parameters)
        {
            return MiniJson_aos.Serialize(parameters);
        }
        private static string getAdBrixRmCommerceProductModelJsonString(AdBrixRmCommerceProductModel productInfo)
        {
            string result = "";
            if (productInfo == null)
            {
                Debug.Log("productInfo is null");
                return result;
            }
            string jsonDataString = "[";
            jsonDataString = jsonDataString + stringifyCommerceItem(productInfo) + "]";
            result = jsonDataString;
            return result;
        }

        private static string getAdBrixRmCommerceProductModelListJsonString(List<AdBrixRmCommerceProductModel> productInfo)
        {
            string result = "";
            if (productInfo == null)
            {
                Debug.Log("productInfo is null");
                return result;
            }
            if (productInfo.Count == 1)
            {
                string jsonDataString = "[";
                jsonDataString = jsonDataString + stringifyCommerceItem(productInfo[0]) + "]";
                result = jsonDataString;
            }
            else if (productInfo.Count >= 2)
            {
                List<AdBrixRmCommerceProductModel> filterList = new List<AdBrixRmCommerceProductModel>();
                for (int i = 0, j = productInfo.Count; i < j; i++)
                {
                    if (productInfo[i] != null)
                    {
                        filterList.Add(productInfo[i]);
                    }
                }

                if (filterList != null && filterList.Count > 0)
                {
                    string jsonDataString = "[";
                    for (int i = 0; i < filterList.Count; i++)
                    {
                        AdBrixRmCommerceProductModel item = filterList[i];
                        if (i == (filterList.Count - 1))
                        {
                            jsonDataString = jsonDataString + stringifyCommerceItem(item) + "]";
                        }
                        else
                        {
                            jsonDataString = jsonDataString + stringifyCommerceItem(item) + ",";
                        }
                    }
                    result = jsonDataString;
                }
            }
            return result;
        }

        public static string stringifyCommerceItem(AdBrixRmCommerceProductModel item)
        {
            string jsonString = "";
            string jsonAttrString = "\"extra_attrs\":{";
            if (item != null)
            {
                string productId = item.productId;
                string productName = item.productName;
                double price = item.price;
                string currency = item.currencyString;
                double discount = item.discount;
                int quantity = item.quantity;
                string category = item.categories;
                Dictionary<string, string> extraAttrsDic = item.extraAttr;

                jsonString = "{ \"productId\": " + "\"" + productId + "\"" + ", " +
                    "\"productName\": " + "\"" + productName + "\"" + ", " +
                    "\"price\": " + price + ", " +
                    "\"currency\": " + "\"" + currency + "\"" + ", " +
                    "\"discount\": " + discount + ", " +
                    "\"quantity\": " + "\"" + quantity + "\"" + ", " +
                    "\"category\": " + "\"" + category + "\"" + ", ";

                if (extraAttrsDic != null)
                {
                    int pCnt = 0;
                    foreach (KeyValuePair<string, string> pair in extraAttrsDic)
                    {
                        if (pCnt == (extraAttrsDic.Count - 1))
                        {
                            jsonAttrString = jsonAttrString + stringifyCommerceItemAttr(pair) + "}";
                        }
                        else
                        {
                            jsonAttrString = jsonAttrString + stringifyCommerceItemAttr(pair) + ",";
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

        public static string stringifyCommerceItemAttr(KeyValuePair<string, string> extraAttr)
        {
            string jsonstring;
            jsonstring = "\"" + extraAttr.Key.ToString() + "\":" + "\"" + extraAttr.Value.ToString() + "\"";
            return jsonstring;
        }
        #endregion

        public class AbxRemotePushModel
        {
            public string title;
            public string body;
            public string bigTextTitle;
            public string bigTextBody;
            public string summaryText;
            public string imageUrl;
            public string largeIconUrl;
            public bool genWhileRun;
            public bool genVibe;
            public bool genSound;
            public string deeplinkUrl;
            public Dictionary<string, string> deeplinkJson;

            public string campaignId;
            public int campaignRevisionNo;
            public string stepId;
            public string cycleTime;
            public int notificationId;

            public AbxRemotePushModel()
            {
                this.title = "";
                this.body = "";
                this.bigTextTitle = "";
                this.bigTextBody = "";
                this.summaryText = "";
                this.imageUrl = "";
                this.largeIconUrl = "";
                this.genWhileRun = false;
                this.genVibe = false;
                this.genSound = false;
                this.deeplinkUrl = "";
                this.deeplinkJson = new Dictionary<string, string>();
                this.notificationId = 0;

                this.campaignId = "";
                this.campaignRevisionNo = 0;
                this.stepId = "";
                this.cycleTime = "";
            }

            public AbxRemotePushModel(string campaignId, int campaignRevisionNo, string stepId, string cycleTime)
            {
                this.title = null;
                this.body = null;
                this.bigTextTitle = null;
                this.bigTextBody = null;
                this.summaryText = null;
                this.imageUrl = null;
                this.largeIconUrl = null;
                this.genWhileRun = false;
                this.genVibe = false;
                this.genSound = false;
                this.deeplinkUrl = null;
                this.deeplinkJson = null;
                this.notificationId = 0;

                this.campaignId = campaignId;
                this.campaignRevisionNo = campaignRevisionNo;
                this.stepId = stepId;
                this.cycleTime = cycleTime;
            }

            public AbxRemotePushModel(Dictionary<string, string> pushJson)
            {
                try
                {
                    var abxGfFcmJson = MiniJson_aos.Deserialize(pushJson["abx:gf:fcm"]) as Dictionary<string, string>;
                    var alertJson = MiniJson_aos.Deserialize(abxGfFcmJson["alert"]) as Dictionary<string, string>;

                    this.campaignId = abxGfFcmJson["abx:gf:campaign_id"] as string;
                    this.campaignRevisionNo = Convert.ToInt32(abxGfFcmJson["abx:gf:campaign_revision_no"]);
                    this.stepId = abxGfFcmJson["abx:gf:step_id"] as string;
                    this.cycleTime = abxGfFcmJson["abx:gf:cycle_time"] as string;

                    if (alertJson.ContainsKey("title")) { this.title = alertJson["title"] as string; }
                    if (alertJson.ContainsKey("body")) { this.body = alertJson["body"] as string; }
                    if (alertJson.ContainsKey("big_text_title")) { this.bigTextTitle = alertJson["big_text_title"] as string; }
                    if (alertJson.ContainsKey("big_text_body")) { this.bigTextBody = alertJson["big_text_body"] as string; }
                    if (alertJson.ContainsKey("summary_text")) { this.summaryText = alertJson["summary_text"] as string; }
                    if (alertJson.ContainsKey("img")) { this.imageUrl = alertJson["img"] as string; }
                    if (alertJson.ContainsKey("large_icon")) { this.largeIconUrl = alertJson["large_icon"] as string; }

                    this.genWhileRun = Convert.ToBoolean(abxGfFcmJson["gen_while_run"]);
                    this.genVibe = Convert.ToBoolean(abxGfFcmJson["gen_vibe"]);
                    this.genSound = Convert.ToBoolean(abxGfFcmJson["gen_sound"]);

                    if (abxGfFcmJson.ContainsKey("deep_link_url")) { this.deeplinkUrl = abxGfFcmJson["deep_link_url"] as string; }
                    if (abxGfFcmJson.ContainsKey("deep_link_json")) { this.deeplinkJson = MiniJson_aos.Deserialize(abxGfFcmJson["deep_link_json"]) as Dictionary<string, string>; }

                    this.notificationId = Convert.ToInt32(abxGfFcmJson["notification_id"]);
                }
                catch (System.Exception ex)
                {
                    throw new Exception("Adbrix push data don't exist! " + ex.ToString());
                }
            }

            public string toOpenPushEventParamJsonstring()
            {
                Dictionary<string, string> pushParam = new Dictionary<string, string>();
                pushParam.Add("abx:gf:campaign_id", this.campaignId);
                pushParam.Add("abx:gf:step_id", this.stepId);
                pushParam.Add("abx:gf:campaign_revision_no", Convert.ToString(this.campaignRevisionNo));
                pushParam.Add("abx:gf:cycle_time", this.cycleTime);

                return MiniJson_aos.Serialize(pushParam);
            }

            public string Tostring()
            {
                return "AbxRemotePushModel{\n" +
                        " title='" + title + '\'' +
                        ",\n body='" + body + '\'' +
                        ",\n bigTextTitle='" + bigTextTitle + '\'' +
                        ",\n bigTextBody='" + bigTextBody + '\'' +
                        ",\n summaryText='" + summaryText + '\'' +
                        ",\n imageUrl='" + imageUrl + '\'' +
                        ",\n largeIconUrl='" + largeIconUrl + '\'' +
                        ",\n genWhileRun=" + genWhileRun +
                        ",\n genVibe=" + genVibe +
                        ",\n genSound=" + genSound +
                        ",\n deeplinkUrl='" + deeplinkUrl + '\'' +
                        ",\n deeplinkJson=" + deeplinkJson +
                        ",\n campaignId='" + campaignId + '\'' +
                        ",\n campaignRevisionNo=" + campaignRevisionNo +
                        ",\n stepId='" + stepId + '\'' +
                        ",\n cycleTime='" + cycleTime + '\'' +
                        ",\n notificationId=" + notificationId +
                        "\n";
            }
        }
        public class PushProperties
        {
            protected long second;
            protected int eventId;
            protected string contentText;
            protected string summaryText;
            protected string bigContentTitle;
            protected string title;
            protected string deepLinkUri;

            public PushProperties()
            {
                second = 0;
                eventId = 0;
                contentText = "";
                summaryText = "";
                bigContentTitle = "";
                title = "";
                deepLinkUri = "";
            }

            public PushProperties setSecond(long second)
            {
                this.second = second;
                return this;
            }

            public PushProperties setEventId(int eventId)
            {
                this.eventId = eventId;
                return this;
            }

            public PushProperties setContentText(string contentText)
            {
                this.contentText = contentText;
                return this;
            }

            public PushProperties setSummaryText(string summaryText)
            {
                this.summaryText = summaryText;
                return this;
            }

            public PushProperties setBigContentTitle(string bigContentTitle)
            {
                this.bigContentTitle = bigContentTitle;
                return this;
            }

            public PushProperties setTitle(string title)
            {
                this.title = title;
                return this;
            }

            public PushProperties setDeepLinkUri(string deepLinkUri)
            {
                this.deepLinkUri = deepLinkUri;
                return this;
            }
        }

        public class BigTextPushProperties : PushProperties
        {
            private string bigText;

            public BigTextPushProperties()
            {
                bigText = "";
            }

            public BigTextPushProperties setBigText(string bigText)
            {
                this.bigText = bigText;
                return this;
            }

            public string ToJsonString()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
				var dict = new Dictionary<string, string>();
				dict.Add("second", Convert.ToString(second));
				dict.Add("eventId", Convert.ToString(eventId));
				dict.Add("contentText", contentText);
				dict.Add("summaryText", summaryText);
				dict.Add("bigContentTitle", bigContentTitle);
				dict.Add("title", title);
				dict.Add("deepLinkUri", deepLinkUri);
				dict.Add("bigText", bigText);

				return MiniJson_aos.Serialize(dict);
#else
                return "{}";
#endif
            }
        }

        public class BigPicturePushProperties : PushProperties
        {
            private string bigPictureUrl;
            private int resourceId;

            public BigPicturePushProperties()
            {
                this.bigPictureUrl = "";
                this.resourceId = 0;
            }

            public BigPicturePushProperties setBigPictureUrl(string bigPictureUrl)
            {
                this.bigPictureUrl = bigPictureUrl;
                return this;
            }

            public BigPicturePushProperties setResourceId(int resourceId)
            {
                this.resourceId = resourceId;
                return this;
            }

            public string ToJsonString()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
				var dict = new Dictionary<string, string>();
				dict.Add("second", Convert.ToString(second));
				dict.Add("eventId", Convert.ToString(eventId));
				dict.Add("contentText", contentText);
				dict.Add("summaryText", summaryText);
				dict.Add("bigContentTitle", bigContentTitle);
				dict.Add("title", title);
				dict.Add("deepLinkUri", deepLinkUri);
				dict.Add("bigPictureUrl", bigPictureUrl);
				dict.Add("resourceId", Convert.ToString(resourceId));

				return MiniJson_aos.Serialize(dict);
#else
                return "{}";
#endif
            }
        }

        public class AdBrixRmCommerceProductAttrModel
        {
            public string[] Key { get; set; }
            public string[] Value { get; set; }

            public static AdBrixRmCommerceProductAttrModel create(Dictionary<string, string> attrData)
            {
                AdBrixRmCommerceProductAttrModel pObject = new AdBrixRmCommerceProductAttrModel();

                pObject.Key = new string[5];
                pObject.Value = new string[5];
                int count = 0;
                foreach (KeyValuePair<string, string> entry in attrData)
                {
                    pObject.setKeyAndVal(count, entry.Key, entry.Value);
                    if (count == 4)
                    {
                        //parameter counts must set less then 5, data from the 6th to the end gonna be missed!!");
                        break;
                    }
                    count++;
                }

                return pObject;
            }

            private void setKeyAndVal(int pIndex, string key, string value)
            {
                Key[pIndex] = key;
                Value[pIndex] = value;
            }

            public string getKey(int pIndex)
            {
                return Key[pIndex];
            }

            public string getValue(int pIndex)
            {
                return Value[pIndex];
            }
        }

        public class AdBrixRmCommerceProductModel
        {
            public string prefixExtAttr = "ex_";

            public string productId { get; set; }
            public string productName { get; set; }
            public int quantity { get; set; }
            public double price { get; set; }
            public double discount { get; set; }
            public string currencyString { get; set; }
            public string categories { get; set; }
            public Dictionary<string, string> extraAttr { get; set; }

            private void initialize()
            {
                this.productId = "";
                this.productName = "";
                this.quantity = 0;
                this.price = 0.0;
                this.discount = 0.0;
                this.currencyString = "";
                this.categories = "";
                this.extraAttr = new Dictionary<string, string>();
            }

            public AdBrixRmCommerceProductModel(string productId, string productName, double price, int quantity, double discount, string currencyString, AdBrixRmCommerceProductCategoryModel categories, AdBrixRmCommerceProductAttrModel extraAttrs)
            {

                this.initialize();

                this.productId = productId;
                this.productName = productName;
                this.quantity = quantity;
                this.price = price;
                this.discount = discount;
                this.currencyString = currencyString;

                this.extraAttr = new Dictionary<string, string>();

                if (extraAttrs != null)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (!String.IsNullOrEmpty(extraAttrs.getKey(i)))
                        {
                            if (!extraAttrs.getKey(i).Equals(""))
                            {
                                this.extraAttr.Add(extraAttrs.getKey(i), extraAttrs.getValue(i));
                            }
                        }
                    }
                }
                else
                {
                    this.extraAttr = null;
                }
            }

            public static AdBrixRmCommerceProductModel create(string productId, string productName, double price, int quantity, double discount, string currencyString, AdBrixRmCommerceProductCategoryModel category, AdBrixRmCommerceProductAttrModel extraAttrs)
            {
                return new AdBrixRmCommerceProductModel(productId, productName, price, quantity, discount, currencyString, category, extraAttrs);
            }
        }

        public class AdBrixRmCommerceProductCategoryModel
        {
            public string[] Key { get; set; }
            public string[] Value { get; set; }
            public string[] Categories { get; set; }
            public string CategoryFullString { get; set; }
            List<string> CategoryArr;

            public string setCategoryFullString(List<string> categories)
            {
                string fullString = @"";
                int count = 0;
                categories.ForEach(delegate (String pCat)
                {
                    if (!String.IsNullOrEmpty(pCat) && (count != 0))
                    {
                        if (!pCat.Equals(""))
                        {
                            fullString = fullString + "." + pCat;
                        }
                    }
                    else if (!String.IsNullOrEmpty(pCat) && (count == 0))
                    {
                        if (!pCat.Equals(""))
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
                pArr.Add(category1);
                pObject.CategoryFullString = pObject.setCategoryFullString(pArr);
                return pObject;
            }

            public static AdBrixRmCommerceProductCategoryModel create(string category1, string category2)
            {
                AdBrixRmCommerceProductCategoryModel pObject = new AdBrixRmCommerceProductCategoryModel();
                List<string> pArr = new List<string>();
                pArr.Add(category1);
                pArr.Add(category2);
                pObject.CategoryFullString = pObject.setCategoryFullString(pArr);
                return pObject;
            }

            public static AdBrixRmCommerceProductCategoryModel create(string category1, string category2, string category3)
            {
                AdBrixRmCommerceProductCategoryModel pObject = new AdBrixRmCommerceProductCategoryModel();
                List<string> pArr = new List<string>();
                pArr.Add(category1);
                pArr.Add(category2);
                pArr.Add(category3);
                pObject.CategoryFullString = pObject.setCategoryFullString(pArr);
                return pObject;
            }

            public static AdBrixRmCommerceProductCategoryModel create(string category1, string category2, string category3, string category4)
            {
                AdBrixRmCommerceProductCategoryModel pObject = new AdBrixRmCommerceProductCategoryModel();
                List<string> pArr = new List<string>();
                pArr.Add(category1);
                pArr.Add(category2);
                pArr.Add(category3);
                pArr.Add(category4);

                pObject.CategoryFullString = pObject.setCategoryFullString(pArr);
                return pObject;
            }

            public static AdBrixRmCommerceProductCategoryModel create(string category1, string category2, string category3, string category4, string category5)
            {
                AdBrixRmCommerceProductCategoryModel pObject = new AdBrixRmCommerceProductCategoryModel();
                List<string> pArr = new List<string>();
                pArr.Add(category1);
                pArr.Add(category2);
                pArr.Add(category3);
                pArr.Add(category4);
                pArr.Add(category5);

                pObject.CategoryFullString = pObject.setCategoryFullString(pArr);
                return pObject;
            }
        }
    }
}
