using UnityEngine;
using System.Collections;

public class DistructableGround : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	public int MaxHealth = 5;
	public Color HealthyColor, DeathColor;
	private int Health;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		Health = MaxHealth;

		FireEventControl.explosionEvent += ExplodingEvent;
	}

	void TakeDamage()
	{
		Health--;
		float ratio = (float)Health / (float)MaxHealth;
		spriteRenderer.color = Color.Lerp (DeathColor, HealthyColor, ratio);
		if(Health <= 0){
			FireEventControl.explosionEvent -= ExplodingEvent;
			Destroy(this.gameObject);
		}
	}

	void ExplodingEvent(Explosion data){
		if (Vector3.Distance(transform.position, data.point) <= data.radius){
			TakeDamage();
			rigidbody2D.isKinematic = false;
			Vector2 direction = new Vector2(transform.position.x - data.point.x,
			                                Mathf.Abs (transform.position.y - data.point.y)); 
			float falloff = (data.radius - direction.magnitude) / data.radius;
			direction = Vector2.ClampMagnitude(direction, 1);
			rigidbody2D.AddRelativeForce(direction * data.power * falloff, ForceMode2D.Impulse);
		}
	}
}
