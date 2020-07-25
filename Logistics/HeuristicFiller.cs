using UnityEngine;
using System.Collections.Generic;
using System;

class HeuristicFiller<T> {
	// 
	// LinkedList<T> list;
	// Func<float, T> Init;
	// Func<float, bool> Eval;
	//
	//
	// // change to constructor
	// public void HeuristicFill(LinkedList<T> list, Func<float, T> Init, Func<float, bool> Eval) {
	// 	FillRec(list.First, 0f, 1f);
	// }
	//
	// void FillRec(LinkedListNode<T> prev, float prevTime, float nextTime) {
	// 	float midTime = 0.5f * (prevTime + nextTime);
	// 	LinkedListNode<T> mid = Init(midTime);
	// 	list.AddAfter(prev, mid);
	// 	if (Eval(prev)) FillRec(prev, prevTime, midTime);
	// 	if (Eval(mid)) FillRec(mid, midTime, nextTime);
	// }
}
