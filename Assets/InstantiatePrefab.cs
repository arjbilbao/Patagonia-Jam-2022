using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    public GameObject prefab, parent;
	public Transform point;
	public float livingTime=0.5f;

    public void Instantiate()
	{
		GameObject instantiatedObject = Instantiate(prefab, point.position, Quaternion.identity) as GameObject;

        instantiatedObject.transform.SetParent(parent.transform, true);

		if (livingTime > 0f) {
			Destroy(instantiatedObject, livingTime);
		}
	}
}
