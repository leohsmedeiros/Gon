using UnityEngine;
using System.Collections;

public class ProjectileBehavior : MonoBehaviour {

	public enum typeOfProjectile { Player, Enemy };

	public typeOfProjectile fromWho;

	public GameObject particleArrowHit;

	void InstantiateFire(Transform enemy, int countFires, Vector3 posSum)
	{
		for(int i=0; i<countFires; i++)
		{
			if(countFires == 1) i=1;
			Vector3 newPos = (enemy.position + posSum) + new Vector3((i*countFires)-countFires, 0, 0 );
			GameObject go = Instantiate (this.transform.GetChild (0).gameObject, newPos, Quaternion.identity) as GameObject;
			Destroy(go, 5);
			go.tag = "Fire";
			go.transform.parent = enemy;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			if (this.transform.childCount > 0) {
				InstantiateFire (coll.transform, 3, new Vector3 (0, 4, 0));
			}
			Destroy (this.gameObject);
		} else if (coll.gameObject.tag == "Enemy" && fromWho == typeOfProjectile.Player) {

			if(coll.transform.GetComponent<EnemyArchersBehavior>() != null)
			{
				coll.transform.GetComponent<EnemyArchersBehavior>().HurtEnemy();
				Destroy((GameObject)Instantiate(particleArrowHit, this.transform.position + new Vector3(2,0,0), Quaternion.identity), 5);
				Destroy (this.gameObject);
			}

		} 
	}
}
