#pragma warning disable 0649 // disables 'never assigned' warnings
using UnityEngine;

class Fighter : MonoBehaviour {
	
	public float health;
	[SerializeField]
	Fighter opponent;
	[SerializeField] // 0 is light, 1 is heavy, 2 is special
	Skill[] skills = new Skill[3];
	
	public void Light() {
		UseSkill(0);
	}
	
	public void Heavy() {
		UseSkill(1);
	}
	
	public void Special() {
		UseSkill(2);
	}
	
	void UseSkill(int index) {
		Skill skill = skills[index];
		Debug.Log("using " + skill);
		opponent.health -= skill.damage;
	}
}
