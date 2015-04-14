using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class boom : MonoBehaviour {

	public Sprite blast, scorch;

	private Texture2D current;
	private SpriteRenderer spriteRenderer;
	private MeshRenderer meshRenderer;
	List<SpriteSlicer2DSliceInfo> sliceReference; 

	void Start () {
		FireEventControl.explosionEvent += ExplodingEvent;
	}

	void ExplodingEvent(Explosion data){
		if (data.col.gameObject.tag == "Destructable") {
			blowItUp (blast, data.col.gameObject, data);
		} else {
			blowItUp (scorch, data.col.gameObject, data);
		}
		MoveManager.EndMove();
	}

	void blowItUp(Sprite b, GameObject g, Explosion data){
		
		if (g.GetComponent<SpriteRenderer>() != null){
			spriteRenderer = g.GetComponent<SpriteRenderer>();
			current = (Texture2D)spriteRenderer.sprite.texture;
		} else if (g.GetComponent<MeshRenderer>() != null) {
			meshRenderer = g.GetComponent<MeshRenderer>();
			current = (Texture2D)meshRenderer.materials[0].GetTexture(0);
		}
		Vector3 offsetV = g.transform.InverseTransformPoint(data.point);
		int offsetX = (int)((-offsetV.x) * 100) - (current.width - b.texture.width) / 2; 
		int offsetY = (int)((-offsetV.y) * 100) - (current.height - b.texture.height) / 2;
		Texture2D output = new Texture2D (current.width, current.height);
		
		int j = 0;
		while (j < output.height) {
			int i = 0;
			while (i < output.width){
				Color original = current.GetPixel(i, j);
				Color color = b.texture.GetPixel(offsetX + i, offsetY + j);
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