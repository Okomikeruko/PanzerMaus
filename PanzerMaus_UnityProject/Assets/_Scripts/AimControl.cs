using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class AimControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public RectTransform rangeCircle;

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
		if ( Vector3.Distance(transform.position, rangeCircle.position) > radius){
			transform.position = rangeCircle.position + Vector3.ClampMagnitude(transform.position - rangeCircle.position, radius);
		}
	}
	#endregion

	#region IEndDragHandler implementation
	public void OnEndDrag (PointerEventData eventData) {}
	#endregion
}