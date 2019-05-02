using UnityEngine;
using System.Collections;

public class EnemyArchersBehavior : MonoBehaviour {

	Animator anim;
	public float HP;
	public Transform arrowEnemy;

	float chanceToDrop = 15;

	public GameObject[] dropItens;

//	float cronometerToArrow = 1;

	public float timeToNextShoot;

//	CircleCollider2D triggerToShoot;

//	bool playingCoroutine = false;

	bool dying = false;

	public GameObject PlayerBatAttackPrefab;


	GameplaySceneController controller;
	bool animBats = false;

	void Start () {
		anim = this.GetComponent<Animator> ();
		anim.SetTrigger("Idle");
//		triggerToShoot = this.GetComponentInChildren<CircleCollider2D> ();
		controller = GameObject.FindGameObjectWithTag ("Controller").GetComponent<GameplaySceneController> ();
		HP = HP + controller.lifeBonusEnemy;
	}
	
	public void HurtEnemy()
	{
		HP -= 1;
		
		if (HP == 0) {
			StartCoroutine(KillEnemy());
		}
	}

	IEnumerator KillEnemy()
	{
		dying = true;
		anim.SetTrigger("Die");
		yield return new WaitForSeconds (1);
		controller.score ++;

		int percent = Random.Range (0, 100);
		if (percent < chanceToDrop) {
			int index = Random.Range(0,dropItens.Length);
			Instantiate(dropItens[index], this.transform.position, Quaternion.identity);
		}

		Destroy (this.gameObject);
	}

	/*
	void KeyTeste()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			anim.SetBool("Attack",true);
		}
		
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			anim.SetTrigger("Die");
		}
		
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			anim.SetTrigger("Walk");
		}
		
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			anim.SetBool("Attack",false);
			anim.SetTrigger("Idle");
		}
	}
*/

	void animationBatPower()
	{
		GameObject go = Instantiate (PlayerBatAttackPrefab, this.transform.position, Quaternion.identity) as GameObject;
		Destroy (go, 3);
	}

	// Update is called once per frame
	void Update () {

//		KeyTeste ();


		if (controller != null && controller.BatPowerMagicWasUsed()) {
			HP = 0;

			if(!animBats)
			{
				StartCoroutine(KillEnemy());
				animBats = true;
				animationBatPower();
			}
		}

//		if (anim.GetCurrentAnimatorStateInfo (0).tagHash == Animator.StringToHash ("TagAttackState")) {
//			cronometerToArrow += Time.deltaTime;

//			Debug.Log(cronometerToArrow);

//			if (cronometerToArrow > 1) {
//				cronometerToArrow = 0;

//				if(!playingCoroutine && !dying)
//				{
//					makeAttack ();
//					StartCoroutine(waitToShoot());
//					playingCoroutine = true;
//				}
//
//			}
//		} 
//		else {
//			cronometerToArrow = 0;
//		}

	}

	void makeAttack()
	{
		Transform go;
		
		go = Instantiate(arrowEnemy, this.transform.position + new Vector3(-2,0,0), Quaternion.identity) as Transform;
		
		go.GetComponent<Arrow>().right = (this.transform.localScale.x > 0);
	}

//	IEnumerator waitToShoot()
//	{
//		Debug.Log("ENTROU WAIT TO SHOOT");
//		triggerToShoot.enabled = true;
//		playingCoroutine = false;
//	}

	IEnumerator playAnimAttack()
	{
		playingAnimAttack = true;
		anim.SetTrigger("Attack");

//		playingCoroutine = true;

		yield return new WaitForSeconds (0.7f - Time.deltaTime);
		makeAttack ();

//		triggerToShoot.enabled = false;
		if(!dying)
			anim.SetTrigger("Idle");

		yield return new WaitForSeconds (anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 + timeToNextShoot);

		playingAnimAttack = false;
	}

//	void OnTriggerEnter2D(Collider2D other) {
//		//		Debug.Log (other.gameObject.tag);
//		if (other.gameObject.tag == "Player") {
//			anim.SetTrigger("Attack");
//			StartCoroutine(waitToShoot());
//		}
//		
//	}

	bool playingAnimAttack = false;

	void OnTriggerStay2D(Collider2D other) {
//		Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "Player" && (anim.GetCurrentAnimatorStateInfo (0).tagHash != Animator.StringToHash ("TagAttackState"))) {
//			Debug.Log("ENTROU!!");

			if(!dying && !playingAnimAttack)
			{
				StartCoroutine(playAnimAttack());
			}
		}

	}


//	void OnTriggerExit2D(Collider2D other) {
//		if(other.gameObject.tag == "Player")
//		{
////			Debug.Log("ENTROU!!");
//			anim.SetTrigger("Idle");
//		}
//
//	}

}
