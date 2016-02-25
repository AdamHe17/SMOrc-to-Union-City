using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour {


	public PlayerReady ready;
	public PlayerStamina stamina;
	public PlayerHealth health;
	public PlayerReady enemyread;

	public void TheClick(int attackno)
	{
		float stam = 0;
		float damagemin =0;
		float damagemax =0;
		float hitstun = 0;
		switch (attackno) {
		case 1:
			stam = 10;
			damagemin = 5;
			damagemax = 7;
			hitstun = 20;
			break;
		case 2:
			stam = 20;
			damagemin = 10;
			damagemax = 16;
			hitstun = 25;
			break;
		case 3:
			stam = 0;
			damagemin = 1;
			damagemax = 2;
			hitstun = 50;
			break;
		case 4:
			stam = 80;
			damagemin = 30;
			damagemax = 60;
			hitstun = 100;
			break;

		}
		if (ready.cur_ready == 100 && stamina.cur_stamina > stam) {
			ready.cur_ready = 0;
			stamina.Regen(-stam);
			health.Damage (damagemin, damagemax);
			enemyread.Regen (-hitstun);
		}

	}
}
