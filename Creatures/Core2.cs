#pragma warning disable 0649
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

class Core2 : Mover {
	
	[SerializeField]
	protected float bonusSpeed;
	[SerializeField]
	protected CircleCollider2D vision;
	Dictionary<Rigidbody2D, Probe2> links = new Dictionary<Rigidbody2D, Probe2>();
	HashSet<Rigidbody2D> targets = new HashSet<Rigidbody2D>();
	HashSet<Probe2> probes = new HashSet<Probe2>();
	internal void AddProbe(Probe2 probe) { probes.Add(probe); }
	
	protected override void FixedUpdate() {
		this.speed = bonusSpeed;
		foreach (Probe2 probe in links.Values) {
			if (probe.body.bodyType == RigidbodyType2D.Static) {
				this.speed += bonusSpeed;
			}
		}
		base.FixedUpdate();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		targets.Add(other.attachedRigidbody);
		AssignGrabs();
	}
	
	void OnTriggerExit2D(Collider2D other) {
		Rigidbody2D target = other.attachedRigidbody;
		if (targets.Contains(target)) {
			targets.Remove(target);
		} else {
			Probe2 probe = links[target];
			probe.Unlink();
			probes.Add(probe);
			links.Remove(target);
			AssignGrabs();
		}
	}
	
	void AssignGrabs() {
		while (probes.Count > 0 && targets.Count > 0) {
			Probe2 probe = probes.ElementAt(0);
			Rigidbody2D target = targets.ElementAt(0);
			probe.target = target;
			links.Add(target, probe);
			probes.Remove(probe);
			targets.Remove(target);
		}
		foreach (Probe2 probe in probes) {
			probe.target = body;
		}
	}
}
