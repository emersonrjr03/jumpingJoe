using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public static class Utils {
		public static List<GameObject> GetChildObjectsByTag(Transform parent, string _tag){
			List<GameObject> actors = new List<GameObject>();
			GetChildObjectsByTagRecursive(parent, _tag, actors);
			return actors;
		}
		public static List<GameObject> GetChildObjectsByTagRecursive(Transform parent, string _tag, List<GameObject> actors) {
			for (int i = 0; i < parent.childCount; i++) {
				Transform child = parent.GetChild(i);
				if (child.tag == _tag) {
				 actors.Add(child.gameObject);
				}
				if (child.childCount > 0) {
				 GetChildObjectsByTagRecursive(child, _tag, actors);
				}
		 	}
		 	return actors;
		}
		
		public static bool AnimatorIsPlaying(Animator animatorController){
	    	return animatorController.GetCurrentAnimatorStateInfo(0).length > animatorController.GetCurrentAnimatorStateInfo(0).normalizedTime;
		}
		public static bool AnimationIsPlaying(Animator animatorController, string stateName){
			return AnimatorIsPlaying(animatorController) && animatorController.GetCurrentAnimatorStateInfo(0).IsName(stateName);
		}
 }
