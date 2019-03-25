using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Dictionary<string, bool> UnlockedAbilities { get; private set; }

	private void Awake()
	{
		UnlockedAbilities = new Dictionary<string, bool>
			{{"Fire", false}, {"SuperJump", false}, {"Power", false}};
	}

	public void UnlockAbility(string ability)
	{
		UnlockedAbilities[ability] = true;
	}

	public void LockAbility(string ability)
	{
		UnlockedAbilities[ability] = false;
	}
}
