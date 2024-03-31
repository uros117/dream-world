using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HydraAI: MonoBehaviour
{
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.

	public float life = 10;

	private bool facingRight = true;

	public float speed = 5f; 

	public bool isInvincible = false;
	private bool isHitted = false;

	[SerializeField] private float m_DashForce = 25f;
	private bool isDashing = false;
	private bool isBackingOff = false;

	public GameObject enemy;
	private float distToPlayer;
	private float distToPlayerY;
	public float meleeDist = 1.5f;
	public float rangeDist = 5f;
	private bool canAttack = true;
	private Transform attackCheck;
	public float dmgValue = 4;

	public float dist_to_palyer_treshold = 2f;
	public float dist_to_palyer_treshold_y = 5f;

	private float randomDecision = 0;
	private bool doOnceDecision = true;
	private bool endDecision = false;
	private Animator anim;

	public float playerDetectionRange = 15f;

	void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		attackCheck = transform.Find("AttackCheck").transform;
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if ((enemy.transform.position - transform.position).magnitude > playerDetectionRange)
			return;

		if (life <= 0)
		{
			StartCoroutine(DestroyEnemy());
		}

		else if (enemy != null) 
		{
			if (!isHitted)
			{
				distToPlayer = enemy.transform.position.x - transform.position.x;
				distToPlayerY = enemy.transform.position.y - transform.position.y;

				if (isBackingOff)
				{
					m_Rigidbody2D.velocity = new Vector2(- distToPlayer / Mathf.Abs(distToPlayer) * speed, m_Rigidbody2D.velocity.y);
				}
				else if (Mathf.Abs(distToPlayer) < dist_to_palyer_treshold)
				{

					GetComponent<Rigidbody2D>().velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
					anim.SetBool("IsWaiting", true);
				}
				else if (Mathf.Abs(distToPlayer) > dist_to_palyer_treshold && Mathf.Abs(distToPlayer) < meleeDist && Mathf.Abs(distToPlayerY) < dist_to_palyer_treshold_y)
				{
					GetComponent<Rigidbody2D>().velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
					if ((distToPlayer > 0f && transform.localScale.x < 0f) || (distToPlayer < 0f && transform.localScale.x > 0f)) 
						Flip();
					if (canAttack)
					{
						MeleeAttack();
					}
				}
				else if (Mathf.Abs(distToPlayer) > meleeDist && Mathf.Abs(distToPlayer) < rangeDist)
				{
					anim.SetBool("IsWaiting", false);
					m_Rigidbody2D.velocity = new Vector2(distToPlayer / Mathf.Abs(distToPlayer) * speed, m_Rigidbody2D.velocity.y);
				}
				else
				{
					if (!endDecision)
					{
						if ((distToPlayer > 0f && transform.localScale.x < 0f) || (distToPlayer < 0f && transform.localScale.x > 0f)) 
							Flip();

						if (randomDecision < 0.9f)
							Run();
						else if (randomDecision >= 0.9f && randomDecision < 0.95f)
							StartCoroutine(BackOff());
						else
							Idle();
					}
					else
					{
						endDecision = false;
					}
				}
			}
			else if (isHitted)
			{
				StartCoroutine(BackOff());
			}
		}
		else 
		{
			enemy = GameObject.Find("DrawCharacter");
		}

		if (transform.localScale.x * m_Rigidbody2D.velocity.x > 0 && !m_FacingRight && life > 0)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (transform.localScale.x * m_Rigidbody2D.velocity.x < 0 && m_FacingRight && life > 0)
		{
			// ... flip the player.
			Flip();
		}
	}
	IEnumerator BackOff()
	{
		//anim.SetBool("IsBackingOff", true);

		isBackingOff = true;
		yield return new WaitForSeconds(1f);
		isBackingOff = false;

		EndDecision();
	}

	void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void ApplyDamage(float damage)
	{
		if (!isInvincible)
		{
			float direction = damage / Mathf.Abs(damage);
			damage = Mathf.Abs(damage);
			anim.SetBool("Hit", true);
			life -= damage;
			transform.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			transform.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * 300f, 100f)); 
			StartCoroutine(HitTime());
			//if(life < 0) GetComponent<DropPointOnDeath>().dropPoint();
        }
	}

	public void MeleeAttack()
	{
		transform.GetComponent<Animator>().SetBool("Attack", true);
		//Collider2D[] collidersEnemies = Physics2D.OverlapBoxAll(attackCheck.position, new Vector2(5.0f, 3.0f), 0);
		//for (int i = 0; i < collidersEnemies.Length; i++)
		//{
		//	if (collidersEnemies[i].gameObject.tag == "Enemy" && collidersEnemies[i].gameObject != gameObject )
		//	{
		//		if (transform.localScale.x < 1)
		//		{
		//			dmgValue = -dmgValue;
		//		}
		//		collidersEnemies[i].gameObject.SendMessage("ApplyDamage", dmgValue);
		//	}
		//	else if (collidersEnemies[i].gameObject.tag == "Player")
		//	{
		//		collidersEnemies[i].gameObject.GetComponent<CharacterController2D>().ApplyDamage(2f, transform.position);
		//	}
		//}
		//StartCoroutine(WaitToAttack(2f));
	}

	public void ExtendHeads()
	{
        Collider2D[] collidersEnemies = Physics2D.OverlapBoxAll(attackCheck.position, new Vector2(4.0f, 2.0f), 0);
        for (int i = 0; i < collidersEnemies.Length; i++)
        {
            if (collidersEnemies[i].gameObject.tag == "Enemy" && collidersEnemies[i].gameObject != gameObject)
            {
                if (transform.localScale.x < 1)
                {
                    dmgValue = -dmgValue;
                }
                collidersEnemies[i].gameObject.SendMessage("ApplyDamage", dmgValue);
            }
            else if (collidersEnemies[i].gameObject.tag == "Player")
            {
                collidersEnemies[i].gameObject.GetComponent<CharacterController2D>().ApplyDamage(2f, transform.position);
            }
        }
        StartCoroutine(WaitToAttack(2f));
    }

	public void RangeAttack()
	{
		if (doOnceDecision)
		{
			// No range attaccs for hydra

			//GameObject throwableProj = Instantiate(throwableObject, transform.position + new Vector3(transform.localScale.x * 0.5f, -0.2f), Quaternion.identity) as GameObject;
			//throwableProj.GetComponent<ThrowableProjectile>().owner = gameObject;
			//Vector2 direction = new Vector2(transform.localScale.x, 0f);
			//throwableProj.GetComponent<ThrowableProjectile>().direction = direction;
			//StartCoroutine(NextDecision(0.5f));
		}
	}

	public void Run()
	{
		anim.SetBool("IsWaiting", false);
		m_Rigidbody2D.velocity = new Vector2(distToPlayer / Mathf.Abs(distToPlayer) * speed, m_Rigidbody2D.velocity.y);
		if (doOnceDecision)
			StartCoroutine(NextDecision(0.5f));
	}

	//public void Jump()
	//{
	//	Vector3 targetVelocity = new Vector2(distToPlayer / Mathf.Abs(distToPlayer) * speed, m_Rigidbody2D.velocity.y);
	//	Vector3 velocity = Vector3.zero;
	//	m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, 0.05f);
	//	if (doOnceDecision)
	//	{
	//		anim.SetBool("IsWaiting", false);
	//		m_Rigidbody2D.AddForce(new Vector2(0f, 850f));
	//		StartCoroutine(NextDecision(1f));
	//	}
	//}

	public void Idle()
	{
		m_Rigidbody2D.velocity = new Vector2(0f, m_Rigidbody2D.velocity.y);
		if (doOnceDecision)
		{
			anim.SetBool("IsWaiting", true);
			StartCoroutine(NextDecision(1f));
		}
	}

	public void EndDecision()
	{
		randomDecision = Random.Range(0.0f, 1.0f); 
		endDecision = true;
	}

	IEnumerator HitTime()
	{
		isInvincible = true;
		isHitted = true;
		yield return new WaitForSeconds(0.1f);
		isHitted = false;
		isInvincible = false;
	}

	IEnumerator WaitToAttack(float time)
	{
		canAttack = false;
		yield return new WaitForSeconds(time);
		canAttack = true;
	}

	IEnumerator NextDecision(float time)
	{
		doOnceDecision = false;
		yield return new WaitForSeconds(time);
		EndDecision();
		doOnceDecision = true;
		anim.SetBool("IsWaiting", false);
	}

	IEnumerator DestroyEnemy()
	{
		CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
		capsule.size = new Vector2(1f, 0.25f);
		capsule.offset = new Vector2(0f, -0.8f);
		capsule.direction = CapsuleDirection2D.Horizontal;
		transform.GetComponent<Animator>().SetBool("IsDead", true);


		yield return new WaitForSeconds(0.25f);
		//m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
		m_Rigidbody2D.velocity = new Vector2(0, 0);
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
