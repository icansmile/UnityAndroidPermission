reference: https://github.com/sanukin39/UniAndroidPermission

# UnityAndroidPermission
AndroidM permission request
## Installation
import UniPermission.unity
## Usage
1 add  <meta-data android:name="unityplayer.SkipPermissionsDialog" android:value="true" />  to your AndroidManifest.xml
```
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" package="com.company.product">
  <application android:icon="@drawable/app_icon" android:label="@string/app_name">
    <activity android:name="com.nick.unipermissionplugin.Example"
             android:label="@string/app_name"
             android:configChanges="fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen">
        <intent-filter>
            <action android:name="android.intent.action.MAIN" />
            <category android:name="android.intent.category.LAUNCHER" />
        </intent-filter>
    </activity>
    <meta-data android:name="unityplayer.SkipPermissionsDialog" android:value="true" />
  </application>
  <uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" /> 
  <uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" /> 
</manifest>
```
2 check permission
```
UniAndroidPermission.Instance.IsPermitted(AndroidPermission.WRITE_EXTERNAL_STORAGE)
```
3.request permissions
```
UniAndroidPermission.Instance.RequestPremission(permissions,
			() => {
				//permissions permitted
			},
			() => {
				//some permissions not permitted
				//show tip there, then request again
			});
```
4 check if it can show dialog, maybe the user select "never ask me again"
```
UniAndroidPermission.Instance.IsNeverAsk(p)
```
5 go to app settings page, if the permission is necessary and user select "never ask", then you can do it
```
UniAndroidPermission.Instance.GoSetting(() => {
			  //back from app settings page, check if the permission is permitted
}); 
```
