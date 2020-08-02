using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
class Skill : ScriptableObject {
	public new string name;
	public int damage;
}
