using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public float wheel, pinch, mouseDragSpeed, fingerDragSpeed, minFoV, maxFoV;
	public Vector2 min, max;

	private float z;


	public void Update()
	{
#if UNITY_WEBPLAYER
		float scroll = Input.GetAxis ("Mouse ScrollWheel"); 
		if(!camera.isOrthoGraphic){
			camera.fieldOfView = Mathf.Clamp (camera.fieldOfView - (scroll * wheel),
			                                  minFoV,
		    	                              maxFoV);
		} else {
			camera.orthographicSize = Mathf.Clamp (camera.orthographicSize - (scroll * wheel),
			                                       minFoV,
			                                       maxFoV);
		}

#endif

#if UNITY_IOS
		if(Input.touches == 2){
			Touch one = Input.GetTouch(0);
			Touch two = Input.GetTouch(1);
			
			Vector2 onePrev = one.position - one.deltaPosition;
			Vector2 twoPrev = two.position - two.deltaPosition;
			
			float oldMag = (onePrev - twoPrev).magnitude;
			float newMag = (one.position - two.position).magnitude;
			
			float magDiff = oldMag - newMag;
			if (!camera.isOrthoGraphic){
				camera.fieldOfView = Mathf.Clamp (camera.fieldOfView - (magDiff * pinch),
				                                  minFoV,
				                                  maxFoV);
			}else{
				camera.orthographicSize = Mathf.Clamp (camera.orthographicSize - (magDiff * pinch),
				                                       minFoV,
				                                       maxFoV);
			}
		}
#endif
	}

}
