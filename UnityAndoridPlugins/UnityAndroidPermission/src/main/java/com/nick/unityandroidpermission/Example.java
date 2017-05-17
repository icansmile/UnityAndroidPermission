package com.nick.unityandroidpermission;

import android.content.Intent;
import android.content.pm.PackageManager;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

/**
 * Created by laishencan on 17/5/17.
 */

public class Example extends UnityPlayerActivity{

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        if(requestCode == UnityPermissionManager.appSettingBackRequestCode){
            UnityPlayer.UnitySendMessage(UnityPermissionManager.unityListenerObject, "BackFromSetting", "");
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, String permissions[], int[] grantResults) {
        switch (requestCode) {

            case UnityPermissionManager.permissionRequestCode : {

                boolean permit = true;

                for (int gr: grantResults) {
                    if (gr != PackageManager.PERMISSION_GRANTED) {
                        permit = false;
                        break;
                    }
                }

                UnityPlayer.UnitySendMessage(UnityPermissionManager.unityListenerObject, permit ? "OnPermit" : "NotPermit", "");

                break;
            }
        }
    }
}
