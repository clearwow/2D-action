using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
	Rigidbody2D rb;
	Animator animator;

	[SerializeField] TextManager text;

	//JUMP
	[SerializeField] private float flap = 550f;
		
	int jumpCount = 0;

	//SE
	[SerializeField] AudioClip jumpSE;
	AudioSource audioSource;

	//左ボタン押下の判定（追加）
	private bool isLButtonDown = false;
	//右ボタン押下の判定（追加）
	private bool isRButtonDown = false;
	//ジャンプボタン押下の判定（追加）
	private bool isJButtonDown = false;

	public float scroll = 5f;
	float direction = 0f;



	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}


	void Update()
	{
		GetInputKey();
		jump();
	}

	void GetInputKey()
	{
		animator.SetFloat("speed", Mathf.Abs(direction));
		if (Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown)
		{
			direction = 1f;
			transform.localScale = new Vector3(1, 1, 1);
		}
		else if (Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown)
		{
			direction = -1f;
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else
		{
			direction = 0f;
		}

		rb.velocity = new Vector2(scroll * direction, rb.velocity.y);

	}

	void jump()
    {
		if ((Input.GetKeyDown("space") || this.isJButtonDown) && jumpCount < 2)
		{
			this.isJButtonDown = false;
			rb.AddForce(Vector2.up * flap);
			jumpCount++;
			audioSource.PlayOneShot(jumpSE);
			animator.SetBool("isJumping", true);
		}
	}



	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Ground")
		{
			jumpCount = 0;
			animator.SetBool("isJumping", false);

		}
	}

	private void  OnTriggerEnter2D(Collider2D collision)
    {
		if(collision.gameObject.tag == "Dead")
        {
			Destroy(this.gameObject);
			text.GameOver();
		}

		if (collision.gameObject.tag == "Clear")
        {
			text.GameClear();
        }
	}

	//ジャンプボタンを押した場合の処理（追加）
	public void GetMyJumpButtonDown()
	{
		this.isJButtonDown = true;
	}


	//左ボタンを押し続けた場合の処理（追加）
	public void GetMyLeftButtonDown()
	{
		this.isLButtonDown = true;
	}
	//左ボタンを離した場合の処理（追加）
	public void GetMyLeftButtonUp()
	{
		this.isLButtonDown = false;
	}

	//右ボタンを押し続けた場合の処理（追加）
	public void GetMyRightButtonDown()
	{
		this.isRButtonDown = true;
	}
	//右ボタンを離した場合の処理（追加）
	public void GetMyRightButtonUp()
	{
		this.isRButtonDown = false;
	}

}


