using UnityEngine;
using System.Collections;


public class InputManager : MonoBehaviour {


	public Texture2D cursorImage;
	public GameObject spawner;
	public GameObject fragment;
	public TextMesh score;
	public GameObject gameOver;
	public GameObject Destination;
	public AudioSource Blop;

	private int cursorWidth = 64;
	private int cursorHeight = 64;
	private Vector3 pz;
	private bool spawnerSet;
	private int spawnCount;
	private bool allrigidbodiesSleeping;

	private int blockerCount;

	private Vector3 spawnerCloneCoord;
	
	void Start () {
		Screen.showCursor = false;
		spawnerSet = false;
		blockerCount = 0;

	}

	void Update () {

		if(int.Parse(score.text) != 20){
			gameOver.SetActive(true);

			//Restart
			if(Input.GetKey(KeyCode.Return)){
				Application.LoadLevel(0);
			}
		}



		//Instantiated 20 Fragments
		if(spawnCount == 20){
			CancelInvoke();

			allrigidbodiesSleeping = true;
			foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[]){
				if(gameObj.name.Contains("Fragment(Clone)")){
					if(!gameObj.rigidbody.IsSleeping() || (gameObj.rigidbody.velocity.x > 0.05f && gameObj.rigidbody.velocity.y > 0.05f && gameObj.rigidbody.velocity.z > 0.05f)){
						allrigidbodiesSleeping = false;
					}
				}
			}

			//check if rigidbodys are sleeping and if 20 spheres are in destination

			if(allrigidbodiesSleeping){
					int newScore = Destination.GetComponent<CheckDest>().insideBoxCount;
					score.text = newScore.ToString();
			}

			if(allrigidbodiesSleeping && Destination.GetComponent<CheckDest>().insideBoxCount == 20){
				if(Application.loadedLevel == 3){
					Application.LoadLevel(0);
				} else {
					Application.LoadLevel(Application.loadedLevel+1);
				}
			}
			
			
		}

		// Set Spawner
		if(Input.GetMouseButtonDown(0)){
			if(spawnerSet == false){
				Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				pz.z = 0;
				GameObject spawnerClone = Instantiate(spawner, pz, Quaternion.identity) as GameObject;
				Blop.audio.Play();
				spawnerCloneCoord = spawnerClone.transform.position;
				spawnerSet = true;
				InvokeRepeating("WaitAndShoot", 0.05f, 0.3f); 
			} else {
				//Set Blocker
				if(blockerCount < 1){
					Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					pz.z = 0;
					Instantiate(spawner, pz, Quaternion.identity);
					blockerCount++;
				}
			}
		}
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Input.mousePosition.x - 32, Screen.height - Input.mousePosition.y-32, cursorWidth, cursorHeight), cursorImage);
	}

	void WaitAndShoot()
	{
		GameObject fragmentClone = Instantiate(fragment, spawnerCloneCoord, Quaternion.identity) as GameObject;
		Blop.audio.Play();
		Rigidbody rigidbodyClone = fragmentClone.AddComponent<Rigidbody>();
		rigidbodyClone.drag = 0;
		rigidbodyClone.angularDrag = 0;
		rigidbodyClone.velocity = Vector3.zero;
		spawnCount++;
	}
}
