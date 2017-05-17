using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Example : MonoBehaviour {

	private List<AndroidPermission> permissionsRequired; 

	// Use this for initialization
	void Start () {
		permissionsRequired = new List<AndroidPermission>(){AndroidPermission.WRITE_EXTERNAL_STORAGE, AndroidPermission.ACCESS_FINE_LOCATION};
		requestPermission(permissionsRequired);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void requestPermissionAgain(List<AndroidPermission> permissions){
		foreach(var p in permissions){
			var permission = new List<AndroidPermission>{p};
			if(!UniAndroidPermission.Instance.IsPermitted(permission)){
				if(UniAndroidPermission.Instance.IsNeverAsk(p)){
					UniAndroidPermission.Instance.GoSetting(() => {
						//back from app settings page, check if the permission is permitted
						requestPermission(permissions);
					}); 
					return;
				}
				else{
					requestPermission(permissions);
					return;
				}
			}
		}
	}

	void requestPermission(List<AndroidPermission> permissions){
		if(!UniAndroidPermission.Instance.IsPermitted(permissions)){
			UniAndroidPermission.Instance.RequestPremission(permissions,
			() => {
				//permissions permitted
				Debug.Log("permissions permitted");
			},
			() => {
				//some permissions not permitted
				//show tip there, then request again
				Debug.Log("app need these permissions");
				requestPermissionAgain(permissions);
			});
		}
		else{
			Debug.Log("permissions permitted");
		}
	}
}
