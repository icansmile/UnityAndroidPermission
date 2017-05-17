package com.nick.unityandroidpermission;

import android.app.Activity;
import com.unity3d.player.UnityPlayer;

/**
 * Created by laishencan on 17/5/17.
 */

public class UnityPermissionManager {
    public static String unityListenerObject = "UniAndroidPermission";
    public static final int permissionRequestCode = 100;
    public static final int appSettingBackRequestCode = 101;

    public static void setUnityListenerObject(String name){
        unityListenerObject = name;
    }

    public static void requestPermissions(String permissionsStr) {
        Activity content = UnityPlayer.currentActivity;
        String[] permissions = permissionsStr.split(";");

        PermissionManager.requestPermissions(content, permissions, permissionRequestCode);
    }

    public static boolean hasPermission(String permission) {
        Activity content = UnityPlayer.currentActivity;
        return PermissionManager.hasPermission(content, permission);
    }

    public static boolean hasPermissions(String permissionsStr) {
        Activity content = UnityPlayer.currentActivity;
        String[] permissions = permissionsStr.split(";");
        return PermissionManager.hasPermissions(content, permissions);
    }

    public  static boolean isPermissionNeverAsk(String permissionStr) {
        Activity content = UnityPlayer.currentActivity;
        return PermissionManager.isPermissionNeverAsk(content, permissionStr);
    }

    public static void goToAppSettings() {
        Activity content = UnityPlayer.currentActivity;
        PermissionManager.goToAppSettings(content, appSettingBackRequestCode);
    }
}
