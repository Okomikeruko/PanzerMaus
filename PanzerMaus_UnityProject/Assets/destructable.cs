﻿using UnityEngine;
using System.Collections;

public class destructable : MonoBehaviour {

	public Sprite[] states;
	[SerializeField]
	private int Health;
	private int index = 0;
	private int MaxHealth;
	private SpriteRenderer spriteRenderer;

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
		if(Health <= 0){
			FireEventControl.explosionEvent -= ExplodingEvent;
			Destroy(this.gameObject);
		}
	}

	void ExplodingEvent(Explosion data){
		if (Vector3.Distance (transform.position, data.point) <= data.radius) {
			TakeDamage();
			if(GetComponent<Rigidbody2D>() == null){
				gameObject.AddComponent<Rigidbody2D>();
			}
			Vector2 direction = new Vector2(transform.position.x - data.point.x,
			                                Mathf.Abs (transform.position.y - data.point.y)); 
			float falloff = (data.radius - direction.magnitude) / data.radius;
			direction = Vector2.ClampMagnitude(direction, 1);
			rigidbody2D.AddRelativeForce(direction * data.power * falloff, ForceMode2D.Impulse);
		}
	}
}
