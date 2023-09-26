package com.igaworks.v2.unity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;

import com.igaworks.v2.core.AbxCommerce;
import com.igaworks.v2.core.AbxCommon;
import com.igaworks.v2.core.AbxGame;
import com.igaworks.v2.core.AdBrixRm;
import com.igaworks.v2.core.application.AbxActivityHelper;
import com.unity3d.player.UnityPlayer;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import io.adbrix.sdk.component.AbxLog;
import io.adbrix.sdk.domain.function.Completion;
import io.adbrix.sdk.domain.model.ActionHistory;
import io.adbrix.sdk.domain.model.DfnInAppMessage;
import io.adbrix.sdk.domain.model.DfnInAppMessageFetchMode;
import io.adbrix.sdk.domain.model.Empty;
import io.adbrix.sdk.domain.model.Result;
import io.adbrix.sdk.utils.CommonUtils;

import com.unity3d.player.UnityPlayerActivity;

public class AbxUnityActivity extends UnityPlayerActivity {
    private static Activity activity;

    //region Activity Callback 처리
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if(!isActivityAvailable()){
            return;
        }
        onNewIntent(activity.getIntent());
    }

    @Override
    protected void onNewIntent(Intent intent) {
        super.onNewIntent(intent);
        if(!isActivityAvailable()){
            return;
        }
        setIntent(intent);
//        deeplinkEvent();
    }
    //endregion

    //region AdBrixRm
    public static void sendGameObjectNameToAbxUnityApplication(String gameObjectName){
        AbxUnityApplication.GameObjectNameReceiveListener listener = AbxUnityApplication.gameObjectNameReceiveListener;
        if(CommonUtils.isNullOrEmpty(gameObjectName)){
            AbxLog.e("gameObjectName is null or empty", false);
            return;
        }
        if(listener == null){
            AbxLog.e("GameObjectNameReceiveListener is null", false);
            return;
        }
        listener.onReceive(gameObjectName);
    }

    public static void init(String appKey, String secretKey) {
        if(!isActivityAvailable()){
            return;
        }
        AbxActivityHelper.initializeSdk(activity.getApplicationContext(), appKey, secretKey);
    }

    public static void login(final String userId) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.login(userId);
            }
        });
    }

    public static void logout() {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.logout();
            }
        });
    }

    public static void gdprForgetMe() {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.gdprForgetMe(activity.getApplicationContext());
            }
        });
    }

    public static void setAge(final int age) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.setAge(age);
            }
        });
    }

    public static void setGender(final int gender) {
        if(!isActivityAvailable()){
            return;
        }
        AdBrixRm.AbxGender abxGender;
        switch (gender) {
            case 0:
                abxGender = AdBrixRm.AbxGender.UNKNOWN;
                break;
            case 1:
                abxGender = AdBrixRm.AbxGender.FEMALE;
                break;
            case 2:
                abxGender = AdBrixRm.AbxGender.MALE;
                break;
            default:
                abxGender = AdBrixRm.AbxGender.UNKNOWN;
                break;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.setGender(abxGender);
            }
        });
    }

    public static void saveUserProperties(String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("saveUserProperties() properties is null or empty", false);
            return;
        }
        AdBrixRm.UserProperties userProperties = getUserPropertiesFromMap(properties);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.saveUserProperties(userProperties);
            }
        });
    }

    public static void clearUserProperties() {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.clearUserProperties();
            }
        });
    }

    public static void setLocation(final double latitude, final double longitude) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.setLocation(latitude, longitude);
            }
        });
    }

    public static void event(final String eventName) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.event(eventName);
            }
        });
    }

    public static void event(String eventName, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("event() properties is null or empty", false);
            return;
        }
        AdBrixRm.AttrModel model = getAttrModelFromMap(properties);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.event(eventName, model);
            }
        });
    }

    public static void flushAllEvents(String callbackObjectName, String callbackMethodName) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.flushAllEvents(new Completion<Result<Empty>>() {
                    @Override
                    public void handle(Result<Empty> emptyResult) {
                        sendMessageToUnity(callbackObjectName, callbackMethodName, emptyResult.toString());
                    }
                });
            }
        });
    }

    public static void deeplinkEvent() {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.deeplinkEvent(activity);
            }
        });
    }

    public static void deeplinkEventWithIntent(final Intent deeplinkIntent) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.deeplinkEventWithIntent(deeplinkIntent);
            }
        });
    }

    public static void setEventUploadCountInterval(int eventUploadCountInterval) {
        if(!isActivityAvailable()){
            return;
        }
        AdBrixRm.AdBrixEventUploadCountInterval interval;
        switch (eventUploadCountInterval) {
            case 10:
                interval = AdBrixRm.AdBrixEventUploadCountInterval.MIN;
                break;
            case 30:
                interval = AdBrixRm.AdBrixEventUploadCountInterval.NORMAL;
                break;
            case 60:
                interval = AdBrixRm.AdBrixEventUploadCountInterval.MAX;
                break;
            default:
                interval = AdBrixRm.AdBrixEventUploadCountInterval.NORMAL;
                break;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.setEventUploadCountInterval(interval);
            }
        });
    }

    public static void setEventUploadTimeInterval(int eventUploadTimeInterval) {
        if(!isActivityAvailable()){
            return;
        }
        AdBrixRm.AdBrixEventUploadTimeInterval interval;
        switch (eventUploadTimeInterval) {
            case 30:
                interval = AdBrixRm.AdBrixEventUploadTimeInterval.MIN;
                break;
            case 60:
                interval = AdBrixRm.AdBrixEventUploadTimeInterval.NORMAL;
                break;
            case 120:
                interval = AdBrixRm.AdBrixEventUploadTimeInterval.MAX;
                break;
            default:
                interval = AdBrixRm.AdBrixEventUploadTimeInterval.NORMAL;
                break;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.setEventUploadTimeInterval(interval);
            }
        });
    }

    public static void setPushEnable(final boolean enable) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.setPushEnable(enable);
            }
        });
    }

    public static void setRegistrationId(final String token) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.setRegistrationId(token);
            }
        });
    }

    public static void setBigTextClientPushEvent(String bigTextPushPropertiesJson, boolean alwaysIsShown) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(bigTextPushPropertiesJson)){
            AbxLog.e("setBigTextClientPushEvent() bigTextPushPropertiesJson is empty", false);
            return;
        }
        JSONObject jsonObject = new JSONObject();
        try {
            jsonObject = new JSONObject(bigTextPushPropertiesJson);
        } catch (JSONException e) {
            AbxLog.e("setBigTextClientPushEvent() parse error: ",e, false);
        }
        AdBrixRm.setBigTextClientPushEvent(activity.getApplicationContext(), AdBrixRm.BigTextPushProperties.fromJSONObject(jsonObject), alwaysIsShown);
    }

    public static void setBigPictureClientPushEvent(String bigPicturePushPropertiesJson, boolean alwaysIsShown) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(bigPicturePushPropertiesJson)){
            AbxLog.e("setBigPictureClientPushEvent() bigPicturePushPropertiesJson is empty", false);
            return;
        }
        JSONObject jsonObject = new JSONObject();
        try {
            jsonObject = new JSONObject(bigPicturePushPropertiesJson);
        } catch (JSONException e) {
            AbxLog.e("setBigPictureClientPushEvent() parse error: ",e, false);
        }
        AdBrixRm.setBigPictureClientPushEvent(activity.getApplicationContext(), AdBrixRm.BigPicturePushProperties.fromJSONObject(jsonObject), alwaysIsShown);
    }

    public static void cancelClientPushEvent(int eventId) {
        if(!isActivityAvailable()){
            return;
        }
        AdBrixRm.cancelClientPushEvent(activity.getApplicationContext(), eventId);
    }

    public static String getPushEventList() {
        JSONArray result = AdBrixRm.getPushEventList();
        return result.toString();
    }

    public static void setPushIconStyle(String smallIconName, String largeIconName, int argb) {
        if(!isActivityAvailable()){
            return;
        }
        AdBrixRm.setPushIconStyle(activity.getApplicationContext(), smallIconName, largeIconName, argb);
    }

    public static void setPushIconStyle(String smallIconName, String largeIconName) {
        if(!isActivityAvailable()){
            return;
        }
        AdBrixRm.setPushIconStyle(activity.getApplicationContext(), smallIconName, largeIconName);
    }

    public static void setNotificationOption(int priority, int visibility) {
        if(!isActivityAvailable()){
            return;
        }
        AdBrixRm.setNotificationOption(activity.getApplicationContext(), priority, visibility);
    }

    public static void setNotificationChannel(String channelName, String channelDescription, int importance, boolean vibrateEnable) {
        if(!isActivityAvailable()){
            return;
        }
        AdBrixRm.setNotificationChannel(activity.getApplicationContext(), channelName, channelDescription, importance, vibrateEnable);
    }

    public static void setKakaoId(final String kakaoId) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.setKakaoId(kakaoId);
            }
        });
    }

    public static void saveCiProperties(final String key, final String value) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.saveCiProperties(key, value);
            }
        });
    }

    public static void openPush(String openPushParamJson) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(openPushParamJson)){
            AbxLog.e("openPush() openPushParamJson is empty", false);
            return;
        }
        try {
            JSONObject simplePushModel = new JSONObject(openPushParamJson);
            AdBrixRm.AbxRemotePushModel abxRemotePushModel = new AdBrixRm.AbxRemotePushModel(
                    simplePushModel.optString("abx:gf:campaign_id", ""),
                    simplePushModel.optInt("abx:gf:campaign_revision_no", 0),
                    simplePushModel.optString("abx:gf:step_id", ""),
                    simplePushModel.optString("abx:gf:cycle_time", "")
            );
            executeMainThread(new Runnable() {
                @Override
                public void run() {
                    AdBrixRm.openPush(abxRemotePushModel);
                }
            });
        } catch (Exception e) {
            Log.d("abxrm", "AbxUnityActivity::openPush - Adbrix push tracking parameters don't exist!");
        }
    }

    public static void deleteUserDataAndStopSDK(String userId, String callbackObjectName, String callbackMethodName) {
        if(!isActivityAvailable()){
            return;
        }
        Runnable onSuccessRunnable = new Runnable() {
            @Override
            public void run() {
                sendMessageToUnity(callbackObjectName, callbackMethodName, "deleteUserDataAndStopSDK() onSuccess");
            }
        };
        Runnable onFailRunnable = new Runnable() {
            @Override
            public void run() {
                sendMessageToUnity(callbackObjectName, callbackMethodName, "deleteUserDataAndStopSDK() onFail");
            }
        };
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.deleteUserDataAndStopSDK(userId, onSuccessRunnable, onFailRunnable);
            }
        });
    }

    public static void restartSDK(String userId, String callbackObjectName, String callbackMethodName) {
        if(!isActivityAvailable()){
            return;
        }
        Runnable onSuccessRunnable = new Runnable() {
            @Override
            public void run() {
                sendMessageToUnity(callbackObjectName, callbackMethodName, "restartSDK() onSuccess");
            }
        };
        Runnable onFailRunnable = new Runnable() {
            @Override
            public void run() {
                sendMessageToUnity(callbackObjectName, callbackMethodName, "restartSDK() onFail");
            }
        };
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.restartSDK(userId, onSuccessRunnable, onFailRunnable);
            }
        });
    }

    public static String getSdkVersion() {
        return AdBrixRm.SDKVersion();
    }

    public static void fetchActionHistoryByUserId(String token, String actionType, String callbackObjectName, String callbackMethodName) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(actionType)){
            AbxLog.e("fetchActionHistoryByUserId() actionType is null or empty", false);
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.fetchActionHistoryByUserId(token, getListFromJson(actionType), new Completion<Result<List<ActionHistory>>>() {
                    @Override
                    public void handle(Result<List<ActionHistory>> listResult) {
                        List<ActionHistory> list = listResult.getOrDefault(new ArrayList<>());
                        JSONArray array = new JSONArray();
                        for(ActionHistory index: list){
                            if(index == null){
                                continue;
                            }
                            try {
                                array.put(index);
                            } catch (Exception e){
                                AbxLog.e("fetchActionHistoryByUserId() ",e, false);
                            }
                        }
                        sendMessageToUnity(callbackObjectName, callbackMethodName, array.toString());
                    }
                });
            }
        });
    }

    public static void fetchActionHistoryByAdid(String token, String actionType, String callbackObjectName, String callbackMethodName) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(actionType)){
            AbxLog.e("fetchActionHistoryByAdid() actionType is null or empty", false);
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.fetchActionHistoryByAdid(token, getListFromJson(actionType), new Completion<Result<List<ActionHistory>>>() {
                    @Override
                    public void handle(Result<List<ActionHistory>> listResult) {
                        List<ActionHistory> list = listResult.getOrDefault(new ArrayList<>());
                        JSONArray array = new JSONArray();
                        for(ActionHistory index: list){
                            if(index == null){
                                continue;
                            }
                            try {
                                array.put(index);
                            } catch (Exception e){
                                AbxLog.e("fetchActionHistoryByAdid() ",e, false);
                            }
                        }
                        sendMessageToUnity(callbackObjectName, callbackMethodName, array.toString());
                    }
                });

            }
        });
    }

    public static void insertPushData(String data) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(data)){
            AbxLog.e("insertPushData() data is empty", false);
            return;
        }
        JSONObject jsonObject = null;
        try {
            jsonObject = new JSONObject(data);
        } catch (JSONException e) {

        }
        if(jsonObject == null){
            AbxLog.e("insertPushData() parsing error", false);
            return;
        }
        Map<String, String> map = getMapFromJSONObject(jsonObject);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.insertPushData(map);
            }
        });
    }

    public static void getActionHistory(int skip, int limit, String actionType, String callbackObjectName, String callbackMethodName) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(actionType)){
            AbxLog.e("getActionHistory() actionType is null or empty", false);
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.getActionHistory(skip, limit, getListFromJson(actionType), new Completion<Result<List<ActionHistory>>>() {
                    @Override
                    public void handle(Result<List<ActionHistory>> listResult) {
                        List<ActionHistory> list = listResult.getOrDefault(new ArrayList<>());
                        JSONArray array = new JSONArray();
                        for(ActionHistory index: list){
                            if(index == null){
                                continue;
                            }
                            try {
                                array.put(index);
                            } catch (Exception e){
                                AbxLog.e("getActionHistory() error",e, false);
                            }
                        }
                        sendMessageToUnity(callbackObjectName, callbackMethodName, array.toString());
                    }
                });

            }
        });
    }

    public static void getAllActionHistory(String actionType, String callbackObjectName, String callbackMethodName) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(actionType)){
            AbxLog.e("getAllActionHistory() actionType is null or empty", false);
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.getAllActionHistory(getListFromJson(actionType), new Completion<Result<List<ActionHistory>>>() {
                    @Override
                    public void handle(Result<List<ActionHistory>> listResult) {
                        List<ActionHistory> list = listResult.getOrDefault(new ArrayList<>());
                        JSONArray array = new JSONArray();
                        for(ActionHistory index: list){
                            if(index == null){
                                continue;
                            }
                            try {
                                array.put(index);
                            } catch (Exception e){
                                AbxLog.e("getAllActionHistory() error",e, false);
                            }
                        }
                        sendMessageToUnity(callbackObjectName, callbackMethodName, array.toString());
                    }
                });

            }
        });
    }

    public static void deleteActionHistory(
            String token,
            String historyId,
            long timestamp,
            String callbackObjectName,
            String callbackMethodName
    ) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.deleteActionHistory(token, historyId, timestamp, new Completion<Result<Empty>>() {
                    @Override
                    public void handle(Result<Empty> emptyResult) {
                        sendMessageToUnity(callbackObjectName, callbackMethodName, emptyResult.toString());
                    }
                });
            }
        });
    }

    public static void deleteAllActionHistoryByUserId(String token, String callbackObjectName, String callbackMethodName) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.deleteAllActionHistoryByUserId(token, new Completion<Result<Empty>>() {
                    @Override
                    public void handle(Result<Empty> emptyResult) {
                        sendMessageToUnity(callbackObjectName, callbackMethodName, emptyResult.toString());
                    }
                });

            }
        });
    }

    public static void deleteAllActionHistoryByAdid(String token, String callbackObjectName, String callbackMethodName) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.deleteAllActionHistoryByAdid(token, new Completion<Result<Empty>>() {
                    @Override
                    public void handle(Result<Empty> emptyResult) {
                        sendMessageToUnity(callbackObjectName, callbackMethodName, emptyResult.toString());
                    }
                });

            }
        });
    }

    public static void clearSyncedActionHistoryInLocalDB(String callbackObjectName, String callbackMethodName) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.clearSyncedActionHistoryInLocalDB(new Completion<Result<Empty>>() {
                    @Override
                    public void handle(Result<Empty> emptyResult) {
                        sendMessageToUnity(callbackObjectName, callbackMethodName, emptyResult.toString());
                    }
                });

            }
        });
    }

    public static void setInAppMessageFetchMode(int mode) {
        if(!isActivityAvailable()){
            return;
        }
        DfnInAppMessageFetchMode dfnInAppMessageFetchMode;
        switch (mode){
            case 0: {
                dfnInAppMessageFetchMode = DfnInAppMessageFetchMode.USER_ID;
                break;
            }
            case 1:{
                dfnInAppMessageFetchMode = DfnInAppMessageFetchMode.ADID;
                break;
            }
            default:{
                dfnInAppMessageFetchMode = DfnInAppMessageFetchMode.USER_ID;
                AbxLog.e("setInAppMessageFetchMode() unknown mode!", false);
                break;
            }
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.setInAppMessageFetchMode(dfnInAppMessageFetchMode);
            }
        });
    }

    public static void setInAppMessageToken(String token) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.setInAppMessageToken(token);
            }
        });
    }

    public static void fetchInAppMessage(String callbackObjectName, String callbackMethodName) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.fetchInAppMessage(new Completion<Result<Empty>>() {
                    @Override
                    public void handle(Result<Empty> emptyResult) {
                        sendMessageToUnity(callbackObjectName, callbackMethodName, emptyResult.toString());
                    }
                });
            }
        });
    }

    public static void getAllInAppMessage(String callbackObjectName, String callbackMethodName) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.getAllInAppMessage(new Completion<Result<List<DfnInAppMessage>>>() {
                    @Override
                    public void handle(Result<List<DfnInAppMessage>> listResult) {
                        List<DfnInAppMessage> list = listResult.getOrDefault(new ArrayList<>());
                        JSONArray array = new JSONArray();
                        for(DfnInAppMessage index: list){
                            if(index == null){
                                continue;
                            }
                            try {
                                array.put(index);
                            } catch (Exception e){
                                AbxLog.e("getAllInAppMessage() error",e, false);
                            }
                        }
                        sendMessageToUnity(callbackObjectName, callbackMethodName, array.toString());
                    }
                });

            }
        });
    }

    public static void openInAppMessage(String campaignId, String callbackObjectName, String callbackMethodName) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AdBrixRm.openInAppMessage(campaignId, new Completion<Result<Empty>>() {
                    @Override
                    public void handle(Result<Empty> emptyResult) {
                        sendMessageToUnity(callbackObjectName, callbackMethodName, emptyResult.toString());
                    }
                });

            }
        });
    }
    //endregion

    //region AbxCommon
    public static void commonPurchase(String orderId, String commerceProductModelListJson, double discount, double deliveryCharge, int paymentMethod) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("purchase() commerceProductModelListJson is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        AdBrixRm.CommercePaymentMethod method = AdBrixRm.CommercePaymentMethod.getMethodByMethodCode(paymentMethod);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommon.purchase(orderId, list, discount, deliveryCharge, method);

            }
        });
    }

    public static void commonPurchase(String orderId, String commerceProductModelListJson, double discount, double deliveryCharge, int paymentMethod, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("purchase() commerceProductModelListJson is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        AdBrixRm.CommercePaymentMethod method = AdBrixRm.CommercePaymentMethod.getMethodByMethodCode(paymentMethod);
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        AdBrixRm.CommonProperties.Purchase purchase = new AdBrixRm.CommonProperties.Purchase();
        purchase.setAttrModel(attrModel);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommon.purchase(orderId, list, discount, deliveryCharge, method, purchase);

            }
        });
    }

    public static void commonPurchase(String orderId, String commerceProductModelListJson, double orderSales, double discount, double deliveryCharge, int paymentMethod) {
        if(!isActivityAvailable()){
            return;
        }if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("purchase() commerceProductModelListJson is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        AdBrixRm.CommercePaymentMethod method = AdBrixRm.CommercePaymentMethod.getMethodByMethodCode(paymentMethod);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommon.purchase(orderId, list, orderSales, discount, deliveryCharge, method);

            }
        });
    }

    public static void commonPurchase(String orderId, String commerceProductModelListJson, double orderSales, double discount, double deliveryCharge, int paymentMethod, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("purchase() commerceProductModelListJson is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        AdBrixRm.CommercePaymentMethod method = AdBrixRm.CommercePaymentMethod.getMethodByMethodCode(paymentMethod);
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        AdBrixRm.CommonProperties.Purchase purchase = new AdBrixRm.CommonProperties.Purchase();
        purchase.setAttrModel(attrModel);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommon.purchase(orderId, list, orderSales, discount, deliveryCharge, method, purchase);

            }
        });
    }

    public static void commonSignUp(int signUpChannel) {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommon.signUp(AdBrixRm.CommonSignUpChannel.getChannelByChannelCode(signUpChannel));

            }
        });
    }

    public static void commonSignUp(int signUpChannel, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("signUp() properties is null or empty", false);
            return;
        }
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        AdBrixRm.CommonProperties.SignUp signUp = new AdBrixRm.CommonProperties.SignUp().setAttrModel(attrModel);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommon.signUp(AdBrixRm.CommonSignUpChannel.getChannelByChannelCode(signUpChannel), signUp);

            }
        });
    }

    public static void commonUseCredit() {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommon.useCredit();

            }
        });
    }

    public static void commonUseCredit(String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("useCredit() properties is null or empty", false);
            return;
        }
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        AdBrixRm.CommonProperties.UseCredit useCredit = new AdBrixRm.CommonProperties.UseCredit().setAttrModel(attrModel);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommon.useCredit(useCredit);

            }
        });
    }

    public static void commonAppUpdate(String previousVersion, String currentVersion, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("appUpdate() properties is null or empty", false);
            return;
        }
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        AdBrixRm.CommonProperties.AppUpdate appUpdate = new AdBrixRm.CommonProperties.AppUpdate()
                .setPrevVersion(previousVersion)
                .setCurrVersion(currentVersion).setAttrModel(attrModel);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommon.appUpdate(appUpdate);

            }
        });
    }

    public static void commonInvite(int channel) {
        if(!isActivityAvailable()){
            return;
        }
        AdBrixRm.CommonInviteChannel commonInviteChannel = AdBrixRm.CommonInviteChannel.getChannelByChannelCode(channel);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommon.invite(commonInviteChannel);

            }
        });
    }

    public static void commonInvite(int channel, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("invite() properties is null or empty", false);
            return;
        }
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        AdBrixRm.CommonInviteChannel commonInviteChannel = AdBrixRm.CommonInviteChannel.getChannelByChannelCode(channel);
        AdBrixRm.CommonProperties.Invite invite = new AdBrixRm.CommonProperties.Invite();
        invite.setAttrModel(attrModel);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommon.invite(commonInviteChannel, invite);

            }
        });
    }
    //endregion

    //region AbxCommerce
    public static void commerceViewHome() {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.viewHome();

            }
        });
    }

    public static void commerceViewHome(String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("viewHome() properties is null or empty", false);
            return;
        }
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.viewHome(attrModel);

            }
        });
    }

    public static void commerceCategoryView(String categories, String commerceProductModelListJson) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(categories)){
            AbxLog.e("categoryView() categories is null or empty", false);
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("categoryView() commerceProductModelListJson is null or empty", false);
            return;
        }
        AdBrixRm.CommerceCategoriesModel model = getCommerceCategoriesModelFromList(categories);
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.categoryView(model, list);

            }
        });
    }

    public static void commerceCategoryView(String categories, String commerceProductModelListJson, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(categories)){
            AbxLog.e("categoryView() categories is null or empty", false);
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("categoryView() commerceProductModelListJson is null or empty", false);
            return;
        }
        AdBrixRm.CommerceCategoriesModel model = getCommerceCategoriesModelFromList(categories);
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.categoryView(model, list, attrModel);

            }
        });
    }

    public static void commerceCategoryView(String categories) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(categories)){
            AbxLog.e("categoryView() categories is null or empty", false);
            return;
        }
        AdBrixRm.CommerceCategoriesModel model = getCommerceCategoriesModelFromList(categories);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.categoryView(model);

            }
        });
    }

    public static void commerceProductView(String commerceProductModelJson) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelJson)){
            AbxLog.e("productView() commerceProductModelJson is null or empty", false);
            return;
        }
        AdBrixRm.CommerceProductModel model = getCommerceProductModelByJsonString(commerceProductModelJson);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.productView(model);

            }
        });
    }

    public static void commerceProductView(String commerceProductModelJson, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelJson)){
            AbxLog.e("productView() commerceProductModelJson is null or empty", false);
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("productView() properties is null or empty", false);
            return;
        }
        AdBrixRm.CommerceProductModel model = getCommerceProductModelByJsonString(commerceProductModelJson);
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.productView(model, attrModel);

            }
        });
    }

    public static void commerceAddToCart(String commerceProductModelListJson) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("addToCart() commerceProductModelListJson is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.addToCart(list);

            }
        });
    }

    public static void commerceAddToCart(String commerceProductModelListJson, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("addToCart() commerceProductModelListJson is null or empty", false);
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("addToCart() properties is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.addToCart(list, attrModel);

            }
        });
    }

    public static void commerceAddToWishList(String commerceProductModelJson) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelJson)){
            AbxLog.e("addToWishList() commerceProductModelJson is null or empty", false);
            return;
        }
        AdBrixRm.CommerceProductModel model = getCommerceProductModelByJsonString(commerceProductModelJson);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.addToWishList(model);

            }
        });
    }

    public static void commerceAddToWishList(String commerceProductModelJson, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelJson)){
            AbxLog.e("addToWishList() commerceProductModelJson is null or empty", false);
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("addToWishList() properties is null or empty", false);
            return;
        }
        AdBrixRm.CommerceProductModel model = getCommerceProductModelByJsonString(commerceProductModelJson);
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.addToWishList(model, attrModel);

            }
        });
    }

    public static void commerceReviewOrder(String orderId, String commerceProductModelListJson, double discount, double deliveryCharge) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("reviewOrder() commerceProductModelListJson is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.reviewOrder(orderId, list, discount, deliveryCharge);

            }
        });
    }

    public static void commerceReviewOrder(String orderId, String commerceProductModelListJson, double discount, double deliveryCharge, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("reviewOrder() commerceProductModelListJson is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.reviewOrder(orderId, list, discount, deliveryCharge, attrModel);

            }
        });
    }

    public static void commerceRefund(String orderId, String commerceProductModelListJson, double penaltyCharge) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("refund() commerceProductModelListJson is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.refund(orderId, list, penaltyCharge);

            }
        });
    }

    public static void commerceRefund(String orderId, String commerceProductModelListJson, double penaltyCharge, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("refund() commerceProductModelListJson is null or empty", false);
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("refund() properties is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.refund(orderId, list, penaltyCharge, attrModel);

            }
        });
    }

    public static void commerceSearch(String keyword, String commerceProductModelListJson) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("search() commerceProductModelListJson is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.search(keyword, list);

            }
        });
    }

    public static void commerceSearch(String keyword, String commerceProductModelListJson, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("search() commerceProductModelListJson is null or empty", false);
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("search() properties is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.search(keyword, list, attrModel);

            }
        });
    }

    public static void commerceShare(String sharingChannelCode, String commerceProductModelJson) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelJson)){
            AbxLog.e("share() commerceProductModelJson is null or empty", false);
            return;
        }
        AdBrixRm.CommerceSharingChannel channel = AdBrixRm.CommerceSharingChannel.getChannelByChannelCode(sharingChannelCode);
        AdBrixRm.CommerceProductModel model = getCommerceProductModelByJsonString(commerceProductModelJson);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.share(channel, model);

            }
        });
    }

    public static void commerceShare(String sharingChannelCode, String commerceProductModelJson, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelJson)){
            AbxLog.e("share() commerceProductModelJson is null or empty", false);
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("share() properties is null or empty", false);
            return;
        }
        AdBrixRm.CommerceSharingChannel channel = AdBrixRm.CommerceSharingChannel.getChannelByChannelCode(sharingChannelCode);
        AdBrixRm.CommerceProductModel model = getCommerceProductModelByJsonString(commerceProductModelJson);
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.share(channel, model, attrModel);

            }
        });
    }

    public static void commerceListView(String commerceProductModelListJson) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("listView() commerceProductModelListJson is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.listView(list);

            }
        });
    }

    public static void commerceListView(String commerceProductModelListJson, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("listView() commerceProductModelListJson is null or empty", false);
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("listView() properties is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.listView(list, attrModel);

            }
        });
    }

    public static void commerceCartView(String commerceProductModelListJson) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("cartView() commerceProductModelListJson is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.cartView(list);

            }
        });
    }

    public static void commerceCartView(String commerceProductModelListJson, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(commerceProductModelListJson)){
            AbxLog.e("cartView() commerceProductModelListJson is null or empty", false);
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("cartView() properties is null or empty", false);
            return;
        }
        List<AdBrixRm.CommerceProductModel> list = getCommerceProductModelListByJsonString(commerceProductModelListJson);
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.cartView(list, attrModel);

            }
        });
    }

    public static void commercePaymentInfoAdded() {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.paymentInfoAdded();

            }
        });
    }

    public static void commercePaymentInfoAdded(String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("paymentInfoAdded() properties is null or empty", false);
            return;
        }
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxCommerce.paymentInfoAdded(attrModel);

            }
        });
    }
    //endregion

    //region AbxGame
    public static void gameTutorialComplete() {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxGame.tutorialComplete();

            }
        });
    }
    public static void gameTutorialComplete(boolean isSkip, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("tutorialComplete() properties is null or empty", false);
            return;
        }
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        AdBrixRm.GameProperties.TutorialComplete tutorialComplete = new AdBrixRm.GameProperties.TutorialComplete().setIsSkip(isSkip).setAttrModel(attrModel);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxGame.tutorialComplete(tutorialComplete);

            }
        });
    }

    public static void gameLevelAchieved() {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxGame.levelAchieved();

            }
        });
    }

    public static void gameLevelAchieved(int level, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("levelAchieved() properties is null or empty", false);
            return;
        }
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        AdBrixRm.GameProperties.LevelAchieved levelAchieved = new AdBrixRm.GameProperties.LevelAchieved().setLevel(level).setAttrModel(attrModel);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxGame.levelAchieved(levelAchieved);

            }
        });
    }

    public static void gameCharacterCreated() {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxGame.characterCreated();

            }
        });
    }

    public static void gameCharacterCreated(String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("characterCreated() properties is null or empty", false);
            return;
        }
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        AdBrixRm.GameProperties.CharacterCreated characterCreated = new AdBrixRm.GameProperties.CharacterCreated().setAttrModel(attrModel);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxGame.characterCreated(characterCreated);

            }
        });
    }

    public static void gameStageCleared() {
        if(!isActivityAvailable()){
            return;
        }
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxGame.stageCleared();

            }
        });
    }

    public static void gameStageCleared(String stageName, String properties) {
        if(!isActivityAvailable()){
            return;
        }
        if(CommonUtils.isNullOrEmpty(properties)){
            AbxLog.e("stageCleared() properties is null or empty", false);
            return;
        }
        AdBrixRm.AttrModel attrModel = getAttrModelFromMap(properties);
        AdBrixRm.GameProperties.StageCleared stageCleared = new AdBrixRm.GameProperties.StageCleared().setStageName(stageName).setAttrModel(attrModel);
        executeMainThread(new Runnable() {
            @Override
            public void run() {
                AbxGame.stageCleared(stageCleared);

            }
        });
    }
    //endregion

    //region Utils
    private static void sendMessageToUnity(String callbackObjectName, String callbackMethodName, String message){
        if(CommonUtils.isNullOrEmpty(callbackObjectName)){
            AbxLog.e("callbackObjectName is null or empty", false);
            return;
        }
        if(CommonUtils.isNullOrEmpty(callbackMethodName)){
            AbxLog.e("callbackMethodName is null or empty", false);
            return;
        }
        UnityPlayer.UnitySendMessage(callbackObjectName, callbackMethodName, message);
    }

    private static boolean isActivityAvailable(){
        AbxUnityActivity.activity = UnityPlayer.currentActivity;
        if(activity!=null){
            return true;
        }
        AbxLog.e("activity is null", false);
        return false;
    }

    /**
     * Parsing Method
     */
    public static AdBrixRm.CommerceProductModel getCommerceProductModelByJsonString(String dataJsonString) {
        try {
            JSONArray root = new JSONArray(dataJsonString);

            if (root.length() < 1) {
                Log.e("abxrm", "commerceV2PlugIn error : No purhcase item.");
                return null;
            }

            for (int i = 0; i < root.length(); i++) {
                try {
                    JSONObject item = root.getJSONObject(i);
                    AdBrixRm.CommerceProductModel pItem = new AdBrixRm.CommerceProductModel();

                    if (item.has("productId")) {
                        pItem.setProductID(item.getString("productId"));
                    } else {
                        throw new Exception("No productId attribute.");
                    }
                    if (item.has("productName")) {
                        pItem.setProductName(item.getString("productName"));
                    } else {
                        throw new Exception("No productName attribute.");
                    }
                    if (item.has("price")) {
                        pItem.setPrice(Double.parseDouble(item.getString("price")));
                    } else {
                        throw new Exception("No price attribute.");
                    }
                    if (item.has("discount")) {
                        pItem.setDiscount(Double.parseDouble(item.getString("discount")));
                    } else {
                        throw new Exception("No discount attribute.");
                    }
                    if (item.has("quantity")) {
                        pItem.setQuantity(Integer.parseInt(item.getString("quantity")));
                    } else {
                        throw new Exception("No quantity attribute.");
                    }
                    if (item.has("currency")) {
                        pItem.setCurrency(AdBrixRm.Currency.getCurrencyByCurrencyCode(item.getString("currency")));
                    } else {
                        throw new Exception("No currency attribute.");
                    }

                    if (item.has("category")) {
                        String[] categories = new String[5];
                        String[] temp = item.getString("category") != null ? item.getString("category").split("\\.") : new String[0];

                        for (int j = 0; j < temp.length; j++) {
                            categories[j] = temp[j];
                        }

                        AdBrixRm.CommerceCategoriesModel categoriesModel = new AdBrixRm.CommerceCategoriesModel();

                        if (categories.length == 1) categoriesModel.setCategory(categories[0]);
                        else if (categories.length == 2)
                            categoriesModel.setCategory(categories[0]).setCategory(categories[1]);
                        else if (categories.length == 3)
                            categoriesModel.setCategory(categories[0]).setCategory(categories[1]).setCategory(categories[2]);
                        else if (categories.length == 4)
                            categoriesModel.setCategory(categories[0]).setCategory(categories[1]).setCategory(categories[2]).setCategory(categories[3]);
                        else if (categories.length == 5)
                            categoriesModel.setCategory(categories[0]).setCategory(categories[1]).setCategory(categories[2]).setCategory(categories[3]).setCategory(categories[4]);
                        pItem.setCategory(categoriesModel);
                    } else {
                        throw new Exception("No category attribute.");
                    }

                    if (item.has("extra_attrs")) {
                        final JSONObject subItem = item.getJSONObject("extra_attrs");
                        JSONObject attrs = new JSONObject();
                        Iterator<?> keys = subItem.keys();

                        while (keys.hasNext()) {
                            String key = (String) keys.next();
                            String value = subItem.getString(key);
                            attrs.put(key, value);
                        }
                        pItem.setAttrModel(getAttrModelFromMap(attrs));
                    } else {
                        throw new Exception("No extra_attrs attribute.");
                    }
                    return pItem;
                } catch (Exception e) {
                    Log.e("abxrm", "purchase error : invalid item = " + dataJsonString);
                }
            }
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return null;
    }

    public static List<AdBrixRm.CommerceProductModel> getCommerceProductModelListByJsonString(String dataJsonString) {
        try {
            JSONArray root = new JSONArray(dataJsonString);

            if (root.length() < 1) {
                Log.e("abxrm", "commerceV2PlugIn error : No purhcase item.");
                return null;
            }

            ArrayList<AdBrixRm.CommerceProductModel> items = new ArrayList<AdBrixRm.CommerceProductModel>();

            for (int i = 0; i < root.length(); i++) {
                try {
                    JSONObject item = root.getJSONObject(i);
                    AdBrixRm.CommerceProductModel pItem = new AdBrixRm.CommerceProductModel();

                    if (item.has("productId")) {
                        Log.i("abxrm", "Productname is " + item.getString("productId"));
                        pItem.setProductID(item.getString("productId"));
                    } else {
                        throw new Exception("No productId attribute.");
                    }
                    if (item.has("productName")) {
                        pItem.setProductName(item.getString("productName"));
                    } else {
                        throw new Exception("No productName attribute.");
                    }
                    if (item.has("price")) {
                        pItem.setPrice(Double.parseDouble(item.getString("price")));
                    } else {
                        throw new Exception("No price attribute.");
                    }
                    if (item.has("discount")) {
                        pItem.setDiscount(Double.parseDouble(item.getString("discount")));
                    } else {
                        throw new Exception("No discount attribute.");
                    }
                    if (item.has("quantity")) {
                        pItem.setQuantity(Integer.parseInt(item.getString("quantity")));
                    } else {
                        throw new Exception("No quantity attribute.");
                    }
                    if (item.has("currency")) {
                        pItem.setCurrency(AdBrixRm.Currency.getCurrencyByCurrencyCode(item.getString("currency")));
                    } else {
                        throw new Exception("No currency attribute.");
                    }
                    if (item.has("category")) {
                        String[] categories = new String[5];
                        String[] temp = item.getString("category") != null ? item.getString("category").split("\\.") : new String[0];

                        for (int j = 0; j < temp.length; j++) {
                            categories[j] = temp[j];
                        }

                        AdBrixRm.CommerceCategoriesModel categoriesModel = new AdBrixRm.CommerceCategoriesModel();

                        if (categories.length == 1) categoriesModel.setCategory(categories[0]);
                        else if (categories.length == 2)
                            categoriesModel.setCategory(categories[0]).setCategory(categories[1]);
                        else if (categories.length == 3)
                            categoriesModel.setCategory(categories[0]).setCategory(categories[1]).setCategory(categories[2]);
                        else if (categories.length == 4)
                            categoriesModel.setCategory(categories[0]).setCategory(categories[1]).setCategory(categories[2]).setCategory(categories[3]);
                        else if (categories.length == 5)
                            categoriesModel.setCategory(categories[0]).setCategory(categories[1]).setCategory(categories[2]).setCategory(categories[3]).setCategory(categories[4]);
                        pItem.setCategory(categoriesModel);
                    } else {
                        throw new Exception("No category attribute.");
                    }
                    if (item.has("extra_attrs")) {
                        final JSONObject subItem = item.getJSONObject("extra_attrs");
                        JSONObject attrs = new JSONObject();
                        Iterator<?> keys = subItem.keys();

                        while (keys.hasNext()) {
                            String key = (String) keys.next();
                            String value = subItem.getString(key);
                            attrs.put(key, value);
                        }
                        pItem.setAttrModel(getAttrModelFromMap(attrs));
                    } else {
                        throw new Exception("No extra_attrs attribute.");
                    }
                    items.add(pItem);
                } catch (Exception e) {
                    Log.e("abxrm", "purchase error : invalid item = " + dataJsonString);
                }
            }
            return items;
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return null;
    }

    public static AdBrixRm.CommerceCategoriesModel getCommerceCategoriesModelFromList(String categoryString) {
        try {
            String[] temp = categoryString != null ? categoryString.split("\\.") : new String[0];
            AdBrixRm.CommerceCategoriesModel categories = new AdBrixRm.CommerceCategoriesModel();

            if (temp.length == 1) return categories.setCategory(temp[0]);
            else if (temp.length == 2) return categories.setCategory(temp[0]).setCategory(temp[1]);
            else if (temp.length == 3)
                return categories.setCategory(temp[0]).setCategory(temp[1]).setCategory(temp[2]);
            else if (temp.length == 4)
                return categories.setCategory(temp[0]).setCategory(temp[1]).setCategory(temp[2]).setCategory(temp[3]);
            else if (temp.length == 5)
                return categories.setCategory(temp[0]).setCategory(temp[1]).setCategory(temp[2]).setCategory(temp[3]).setCategory(temp[4]);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }

    private static AdBrixRm.AttrModel getAttrModelFromMap(String jsonString) {
        AdBrixRm.AttrModel result = new AdBrixRm.AttrModel();
        if (CommonUtils.isNullOrEmpty(jsonString)){
            return result;
        }
        JSONObject jsonObject = null;
        try {
            jsonObject = new JSONObject(jsonString);
        } catch (JSONException e) {

        }
        if(jsonObject == null){
            return result;
        }
        return getAttrModelFromMap(jsonObject);
    }

    private static AdBrixRm.AttrModel getAttrModelFromMap(JSONObject jsonObject) {
        AdBrixRm.AttrModel result = new AdBrixRm.AttrModel();
        if(jsonObject == null){
            return result;
        }
        Iterator<String> keys = jsonObject.keys();

        while(keys.hasNext()) {
            String key = keys.next();

            try {
                result.setAttrs(key, jsonObject.get(key));
            } catch (JSONException e) {

            }
        }
        return result;
    }

    private static AdBrixRm.CiProperties toCiProperties(JSONObject jsonObject) {
        if (jsonObject == null)
            return new AdBrixRm.CiProperties();

        AdBrixRm.CiProperties ciProperties = new AdBrixRm.CiProperties();
        Iterator<String> keys = jsonObject.keys();

        while(keys.hasNext()) {
            String key = keys.next();

            try {
                ciProperties.setAttrs(key, jsonObject.get(key));
            } catch (JSONException e) {

            }
        }

        return ciProperties;
    }

    public static Map<String, String> getMapFromJSONObject(JSONObject object) {
        Map<String, String> map = new HashMap<>();
        Iterator<String> keysItr = object.keys();
        try {
            while (keysItr.hasNext()) {
                String key = keysItr.next();
                String value = object.getString(key);
                map.put(key, value);
            }
        } catch (JSONException e) {
            AbxLog.e(e, true);
        }
        return map;
    }

    public static AdBrixRm.UserProperties getUserPropertiesFromMap(String jsonString){
        AdBrixRm.UserProperties result = new AdBrixRm.UserProperties();
        JSONObject jsonObject = null;
        try {
            jsonObject = new JSONObject(jsonString);
        } catch (JSONException e) {

        }
        if(jsonObject == null){
            return result;
        }
        Iterator<String> keysItr = jsonObject.keys();
        try {
            while (keysItr.hasNext()) {
                String key = keysItr.next();
                Object value = jsonObject.get(key);
                result.setAttrs(key, value);
            }
        } catch (JSONException e) {
            AbxLog.e(e, true);
        }
        return result;
    }
    public static List<String> getListFromJson(String json){
        List<String> result = new ArrayList<>();
        JSONArray array = null;
        try {
            array = new JSONArray(json);
        } catch (JSONException e) {

        }
        if(array == null){
            return result;
        }
        for(int i=0; i<array.length(); i++){
            try {
                result.add(array.getString(i));
            } catch (JSONException e) {

            }
        }
        return result;
    }
    public static void executeMainThread(Runnable runnable){
        UnityPlayer.currentActivity.runOnUiThread(runnable);
    }
    //endregion
}
