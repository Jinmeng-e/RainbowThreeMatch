package com.igaworks.v2.unity;

import android.util.Log;

import com.igaworks.v2.core.AdBrixRm;
import com.igaworks.v2.core.application.AbxApplication;
import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import io.adbrix.sdk.component.AbxLog;
import io.adbrix.sdk.domain.model.Empty;
import io.adbrix.sdk.domain.model.Result;
import io.adbrix.sdk.utils.CommonUtils;

public abstract class AbxUnityApplication extends AbxApplication {
    public static GameObjectNameReceiveListener gameObjectNameReceiveListener;

    @Override
    public void onCreate() {
        super.onCreate();
        registerListener();
    }

    abstract public void registerListener();

    public void setGameObjectNameReceiveListener(GameObjectNameReceiveListener listener){
        AbxUnityApplication.gameObjectNameReceiveListener = listener;
    }
    public void unregisterGameObjectNameReceiveListener(){
        AbxUnityApplication.gameObjectNameReceiveListener = null;
    }

    public void setDeeplinkListener(String callbackObjectName, String callbackMethodName) {
        AdBrixRm.setDeeplinkListener(new AdBrixRm.DeeplinkListener() {
            @Override
            public void onReceiveDeeplink(String uriStr) {
                sendMessageToUnity(callbackObjectName, callbackMethodName, uriStr);
            }
        });
    }
    public void unregisterDeeplinkListener(){
        AdBrixRm.setDeeplinkListener(null);
    }
    public void setDeferredDeeplinkListener(String callbackObjectName, String callbackMethodName) {
        AdBrixRm.setDeferredDeeplinkListener(new AdBrixRm.DeferredDeeplinkListener() {
            @Override
            public void onReceiveDeferredDeeplink(String uriStr) {
                sendMessageToUnity(callbackObjectName, callbackMethodName, uriStr);
            }
        });
    }
    public void unregisterDeferredDeeplinkListener(){
        AdBrixRm.setDeferredDeeplinkListener(null);
    }
    public void setLocalPushMessageListener(String callbackObjectName, String callbackMethodName) {
        AdBrixRm.setLocalPushMessageListener(new AdBrixRm.onTouchLocalPushListener() {
            @Override
            public void onTouchLocalPush(String callbackJsonString) {
                sendMessageToUnity(callbackObjectName, callbackMethodName, callbackJsonString);
            }
        });
    }
    public void unregisterLocalPushMessageListener(){
        AdBrixRm.setLocalPushMessageListener(null);
    }
    public void setRemotePushMessageListener(String callbackObjectName, String callbackMethodName) {
        AdBrixRm.setRemotePushMessageListener(new AdBrixRm.onTouchRemotePushListener() {
            @Override
            public void onTouchRemotePush(String callbackJsonString) {
                sendMessageToUnity(callbackObjectName, callbackMethodName, callbackJsonString);
            }
        });
    }
    public void unregisterRemotePushMessageListener(){
        AdBrixRm.setRemotePushMessageListener(null);
    }
    public void setInAppMessageClickListener(String callbackObjectName, String callbackMethodName) {
        AdBrixRm.setInAppMessageClickListener(new AdBrixRm.InAppMessageClickListener() {
            @Override
            public void onReceiveInAppMessageClick(String actionId, String actionType, String actionArg, boolean isClosed) {
                JSONObject jsonObject = new JSONObject();
                try {
                    jsonObject.put("actionId", actionId);
                    jsonObject.put("actionType", actionType);
                    jsonObject.put("actionArg", actionArg);
                    jsonObject.put("isClosed", isClosed);
                } catch (JSONException e) {
                    AbxLog.e("onReceiveInAppMessageClick() parse error",e, false);
                }
                sendMessageToUnity(callbackObjectName, callbackMethodName, jsonObject.toString());
            }
        });
    }
    public void unregisterInAppMessageClickListener(){
        AdBrixRm.setInAppMessageClickListener(null);
    }

    public void setDfnInAppMessageAutoFetchListener(String callbackObjectName, String callbackMethodName) {
        AdBrixRm.setDfnInAppMessageAutoFetchListener(new AdBrixRm.DfnInAppMessageAutoFetchListener() {
            @Override
            public void onFetchInAppMessage(Result<Empty> result) {
                sendMessageToUnity(callbackObjectName, callbackMethodName, result.toString());
            }
        });
    }
    public void unregisterDfnInAppMessageAutoFetchListener(){
        AdBrixRm.setDfnInAppMessageAutoFetchListener(null);
    }
    public void setLogListener(String callbackObjectName, String callbackMethodName) {
        if(CommonUtils.isNullOrEmpty(callbackObjectName)){
            AbxLog.d("callbackObjectName is null or empty", false);
            return;
        }
        if(CommonUtils.isNullOrEmpty(callbackMethodName)){
            AbxLog.d("callbackMethodName is null or empty", false);
            return;
        }
        AdBrixRm.setLogListener(new AdBrixRm.LogListener() {
            @Override
            public void onPrintLog(int level, String message) {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.append("[");
                switch (level){
                    case Log.VERBOSE:
                        stringBuilder.append("V");
                        break;
                    case Log.DEBUG:
                        stringBuilder.append("D");
                        break;
                    case Log.INFO:
                        stringBuilder.append("I");
                        break;
                    case Log.ASSERT:
                        stringBuilder.append("A");
                        break;
                    case Log.WARN:
                        stringBuilder.append("W");
                        break;
                    case Log.ERROR:
                        stringBuilder.append("E");
                        break;
                }
                stringBuilder.append("] ");
                stringBuilder.append(message);
                sendMessageToUnity(callbackObjectName, callbackMethodName, stringBuilder.toString());
            }
        });
    }
    public void unregisterLogListener(){
        AdBrixRm.setLogListener(null);
        AbxLog.setLogObserver(null);
    }

    public void unregisterListenersForChangingGameObjectName(){
        unregisterDeeplinkListener();
        unregisterDeferredDeeplinkListener();
        unregisterLocalPushMessageListener();
        unregisterRemotePushMessageListener();
        unregisterInAppMessageClickListener();
        unregisterDfnInAppMessageAutoFetchListener();
        unregisterLogListener();
    }

    private void sendMessageToUnity(String callbackObjectName, String callbackMethodName, String message){
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

    public interface GameObjectNameReceiveListener {
        void onReceive(String gameObjectName);
    }
}
