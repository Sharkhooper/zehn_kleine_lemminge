using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FinalScript : MonoBehaviour
{
	[SerializeField] private GameObject Group;
	[SerializeField] private GameObject Lemming1;
	[SerializeField] private Animator Animator1;
	[SerializeField] private GameObject Lemming2;
	[SerializeField] private Animator Animator2;
	[SerializeField] private GameObject Baby;
	[SerializeField] private Animator BabyAnim;
	[SerializeField] private GameObject cam1;
	[SerializeField] private GameObject cam2;
	[SerializeField] private GameObject light1;
	[SerializeField] private SpriteRenderer Spikes1;
	[SerializeField] private SpriteRenderer Spikes2;
	[SerializeField] private Sprite DeadSpike;
	[SerializeField] private GameObject[] Waypoints;
	[SerializeField] private GameObject BabyGroup;
	[SerializeField] private AudioSource audio;

	// Start is called before the first frame update
	void Start()
	{

	}

	public void StartCutScene()
	{
		StartCoroutine(Final());
	}

	IEnumerator Final()
	{
		audio.Play();

		yield return new WaitForSeconds(40);

		//Lemming1.SetActive(true);

		//while (!(Vector3.Distance(Lemming1.transform.position, Waypoints[0].transform.position) == 0))
		//{

		//	Lemming1.transform.position = Vector3.MoveTowards(Lemming1.transform.position, Waypoints[0].transform.position, 1f * Time.deltaTime);
		//	Animator1.SetBool("Walke", true);
		//	Animator1.SetBool("IsGrounded", true);
		//	Animator1.SetFloat("Speed", 1);

		//	yield return new WaitForSeconds(1);
		//}

		//Lemming1.SetActive(false);
		//Lemming2.SetActive(true);
		//Spikes1.sprite = DeadSpike;


		//while (Vector3.Distance(Lemming2.transform.position, Waypoints[1].transform.position) == 0)
		//{

		//	Lemming2.transform.position = Vector3.MoveTowards(Lemming1.transform.position, Waypoints[1].transform.position, 1f * Time.deltaTime);
		//	Animator2.SetBool("Walke", true);
		//	Animator2.SetBool("IsGrounded", true);
		//	Animator2.SetFloat("Speed", 1);

		//	yield return new WaitForSeconds(1);
		//}

		//Lemming2.SetActive(false);
		//Spikes2.sprite = DeadSpike;

		Spikes1.sprite = DeadSpike;
		Spikes2.sprite = DeadSpike;
		BabyGroup.SetActive(true);
		Group.SetActive(false);

		yield return new WaitForSeconds(65);



		//	End
		light1.SetActive(false);
		yield return new WaitForSeconds(5);
		cam2.SetActive(true);
		cam1.SetActive(false);
		yield return new WaitForSeconds(9);
		BabyAnim.SetBool("End", true);
	}
}
