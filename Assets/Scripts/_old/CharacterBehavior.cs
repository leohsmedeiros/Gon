using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterBehavior : MonoBehaviour {

	public Transform limiteOfScenario;
	public GameplaySceneController controller;
	//public CameraBehavior cameraBv;

	public Transform arrowPrefab;
	public Transform fireMagicPrefab;

	public Transform hand;
	public float arrowDelay=0.3f;

	public float speed=5;
	public float jumpForce;

	public Slider MoveSlider;

	private Animator animator;

	bool isJumping = false;
	public Transform groundCheck;
	private Rigidbody2D rb2d;

	float cronometer = 0;
	float timeToWaitToJump = 1;

	bool shoot = false;
	bool wasShooted = false;
	float cronometerToAttack = 0;

	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	// index 0 = arrow index 1 = fireMagic
	void makeAttack(float delay, bool attackBuffed)
	{
//		yield return new WaitForSeconds(delay);

	}
	
	void Update () {
		if (Input.GetKey (KeyCode.Z))
			Jump ();

		if (shoot) {

			if(!wasShooted)
			{
				animator.SetTrigger ("attack");

				Transform go;
				
				go = Instantiate (arrowPrefab, hand.position, Quaternion.identity) as Transform;
				go.GetComponent<Arrow>().right = (this.transform.localScale.x > 0);
				wasShooted = true;
			}

			cronometerToAttack += Time.deltaTime;

			if(cronometerToAttack > arrowDelay)
			{
				cronometerToAttack = 0;
				wasShooted = false;
				shoot = false;
			}
		}

		Vector3 posCurr = this.transform.position;
		posCurr.x += MoveSlider.value * Time.deltaTime * speed;

		if (posCurr.x < limiteOfScenario.position.x)
			posCurr.x = limiteOfScenario.position.x;
		
		if (posCurr.y > limiteOfScenario.position.y)
			posCurr.y = limiteOfScenario.position.y;

		if (MoveSlider.value != 0) {

			if(MoveSlider.value < 0)
				this.transform.localScale = new Vector3(-1,1,1);
			else
				this.transform.localScale = new Vector3(1,1,1);

			animator.SetBool ("move", true);
		} else {
			posCurr.x = this.transform.position.x;
			animator.SetBool ("move", false);
		}

		this.transform.position = posCurr;
		this.transform.eulerAngles = Vector3.zero;

		if (isJumping) {
			if (!controller.isDoubleJump) {
				cronometer += Time.deltaTime;
				if (cronometer > timeToWaitToJump) {
					cronometer = 0;
					isJumping = false;
				}
			}
			else
			{
				isJumping = false;
			}
		}
	
	}

	public void Jump()
	{
		if (!isJumping || controller.isDoubleJump) {
			isJumping = true;
			rb2d.velocity = (new Vector2 (0, jumpForce));
			this.GetComponent<AudioSource>().Play();
//			rb2d.AddForce (new Vector2 (0, jumpForce), ForceMode2D.Force);
		}
	}

	public void ThrowArrow()
	{
		if (shoot)
			return;

		shoot = true;
//		StartCoroutine (makeAttack (arrowDelay, controller.isAttackBuffed));
	}

	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.tag == "ArrowEnemy") {
			controller.looseLife ();
			Destroy(coll.gameObject);
		}

		if (coll.gameObject.tag == "Coin") {
			controller.AddCoin(1);
			Destroy(coll.gameObject);
		}
		
		if (coll.gameObject.tag == "Heart") {
			controller.red_crystal_button (1);
			Destroy(coll.gameObject);
		}
		
		if (coll.gameObject.tag == "Mana") {
			controller.blue_crystal_button (1);
			Destroy(coll.gameObject);
		}

		if (coll.gameObject.tag == "DeathZone") {
			controller.looseLife(controller.lifesFromGUI.Length);
		}
		
		if (coll.gameObject.tag == "Fire") {
			controller.looseLife(controller.lifesFromGUI.Length);
		}
	}

}
