using UnityEngine;
using System.Collections;

public class EnemyAI_Attack : MonoBehaviour {
	public GameObject target;
	public float attackTimer;
	public float coolDown;
	
	// Use this for initialization
	void Start () {
		attackTimer = 0.0f;
		coolDown = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		//Every frame the attackTimer is greater than zero it gets reduce the time that it took to render the frame
		if (attackTimer > 0)
			attackTimer -= Time.deltaTime;
		// We don't want our attackTime to go below zero
		if (attackTimer < 0)
			attackTimer = 0;
		
		if(attackTimer == 0)
		{
			_Attack();
			//Everytime we attack, attackTimer is set to cool down. And keeps decreasing every frame until reach zero
			attackTimer = coolDown;
		}
	}
	
	private void _Attack()
	{									//Distance between Our target transform and our transform
		float distance = Vector3.Distance (target.transform.position,transform.position);
		
		
		//Takes the position of target and our position and create a vector. Makes it 1 unit of law
		Vector3 dir = (target.transform.position- transform.position).normalized;
		//To tell the direction of player to the enemy. 1 is infront of player, -1 is behind. 0 is beside
		float direction = Vector3.Dot (dir, transform.forward);
		
		Debug.Log (distance);
		if (distance < 2.5f) 
		{
			if(direction > 0)
			{
//				PlayerHealth ph = (PlayerHealth)target.GetComponent ("PlayerHealth");
//				ph.AdjustCurrentHealth (-10);
			}
		}
	}
}
