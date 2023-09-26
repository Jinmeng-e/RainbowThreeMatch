//
//  AdBrixRmBridge.mm
//
//  Created by freddy on 2018.
//  Copyright © 2018년 igaworks. All rights reserved.
//
//다른 cs  클래스에서 본 브릿지의 static 함수를 호출하여 사용하며,
//각 함수별로 AdBrix API를 사용하는 AdBrixRmBridge.mm 클래스 파일내의 함수들을 호출합니다.
//본 브릿지 함수들 구조는 연동 개발간 수정하지 않도록 유의해주십시오. 본 플러그인 수정에 따른 데이터 분석 불가 현상이 발생하는 경우, 이에 대한 귀책사유는 연동 개발자에게 귀속됩니다.

#import <Foundation/Foundation.h>
//#import "unityswift-Swift.h"
#import "AdBrixRmKit-Swift.h"
#import <AdSupport/AdSupport.h>
#import <UserNotifications/UserNotifications.h>
#import "AdBrixRmBridge.h"

UIViewController *UnityGetGLViewController();

static AdBrixRmBridge *_sharedInstance = nil; //To make IgaworksCorePlugin Singleton

@implementation AdBrixRmBridge

@synthesize abxDeeplinkDelegateObject = _abxDeeplinkDelegateObject;
@synthesize abxDeeplinkDelegateFunction = _abxDeeplinkDelegateFunction;
@synthesize abxDeferredDeeplinkDelegateObject = _abxDeferredDeeplinkDelegateObject;
@synthesize abxDeferredDeeplinkDelegateFunction = _abxDeferredDeeplinkDelegateFunction;
@synthesize abxPushLocalDelegateObject = _abxPushLocalDelegateObject;
@synthesize abxPushLocalDelegateFunction = _abxPushLocalDelegateFunction;
@synthesize abxPushRemoteDelegateObject = _abxPushRemoteDelegateObject;
@synthesize abxPushRemoteDelegateFunction = _abxPushRemoteDelegateFunction;
@synthesize abxLogDelegateObject = _abxLogDelegateObject;
@synthesize abxLogDelegateFunction = _abxLogDelegateFunction;
@synthesize abxInAppMessageClickDelegateObject = _abxInAppMessageClickDelegateObject;
@synthesize abxInAppMessageClickDelegateFunction = _abxInAppMessageClickDelegateFunction;
@synthesize abxInAppMessageAutoFetchDelegateObject = _abxInAppMessageAutoFetchDelegateObject;
@synthesize abxInAppMessageAutoFetchDelegateFunction = _abxInAppMessageAutoFetchDelegateFunction;

- (void)setAdBrixDeeplinkDelegate:(NSString *)object function:(NSString *)function
{
    _abxDeeplinkDelegateObject = object;
    _abxDeeplinkDelegateFunction = function;
    [[AdBrixRM sharedInstance] setDeeplinkDelegateWithDelegate:self];
}
- (void)setAdBrixDeferredDeeplinkDelegate:(NSString *)object function:(NSString *)function
{
    _abxDeferredDeeplinkDelegateObject = object;
    _abxDeferredDeeplinkDelegateFunction = function;
    [[AdBrixRM sharedInstance] setDeferredDeeplinkDelegateWithDelegate:self];
}
- (void)setAdBrixRmPushLocalDelegate:(NSString *)object function:(NSString *)function
{
    _abxPushLocalDelegateObject = object;
    _abxPushLocalDelegateFunction = function;
    [[AdBrixRM sharedInstance] setAdBrixRmPushLocalDelegateWithDelegate:self];
}

- (void)setAdBrixRmPushRemoteDelegate:(NSString *)object function:(NSString *)function
{
    _abxPushRemoteDelegateObject = object;
    _abxPushRemoteDelegateFunction = function;
    [[AdBrixRM sharedInstance] setAdBrixRmPushRemoteDelegateWithDelegate:self];
}
- (void)setLogDelegate:(NSString *)object function:(NSString *)function
{
    _abxLogDelegateObject = object;
    _abxLogDelegateFunction = function;
    [[AdBrixRM sharedInstance] setLogDelegateWithDelegate:self];
}
- (void)setInAppMessageClickDelegate:(NSString *)object function:(NSString *)function
{
    _abxInAppMessageClickDelegateObject = object;
    _abxInAppMessageClickDelegateFunction = function;
    [[AdBrixRM sharedInstance] setInAppMessageClickDelegateWithDelegate:self];
}
- (void)setInAppMessageAutoFetchDelegate:(NSString *)object function:(NSString *)function
{
    _abxInAppMessageAutoFetchDelegateObject = object;
    _abxInAppMessageAutoFetchDelegateFunction = function;
    [[AdBrixRM sharedInstance] setInAppMessageAutoFetchDelegateWithDelegate:self];
}

- (void)didReceiveDeeplinkWithDeeplink:(NSString *)deeplink
{
    if(deeplink != nil && _abxDeeplinkDelegateObject != nil && _abxDeeplinkDelegateFunction != nil) {
        UnitySendMessage([_abxDeeplinkDelegateObject UTF8String], [_abxDeeplinkDelegateFunction UTF8String], [deeplink UTF8String]);
    }
}

- (void)didReceiveDeferredDeeplinkWithDeeplink:(NSString *)deeplink
{
    
    if(deeplink != nil && _abxDeferredDeeplinkDelegateObject != nil && _abxDeferredDeeplinkDelegateFunction != nil) {
        UnitySendMessage([_abxDeferredDeeplinkDelegateObject UTF8String], [_abxDeferredDeeplinkDelegateFunction UTF8String], [deeplink UTF8String]);
    }
}

- (void)pushLocalCallbackWithData:(NSDictionary<NSString *,id> *)data state:(UIApplicationState)state
{
    if(data != nil && _abxPushLocalDelegateObject != nil && _abxPushLocalDelegateFunction != nil) {
        UnitySendMessage([_abxPushLocalDelegateObject UTF8String], [_abxPushLocalDelegateFunction UTF8String], [[NSString stringWithFormat:@"%@", data] UTF8String]);
    }
}

- (void)pushRemoteCallbackWithData:(NSDictionary<NSString *,id> *)data state:(UIApplicationState)state
{
    if(data != nil && _abxPushRemoteDelegateObject != nil && _abxPushRemoteDelegateFunction != nil) {
        UnitySendMessage([_abxPushRemoteDelegateObject UTF8String], [_abxPushRemoteDelegateFunction UTF8String], [[NSString stringWithFormat:@"%@", data] UTF8String]);
    }
}

- (void)didPrintLogWithLevel:(enum AdBrixLogLevel)level log:(NSString *)log
{
    if(log != nil && _abxLogDelegateObject != nil && _abxLogDelegateFunction != nil) {
        UnitySendMessage([_abxLogDelegateObject UTF8String], [_abxLogDelegateFunction UTF8String], [log UTF8String]);
    }
}

- (void)onReceiveInAppMessageClickWithActionId:(NSString *)actionId actionType:(NSString *)actionType actionArg:(NSString *)actionArg isClosed:(BOOL)isClosed
{
    NSDictionary *dictionary = [NSMutableDictionary new];
    [dictionary setValue:actionId forKey:@"actionId"];
    [dictionary setValue:actionType forKey:@"actionType"];
    [dictionary setValue:actionArg forKey:@"actionArg"];
    [dictionary setValue:[NSNumber numberWithBool: isClosed] forKey: @"isClosed"];
    
    if(_abxInAppMessageClickDelegateObject != nil && _abxInAppMessageClickDelegateFunction != nil) {
        UnitySendMessage([_abxInAppMessageClickDelegateObject UTF8String], [_abxInAppMessageClickDelegateFunction UTF8String], [[NSString stringWithFormat:@"%@", dictionary] UTF8String]);
    }
}

- (void)didFetchInAppMessageWithResult:(DfnInAppMessageFetchResult *)result
{
    
    NSDictionary *dictionary = [NSMutableDictionary new];
    [dictionary setValue:[NSNumber numberWithBool: result.isSucceeded] forKey: @"isSucceeded"];
    
    if(_abxInAppMessageAutoFetchDelegateObject != nil && _abxInAppMessageAutoFetchDelegateFunction != nil) {
        UnitySendMessage([_abxInAppMessageAutoFetchDelegateObject UTF8String], [_abxInAppMessageAutoFetchDelegateFunction UTF8String], [[NSString stringWithFormat:@"%@", dictionary] UTF8String]);
    }
    
}

+ (void)initialize
{
    if (self == [AdBrixRmBridge class])
    {
        _sharedInstance = [[self alloc] init];
    }
}


+ (AdBrixRmBridge *)sharedAdBrixRmBridge
{
    return _sharedInstance;
}

- (id)init
{
    self = [super init];
    
    if (self)
    {
        
    }
    return self;
}

+ (NSString *)checkNilToBlankString:(id)target
{
    NSString *returnString = @"";
    if (!([target isEqual:[NSNull null]] || target == nil))
    {
        returnString = target;
    }
    
    return returnString;
}

+ (double)checkDoubleNilToZero:(id)target
{
    double returnDouble = 0.0f;
    if (!([target isEqual:[NSNull null]] || target == nil))
    {
        returnDouble = (double)[target doubleValue];
    }
    
    return returnDouble;
}

+ (NSInteger)checkIntegerNilToZero:(id)target
{
    NSInteger returnInteger = 0;
    if (!([target isEqual:[NSNull null]] || target == nil))
    {
        returnInteger = [target integerValue];
    }
    
    return returnInteger;
}

+ (AdBrixRmCommerceProductModel *)makeProductFromJsonForCommerce:(NSString *)purchaseDataJsonString
{
    try {
        
        NSString *_productId = @"";
        NSString *_productName = @"";
        double _price = 0.0;
        double _discount = 0.0;
        NSUInteger _quantity = 1;
        NSString *_currency = @"";
        
        NSMutableDictionary *_extraAttrs;
        
        id dict=[NSJSONSerialization JSONObjectWithData:[purchaseDataJsonString dataUsingEncoding:NSUTF8StringEncoding] options:kNilOptions error:nil];
        AdBrixRmCommerceProductCategoryModel * _cate;
        for (id element in dict)
        {
            for(NSString* key in element)
            {
                
                
                if(![key isKindOfClass:[NSNull class]])
                {
                    if ([key isEqualToString:@"productId"])
                    {
                        _productId = [self checkNilToBlankString : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"productName"])
                    {
                        _productName = [self checkNilToBlankString : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"price"])
                    {
                        _price = [self checkDoubleNilToZero : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"discount"])
                    {
                        _discount = [self checkDoubleNilToZero : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"quantity"])
                    {
                        _quantity = [self checkIntegerNilToZero : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"currency"])
                    {
                        _currency = [self checkNilToBlankString : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"category"])
                    {
                        NSString *categories[5];
                        NSString *pCategories = [self checkNilToBlankString : [element objectForKey:key]];
                        if (pCategories) {
                            NSArray* categoryList = [pCategories componentsSeparatedByString:@"."];
                            for (int i=0; i<categoryList.count; ++i)
                            {
                                categories[i] = categoryList[i];
                            }
                        }
                        _cate = [[AdBrixRM sharedInstance] createCommerceProductCategoryDataWithCategory:categories[0] category2:categories[1] category3:categories[2] category4:categories[3] category5:categories[4]];
                    }
                    if ([key isEqualToString:@"extra_attrs"])
                    {
                        _extraAttrs = [element objectForKey:key];
                    }
                }
            }
        }
        
        return [[AdBrixRM sharedInstance] createCommerceProductDataWithAttrWithProductId:_productId
                                                                     productName:_productName
                                                                           price:_price
                                                                        quantity:_quantity
                                                                        discount:_discount
                                                                  currencyString:_currency
                                                                        category:_cate
                                                                 productAttrsMap:[[AdBrixRM sharedInstance] createAttrModelWithDictionary:_extraAttrs]];
    }
    catch (NSException *exception)
    {
        NSLog(@"fail to make product for iOS native : %@", exception);
    }
    return nil;
}

+ (NSArray<AdBrixRmCommerceProductModel *> *)makeProductsFromJsonForCommerce:(NSString *)purchaseDataJsonString
{
    try {
        
        NSString *_productId = @"";
        NSString *_productName = @"";
        double _price = 0.0;
        double _discount = 0.0;
        NSUInteger _quantity = 1;
        NSString *_currency = @"";
        
        NSMutableDictionary *_extraAttrs;
        NSMutableArray<AdBrixRmCommerceProductModel *> *productArray = [NSMutableArray array];
        AdBrixRmCommerceProductCategoryModel * _cate;
        
        id dict=[NSJSONSerialization JSONObjectWithData:[purchaseDataJsonString dataUsingEncoding:NSUTF8StringEncoding] options:kNilOptions error:nil];
        
        for (id element in dict)
        {
            for(NSString* key in element)
            {
                
                if(![key isKindOfClass:[NSNull class]])
                {
                    if ([key isEqualToString:@"productId"])
                    {
                        _productId = [self checkNilToBlankString : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"productName"])
                    {
                        _productName = [self checkNilToBlankString : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"price"])
                    {
                        _price = [self checkDoubleNilToZero : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"discount"])
                    {
                        _discount = [self checkDoubleNilToZero : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"quantity"])
                    {
                        _quantity = [self checkIntegerNilToZero : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"currency"])
                    {
                        _currency = [self checkNilToBlankString : [element objectForKey:key]];
                    }
                    if ([key isEqualToString:@"category"])
                    {
                        NSString *categories[5];
                        NSString *pCategories = [self checkNilToBlankString : [element objectForKey:key]];
                        if (pCategories) {
                            NSArray* categoryList = [pCategories componentsSeparatedByString:@"."];
                            for (int i=0; i<categoryList.count; ++i)
                            {
                                categories[i] = categoryList[i];
                            }
                        }
                        _cate = [[AdBrixRM sharedInstance] createCommerceProductCategoryDataWithCategory:categories[0] category2:categories[1] category3:categories[2] category4:categories[3] category5:categories[4]];
                    }
                    if ([key isEqualToString:@"extra_attrs"])
                    {
                        _extraAttrs = [element objectForKey:key];
                    }
                }
            }
            
            AdBrixRmCommerceProductModel *productModel = [[AdBrixRM sharedInstance]
                                                          createCommerceProductDataWithAttrWithProductId:_productId
                                                          productName:_productName
                                                          price:_price
                                                          quantity:_quantity
                                                          discount:_discount
                                                          currencyString:_currency
                                                          category:_cate
                                                          productAttrsMap:[[AdBrixRM sharedInstance] createAttrModelWithDictionary:_extraAttrs]
            ];

            [productArray addObject: productModel];
        }
        return productArray;
    }
    catch (NSException *exception)
    {
        NSLog(@"fail to make product for iOS native : %@", exception);
    }
    return nil;
}

+(AdBrixRmCommerceProductCategoryModel *)makeCategoryFromStringForCommerce: (NSString *)categoryString
{
    NSString *categories[5];
    if (categoryString) {
        NSArray* categoryList = [categoryString componentsSeparatedByString:@"."];
        for (int i=0; i<categoryList.count; ++i)
        {
            categories[i] = categoryList[i];
        }
    }
    
    return [[AdBrixRM sharedInstance] createCommerceProductCategoryDataWithCategory:categories[0] category2:categories[1] category3:categories[2] category4:categories[3] category5:categories[4]];
}

+ (NSMutableDictionary* )makeExtraAttrDictionaryFromJson:(NSString *)jsonString
{
    try {
        
        NSMutableDictionary *_extraAttrs = [NSMutableDictionary dictionary];
        
        id dict = [NSJSONSerialization JSONObjectWithData:[jsonString dataUsingEncoding:NSUTF8StringEncoding] options:kNilOptions error:nil];
        
        for(NSString* key in dict)
        {
            if(![key isKindOfClass:[NSNull class]])
            {
                [_extraAttrs setValue:[dict objectForKey:key] forKey:key];
            }
        }
        
        return _extraAttrs;
    }
    catch (NSException *exception)
    {
        NSLog(@"fail to make product for iOS native : %@", exception);
    }
    return nil;
}

extern "C" {
    void _logToiOS(const char* debugMessage) {
         NSLog(@"abxrm [unity] :: %@", [NSString stringWithUTF8String:debugMessage]);
    }
    
    void _SetAdBrixDeeplinkDelegate(const char* object, const char* function)
    {
        [[AdBrixRmBridge sharedAdBrixRmBridge] setAdBrixDeeplinkDelegate:[NSString stringWithUTF8String:object] function:[NSString stringWithUTF8String:function]];
    }
    
    void _SetAdBrixDeferredDeeplinkDelegate(const char* object, const char* function)
    {
        [[AdBrixRmBridge sharedAdBrixRmBridge] setAdBrixDeferredDeeplinkDelegate:[NSString stringWithUTF8String:object] function:[NSString stringWithUTF8String:function]];
    }
    
    void _SetAdBrixRmPushLocalDelegate(const char* object, const char* function)
    {
        [[AdBrixRmBridge sharedAdBrixRmBridge] setAdBrixRmPushLocalDelegate:[NSString stringWithUTF8String:object] function:[NSString stringWithUTF8String:function]];
    }
    
    void _SetAdBrixRmPushRemoteDelegate(const char* object, const char* function)
    {
        [[AdBrixRmBridge sharedAdBrixRmBridge] setAdBrixRmPushRemoteDelegate:[NSString stringWithUTF8String:object] function:[NSString stringWithUTF8String:function]];
    }
    
    void _SetLogDelegate(const char* object, const char* function)
    {
        [[AdBrixRmBridge sharedAdBrixRmBridge] setLogDelegate:[NSString stringWithUTF8String:object] function:[NSString stringWithUTF8String:function]];
    }
    
    void _SetInAppMessageClickDelegate(const char* object, const char* function)
    {
        [[AdBrixRmBridge sharedAdBrixRmBridge] setInAppMessageClickDelegate:[NSString stringWithUTF8String:object] function:[NSString stringWithUTF8String:function]];
    }
    
    void _SetInAppMessageAutoFetchDelegate(const char* object, const char* function)
    {
        [[AdBrixRmBridge sharedAdBrixRmBridge] setInAppMessageAutoFetchDelegate:[NSString stringWithUTF8String:object] function:[NSString stringWithUTF8String:function]];
    }
    
    void _initAdBrix(const char* appKey, const char* secretKey) {
         NSLog(@"AdBrixRM: _initAdBrix");
        [[AdBrixRM sharedInstance] initAdBrixWithAppKey:[NSString stringWithUTF8String:appKey] secretKey:[NSString stringWithUTF8String:secretKey]];
    }
    
    void _setPushEnable(BOOL toEnable) {
        NSLog(@"AdBrixRM: _setPushEnable");
        [[AdBrixRM sharedInstance] setPushEnableToPushEnable:toEnable];
    }
    
    void _setRegistrationId(const char* deviceToken) {
        NSLog(@"AdBrixRM: _setRegistrationId");
        
        NSString *pToken =  [NSString stringWithUTF8String:deviceToken];
        pToken = [pToken stringByReplacingOccurrencesOfString: @ "-" withString: @ ""];
        pToken = [pToken lowercaseString];
        
        
        [[AdBrixRM sharedInstance] setRegistrationIdForUnityWithDeviceToken:pToken];
    }
    
    void _gdprForgetMe() {
         NSLog(@"AdBrixRM: _gdprForgetMe");
        [[AdBrixRM sharedInstance] gdprForgetMe];
    }
    
    void _setEventUploadCountInterval(int countInterval) {
        [[AdBrixRM sharedInstance] setEventUploadCountInterval:[[AdBrixRM sharedInstance] convertCountInterval:countInterval]];
        
    }
    
    void _setEventUploadTimeInterval(int timeInterval) {
        [[AdBrixRM sharedInstance] setEventUploadTimeInterval:[[AdBrixRM sharedInstance] convertTimeInterval:timeInterval]];
    }
    
    void _deepLinkOpenWithUrl(const char* url) {
        NSLog(@"AdBrixRM: _deepLinkOpenWithUrl :: %@", url);
        [[AdBrixRM sharedInstance] deepLinkOpenWithUrl:[NSURL URLWithString: [NSString stringWithUTF8String:url]]];
    }
    
    void _setLocationWithLatitude(double latitude, double longitude) {
        [[AdBrixRM sharedInstance] setLocationWithLatitude:latitude longitude:longitude];
    }
    
    void _setAgeWithInt(int age) {
        [[AdBrixRM sharedInstance] setAgeWithInt:age];
    }
    
    void _setGenderWithAdBrixGenderType(int adBrixGenderType) {
        [[AdBrixRM sharedInstance] setGenderWithAdBrixGenderType:[[AdBrixRM sharedInstance] convertGender:adBrixGenderType]];
    }
    
    void _setUserPropertiesWithDictionary(const char* key[], const char* value[], int count) {
        
        if (key != nil && value != nil && count > 0) {
            NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
            for(int i=0; i < count; i++) {
                [dictionary setObject:[NSString stringWithUTF8String:value[i]] forKey:[NSString stringWithUTF8String:key[i]]];
            }
            
            AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:dictionary];
            [[AdBrixRM sharedInstance] setUserPropertiesWithAttrWithAttrModel:attrModel];
        }
    }
    
    void _setCiPropertiesWithDictionary(const char* key[], const char* value[], int count) {
        
        if (key != nil && value != nil && count > 0) {
            NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
            for(int i=0; i < count; i++) {
                [dictionary setObject:[NSString stringWithUTF8String:value[i]] forKey:[NSString stringWithUTF8String:key[i]]];
            }
            
            AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:dictionary];
            [[AdBrixRM sharedInstance] setUserCiWithAttrWithAttrModel:attrModel];
        }
    }
    
    void _setKakaoId(const char* kakaoId) {
        if( kakaoId == nil ) {
            NSLog(@"_setKakaoId :: kakao id is nil");
        }
        
        [[AdBrixRM sharedInstance] setKakaoIdWithKakaoId:[NSString stringWithUTF8String:kakaoId]];
    }
    
    void _clearUserProperties() {
        [[AdBrixRM sharedInstance] clearUserProperties];
    }
    
    void _logout(){
        [[AdBrixRM sharedInstance] logout];
    }
    void _loginWithUserId(const char* userId) {
        [[AdBrixRM sharedInstance] loginWithUserId:[NSString stringWithUTF8String:userId]];
    }
    
    void _eventWithName(const char* eventName) {
        [[AdBrixRM sharedInstance] eventWithEventName:[NSString stringWithUTF8String:eventName]];
    }

    void _eventWithEventNameAndValue(const char* eventName, const char* key[], const char* value[], int count) {
        
        
        if (key != nil && value != nil && count > 0) {
            NSMutableDictionary *dictionary = [NSMutableDictionary dictionary];
            for(int i=0; i < count; i++) {
                [dictionary setObject:[NSString stringWithUTF8String:value[i]] forKey:[NSString stringWithUTF8String:key[i]]];
            }
            AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:dictionary];
            [[AdBrixRM sharedInstance] eventWithAttrWithEventName:[NSString stringWithUTF8String:eventName] value:attrModel];
        }
    }
    
    // MARK: - Commerce Event
    
    void _commerceViewHome() {
        [[AdBrixRM sharedInstance] commerceViewHome];
    }
    
    
    void _commerceViewHomeWithExtraAttr(const char* jsonCommerceExtraAttrString) {
        printf("=== commerceExtraAttr json:: %s", jsonCommerceExtraAttrString);
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        [[AdBrixRM sharedInstance] commerceViewHomeWithAttrWithOrderAttr:attrModel];
    }
    
    void _commerceCategoryViewWithCategoryAndProduct(const char* categoryString, const char* jsonDataString) {
        
        AdBrixRmCommerceProductCategoryModel *cate = [AdBrixRmBridge makeCategoryFromStringForCommerce:[NSString stringWithUTF8String:categoryString]];
        
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        
        
        [[AdBrixRM sharedInstance] commerceCategoryViewWithCategory:cate productInfo:productArray];
    }
    
    void _commerceCategoryViewBulkWithCategoryAndProduct(const char* categoryString, const char* jsonDataString) {
        
        AdBrixRmCommerceProductCategoryModel *cate = [AdBrixRmBridge makeCategoryFromStringForCommerce:[NSString stringWithUTF8String:categoryString]];
        
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        
        
        [[AdBrixRM sharedInstance] commerceCategoryViewWithCategory:cate productInfo:productArray];
    }
    
    void _commerceCategoryViewWithCategoryAndProductAndExtraAttr(const char* categoryString, const char* jsonDataString, const char* jsonCommerceExtraAttrString) {
        AdBrixRmCommerceProductCategoryModel *cate = [AdBrixRmBridge makeCategoryFromStringForCommerce:[NSString stringWithUTF8String:categoryString]];
        
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        [[AdBrixRM sharedInstance] commerceCategoryViewWithAttrWithCategory:cate productInfo:productArray orderAttr:attrModel];
    }
    
    void _commerceCategoryViewBulkWithCategoryAndProductAndExtraAttr(const char* categoryString, const char* jsonDataString, const char* jsonCommerceExtraAttrString) {
        AdBrixRmCommerceProductCategoryModel *cate = [AdBrixRmBridge makeCategoryFromStringForCommerce:[NSString stringWithUTF8String:categoryString]];
        
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        [[AdBrixRM sharedInstance] commerceCategoryViewWithAttrWithCategory:cate productInfo:productArray orderAttr:attrModel];
    }
    
    void _commerceProductViewWithProduct(const char* jsonDataString) {
        [[AdBrixRM sharedInstance] commerceProductViewWithProductInfo:[AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]]];
    }
    
    void _commerceProductViewWithProductAndExtraAttr(const char* jsonDataString, const char* jsonCommerceExtraAttrString) {
        
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        
        [[AdBrixRM sharedInstance] commerceProductViewWithAttrWithProductInfo:productModel orderAttr:attrModel];
    }
    
    void _commerceAddToCartWithProduct(const char* jsonDataString) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        
        [[AdBrixRM sharedInstance] commerceAddToCartWithProductInfo:productArray];
    }
    
    void _commerceAddToCartBulkWithProduct(const char* jsonDataString) {
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        [[AdBrixRM sharedInstance] commerceAddToCartWithProductInfo:productArray];
    }
    
    void _commerceAddToCartWithProductAndExtraAttr(const char* jsonDataString, const char* jsonCommerceExtraAttrString) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        [[AdBrixRM sharedInstance] commerceAddToCartWithAttrWithProductInfo:productArray orderAttr:attrModel];
    }
    
    void _commerceAddToCartBulkWithProductAndExtraAttr(const char* jsonDataString, const char* jsonCommerceExtraAttrString) {
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        [[AdBrixRM sharedInstance] commerceAddToCartWithAttrWithProductInfo:productArray orderAttr:attrModel];
    }
    
    void _commerceAddToWishListWithProduct(const char* jsonDataString) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        [[AdBrixRM sharedInstance] commerceAddToWishListWithProductInfo:productModel];
    }
    
    void _commerceAddToWishListWithProductAndExtraAttr(const char* jsonDataString, const char* jsonCommerceExtraAttrString) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        [[AdBrixRM sharedInstance] commerceAddToWishListWithAttrWithProductInfo:productModel orderAttr:attrModel];
    }
    
    void _commerceReviewOrderWithOrderId(const char* orderId, const char* jsonDataString, double discount, double deliveryCharge) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        [[AdBrixRM sharedInstance] commerceReviewOrderWithOrderId:[NSString stringWithUTF8String:orderId] productInfo:productArray discount:discount deliveryCharge:deliveryCharge];
    }
    
    void _commerceReviewOrderBulkWithOrderId(const char* orderId, const char* jsonDataString, double discount, double deliveryCharge) {
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        [[AdBrixRM sharedInstance] commerceReviewOrderWithOrderId:[NSString stringWithUTF8String:orderId] productInfo:productArray discount:discount deliveryCharge:deliveryCharge];
    }
    
    void _commerceReviewOrderWithOrderIdAndExtraAttr(const char* orderId, const char* jsonDataString, double discount, double deliveryCharge, const char* jsonCommerceExtraAttrString) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        
        [[AdBrixRM sharedInstance] commerceReviewOrderWithAttrWithOrderId: [NSString stringWithUTF8String:orderId] productInfo:productArray discount:discount deliveryCharge:deliveryCharge orderAttr:attrModel];
    }
    
    void _commerceReviewOrderBulkWithOrderIdAndExtraAttr(const char* orderId, const char* jsonDataString, double discount, double deliveryCharge, const char* jsonCommerceExtraAttrString) {
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        
        [[AdBrixRM sharedInstance] commerceReviewOrderWithAttrWithOrderId:[NSString stringWithUTF8String:orderId] productInfo:productArray discount:discount deliveryCharge:deliveryCharge orderAttr:attrModel];
    }
    
    void _commerceRefundWithOrderId(const char* orderId, const char* jsonDataString, double penaltyCharge) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        [[AdBrixRM sharedInstance] commerceRefundWithOrderId:[NSString stringWithUTF8String:orderId] productInfo:productArray penaltyCharge:penaltyCharge];
    }
    
    void _commerceRefundBulkWithOrderId(const char* orderId, const char* jsonDataString, double penaltyCharge) {
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        [[AdBrixRM sharedInstance] commerceRefundWithOrderId:[NSString stringWithUTF8String:orderId] productInfo:productArray penaltyCharge:penaltyCharge];
        
    }
    
    void _commerceRefundWithOrderIdAndExtraAttr(const char* orderId, const char* jsonDataString, double penaltyCharge, const char* jsonCommerceExtraAttrString) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        
        [[AdBrixRM sharedInstance] commerceRefundWithAttrWithOrderId:[NSString stringWithUTF8String:orderId] productInfo:productArray penaltyCharge:penaltyCharge orderAttr:attrModel];
    }
    
    void _commerceRefundBulkWithOrderIdAndExtraAttr(const char* orderId, const char* jsonDataString, double penaltyCharge, const char* jsonCommerceExtraAttrString) {
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        
        [[AdBrixRM sharedInstance] commerceRefundWithAttrWithOrderId:[NSString stringWithUTF8String:orderId] productInfo:productArray penaltyCharge:penaltyCharge orderAttr:attrModel];
    }
    
    void _commerceSearchWithProduct(const char* jsonDataString, const char* keyword) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        [[AdBrixRM sharedInstance] commerceSearchWithProductInfo:productArray keyword:[NSString stringWithUTF8String:keyword]];
    }
    
    void _commerceSearchBulkWithProduct(const char* jsonDataString, const char* keyword) {
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        [[AdBrixRM sharedInstance] commerceSearchWithProductInfo:productArray keyword:[NSString stringWithUTF8String:keyword]];
    }
    
    void _commerceSearchWithProductAndExtraAttr(const char* jsonDataString, const char* keyword, const char* jsonCommerceExtraAttrString) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        
        [[AdBrixRM sharedInstance] commerceSearchWithAttrWithProductInfo:productArray keyword:[NSString stringWithUTF8String:keyword] orderAttr: attrModel];
    }
    
    void _commerceSearchBulkWithProductAndExtraAttr(const char* jsonDataString, const char* keyword, const char* jsonCommerceExtraAttrString) {
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        
        [[AdBrixRM sharedInstance] commerceSearchWithAttrWithProductInfo:productArray keyword:[NSString stringWithUTF8String:keyword] orderAttr:attrModel];
    }
    
    void _commerceShareWithChannel(int channel, const char* jsonDataString) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        
        [[AdBrixRM sharedInstance] commerceShareWithChannel:[[AdBrixRM sharedInstance] convertChannel:channel] productInfo:productModel];
    }
    
    void _commerceShareWithChannelAndExtraAttr(int channel, const char* jsonDataString, const char* jsonCommerceExtraAttrString) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        AdBrixRmSharingChannel sharingChannel = (AdBrixRmSharingChannel) channel;
        
        [[AdBrixRM sharedInstance] commerceShareWithAttrWithChannel:sharingChannel productInfo:productModel orderAttr:attrModel];
    }
    
    void _commerceListViewWithProduct(const char* jsonDataString) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        
        [[AdBrixRM sharedInstance] commerceListViewWithProductInfo:productArray];
    }
    
    void _commerceListViewBulkWithProduct(const char* jsonDataString) {
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        
        [[AdBrixRM sharedInstance] commerceListViewWithProductInfo:productArray];
    }
    
    void _commerceListViewProductAndOrderAttr(const char* jsonDataString, const char* jsonOrderAttrString) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        NSMutableDictionary *orderAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonOrderAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary: orderAttr];
        
        [[AdBrixRM sharedInstance] commerceListViewWithAttrWithProductInfo:productArray orderAttr:attrModel];
    }
    
    
    void _commerceListViewBulkWithProductAndOrderAttr(const char* jsonDataString, const char* jsonOrderAttrString) {
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSMutableDictionary *orderAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonOrderAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:orderAttr];
        
        [[AdBrixRM sharedInstance] commerceListViewWithAttrWithProductInfo:productArray orderAttr:attrModel];
    }
    
    void _commerceCartViewWithProduct(const char* jsonDataString) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        
        [[AdBrixRM sharedInstance] commerceCartViewWithProductInfo:productArray];
    }
    
    void _commerceCartViewBulkWithProduct(const char* jsonDataString) {
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        
        [[AdBrixRM sharedInstance] commerceCartViewWithProductInfo:productArray];
    }
    
    void _commerceCartViewProductAndOrderAttr(const char* jsonDataString, const char* jsonOrderAttrString) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        NSMutableDictionary *orderAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonOrderAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:orderAttr];
        
        [[AdBrixRM sharedInstance] commerceCartViewWithAttrWithProductInfo:productArray orderAttr:attrModel];
    }
    
    
    void _commerceCartViewBulkWithProductAndOrderAttr(const char* jsonDataString, const char* jsonOrderAttrString) {
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSMutableDictionary *orderAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonOrderAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:orderAttr];
        
        [[AdBrixRM sharedInstance] commerceCartViewWithAttrWithProductInfo:productArray orderAttr:attrModel];
    }
    
    void _commercePaymentInfoAddedWithExtraAttr(const char* jsonCommerceExtraAttrString) {
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        
        [[AdBrixRM sharedInstance] commercePaymentInfoAddedWithAttrWithPaymentInfoAttr:attrModel];
    }
    
    void _gameLevelAchievedWithLevel(int level) {
        [[AdBrixRM sharedInstance] gameLevelAchievedWithLevel:level];
    }
    
    void _gameLevelAchievedWithLevelWithGameAttr(int level, const char* jsonGameAttrString) {
        NSMutableDictionary *gameAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonGameAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:gameAttr];
        
        [[AdBrixRM sharedInstance] gameLevelAchievedWithAttrWithLevel:level gameInfoAttr:attrModel];
    }
    
    
    void _gameTutorialCompletedWithIsSkip(BOOL isSkip) {
        [[AdBrixRM sharedInstance] gameTutorialCompletedWithIsSkip:isSkip];
    }
    
    void _gameTutorialCompletedWithIsSkipAndGameAttr(BOOL isSkip, const char* jsonGameAttrString) {
        NSMutableDictionary *gameAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonGameAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:gameAttr];

        [[AdBrixRM sharedInstance] gameTutorialCompletedWithAttrWithIsSkip:isSkip gameInfoAttr:attrModel];
    }
    
    
    void _gameCharacterCreated() {
        [[AdBrixRM sharedInstance] gameCharacterCreated];
    }
    
    void _gameCharacterCreatedWithGameAttr(const char* jsonGameAttrString) {
        NSMutableDictionary *gameAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonGameAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:gameAttr];

        [[AdBrixRM sharedInstance] gameCharacterCreatedWithAttrWithGameInfoAttr:attrModel];
    }
    
    void _gameStageClearedWithStageName(const char* stageName) {
        [[AdBrixRM sharedInstance] gameStageClearedWithStageName:[NSString stringWithUTF8String:stageName]];
    }
    
    void _gameStageClearedWithStageNameAndGameAttr(const char* stageName, const char* jsonGameAttrString) {
        NSMutableDictionary *gameAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonGameAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:gameAttr];

        [[AdBrixRM sharedInstance] gameStageClearedWithAttrWithStageName:[NSString stringWithUTF8String:stageName] gameInfoAttr:attrModel];
        
    }
    
    void _commonPurchaseWithOrderId(const char* orderId, const char* jsonDataString, double discount, double deliveryCharge, int paymentMethod) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        AdbrixRmPaymentMethod paymentMethodEnum = (AdbrixRmPaymentMethod) paymentMethod;
        
        [[AdBrixRM sharedInstance] commonPurchaseWithOrderId:[NSString stringWithUTF8String:orderId] productInfo:productArray orderSales:0.0 discount:discount deliveryCharge:deliveryCharge paymentMethod:paymentMethodEnum];
    }
    
    void _commonPurchaseBulkWithOrderId(const char* orderId, const char* jsonDataString, double discount, double deliveryCharge,int paymentMethod) {
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        AdbrixRmPaymentMethod paymentMethodEnum = (AdbrixRmPaymentMethod) paymentMethod;
        
        [[AdBrixRM sharedInstance] commonPurchaseWithOrderId:[NSString stringWithUTF8String:orderId] productInfo:productArray orderSales:0.0 discount:discount deliveryCharge:deliveryCharge paymentMethod:paymentMethodEnum];
    }
    
    void _commonPurchaseWithOrderIdAndExtraAttr(const char* orderId, const char* jsonDataString, double discount, double deliveryCharge, int paymentMethod, const char* jsonCommerceExtraAttrString) {
        AdBrixRmCommerceProductModel *productModel = [AdBrixRmBridge makeProductFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [[NSArray alloc] initWithObjects:productModel, nil];
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        AdbrixRmPaymentMethod paymentMethodEnum = (AdbrixRmPaymentMethod) paymentMethod;
        
        [[AdBrixRM sharedInstance] commonPurchaseWithAttrWithOrderId:[NSString stringWithUTF8String:orderId] productInfo:productArray orderSales:0.0 discount:discount deliveryCharge:deliveryCharge paymentMethod:paymentMethodEnum orderAttr:attrModel];
    }
    
    void _commonPurchaseBulkWithOrderIdAndExtraAttr(const char* orderId, const char* jsonDataString, double discount, double deliveryCharge, int paymentMethod, const char* jsonCommerceExtraAttrString) {
        NSArray<AdBrixRmCommerceProductModel *> *productArray = [AdBrixRmBridge makeProductsFromJsonForCommerce:[NSString stringWithUTF8String:jsonDataString]];
        NSMutableDictionary *commerceExtraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:jsonCommerceExtraAttrString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:commerceExtraAttr];
        AdbrixRmPaymentMethod paymentMethodEnum = (AdbrixRmPaymentMethod) paymentMethod;
        
        [[AdBrixRM sharedInstance] commonPurchaseWithAttrWithOrderId:[NSString stringWithUTF8String:orderId] productInfo:productArray orderSales:0.0 discount:discount deliveryCharge:deliveryCharge paymentMethod:paymentMethodEnum orderAttr:attrModel];
    }
    
    //sign_up
    void _commonSignUp(int channel) {
        [[AdBrixRM sharedInstance] commonSignUpWithChannel:[[AdBrixRM sharedInstance] convertSignUpChannel:channel]];
    }
    
    void _commonSignUpWithAttr(int channel, const char* extraAttrJsonString) {
        NSMutableDictionary *extraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:extraAttrJsonString]];
        
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:extraAttr];
        
        AdBrixRmSignUpChannel channelEnum = (AdBrixRmSignUpChannel) channel;
        [[AdBrixRM sharedInstance] commonSignUpWithAttrWithChannel:channelEnum commonAttr:attrModel];
    }
    
    //use_credit
    void _commonUseCredit() {
        [[AdBrixRM sharedInstance] commonUseCredit];
    }
    
    void _commonUseCreditWithAttr(const char* extraAttrJsonString) {
        NSMutableDictionary *extraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:extraAttrJsonString]];
        
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:extraAttr];
        [[AdBrixRM sharedInstance] commonUseCreditWithAttrWithCommonAttr:attrModel];
    }
    
    //app_update
    void _commonAppUpdate(const char* prev_ver, const char* curr_ver) {
        [[AdBrixRM sharedInstance] commonAppUpdateWithPrev_ver:[NSString stringWithUTF8String:prev_ver] curr_ver:[NSString stringWithUTF8String:curr_ver]];
    }
    
    void _commonAppUpdateWithAttr(const char* prev_ver, const char* curr_ver, const char* extraAttrJsonString) {
        NSMutableDictionary *extraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:extraAttrJsonString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:extraAttr];

        [[AdBrixRM sharedInstance] commonAppUpdateWithAttrWithPrev_ver:[NSString stringWithUTF8String:prev_ver] curr_ver:[NSString stringWithUTF8String:curr_ver] commonAttr:attrModel];
    }
    
    //invite
    void _commonInvite(int channel) {
        [[AdBrixRM sharedInstance] commonInviteWithChannel:[[AdBrixRM sharedInstance] convertInviteChannel:channel]];
    }
    
    void _commonInviteWithAttr(int channel, const char* extraAttrJsonString) {
        NSMutableDictionary *extraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:extraAttrJsonString]];
        AdBrixRmAttrModel *attrModel = [[AdBrixRM sharedInstance] createAttrModelWithDictionary:extraAttr];
        AdBrixRmInviteChannel inviteChannelEnum = (AdBrixRmInviteChannel) channel;
        
        [[AdBrixRM sharedInstance] commonInviteWithAttrWithChannel:inviteChannelEnum commonAttr:attrModel];
    }
    
    //push notification
    void _registerLocalPushNotification(int delaySecond, const char* title, const char* subTitle, const char* body, const char* categoryId, const char* threadId, int badgeNumber, const char* imageUrlString, const char* attrDicJsonString, const char* object, const char* function) {
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        NSCalendar *calendar = [NSCalendar currentCalendar];
        NSDate *alarmTime = [calendar dateByAddingUnit: NSCalendarUnitSecond value:delaySecond toDate:[NSDate date] options:NSCalendarMatchNextTime];
        
        NSMutableDictionary *extraAttr = [AdBrixRmBridge makeExtraAttrDictionaryFromJson:[NSString stringWithUTF8String:attrDicJsonString]];
        
        NSURL *imageUrl;
        if (imageUrlString != nil) {
            imageUrl = [NSURL URLWithString:[NSString stringWithUTF8String:imageUrlString]];
        }
        
        [[AdBrixRM sharedInstance]
         registerLocalPushNotificationWithDate:alarmTime
         title:[NSString stringWithUTF8String:title]
         subtitle:[NSString stringWithUTF8String:subTitle]
         body:[NSString stringWithUTF8String:body]
         sound:nil
         categoryId:[NSString stringWithUTF8String:categoryId]
         threadId:[NSString stringWithUTF8String:threadId]
         badgeNumber:[NSNumber numberWithInt:badgeNumber]
         image:imageUrl
         attrDic:extraAttr
         completionHandler:^(BOOL response, NSError *_Nullable error, NSString *pushId) {
            UnitySendMessage([_object UTF8String], [_function UTF8String], [pushId UTF8String]);
         }
        ];
    }
    
    void _getRegisteredLocalPushNotification(const char* object, const char* function) {
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        [[AdBrixRM sharedInstance]
         getRegisteredLocalPushNotificationWithCompleteHandler:^(NSArray *idArr){
            NSData* jsonData = [NSJSONSerialization dataWithJSONObject:idArr options:NSJSONWritingPrettyPrinted error:nil];
            NSString* jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            UnitySendMessage([_object UTF8String], [_function UTF8String], [jsonString UTF8String]);
        }];
    }
    
    void _cancelLocalPushNotification(const char* pushId) {
        NSMutableArray<NSString *> *array = [NSMutableArray new];
        [array addObject:[NSString stringWithUTF8String:pushId]];
        
        [[AdBrixRM sharedInstance] cancelLocalPushNotificationWithIds:array];
    }
    
    void _cancelLocalPushNotificationAll() {
        [[AdBrixRM sharedInstance] cancelLocalPushNotificationAll];
    }
    
    //Delete Restart
    void _deleteUserDataAndStopSDK(const char* userId, const char* object, const char* function) {
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        [[AdBrixRM sharedInstance] deleteUserDataAndStopSDK:[NSString stringWithUTF8String:userId] :^(Completion completion){
            if(completion == CompletionSuccess) {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "success");
            } else {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "fail");
            }
        }];
    }
    
    void _restartSDK(const char* userId, const char* object, const char* function) {
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        [[AdBrixRM sharedInstance] restartSDK:[NSString stringWithUTF8String:userId] :^(Completion completion){
            if(completion == CompletionSuccess) {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "success");
            } else {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "fail");
            }
        }];
    }
    
    //flushAllEvents
    void _flushAllEvents(const char* object, const char* function) {
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        [[AdBrixRM sharedInstance] flushAllEventsWithCompletion:^(DfnResult* dfnResult){
            if(dfnResult.isSuccess) {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "success");
            } else {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "fail");
            }
        }];
    }
    
    //flushAllEvents
    const char* _getSdkVersion() {
        NSString* sdkVersion = [[AdBrixRM sharedInstance] getSDKVersion];
        
        char* res = (char*)malloc(sdkVersion.length+1);
        strcpy(res, [sdkVersion UTF8String]);
        
        return res;
    }
    
    //Action History
    void _fetchActionHistoryByUserId(const char* token, const char* actionType[], int actionTypeCount, const char* object, const char* function) {
        NSMutableArray *actionTypeArray = [NSMutableArray new];
        
        if (actionType != nil && actionTypeCount > 0) {
            for(int i=0; i < actionTypeCount; i++) {
                [actionTypeArray addObject:[NSString stringWithUTF8String:actionType[i]]];
            }
        }
        
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        [[AdBrixRM sharedInstance] fetchActionHistoryByAdidWithToken:[NSString stringWithUTF8String:token] actionType:actionTypeArray completion:^(ActionHistoryResult* result){
            if(result.isSucceeded) {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "success");
            } else {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "fail");
            }
        }];
    }
    
    void _fetchActionHistoryByAdid(const char* token, const char* actionType[], int actionTypeCount, const char* object, const char* function) {
        NSMutableArray *actionTypeArray = [NSMutableArray new];
        
        if (actionType != nil && actionTypeCount > 0) {
            for(int i=0; i < actionTypeCount; i++) {
                [actionTypeArray addObject:[NSString stringWithUTF8String:actionType[i]]];
            }
        }
        
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        [[AdBrixRM sharedInstance] fetchActionHistoryByAdidWithToken:[NSString stringWithUTF8String:token] actionType:actionTypeArray completion:^(ActionHistoryResult* result){
            if(result.isSucceeded) {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "success");
            } else {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "fail");
            }
        }];
    }
    
    void _getActionHistory(int skip, int limit, const char* actionType[], int actionTypeCount, const char* object, const char* function) {
        NSMutableArray *actionTypeArray = [NSMutableArray new];
        
        if (actionType != nil && actionTypeCount > 0) {
            for(int i=0; i < actionTypeCount; i++) {
                [actionTypeArray addObject:[NSString stringWithUTF8String:actionType[i]]];
            }
        }
        
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        [[AdBrixRM sharedInstance] getActionHistoryWithSkip:skip limit:limit actionType:actionTypeArray  completion:^(ActionHistoryResult* result){
            if(result.isSucceeded) {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "success");
            } else {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "fail");
            }
        }];
    }
    
    void _getAllActionHistory(const char* actionType[], int actionTypeCount, const char* object, const char* function) {
        NSMutableArray *actionTypeArray = [NSMutableArray new];
        
        if (actionType != nil && actionTypeCount > 0) {
            for(int i=0; i < actionTypeCount; i++) {
                [actionTypeArray addObject:[NSString stringWithUTF8String:actionType[i]]];
            }
        }
        
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        [[AdBrixRM sharedInstance] getAllActionHistoryWithActionType:actionTypeArray  completion:^(ActionHistoryResult* result){
            if(result.isSucceeded) {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "success");
            } else {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "fail");
            }
        }];
    }
    
    void _deleteActionHistory(const char* token, const char* historyId, long timestamp, const char* object, const char* function) {
        
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        [[AdBrixRM sharedInstance] deleteActionHistoryWithToken:[NSString stringWithUTF8String:token] historyId:[NSString stringWithUTF8String:historyId] timeStamp:timestamp completion:^(ActionHistoryResult* result){
            if(result.isSucceeded) {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "success");
            } else {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "fail");
            }
        }];
    }
    
    void _deleteAllActionHistoryByUserId(const char* token, const char* object, const char* function) {
        
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        [[AdBrixRM sharedInstance] deleteAllActionHistoryByUserIdWithToken:[NSString stringWithUTF8String:token] completion:^(ActionHistoryResult* result){
            if(result.isSucceeded) {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "success");
            } else {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "fail");
            }
        }];
    }
    
    void _deleteAllActionHistoryByAdid(const char* token, const char* object, const char* function) {
        
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        [[AdBrixRM sharedInstance] deleteAllActionHistoryByAdidWithToken:[NSString stringWithUTF8String:token] completion:^(ActionHistoryResult* result){
            if(result.isSucceeded) {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "success");
            } else {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "fail");
            }
        }];
    }
    
    void _clearSyncedActionHistoryInLocalDB(const char* object, const char* function) {
        
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        [[AdBrixRM sharedInstance] clearSyncedActionHistoryInLocalDBWithCompletion:^(ActionHistoryResult* result){
            if(result.isSucceeded) {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "success");
            } else {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "fail");
            }
        }];
    }
    
    
    //InAppMessage
    void _getAllInAppMessage(const char* object, const char* function) {
        
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        [[AdBrixRM sharedInstance] getAllInAppMessageWithCompletion:^(DfnInAppMessageResult* result){
            NSArray* dataArray = [result getData];
            
            if (dataArray != nil) {
                NSData* jsonData = [NSJSONSerialization dataWithJSONObject:dataArray options:NSJSONWritingPrettyPrinted error:nil];
                NSString* jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
                UnitySendMessage([_object UTF8String], [_function UTF8String], [jsonString UTF8String]);
            } else {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "[]");
            }
        }];
    }
    
    void _openInAppMessage(const char* campaignId, const char* object, const char* function) {
        
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        [[AdBrixRM sharedInstance] openInAppMessageWithCampaignId:[NSString stringWithUTF8String:campaignId] completion:^(Completion completion){
            
            if (completion == CompletionSuccess) {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "success");
            } else {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "fail");
            }
        }];
    }
    
    void _fetchInAppMessage(const char* object, const char* function) {
        
        NSString* _object = [NSString stringWithUTF8String: object];
        NSString* _function = [NSString stringWithUTF8String: function];
        
        [[AdBrixRM sharedInstance] fetchInAppMessageWithCompletion:^(DfnInAppMessageFetchResult* result){
            if (result.isSucceeded) {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "success");
            } else {
                UnitySendMessage([_object UTF8String], [_function UTF8String], "fail");
            }
        }];
    }
    
    void _setInAppMessageFetchMode(int mode) {
        [[AdBrixRM sharedInstance] setInAppMessageFetchModeWithMode:(DfnInAppMessageFetchMode)mode];
    }
    
    void _setInAppMessageToken(const char* token) {
        [[AdBrixRM sharedInstance] setInAppMessageTokenWithToken:[NSString stringWithUTF8String:token]];
    }

    const char* _AdBrixCurrencyNameRm (int currency)
    {
        NSString* str = [[AdBrixRM sharedInstance] getCurrencyString:currency];
        
        char* res = (char*)malloc(str.length+1);
        strcpy(res, [str UTF8String]);
        
        return res;
    }
    
    const char* _AdBrixPaymentMethodNameRm (int method)
    {
        NSString* str = [[AdBrixRM sharedInstance] getPaymentMethod:method];
        
        char* res = (char*)malloc(str.length+1);
        strcpy(res, [str UTF8String]);
        
        return res;
    }
    
    const char* _AdBrixSharingChannelNameRm (int channel)
    {
        NSString* str = [[AdBrixRM sharedInstance] getSharingChannel:channel];
        
        char* res = (char*)malloc(str.length+1);
        strcpy(res, [str UTF8String]);
        
        return res;
    }
    
    const char* _AdBrixSignUpChannelName (int channel)
    {
        NSString* str = [[AdBrixRM sharedInstance] getSignUpChannel:channel];
        
        char* res = (char*)malloc(str.length+1);
        strcpy(res, [str UTF8String]);
        
        return res;
    }
    
    const char* _AdBrixInviteChannelName (int channel)
    {
        NSString* str = [[AdBrixRM sharedInstance] getInviteChannel:channel];
        
        char* res = (char*)malloc(str.length+1);
        strcpy(res, [str UTF8String]);
        
        return res;
    }
    
    
}
@end
