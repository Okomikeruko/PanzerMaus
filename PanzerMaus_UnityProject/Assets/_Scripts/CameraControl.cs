using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public float wheel, pinch, mouseDragSpeed, fingerDragSpeed, minFoV, maxFoV;
	public Vector3[] limit = new Vector3[4];
	public int playerCount;
	public Transform[] tanks;
	public Transform shot;

	public static bool followBullet = false;

	private bool newPlayer = true;
	private float z;
	private Vector3 lastMousePos, lowerLeft, upperRight;
	
	void Start(){
		PlayerTurnControl.cameraRefresh += refreshCamera;
	}

	public void Update()
	{
		float ratio = ClampRange((camera.isOrthoGraphic) ? camera.orthographicSize : camera.fieldOfView, minFoV, maxFoV); 
		lowerLeft = Vector3.Lerp (limit[(int)Limit.ZoomOutBottomLeft], limit[(int)Limit.ZoomInBottomLeft], ratio);
		upperRight = Vector3.Lerp (limit[(int)Limit.ZoomOutTopRight], limit[(int)Limit.ZoomInTopRight], ratio); 


#if UNITY_WEBPLAYER
		float scroll = Input.GetAxis ("Mouse ScrollWheel"); 
		if(!camera.isOrthoGraphic){
			camera.fieldOfView = Mathf.Clamp (camera.fieldOfView - (scroll * wheel), minFoV, maxFoV);
		} else {
			camera.orthographicSize = Mathf.Clamp (camera.orthographicSize - (scroll * wheel), minFoV, maxFoV);
		}
		if (followBullet){
			transform.position = LimitPosition(shot.transform.position, lowerLeft, upperRight);
		}
		else if(PlayerTurnControl.GetMove(PlayerTurnControl.GetTurn()) != 0 || newPlayer)
		{
			newPlayer = false;
			transform.position = LimitPosition(tanks[PlayerTurnControl.GetTurn()].position, lowerLeft, upperRight); 
		}else{
			transform.position = LimitPosition(transform.position, lowerLeft, upperRight);
		}

		if (Input.GetMouseButtonDown (2)){
			lastMousePos = Input.mousePosition;
			return;
		}

		if (!Input.GetMouseButton(2)){
			return;
		}

		Vector3 deltaMousePos = (Input.mousePosition - lastMousePos) * -mouseDragSpeed;
		transform.Translate(deltaMousePos, Space.World);
		transform.position = LimitPosition(transform.position, lowerLeft, upperRight);
		lastMousePos = Input.mousePosition;
#endif

#if UNITY_IOS
		if(Input.touches == 2){
			Touch one = Input.GetTouch(0);
			Touch two = Input.GetTouch(1);
			
			Vector2 onePrev = one.position - one.deltaPosition;
			Vector2 twoPrev = two.position - two.deltaPosition;

			Vector2 center = Vector2.Lerp(one.position, two.position, 0.5f);
			Vector2 deltaCenter = Vector2.Lerp(one.deltaPosition, two.deltaPosition, 0.5f);

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

	private float ClampRange(float num, float min, float max)
	{
		if (max != min && max > 0 && min > 0)
		{
			float diff = max - min;
			float output = (num - min) / diff;
			return Mathf.Abs(1 - output);
		}
		else
		{
			return 0;
		}
	}

	private Vector3 LimitPosition(Vector3 pos, Vector3 low, Vector3 high){
		return new Vector3 (MinMax (pos.x, low.x, high.x),
		                    MinMax (pos.y, low.y, high.y),
		                    MinMax (pos.z, low.z, high.z));
	}

	private float MinMax(float num, float min, float max){
		return (num >= min) ? (num <= max) ? num : max : min ;
	}

	public void SetLimit(Limit l){
		limit[(int)l] = transform.position;
	}

	public void refreshCamera(){
		newPlayer = true;
	}
}

public enum Limit{
	ZoomInBottomLeft,
	ZoomInTopRight,
	ZoomOutBottomLeft,
	ZoomOutTopRight
}