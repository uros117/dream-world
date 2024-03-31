using UnityEngine;
using System.Collections;
using UnityEditor.Animations;

public class EnemyFlying : MonoBehaviour {

	public GameObject player_game_object;
	public float stop_distance_from_player = 0.5f;

	public float life = 10;
	private bool isPlat;
	private bool isObstacle;
	private Transform fallCheck;
	private Transform wallCheck;
	public LayerMask turnLayerMask;
	private Rigidbody2D rb;

	private bool facingRight = true;
	
	public float speed = 5f;
	public float speedNormal = 5f;
	public float speedDream = 7f;

	public AnimatorController animatorNormal;
	public AnimatorController animatorDream;

	public bool isInvincible = false;
	private bool isHitted = false;
	private bool isBackingOff = false;

	private float startedBackingOff;

	public float playerDetectionRange = 20f;

	const int lifeFantasy = 10;
	const int lifePhobia = 4;



	void Awake () {
		fallCheck = transform.Find("FallCheck");
		wallCheck = transform.Find("WallCheck");
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if ((player_game_object.transform.position - transform.position).magnitude > playerDetectionRange)
			return;

        //Quaternion rotation = Quaternion.LookRotation(
        //    player_game_object.transform.position - transform.position,
        //    transform.TransformDirection(Vector3.up)
        //);
        //transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

		float distance_from_player = (player_game_object.transform.position - transform.position).magnitude;

        if (life <= 0) {
			transform.GetComponent<Animator>().SetBool("IsDead", true);
			StartCoroutine(DestroyEnemy());
		}

		//isPlat = Physics2D.OverlapCircle(fallCheck.position, .2f, 1 << LayerMask.NameToLayer("Default"));
		isPlat = true;
		isObstacle = Physics2D.OverlapCircle(wallCheck.position, .2f, turnLayerMask);

		if (life > 0)
		{
			Vector3 temp = rb.velocity;
			if (isBackingOff)
			{
				rb.velocity = -(player_game_object.transform.position - transform.position).normalized * speed * (1.0f - (Time.time - startedBackingOff));
			} else
			{
				rb.velocity = (player_game_object.transform.position - transform.position).normalized * speed;
			}

			if(Mathf.Sign(temp.x) != Mathf.Sign(rb.velocity.x) && Mathf.Sign(temp.x) != 0 && Mathf.Sign(rb.velocity.x) != 0)
			{
				Flip();
			}


			//Debug.Log(rb.velocity);

			// rb.velocity = rb.transform.right * (-speed); // new Vector2(speed, rb.velocity.y);
			//if (isPlat && !isObstacle && !isHitted && distance_from_player > stop_distance_from_player)
			//{

			//	//rb.velocity = new Vector2(speed, rb.velocity.y);
			//	//if (facingRight)
			//	//{
			//	//	rb.velocity = new Vector2(-speed, rb.velocity.y);
			//	//}
			//	//else
			//	//{
			//	//	rb.velocity = new Vector2(speed, rb.velocity.y);
			//	//}

			//	rb.velocity = rb.transform.right * -speed; // new Vector2(speed, rb.velocity.y);
			//}
			//else
			//{
			//	//Flip();
			//}
		}
	}

	void Flip (){
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void ApplyDamage(float damage) {
		if (!isInvincible) 
		{
			float direction = damage / Mathf.Abs(damage);
			damage = Mathf.Abs(damage);
			transform.GetComponent<Animator>().SetBool("Hit", true);
			life -= damage;
			rb.velocity = Vector2.zero;
			if (life > 0) rb.AddForce(new Vector2(direction * 500f, 100f));
			else GetComponent<Rigidbody2D>().simulated = false;
			StartCoroutine(HitTime());
			StartCoroutine(BackOffTime());
		}
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && life > 0)
		{
			collision.gameObject.GetComponent<CharacterController2D>().ApplyDamage(2f, transform.position);
			StartCoroutine(BackOffTime());
		}
	}

	IEnumerator HitTime()
	{
		isHitted = true;
		isInvincible = true;
		yield return new WaitForSeconds(0.1f);
		isHitted = false;
		isInvincible = false;
	}
	IEnumerator BackOffTime()
	{
		startedBackingOff = Time.time;
		isBackingOff = true;
		//isInvincible = true;
		yield return new WaitForSeconds(1.0f);
		isBackingOff = false;
		//isInvincible = false;
	}

	IEnumerator DestroyEnemy()
	{
		CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
		capsule.size = new Vector2(1f, 0.25f);
		capsule.offset = new Vector2(0f, -0.8f);
		capsule.direction = CapsuleDirection2D.Horizontal;
		yield return new WaitForSeconds(0.25f);
		rb.velocity = new Vector2(0, rb.velocity.y);
		yield return new WaitForSeconds(3f);
		Destroy(gameObject);
	}

	public void swapToFantasy()
	{
		life = life * lifeFantasy / lifePhobia;
		speed = speedNormal;
		GetComponent<CapsuleCollider2D>().enabled = true;
		GetComponent<PolygonCollider2D>().enabled = false;
		GetComponent<Animator>().runtimeAnimatorController = animatorNormal;
	}

    public void swapToPhobia()
    {
        life = life * lifePhobia / lifeFantasy;
        speed = speedDream;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = true;
        GetComponent<Animator>().runtimeAnimatorController = animatorDream;
    }
}
