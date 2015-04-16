using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {
	private KartController kartController;
	private int navCounter;
	private float forwardAngle;
	private float rightAngle;
	private GameObject[] navPoints;
	private bool backing;
	public Vector3 randomizedNavPoint;

	public Vector3 heading;


	// Use this for initialization
	void Start () {
		kartController = gameObject.GetComponent<KartController> ();
		navCounter = 0;
		navPoints = GameObject.FindGameObjectWithTag ("navPointsList").GetComponent<navPointsList>().NavList;
		backing = false;
		RandomizeNavPoint(navPoints[navCounter].transform.position, navPoints[navCounter].GetComponent<SphereCollider>().radius / 2);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		heading = randomizedNavPoint - gameObject.transform.position;
		forwardAngle = Vector3.Angle(heading, transform.forward);
		rightAngle = Vector3.Angle(heading, transform.right);
		Debug.DrawLine (randomizedNavPoint, transform.position);

		//If the point is in front of us
		if (forwardAngle < 90 && !backing) {
			if (forwardAngle <= 5){
				Forward ();
			}
			else if (rightAngle <= 90) {
				Right ();
			}
			else{
				Left ();
			}
		}
		else {
			//We use this to keep the kart backing up until it gets a decent angle of attack on the next nav point
			backing = true;
			if (forwardAngle >= 45){
				if(rightAngle <= 90)
					BackLeft ();
			
				if(rightAngle > 90)
					BackRight ();
			} else {
				backing = false;
			}
		}
			
	}

	public void HitNav(GameObject hitNav){
		if (hitNav == navPoints [navCounter]) {
			if (navCounter == navPoints.Length - 1)
				navCounter = 0;
			else
				navCounter++;
			RandomizeNavPoint(navPoints[navCounter].transform.position, hitNav.GetComponent<SphereCollider>().radius / 2);

		}
	}

	void Forward(){
		kartController.gameInput (1, 0);
	}

	void Left(){
		kartController.gameInput (1, -1);
	}

	void Right(){
		kartController.gameInput (1, 1);
	}

	void Back(){
		kartController.gameInput (-1, 0);
	}

	void BackLeft(){
		kartController.gameInput (-1, -1);
	}

	void BackRight(){
		kartController.gameInput (-1, 1);
	}

	void RandomizeNavPoint(Vector3 point , float radius)
	{
		randomizedNavPoint = point;
		randomizedNavPoint.x = randomizedNavPoint.x + Random.Range(-radius,radius);
		randomizedNavPoint.z = randomizedNavPoint.z + Random.Range(-radius,radius);
	}
}
