using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(LineRenderer))]
public class AimControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public RectTransform rangeCircle;
	public int Steps; 
	public static float WeaponPower;
	public Transform canonTip;

	public delegate Vector3 Vectors();
	public static event Vectors trajectory;

	private float radius {
		get {
			return rangeCircle.sizeDelta.x/2 - GetComponent<RectTransform>().sizeDelta.x/2;
		}
	}
	private Vector3 screenPoint, offset;
	public float power {
		get {
			return Vector3.Distance(transform.position, rangeCircle.position) / radius;
		}
	}

	void Start()
	{
		FireEvent.power += GetPower;
	}

	public float GetPower()
	{
		return power;
	}

	void Update(){
		UpdateTrajectory(canonTip.position, trajectory() * power * WeaponPower, Physics.gravity);
	}

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData) {
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
		                                                                         Input.mousePosition.y,
		                                                                         screenPoint.z));
	}
	#endregion

	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData) {
		Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
		if (transform.localPosition.y <= rangeCircle.localPosition.y) { 
			transform.localPosition = new Vector3(transform.localPosition.x, rangeCircle.localPosition.y, transform.localPosition.z);
		}
		if ( Vector3.Distance(transform.position, rangeCircle.position) > radius){
			transform.position = rangeCircle.position + Vector3.ClampMagnitude(transform.position - rangeCircle.position, radius);
		}
	}
	#endregion

	#region IEndDragHandler implementation
	public void OnEndDrag (PointerEventData eventData) {}
	#endregion

	void UpdateTrajectory(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity){
		float timeDelta = 1f / initialVelocity.magnitude;

		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(Steps);
		lineRenderer.sortingOrder = 5;
		Vector3 position = initialPosition;
		Vector3 velocity = initialVelocity;
		for (int i = 0; i < Steps; ++i)
		{
			lineRenderer.SetPosition(i, new Vector3(position.x, position.y, initialPosition.z));
			position += velocity * timeDelta + 0.5f * gravity * timeDelta * timeDelta;
			velocity += gravity * timeDelta;
		}
	}
}