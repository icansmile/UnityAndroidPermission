using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_ANDROID
public class UniAndroidPermission : MonoBehaviour {

    private Action permitCallBack;
    private Action notPermitCallBack;
    private Action backFromSettingCallBack;

    const string PackageClassName = "com.nick.unipermissionplugin.UnityPermissionManager";
    AndroidJavaClass permissionManager;

    private static UniAndroidPermission instance = null;
    public static UniAndroidPermission Instance{
        get{
            if(instance == null){
                var go = new GameObject("UniAndroidPermission");
                instance = go.AddComponent<UniAndroidPermission>();
            }

            return instance;
        }
    }

    void Awake(){
        DontDestroyOnLoad (gameObject);
    }

    public bool IsPermitted(List<AndroidPermission> permissions){
#if UNITY_EDITOR
		return true;
#elif UNITY_ANDROID
        AndroidJavaClass permissionManager = new AndroidJavaClass (PackageClassName);
        return permissionManager.CallStatic<bool> ("hasPermissions", GetPermissionStr(permissions));
#endif
    }

    public void RequestPremission(List<AndroidPermission> permissions, Action onPermit = null, Action notPermit = null){
#if UNITY_EDITOR
		return;
#elif UNITY_ANDROID
        AndroidJavaClass permissionManager = new AndroidJavaClass (PackageClassName);
        permissionManager.CallStatic("requestPermissions", GetPermissionStr(permissions));
        permitCallBack = onPermit;
        notPermitCallBack = notPermit;
        return;
#endif
    }

	public bool IsNeverAsk(AndroidPermission permission){
#if UNITY_EDITOR
		return false;
#elif UNITY_ANDROID
		AndroidJavaClass permissionManager = new AndroidJavaClass (PackageClassName);
        return permissionManager.CallStatic<bool>("isPermissionNeverAsk", GetPermissionStr(new List<AndroidPermission>(){permission}));
#endif
	}

	public void GoSetting(Action backFromSetting){
#if UNITY_EDITOR
		return;
#elif UNITY_ANDROID
		AndroidJavaClass permissionManager = new AndroidJavaClass (PackageClassName);
		backFromSettingCallBack = backFromSetting;
        permissionManager.CallStatic("goToAppSettings");
		return;
#endif

	}

    private string GetPermissionStr(List<AndroidPermission> permissions){
		var str = "";

		foreach(var p in permissions){
			str += ";" + "android.permission." + p.ToString ();
		}

		if(str.Length > 0){
			str = str.Substring(1, str.Length - 1);
		}

		return str;
    }

    private void OnPermit(){
        if (permitCallBack != null) {
            permitCallBack ();
        }
    }

    private void NotPermit(){
        if (notPermitCallBack != null) {
            notPermitCallBack ();
        }
    }

    private void BackFromSetting(){
        if (backFromSettingCallBack!= null) {
            backFromSettingCallBack ();
        }
    }

    private void ResetCallBacks(){
        notPermitCallBack = null;
        permitCallBack = null;
    }
}

// Protection level: dangerous permissions 2015/11/25
// http://developer.android.com/intl/ja/reference/android/Manifest.permission.html
public enum AndroidPermission{
    ACCESS_COARSE_LOCATION,
    ACCESS_FINE_LOCATION,
    ADD_VOICEMAIL,
    BODY_SENSORS,
    CALL_PHONE,
    CAMERA,
    GET_ACCOUNTS,
    PROCESS_OUTGOING_CALLS,
    READ_CALENDAR,
    READ_CALL_LOG,
    READ_CONTACTS,
    READ_EXTERNAL_STORAGE,
    READ_PHONE_STATE,
    READ_SMS,
    RECEIVE_MMS,
    RECEIVE_SMS,
    RECEIVE_WAP_PUSH,
    RECORD_AUDIO,
    SEND_SMS,
    USE_SIP,
    WRITE_CALENDAR,
    WRITE_CALL_LOG,
    WRITE_CONTACTS,
    WRITE_EXTERNAL_STORAGE
}
#endif
