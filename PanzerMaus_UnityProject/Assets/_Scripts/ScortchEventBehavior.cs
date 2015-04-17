using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class ScortchEventBehavior : MonoBehaviour {

	public Sprite blast, scorch;
	
	private Texture2D current;
	private SpriteRenderer spriteRenderer;
	private MeshRenderer meshRenderer;
	List<SpriteSlicer2DSliceInfo> sliceReference; 
	
	void Start () {
		FireEventControl.explosionEvent += ExplodingEvent;
	}
	
	void ExplodingEvent(Explosion data){
		if (Vector3.Distance (transform.position, data.point) <= data.radius) {
			if (this.gameObject.tag == "Destructable") {
				blowItUp (blast, data);
			} else {
				blowItUp (scorch, data);
			}
		}
	}
	
	void blowItUp(Sprite b, Explosion data){
		
		spriteRenderer = GetComponent<SpriteRenderer>();
		current = (Texture2D)spriteRenderer.sprite.texture;

		Vector3 offsetV = transform.InverseTransformPoint(data.point);
		
		int x = (int)((offsetV.x) * 100) + (current.width / 2);
		int y = (int)((offsetV.y) * 100) + (current.height / 2);
		
		TextureData Cur = new TextureData().GetOverlap(current, b.texture, new Vector2(x, y), new Vector2(b.texture.width / 2, b.texture.height / 2), "a");
		TextureData Blast = new TextureData().GetOverlap(current, b.texture, new Vector2(x, y), new Vector2(b.texture.width / 2, b.texture.height / 2), "b");
		Color[] ChangeArea = current.GetPixels(Cur.x, Cur.y, Cur.width, Cur.height); 
		Color[] BlastArea = b.texture.GetPixels(Blast.x, Blast.y, Blast.width, Blast.height);
		
		for (int i = 0; i < ChangeArea.Length; i++){
			ChangeArea[i] *= BlastArea[i];
		}

		Texture2D output = Instantiate(current) as Texture2D;
		output.SetPixels(Cur.x, Cur.y, Cur.width, Cur.height, ChangeArea); 
		output.Apply ();
		
		if(spriteRenderer != null) {
			spriteRenderer.sprite = Sprite.Create (output, 
			                                       spriteRenderer.sprite.textureRect, 
			                                       new Vector2 (0.5f, 0.5f)
			                                       );
			Destroy (GetComponent<PolygonCollider2D>());
			this.gameObject.AddComponent<PolygonCollider2D> ();
		} else if (meshRenderer != null){
			meshRenderer.materials[0].SetTexture(0, output);
		}
		//SpriteSlicer2D.SliceAllSprites(data.start, data.end, data.col.gameObject.tag); 
	}
}