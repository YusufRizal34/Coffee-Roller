using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelanggan : MonoBehaviour
{
    public float speed = 5;
    Vector3[] path;
    int targetIndex;
	Transform keKasir;
	Transform kePintu;

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			StopCoroutine("FollowPath");
			path = newPath;
			targetIndex = 0;
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = path[0];
		while (true)
		{
			if (transform.position == currentWaypoint)
			{
				targetIndex++;
				if (targetIndex >= path.Length)
				{
					targetIndex = 0;
					path = new Vector3[0];
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			yield return null;

		}
	}

	public void OnDrawGizmos()
	{
		if (path != null)
		{
			for (int i = targetIndex; i < path.Length; i++)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex)
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else
				{
					Gizmos.DrawLine(path[i - 1], path[i]);
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            var objects = other.gameObject.GetComponent<IInteractable>();
            objects.InteraksiPelanggan();
/*            Unit pelayan = FindObjectOfType<Unit>();
            objects.InteraksiPelayan(pelayan.gameObject);*/
        }
        else
        {

        }
    }
}
