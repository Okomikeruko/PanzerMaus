using UnityEngine;
using System.Collections;

public class FireEventControl : MonoBehaviour {

	public delegate void FireEvent();
	public static event FireEvent fireEvent;

	public void Fire()
	{
		if (fireEvent != null)
		{
			fireEvent();
		}
	}
}
