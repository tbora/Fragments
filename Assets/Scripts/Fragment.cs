using UnityEngine;
using System.Collections;

public class Fragment : MonoBehaviour {

	public TextMesh score;

	void OnBecameInvisible() {

		if(score != null){
			int currentScore = int.Parse(score.text);
			currentScore--;
			score.text = currentScore.ToString();
			Destroy(gameObject);
		}
	}
}
