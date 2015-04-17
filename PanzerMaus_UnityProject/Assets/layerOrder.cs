using UnityEngine;
using System.Collections;

public class layerOrder : MonoBehaviour {

	public int Layer;

	void Start()
	{
		particleSystem.renderer.sortingOrder = Layer;
	}
}
