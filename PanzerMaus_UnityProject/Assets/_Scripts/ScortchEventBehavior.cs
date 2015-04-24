using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class ScortchEventBehavior : MonoBehaviour {

	public Sprite blast, scorch;
	
	private Texture2D current;
	private SpriteRenderer spriteRenderer;
	List<SpriteSlicer2DSliceInfo> sliceReference; 
	
	void Awake () {
		FireEventControl.explosionEvent += ExplodingEvent;
		spriteRenderer = GetComponent<SpriteRenderer>();
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
		Rigidbody2D r = this.gameObject.GetComponent<Rigidbody2D> ();
		Debug.Log (r);
		if (r != null) {
			Debug.Log ("hello");
			r.isKinematic = false;
			Vector2 direction = new Vector2(transform.position.x - data.point.x,
			                                Mathf.Abs (transform.position.y - data.point.y)); 
			float falloff = (data.radius - direction.magnitude) / data.radius;
			direction = Vector2.ClampMagnitude(direction, 1);
			r.AddRelativeForce(direction * data.power * falloff, ForceMode2D.Impulse);
		}
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
		}
		//SpriteSlicer2D.SliceAllSprites(data.start, data.end, data.col.gameObject.tag); 
	}
}