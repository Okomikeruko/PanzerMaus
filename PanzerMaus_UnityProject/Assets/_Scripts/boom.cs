using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class boom : MonoBehaviour {

	public Sprite blast;

	private Texture2D current;
	private SpriteRenderer spriteRenderer;
	private MeshRenderer meshRenderer;
	List<SpriteSlicer2DSliceInfo> sliceReference; 

	void Start () {
		FireEventControl.explosionEvent += ExplodingEvent;
	}

	void ExplodingEvent(Explosion data){
		if (data.col.gameObject.tag == "Destructable"){
			if (data.col.gameObject.GetComponent<SpriteRenderer>() != null){
				spriteRenderer = data.col.gameObject.GetComponent<SpriteRenderer>();
				current = (Texture2D)spriteRenderer.sprite.texture;
			} else if (data.col.gameObject.GetComponent<MeshRenderer>() != null) {
				meshRenderer = data.col.gameObject.GetComponent<MeshRenderer>();
				current = (Texture2D)meshRenderer.materials[0].GetTexture(0);
			}
			Vector3 offsetV = data.col.gameObject.transform.InverseTransformPoint(data.point);
			int offsetX = (int)((-offsetV.x) * 100) - (current.width - blast.texture.width) / 2; 
			int offsetY = (int)((-offsetV.y) * 100) - (current.height - blast.texture.height) / 2;
			Texture2D output = new Texture2D (current.width, current.height);
			Debug.Log (output.width + " " + offsetX + " | " + output.height + " " + offsetY);
			int j = 0;
			while (j < output.height) {
				int i = 0;
				while (i < output.width){
					Color original = current.GetPixel(i, j);
					Color color = blast.texture.GetPixel(offsetX + i, offsetY + j);
					output.SetPixel(i, j, color * original);
					i++;
				}
				j++;
			}
			output.Apply ();

			if(spriteRenderer != null) {
				spriteRenderer.sprite = Sprite.Create (output, 
				                                       spriteRenderer.sprite.textureRect, 
				                                       new Vector2 (0.5f, 0.5f)
				                                       );
				Destroy (data.col.gameObject.GetComponent<PolygonCollider2D>());
				data.col.gameObject.AddComponent<PolygonCollider2D> ();
			} else if (meshRenderer != null){
				meshRenderer.materials[0].SetTexture(0, output);
			}
			SpriteSlicer2D.SliceAllSprites(data.start, data.end, data.col.gameObject.tag); 
		}
	}
}