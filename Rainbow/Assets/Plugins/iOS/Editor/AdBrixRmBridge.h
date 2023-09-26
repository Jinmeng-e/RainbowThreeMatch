//
//  AdPopcornSDKPlugin.h
//  IgaworksAd
//
//  Created by wonje,song on 2014. 1. 21..
//  Copyright (c) 2014년 wonje,song. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface AdBrixRmBridge : NSObject <AdBrixRMDeeplinkDelegate, AdBrixRMDeferredDeeplinkDelegate, AdBrixRmPushLocalDelegate, AdBrixRmPushRemoteDelegate, AdBrixRMLogDelegate, AdBrixRMInAppMessageClickDelegate, DfnInAppMessageAutoFetchDelegate>

@property (nonatomic, copy) NSString *abxDeeplinkDelegateObject;
@property (nonatomic, copy) NSString *abxDeeplinkDelegateFunction;
@property (nonatomic, copy) NSString *abxDeferredDeeplinkDelegateObject;
@property (nonatomic, copy) NSString *abxDeferredDeeplinkDelegateFunction;
@property (nonatomic, copy) NSString *abxPushLocalDelegateObject;
@property (nonatomic, copy) NSString *abxPushLocalDelegateFunction;
@property (nonatomic, copy) NSString *abxPushRemoteDelegateObject;
@property (nonatomic, copy) NSString *abxPushRemoteDelegateFunction;
@property (nonatomic, copy) NSString *abxLogDelegateObject;
@property (nonatomic, copy) NSString *abxLogDelegateFunction;
@property (nonatomic, copy) NSString *abxInAppMessageClickDelegateObject;
@property (nonatomic, copy) NSString *abxInAppMessageClickDelegateFunction;
@property (nonatomic, copy) NSString *abxInAppMessageAutoFetchDelegateObject;
@property (nonatomic, copy) NSString *abxInAppMessageAutoFetchDelegateFunction;

+ (AdBrixRmBridge *)sharedAdBrixRmBridge;
- (void)setAdBrixDeeplinkDelegate:(NSString *)object function:(NSString *)function;
- (void)setAdBrixDeferredDeeplinkDelegate:(NSString *)object function:(NSString *)function;
- (void)setAdBrixRmPushLocalDelegate:(NSString *)object function:(NSString *)function;
- (void)setAdBrixRmPushRemoteDelegate:(NSString *)object function:(NSString *)function;
- (void)setLogDelegate:(NSString *)object function:(NSString *)function;
- (void)setInAppMessageClickDelegate:(NSString *)object function:(NSString *)function;
- (void)setInAppMessageAutoFetchDelegate:(NSString *)object function:(NSString *)function;


@end
