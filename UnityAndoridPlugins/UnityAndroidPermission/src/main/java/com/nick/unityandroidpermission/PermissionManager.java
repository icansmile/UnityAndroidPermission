package com.nick.unityandroidpermission;

import android.app.Activity;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.net.Uri;
import android.os.Build;
import android.provider.Settings;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.util.Log;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by laishencan on 17/5/17.
 */

public class PermissionManager {
    public static void requestPermissions(Activity content, String[] permissionsStr, int requestCode) {
        String[] permissionDenied = getPermissionDenied(content, permissionsStr).toArray(new String[0]);
        if(getPermissionDenied(content, permissionsStr).size() > 0) {
            ActivityCompat.requestPermissions(content, permissionDenied, requestCode);
        }
    }

    public static boolean hasPermission(Activity content, String permissionStr) {
        if(Build.VERSION.SDK_INT < 23) {
            return true;
        }

        return ContextCompat.checkSelfPermission(content, permissionStr) == PackageManager.PERMISSION_GRANTED;
    }

    public static boolean hasPermissions(Activity content, String[] permissionsStr) {
        if(Build.VERSION.SDK_INT < 23) {
            return true;
        }

        for (String s : permissionsStr) {
            if(!hasPermission(content, s)) {
                return false;
            }
            else{
                Log.d("unity", "hasPermissions: " +s);
            }
        }
        return true;
    }

    public  static boolean isPermissionNeverAsk(Activity content, String permissionStr) {
        if(Build.VERSION.SDK_INT < 23) {
            return false;
        }

        return !ActivityCompat.shouldShowRequestPermissionRationale(content, permissionStr);
    }

    public static void goToAppSettings(Activity content, int requestCode) {
        Intent appSettings = new Intent(Settings.ACTION_APPLICATION_DETAILS_SETTINGS, Uri.parse("package:" + content.getPackageName()));
//        appSettings.addCategory(Intent.CATEGORY_DEFAULT);
        content.startActivityForResult(appSettings, requestCode);
    }

    public static List<String> getPermissionDenied(Activity content, String[] permissionsStr) {
        List<String> permissionsDenied = new ArrayList<String>();

        for (String s : permissionsStr) {
            if(!hasPermission(content, s)) {
                permissionsDenied.add(s);
            }
        }

        return permissionsDenied;
    }
}
