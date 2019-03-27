using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrigger
{
	void OnLemmingEnter();
	void OnLemmingExit();
	void OnGroupEnter();
	void OnGroupExit();
}
