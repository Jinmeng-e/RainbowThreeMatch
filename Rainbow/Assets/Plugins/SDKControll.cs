using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Threading.Tasks;

#if UNITY_IOS
using UnityEngine.iOS;
using AdBrixRmIOS;

#elif UNITY_ANDROID
using AdBrixRmAOS;

#endif

public class SDKControll : MonoBehaviour
{
    private string callbackObject = "";
    void Awake()
    {
        callbackObject = this.gameObject.name;
        Debug.Log("callbackObject: "+callbackObject);
    }

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_IOS
        AdBrixRm.setEventUploadCountInterval(AdBrixRm.AdBrixEventUploadCountInterval.MIN);
        AdBrixRm.setEventUploadTimeInterval(AdBrixRm.AdBrixEventUploadTimeInterval.MIN);
        Application.logMessageReceived += DfnIOSLogger.LogToiOS;

#elif UNITY_ANDROID
        AdBrixRm.setEventUploadCountInterval(AdBrixRm.AdBrixEventUploadCountInterval.NORMAL);
        AdBrixRm.setEventUploadTimeInterval(AdBrixRm.AdBrixEventUploadTimeInterval.NORMAL);
#endif
    }

    protected bool LogTaskCompletion(Task task, string operation)
    {
        bool complete = false;
        if (task.IsCanceled)
        {
            DebugLog(operation + " canceled.");
        }
        else if (task.IsFaulted)
        {
            DebugLog(operation + " encounted an error.");
        }
        else if (task.IsCompleted)
        {
            DebugLog(operation + " completed");
            complete = true;
        }
        return complete;
    }

    void DebugLog(string log)
    {
#if UNITY_IOS
        Debug.Log(log);
#elif UNITY_ANDROID
        Debug.Log(log);
#endif
    }

    void OnDisable()
    {
#if UNITY_IOS

#elif UNITY_ANDROID

#endif
    }

    //딥링크
    public void HandleDidReceiveDeeplink(string deepLinkValue)
    {
#if UNITY_IOS
        Debug.Log("HandleDidReceiveDeeplink : " + deepLinkValue);

#elif UNITY_ANDROID
        Debug.Log("HandleDidReceiveDeeplink : "+ deepLinkValue);

#endif
    }

    //디퍼드 딥링크
    public void HandleDidReceiveDeferredDeeplink(string deferredDeepLinkValue)
    {
#if UNITY_IOS
        Debug.Log("HandleDidReceiveDeferredDeeplink : "+ deferredDeepLinkValue);

#elif UNITY_ANDROID
        Debug.Log("HandleDidReceiveDeferredDeeplink : "+ deferredDeepLinkValue);

#endif
    }

    //Restart 콜백
    public void restartCallback(string result)
    {
#if UNITY_IOS
        Debug.Log("restart callback result : "+ result);
#elif UNITY_ANDROID
        Debug.Log("restart callback result : "+ result);
#endif
    }

    //Delete 콜백
    public void deleteCallback(string result)
    {
#if UNITY_IOS
        Debug.Log("delete callback result : "+ result);
#elif UNITY_ANDROID
        Debug.Log("delete callback result : "+ result);
#endif
    }

    //flushAllEvents 콜백
    public void flushAllEventsCallback(string result)
    {
#if UNITY_IOS
        Debug.Log("flushAllEvents callback result : "+ result);
#elif UNITY_ANDROID
        Debug.Log("flushAllEvents callback result : "+ result);
#endif
    }

    public void setLogListenerCallback(string message)
    {
#if UNITY_IOS

#elif UNITY_ANDROID
        Debug.Log("setLogListenerCallback : " + message);
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void login()
    {
#if UNITY_IOS
        AdBrixRm.login ("william_IOS");

#elif UNITY_ANDROID
        AdBrixRm.login ("william_AOS");

#endif
    }

    public void logout()
    {
#if UNITY_IOS
        AdBrixRm.logout();

#elif UNITY_ANDROID
        AdBrixRm.logout();

#endif
    }

    public void userInfo()
    {
#if UNITY_IOS
        //유저 나이
        AdBrixRm.setAge(30);
        //유저 성별
        AdBrixRm.setGender(AdBrixRm.Gender.FEMALE);
        //기타 유저 정보
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("nickname", "AdBrixRm");
        AdBrixRm.saveUserProperties(dict);

        Dictionary<string, string> ciDict = new Dictionary<string, string>();
        ciDict.Add("ci_nickname", "unity");
        AdBrixRm.saveCiProperties(ciDict);

        AdBrixRm.setKakaoId("user_kakao");

#elif UNITY_ANDROID
        //유저 나이
        AdBrixRm.setAge (30);
        //유저 설별
        AdBrixRm.setGender(AdBrixRm.Gender.FEMALE);
        //기타 유저 정보
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("nickname", "william");
        AdBrixRm.saveUserProperties (dict);

#endif
    }

    public void customEvent(){
        #if UNITY_IOS
         AdBrixRm.eventWithName("customEvent_IOS");

#elif UNITY_ANDROID
        AdBrixRm.eventWithName("customEvent_AOS");

#endif
    }

    public void userSignup()
    {
#if UNITY_IOS
        // 유저 정보를 adbrix 로 전달할 경우 설정합니다.
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("nickname", "william_IOS");
        AdBrixRm.commonSignUp(AdBrixRm.SignUpChannel.Google, dict);

#elif UNITY_ANDROID
        // 유저 정보를 Adbrix 로 전달할 경우 설정합니다.
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("nickname", "william_AOS");

        AdBrixRm.commonSignUp(AdBrixRm.SignUpChannel.Google, dict);

#endif
    }

    public void update()
    {
#if UNITY_IOS
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("nickname", "AdBrixRm");
        AdBrixRm.commonAppUpdate("1.2.2a", "1.3.0", dict);

#elif UNITY_ANDROID
        // 유저 정보
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("nickname", "AdBrixRm");

        AdBrixRm.commonAppUpdate("1.2.2a","1.3.0",dict);

#endif
    }

    public void Invite()
    {
#if UNITY_IOS
        // 유저 정보
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("nickname", "AdBrixRm");
        AdBrixRm.commonInvite(AdBrixRm.InviteChannel.Facebook, dict);

#elif UNITY_ANDROID
        // 유저 정보
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("nickname", "AdBrixRm");

        AdBrixRm.commonInvite(AdBrixRm.InviteChannel.Facebook, dict);

#endif
    }

    public void Credit()
    {
#if UNITY_IOS
        // 크레딧 사용 정보
        Dictionary<string, string> credit_info = new Dictionary<string, string>();
        credit_info.Add("credit", "20000");
        AdBrixRm.commonUseCredit(credit_info);

#elif UNITY_ANDROID
        // 크레딧 사용 정보
        Dictionary<string, string> credit_info= new Dictionary<string, string>();
        credit_info.Add("credit", "20000");

        AdBrixRm.commonUseCredit(credit_info);

#endif
    }

    public void purchase()
    {
#if UNITY_IOS
        //상품을 담을 배열
        List<AdBrixRmCommerceProductModel> items = new List<AdBrixRmCommerceProductModel>();
        //상품 상세 옵션 Dictionary
        Dictionary<string, string> productAttrs = new Dictionary<string, string>();
        productAttrs.Add("Att1", "Value1");
        productAttrs.Add("Att2", "Value2");
        productAttrs.Add("Att3", "Value3");

        //상품 생성
        AdBrixRmCommerceProductModel productModel = AdBrixRmCommerceProductModel.create(
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRm.AdBrixCurrencyName(AdBrixRm.Currency.KR_KRW),
                    AdBrixRmCommerceProductCategoryModel.create("Cate1", "Cate2", "Cate3"),
                    AdBrixRmCommerceProductAttrModel.create(productAttrs)
        );

        //상품 생성
        AdBrixRmCommerceProductModel productModel2 = AdBrixRmCommerceProductModel.create(
                    "productId02",
                    "productName02",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRm.AdBrixCurrencyName(AdBrixRm.Currency.KR_KRW),
                    AdBrixRmCommerceProductCategoryModel.create("Cate1", "Cate2", "Cate3"),
                    AdBrixRmCommerceProductAttrModel.create(productAttrs)
        );

        items.Add(productModel);
        items.Add(productModel2);

        //상품 결제하기 이벤트
        AdBrixRm.commonPurchase("30290121", items, 1000.00, 3500.00, AdBrixRm.PaymentMethod.CREDIT_CARD);

#elif UNITY_ANDROID
        //상품을 담을 배열
        List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List <AdBrixRm.AdBrixRmCommerceProductModel>();

        //상품 상세 옵션 Dictionary
        Dictionary<string, string>productAttrs = new Dictionary<string, string>();
        productAttrs.Add ("Att1", "Value1");
        productAttrs.Add ("Att2", "Value2");
        productAttrs.Add ("Att3", "Value3");

        //상품 생성
        AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRmAOS.AdBrixRm.Currency.KR_KRW.ToString().ToString(),
                    AdBrixRmAOS.AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
                    AdBrixRmAOS.AdBrixRm.AdBrixRmCommerceProductAttrModel.create (productAttrs)
                );

        //상품 생성
        AdBrixRm.AdBrixRmCommerceProductModel productModel2 = AdBrixRm.AdBrixRmCommerceProductModel.create (
                    "productId02",
                    "productName02",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRm.Currency.KR_KRW.ToString(),
                    AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
                    AdBrixRm.AdBrixRmCommerceProductAttrModel.create (productAttrs)
        );

        items.Add (productModel);
        items.Add (productModel2);

        //상품 결제하기 이벤트
        AdBrixRm.commonPurchase ("30290121", items, 1000.00, 3500.00, AdBrixRm.PaymentMethod.CreditCard);

#endif
    }

    public void viewHome()
    {
#if UNITY_IOS
        AdBrixRm.commerceViewHome();

#elif UNITY_ANDROID
        AdBrixRm.commerceViewHome();

#endif
    }

    public void viewCategory()
    {
#if UNITY_IOS
        //상품을 담을 배열
        List<AdBrixRmCommerceProductModel> items = new List<AdBrixRmCommerceProductModel>();

        //상품 상세 옵션 Dictionary
        Dictionary<string, string> productAttrs = new Dictionary<string, string>();
        productAttrs.Add("Att1", "Value1");
        productAttrs.Add("Att2", "Value2");
        productAttrs.Add("Att3", "Value3");

        //카테고리에 포함된 상품 생성
        AdBrixRmCommerceProductModel productModel = AdBrixRmCommerceProductModel.create(
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRm.AdBrixCurrencyName(AdBrixRm.Currency.KR_KRW),
                    AdBrixRmCommerceProductCategoryModel.create("하의기획전"),
                    AdBrixRmCommerceProductAttrModel.create(productAttrs)
        );

        items.Add(productModel);

        //카테고리 진입 이벤트
        AdBrixRm.commerceCategoryView(AdBrixRmCommerceProductCategoryModel.create("기획전"), items);

#elif UNITY_ANDROID

        //상품 상세 옵션 Dictionary
        Dictionary<string, string> productAttrs = new Dictionary<string, string>();
        productAttrs.Add("Att1", "Value1");
        productAttrs.Add("Att2", "Value2");
        productAttrs.Add("Att3", "Value3");

        //상품을 담을 배열
        List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List <AdBrixRm.AdBrixRmCommerceProductModel>();

        //카테고리에 포함된 상품 생성
        AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRm.Currency.KR_KRW.ToString(),
                    AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("하의기획전"),
                    AdBrixRm.AdBrixRmCommerceProductAttrModel.create(productAttrs)
        );

        items.Add (productModel);
        List<string> categoryList = new List<string>();
        categoryList.Add("기획전");

        //카테고리 진입 이벤트
        AdBrixRm.commerceCategoryView(categoryList, items);

#endif
    }

    public void ProductDetail()
    {
#if UNITY_IOS
        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string> productAttrs = new Dictionary<string, string>();
        productAttrs.Add("Att1", "Value1");
        productAttrs.Add("Att2", "Value2");
        productAttrs.Add("Att3", "Value3");

        //상품 생성
        AdBrixRmCommerceProductModel productModel = AdBrixRmCommerceProductModel.create(
                "productId01",
                "productName01",
                10000.00,
                1,
                5000,
                AdBrixRm.AdBrixCurrencyName(AdBrixRm.Currency.KR_KRW),
                AdBrixRmCommerceProductCategoryModel.create("Cate1", "Cate2", "Cate3"),
                AdBrixRmCommerceProductAttrModel.create(productAttrs)
        );
        //상품 상세보기
        AdBrixRm.commerceProductView(productModel);

#elif UNITY_ANDROID
        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string> productAttrs = new Dictionary<string, string>();
        productAttrs.Add ("Att1", "Value1");
        productAttrs.Add ("Att2", "Value2");
        productAttrs.Add ("Att3", "Value3");

        //상품 생성
        AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000,
                    AdBrixRmAOS.AdBrixRm.Currency.KR_KRW.ToString(),
                    AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
                    AdBrixRm.AdBrixRmCommerceProductAttrModel.create (productAttrs)
        );
        //상품 상세보기
        AdBrixRm.commerceProductView (productModel);

#endif
    }

    public void addToCart()
    {
#if UNITY_IOS
        //상품을 담을 배열
        List<AdBrixRmCommerceProductModel> items = new List<AdBrixRmCommerceProductModel>();

        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string> productAttrs = new Dictionary<string, string>();
        productAttrs.Add("Att1", "Value1");
        productAttrs.Add("Att2", "Value2");
        productAttrs.Add("Att3", "Value3");

        //상품 생성
        AdBrixRmCommerceProductModel productModel =
        AdBrixRmCommerceProductModel.create(
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRm.AdBrixCurrencyName(AdBrixRm.Currency.KR_KRW),
                    AdBrixRmCommerceProductCategoryModel.create("Cate1", "Cate2", "Cate3"),
                    AdBrixRmCommerceProductAttrModel.create(productAttrs)
                );

        items.Add(productModel);

        //장바구니 이벤트
        AdBrixRm.commerceAddToCart(items);

#elif UNITY_ANDROID
        //상품을 담을 배열
        List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List <AdBrixRm.AdBrixRmCommerceProductModel>();

        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string>productAttrs = new Dictionary<string, string>();
        productAttrs.Add ("Att1", "Value1");
        productAttrs.Add ("Att2", "Value2");
        productAttrs.Add ("Att3", "Value3");

        //상품 생성
        AdBrixRm.AdBrixRmCommerceProductModel productModel = AdBrixRm.AdBrixRmCommerceProductModel.create (
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRmAOS.AdBrixRm.Currency.KR_KRW.ToString(),
                    AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
                    AdBrixRm.AdBrixRmCommerceProductAttrModel.create (productAttrs)
        );

        items.Add (productModel);

        //장바구니 이벤트
        AdBrixRm.commerceAddToCart(items);

#endif
    }

    public void addToWishList()
    {
#if UNITY_IOS
        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string> productAttrs = new Dictionary<string, string>();
        productAttrs.Add("Att1", "Value1");
        productAttrs.Add("Att2", "Value2");
        productAttrs.Add("Att3", "Value3");

        //상품 생성
        AdBrixRmCommerceProductModel productModel =
        AdBrixRmCommerceProductModel.create(
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRm.AdBrixCurrencyName(AdBrixRm.Currency.KR_KRW),
                    AdBrixRmCommerceProductCategoryModel.create("Cate1", "Cate2", "Cate3"),
                    AdBrixRmCommerceProductAttrModel.create(productAttrs)
        );


        //관심상품 이벤트
        AdBrixRm.commerceAddToWishList(productModel);

#elif UNITY_ANDROID
        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string>productAttrs = new Dictionary<string, string>();
        productAttrs.Add ("Att1", "Value1");
        productAttrs.Add ("Att2", "Value2");
        productAttrs.Add ("Att3", "Value3");

        //상품 생성
        AdBrixRm.AdBrixRmCommerceProductModel productModel =
        AdBrixRm.AdBrixRmCommerceProductModel.create (
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRmAOS.AdBrixRm.Currency.KR_KRW.ToString(),
                    AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
                    AdBrixRm.AdBrixRmCommerceProductAttrModel.create (productAttrs)
        );


        //관심상품 이벤트
        AdBrixRm.commerceAddToWishList (productModel);

#endif
    }

    public void reviewOrder()
    {
#if UNITY_IOS
        //상품을 담을 배열
        List<AdBrixRmCommerceProductModel> items = new List<AdBrixRmCommerceProductModel>();

        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string> productAttrs = new Dictionary<string, string>();
        productAttrs.Add("Att1", "Value1");
        productAttrs.Add("Att2", "Value2");
        productAttrs.Add("Att3", "Value3");

        //상품 생성
        AdBrixRmCommerceProductModel productModel =
        AdBrixRmCommerceProductModel.create(
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRm.AdBrixCurrencyName(AdBrixRm.Currency.KR_KRW),
                    AdBrixRmCommerceProductCategoryModel.create("Cate1", "Cate2", "Cate3"),
                    AdBrixRmCommerceProductAttrModel.create(productAttrs)
        );

        items.Add(productModel);

        //주문확인 이벤트
        AdBrixRm.commerceReviewOrder("30290121", items, 1000.00, 3500.00);

#elif UNITY_ANDROID
        //상품을 담을 배열
        List<AdBrixRm.AdBrixRmCommerceProductModel>items = new List <AdBrixRm.AdBrixRmCommerceProductModel>();

        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string>productAttrs = new Dictionary<string, string>();
        productAttrs.Add ("Att1", "Value1");
        productAttrs.Add ("Att2", "Value2");
        productAttrs.Add ("Att3", "Value3");

        //상품 생성
        AdBrixRm.AdBrixRmCommerceProductModel productModel =
        AdBrixRm.AdBrixRmCommerceProductModel.create (
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRmAOS.AdBrixRm.Currency.KR_KRW.ToString(),
                    AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
                    AdBrixRm.AdBrixRmCommerceProductAttrModel.create (productAttrs)
        );

        items.Add (productModel);

        //주문확인 이벤트
        AdBrixRm.commerceReviewOrder ("30290121", items, 1000.00, 3500.00);

#endif
    }

    public void refundOrder()
    {
#if UNITY_IOS
        //상품을 담을 배열
        List<AdBrixRmCommerceProductModel> items = new List<AdBrixRmCommerceProductModel>();

        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string> productAttrs = new Dictionary<string, string>();
        productAttrs.Add("Att1", "Value1");
        productAttrs.Add("Att2", "Value2");
        productAttrs.Add("Att3", "Value3");

        //상품 생성
        AdBrixRmCommerceProductModel productModel =
        AdBrixRmCommerceProductModel.create(
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRm.AdBrixCurrencyName(AdBrixRm.Currency.KR_KRW),
                    AdBrixRmCommerceProductCategoryModel.create("Cate1", "Cate2", "Cate3"),
                    AdBrixRmCommerceProductAttrModel.create(productAttrs)
        );

        items.Add(productModel);

        //주문 취소 이벤트
        AdBrixRm.commerceRefund("30290121", items, 3500.00);

#elif UNITY_ANDROID
        //상품을 담을 배열
        List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List <AdBrixRm.AdBrixRmCommerceProductModel>();

        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string>productAttrs = new Dictionary<string, string>();
        productAttrs.Add ("Att1", "Value1");
        productAttrs.Add ("Att2", "Value2");
        productAttrs.Add ("Att3", "Value3");

        //상품 생성
        AdBrixRm.AdBrixRmCommerceProductModel productModel =
        AdBrixRm.AdBrixRmCommerceProductModel.create (
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRmAOS.AdBrixRm.Currency.KR_KRW.ToString(),
                    AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
                    AdBrixRm.AdBrixRmCommerceProductAttrModel.create (productAttrs)
        );

        items.Add (productModel);

        //주문 취소 이벤트
        AdBrixRm.commerceRefund ("30290121", items, 3500.00);

#endif
    }

    public void searchProduct()
    {
#if UNITY_IOS
        //상품을 담을 배열
        List<AdBrixRmCommerceProductModel> items = new List<AdBrixRmCommerceProductModel>();

        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string> productAttrs = new Dictionary<string, string>();
        productAttrs.Add("Att1", "Value1");
        productAttrs.Add("Att2", "Value2");
        productAttrs.Add("Att3", "Value3");

        //상품 생성
        AdBrixRmCommerceProductModel productModel =
        AdBrixRmCommerceProductModel.create(
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRm.AdBrixCurrencyName(AdBrixRm.Currency.KR_KRW),
                    AdBrixRmCommerceProductCategoryModel.create("Cate1", "Cate2", "Cate3"),
                    AdBrixRmCommerceProductAttrModel.create(productAttrs)
        );

        items.Add(productModel);

        //상품 검색 이벤트
        AdBrixRm.commerceSearch(items, "나이키");

#elif UNITY_ANDROID
        //상품을 담을 배열
        List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List <AdBrixRm.AdBrixRmCommerceProductModel>();

        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string>productAttrs = new Dictionary<string, string>();
        productAttrs.Add ("Att1", "Value1");
        productAttrs.Add ("Att2", "Value2");
        productAttrs.Add ("Att3", "Value3");

        //상품 생성
        AdBrixRm.AdBrixRmCommerceProductModel productModel =
        AdBrixRm.AdBrixRmCommerceProductModel.create (
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRmAOS.AdBrixRm.Currency.KR_KRW.ToString(),
                    AdBrixRmAOS.AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
                    AdBrixRmAOS.AdBrixRm.AdBrixRmCommerceProductAttrModel.create (productAttrs)
                );

                items.Add(productModel);

        //상품 검색 이벤트
        AdBrixRmAOS.AdBrixRm.commerceSearch("나이키", items);

#endif
    }

    public void shareProduct()
    {
#if UNITY_IOS
        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string> productAttrs = new Dictionary<string, string>();
        productAttrs.Add("Att1", "Value1");
        productAttrs.Add("Att2", "Value2");
        productAttrs.Add("Att3", "Value3");

        //상품 생성
        AdBrixRmCommerceProductModel productModel =
        AdBrixRmCommerceProductModel.create(
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRm.AdBrixCurrencyName(AdBrixRm.Currency.KR_KRW),
                    AdBrixRmCommerceProductCategoryModel.create("Cate1", "Cate2", "Cate3"),
                    AdBrixRmCommerceProductAttrModel.create(productAttrs)
        );


        //상품 공유하기 이벤트
        AdBrixRm.commerceShare(AdBrixRm.SharingChannel.KAKAOTALK, productModel);

#elif UNITY_ANDROID
        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string>productAttrs = new Dictionary<string, string>();
        productAttrs.Add ("Att1", "Value1");
        productAttrs.Add ("Att2", "Value2");
        productAttrs.Add ("Att3", "Value3");

        //상품 생성
        AdBrixRm.AdBrixRmCommerceProductModel productModel =
        AdBrixRm.AdBrixRmCommerceProductModel.create (
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRmAOS.AdBrixRm.Currency.KR_KRW.ToString(),
                    AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
                    AdBrixRm.AdBrixRmCommerceProductAttrModel.create (productAttrs)
        );


        //상품 공유하기 이벤트
        AdBrixRm.commerceShare (AdBrixRm.SharingChannel.KAKAOTALK, productModel);

#endif
    }

    public void listViewProducts()
    {
#if UNITY_IOS
        //상품을 담을 배열
        List<AdBrixRmCommerceProductModel> items = new List<AdBrixRmCommerceProductModel>();

        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string> productAttrs = new Dictionary<string, string>();
        productAttrs.Add("Att1", "Value1");
        productAttrs.Add("Att2", "Value2");
        productAttrs.Add("Att3", "Value3");

        //상품 생성
        AdBrixRmCommerceProductModel productModel =
        AdBrixRmCommerceProductModel.create(
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRm.AdBrixCurrencyName(AdBrixRm.Currency.KR_KRW),
                    AdBrixRmCommerceProductCategoryModel.create("Cate1", "Cate2", "Cate3"),
                    AdBrixRmCommerceProductAttrModel.create(productAttrs)
        );

        items.Add(productModel);

        //상품 목록 조회 이벤트
        AdBrixRm.commerceListView(items);

#elif UNITY_ANDROID
        //상품을 담을 배열
        List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List <AdBrixRm.AdBrixRmCommerceProductModel>();

        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string>productAttrs = new Dictionary<string, string>();
        productAttrs.Add ("Att1", "Value1");
        productAttrs.Add ("Att2", "Value2");
        productAttrs.Add ("Att3", "Value3");

        //상품 생성
        AdBrixRm.AdBrixRmCommerceProductModel productModel =
        AdBrixRm.AdBrixRmCommerceProductModel.create (
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRmAOS.AdBrixRm.Currency.KR_KRW.ToString(),
                    AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
                    AdBrixRm.AdBrixRmCommerceProductAttrModel.create (productAttrs)
        );

        items.Add (productModel);

        //상품 목록 조회 이벤트
        AdBrixRm.commerceListView (items);

#endif
    }

    public void cartViewProducts()
    {
#if UNITY_IOS
        //상품을 담을 배열
        List<AdBrixRmCommerceProductModel> items = new List<AdBrixRmCommerceProductModel>();

        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string> productAttrs = new Dictionary<string, string>();
        productAttrs.Add("Att1", "Value1");
        productAttrs.Add("Att2", "Value2");
        productAttrs.Add("Att3", "Value3");

        //상품 생성
        AdBrixRmCommerceProductModel productModel =
        AdBrixRmCommerceProductModel.create(
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRm.AdBrixCurrencyName(AdBrixRm.Currency.KR_KRW),
                    AdBrixRmCommerceProductCategoryModel.create("Cate1", "Cate2", "Cate3"),
                    AdBrixRmCommerceProductAttrModel.create(productAttrs)
        );

        items.Add(productModel);

        //장바구니 조회하기 이벤트
        AdBrixRm.commerceCartView(items);

#elif UNITY_ANDROID
        //상품을 담을 배열
        List<AdBrixRm.AdBrixRmCommerceProductModel> items = new List <AdBrixRm.AdBrixRmCommerceProductModel>();

        //상품 상세 옵션 Dictionary 생성
        Dictionary<string, string>productAttrs = new Dictionary<string, string>();
        productAttrs.Add ("Att1", "Value1");
        productAttrs.Add ("Att2", "Value2");
        productAttrs.Add ("Att3", "Value3");

        //상품 생성
        AdBrixRm.AdBrixRmCommerceProductModel productModel =
        AdBrixRm.AdBrixRmCommerceProductModel.create (
                    "productId01",
                    "productName01",
                    10000.00,
                    1,
                    5000.00,
                    AdBrixRmAOS.AdBrixRm.Currency.KR_KRW.ToString(),
                    AdBrixRm.AdBrixRmCommerceProductCategoryModel.create ("Cate1", "Cate2", "Cate3"),
                    AdBrixRm.AdBrixRmCommerceProductAttrModel.create (productAttrs)
        );

        items.Add (productModel);

        //장바구니 조회하기 이벤트
        AdBrixRm.commerceCartView (items);

#endif
    }

    public void paymentInfoAdded()
    {
#if UNITY_IOS
        //구매 정보 상세 옵션 Dictionary
        Dictionary<string, string> paymentAttrs = new Dictionary<string, string>();
        paymentAttrs.Add("creditcard", "oocard");

        AdBrixRm.commercePaymentInfoAdded(paymentAttrs);

#elif UNITY_ANDROID
        //구매 정보 상세 옵션 Dictionary
        Dictionary<string, string> paymentAttrs = new Dictionary<string, string>();
        paymentAttrs.Add("creditcard", "oocard");

        AdBrixRm.commercePaymentInfoAdded(paymentAttrs);

#endif
    }

    public void tutorialComplete()
    {
#if UNITY_IOS
        AdBrixRm.gameTutorialComplete(false);

#elif UNITY_ANDROID
        Dictionary<string, string> attrs = new Dictionary<string, string>();
        attrs.Add("username", "oocard");
        AdBrixRm.gameTutorialComplete(false, attrs);

#endif
    }

    public void characterCreated()
    {
#if UNITY_IOS
        AdBrixRm.gameCharacterCreated();

#elif UNITY_ANDROID
        AdBrixRm.gameCharacterCreated();

#endif
    }

    public void stageCleared()
    {
#if UNITY_IOS
        AdBrixRm.gameStageCleared("1-1");

#elif UNITY_ANDROID
        Dictionary<string, string> attrs = new Dictionary<string, string>();
        attrs.Add("username", "oocard");
        AdBrixRm.gameStageCleared("1-1", attrs);

#endif

    }

    public void levelAchieved()
    {
#if UNITY_IOS
        AdBrixRm.gameLevelAchieved(15);

#elif UNITY_ANDROID
        Dictionary<string, string> attrs = new Dictionary<string, string>();
        attrs.Add("username", "oocard");
        AdBrixRm.gameLevelAchieved(15, attrs);

#endif
    }

    public void deleteUserDataAndStopSDK()
    {
#if UNITY_IOS
        AdBrixRm.deleteUserDataAndStopSDK("user_1234", callbackObject, "deleteCallback");

#elif UNITY_ANDROID
        AdBrixRm.deleteUserDataAndStopSDK("user_1234", callbackObject, "deleteCallback");

#endif
    }

    public void restartSDK()
    {
#if UNITY_IOS
        AdBrixRm.restartSDK("user_1234", callbackObject, "restartCallback");
#elif UNITY_ANDROID
        AdBrixRm.restartSDK("user_1234", callbackObject, "restartCallback");

#endif
    }

    public void flushAllEvents()
    {
#if UNITY_IOS
        AdBrixRm.flushAllEvents(callbackObject, "flushAllEventsCallback");
#elif UNITY_ANDROID
        AdBrixRm.flushAllEvents(callbackObject, "flushAllEventsCallback");
#endif
    }

    public void getSdkVersion()
    {
#if UNITY_IOS
        string sdkVersion = AdBrixRm.getSdkVersion();
        Debug.Log("sdkversion : " + sdkVersion);

#endif
    }
}
