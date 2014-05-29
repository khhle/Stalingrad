using UnityEngine;
using System.Collections;

public class tank_script : MonoBehaviour {

	//private WeaponScript[] weapons;
	private WeaponScript weapon;
	public int angle_type;
	private bool isAttack = false;
	public bool isRepeat = false;
	public bool isInit = false;
	void Awake()
	{
		weapon = this.GetComponentInChildren<WeaponScript> ();
		//weapons = GetComponentsInChildren<WeaponScript>();
	}

	// Use this for initialization
	void Start () {
		//foreach (WeaponScript weapon in weapons)
		//{
		//	weapon.enabled = false;
		//}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void attack(){

			if (weapon != null )
			{
				weapon.angle_type = angle_type;
			weapon.changeAngle();
				weapon.isRepeat = isRepeat;
			//weapon.isInit = isInit;
				//isAttack = true;
				//weapon.Attack(true);
				//SoundEffectsHelper.Instance.MakeEnemyShotSound();
			}

	}
}
