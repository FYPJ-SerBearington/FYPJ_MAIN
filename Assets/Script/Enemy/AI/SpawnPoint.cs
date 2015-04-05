using UnityEngine;
using System.Collections;

[AddComponentMenu("Spawn Points Mob/Object/ Spawn Point")]
public class SpawnPoint : MonoBehaviour {
	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 2);
	}

}
