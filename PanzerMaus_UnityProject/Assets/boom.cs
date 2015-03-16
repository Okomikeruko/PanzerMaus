using UnityEngine;
using System.Collections;

public class boom : MonoBehaviour {

	public Sprite blast;

	private Sprite current;
	private SpriteRenderer spriteRenderer;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		FireEventControl.explosionEvent += ExplodingEvent;
	}

	void ExplodingEvent(Explosion data){
		int offsetX = (int)((transform.position.x - data.point.x) * 100);
		int offsetY = (int)((transform.position.y - data.point.y) * 100);
		current = spriteRenderer.sprite;
		Texture2D output = new Texture2D (current.texture.width, current.texture.height);
		int j = 0;
		while (j < output.height) {
			int i = 0;
			while (i < output.width){
				Color original = current.texture.GetPixel(i, j);
				Color color = (blast.texture.GetPixel(offsetX + i, offsetY + j) != null) ? 
					blast.texture.GetPixel(offsetX + i, offsetY + j) : 
						Color.white;
				output.SetPixel(i, j, color * original);
				i++;
			}
			j++;
		}
		output.Apply ();
		current = Sprite.Create (output, 
		                         new Rect (0, 0, output.width, output.height), 
		                         new Vector2 (0.5f, 0.5f));
		spriteRenderer.sprite = current;
	}
}