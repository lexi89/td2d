using UnityEngine;
using UnityEngine.UI;

public class CoinCount : MonoBehaviour, ICounter {

	Text CounterText;
	[SerializeField] int Count;

	void Awake(){
		CounterText = GetComponent <Text> ();
	}

	void Start(){
		// get the number of coins the player has.
	}


	#region ICounter implementation

	public void Add (int numberToAdd)
	{
		Count += numberToAdd;
		Update ();
	}

	public void Remove (int numberToRemove)
	{
		throw new System.NotImplementedException ();
	}

	public void Update(){
		CounterText.text = Count.ToString ();
	}

	#endregion
}
