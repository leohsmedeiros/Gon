using UnityEngine;
using System.Collections;

public class FireMagicBehavior : MonoBehaviour {

	float cronometer = 0;
	float timeToApplyDamage = 0;

//	public void DestroyInSecods(int sec)
//	{
//		Destroy (this.gameObject, sec);
//	}

	void OnTriggerEnter2D(Collider2D other) {
		//		Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "Enemy") {
			cronometer += Time.deltaTime;

			if(cronometer > timeToApplyDamage)
			{

				other.transform.GetComponent<EnemyArchersBehavior>().HurtEnemy();
				cronometer = 0;
//				Debug.Log("Damage");
			}
		}
		
	}
	
	
	void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.tag == "Enemy")
		{
			cronometer = 0;
		}
		
	}
}
