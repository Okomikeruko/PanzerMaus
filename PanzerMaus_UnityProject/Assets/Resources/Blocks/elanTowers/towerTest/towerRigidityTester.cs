using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class towerRigidityTester : MonoBehaviour {

	public GameObject[] Michael_Towers = new GameObject[2];
	public GameObject[] Ben_Towers = new GameObject[2];
	public GameObject[] Brad_Towers = new GameObject[2];
	public GameObject[] Lee_Towers = new GameObject[2];
	public GameObject[] Elan_Towers = new GameObject[2];
	public GameObject[] Bob_Towers = new GameObject[2];
	public GameObject[] Kevin_Towers = new GameObject[2];
	private GameObject empty;
	public int numberOfHits;
	public Text hitText;

	public GameObject _currentTower;

	void Start () {
		empty = new GameObject();
	}
	

	void Update () {

		hitText.text = numberOfHits.ToString ();
	}
	public void Michael1()
	{
		if (Michael_Towers[0] != null)
		{
			numberOfHits = 0;
		_currentTower.SetActive (false);
		_currentTower = Instantiate (Michael_Towers[0], _currentTower.transform.position, Quaternion.identity) as GameObject;
		_currentTower.SetActive (true);
		}
	}

	public void Michael2()
	{
		if (Michael_Towers[1] != null)
		{
			numberOfHits = 0;
			_currentTower.SetActive (false);
			_currentTower = Instantiate (Michael_Towers[1], _currentTower.transform.position, Quaternion.identity) as GameObject;
			_currentTower.SetActive (true);
		}
	}

	public void Ben1()
	{
		if (Ben_Towers[0] != null)
		{
			numberOfHits = 0;
			_currentTower.SetActive (false);
			_currentTower = Instantiate (Ben_Towers[0], _currentTower.transform.position, Quaternion.identity) as GameObject;
			_currentTower.SetActive (true);
		}
	}

	public void Ben2()
	{
		if (Ben_Towers[1] != null)
		{
			numberOfHits = 0;
			_currentTower.SetActive (false);
			_currentTower = Instantiate (Ben_Towers[1], _currentTower.transform.position, Quaternion.identity) as GameObject;
			_currentTower.SetActive (true);
		}
	}

	public void Brad1()
	{
		if (Brad_Towers[0] != null)
		{
			numberOfHits = 0;
			_currentTower.SetActive (false);
			_currentTower = Instantiate (Brad_Towers[0], _currentTower.transform.position, Quaternion.identity) as GameObject;
			_currentTower.SetActive (true);
		}
	}

	public void Brad2()
	{
		if (Brad_Towers[1] != null)
		{
			numberOfHits = 0;
			_currentTower.SetActive (false);
			_currentTower = Instantiate (Brad_Towers[1], _currentTower.transform.position, Quaternion.identity) as GameObject;
			_currentTower.SetActive (true);
		}
	}

	public void Lee1()
	{
		if (Lee_Towers[0] != null)
		{
			numberOfHits = 0;
			_currentTower.SetActive (false);
			_currentTower = Instantiate (Lee_Towers[0], _currentTower.transform.position, Quaternion.identity) as GameObject;
			_currentTower.SetActive (true);
		}
	}

	public void Lee2()
	{
		if (Lee_Towers[1] != null)
		{
			numberOfHits = 0;
			_currentTower.SetActive (false);
			_currentTower = Instantiate (Lee_Towers[1], _currentTower.transform.position, Quaternion.identity) as GameObject;
			_currentTower.SetActive (true);
		}
	}

	public void Elan1()
	{
		if (Elan_Towers[0] != null)
		{
			numberOfHits = 0;
			_currentTower.SetActive (false);
			_currentTower = Instantiate (Elan_Towers[0], _currentTower.transform.position, Quaternion.identity) as GameObject;
			_currentTower.SetActive (true);
		}
	}

	public void Elan2()
	{
		if (Elan_Towers[1] != null)
		{
			numberOfHits = 0;
			_currentTower.SetActive (false);
			_currentTower = Instantiate (Elan_Towers[1], _currentTower.transform.position, Quaternion.identity) as GameObject;
			_currentTower.SetActive (true);
		}
	}

	public void Bob1()
	{
		if (Bob_Towers[0] != null)
		{
			numberOfHits = 0;
			_currentTower.SetActive (false);
			_currentTower = Instantiate (Bob_Towers[0], _currentTower.transform.position, Quaternion.identity) as GameObject;
			_currentTower.SetActive (true);
		}
	}

	public void Bob2()
	{
		if (Bob_Towers[1] != null)
		{
			numberOfHits = 0;
			_currentTower.SetActive (false);
			_currentTower = Instantiate (Bob_Towers[1], _currentTower.transform.position, Quaternion.identity) as GameObject;
			_currentTower.SetActive (true);
		}
	}

	public void Kevin1()
	{
		if (Kevin_Towers[0] != null)
		{
			numberOfHits = 0;
			_currentTower.SetActive (false);
			_currentTower = Instantiate (Kevin_Towers[0], _currentTower.transform.position, Quaternion.identity) as GameObject;
			_currentTower.SetActive (true);
		}
	}

	public void Kevin2()
	{
		if (Kevin_Towers[1] != null)
		{
			numberOfHits = 0;
			_currentTower.SetActive (false);
			_currentTower = Instantiate (Kevin_Towers[1], _currentTower.transform.position, Quaternion.identity) as GameObject;
			_currentTower.SetActive (true);
		}
	}

	public void Reset()
	{
		numberOfHits = 0;
		_currentTower.SetActive (false);
		_currentTower = empty;
		_currentTower.SetActive (true);
	}





}
