using UnityEngine;
using System.Collections;

public class destructable : MonoBehaviour {

	public Sprite[] states;
	[SerializeField]
	private int Health;
	private int index = 0;
	private int MaxHealth;
	private SpriteRenderer spriteRenderer;

	public MaterialType materialType;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		MaxHealth = Health;

		FireEventControl.explosionEvent += ExplodingEvent;
	}

	void TakeDamage()
	{
		Health--;
		index = (index < states.Length - 1) ? index + 1 : index ;
		spriteRenderer.sprite = states[index];
		this.transform.parent = null;
		if (Health < MaxHealth) {
			while (this.transform.childCount > 0)
			{
				if(this.transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>() == null){
					this.transform.GetChild(0).gameObject.AddComponent<Rigidbody2D>();
				}
				this.transform.GetChild(0).parent = null;
			}
		}
		if(Health <= 0){
			FireEventControl.explosionEvent -= ExplodingEvent;
//			destructable[] children = this.GetComponentsInChildren<destructable>();
//			foreach (destructable d in children)
//			{
//				FireEventControl.explosionEvent -= d.ExplodingEvent;
//			}
			Destroy(this.gameObject);
		}
	}

	public void ExplodingEvent(Explosion data){
		if (Vector3.Distance (transform.position, data.point) <= data.radius) {

			switch (materialType){
			case MaterialType.metal:

				TakeDamage();
				if(GetComponent<Rigidbody2D>() == null){
					gameObject.AddComponent<Rigidbody2D>();
				}
				Vector2 direction = new Vector2(transform.position.x - data.point.x,
				                                Mathf.Abs (transform.position.y - data.point.y)); 
				float falloff = (data.radius - direction.magnitude) / data.radius;
				direction = Vector2.ClampMagnitude(direction, 1);
				rigidbody2D.AddRelativeForce(direction * data.power * falloff, ForceMode2D.Impulse);
				break;
			case MaterialType.paper:
				break;
			default:
				break;
			}
		}
	}
}

public enum MaterialType{
	wood,
	tile,
	metal,
	paper
}
