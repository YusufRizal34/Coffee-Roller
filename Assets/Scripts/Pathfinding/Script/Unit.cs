using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{


	public Queue<Kursi> target = new Queue<Kursi>();
	float speed = 1;
	Vector3[] path;
	int targetIndex;

	void Start()
	{
/*		CariKursi();
        currentTarget = CompareDistance();
        PathRequestManager.RequestPath(this.transform.position, target[currentTarget].transform.position, OnPathFound);*/
    }

	void LateUpdate()
	{
        /*PathRequestManager.RequestPath(this.transform.position, target[currentTarget].transform.position, OnPathFound);*/
    }

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			StopCoroutine("FollowPath");
			path = newPath;
			targetIndex = 0;
/*			float distanceFromTarget = Vector3.Distance(target.gameObject.transform.position, transform.position);

			if (distanceFromTarget > 0.5f)
			{
				
			}*/
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

   /* private int CompareDistance()
    {
        int minDistance = -1;
        for (int i = 0; i < target.Count; i++)
        {
            if (target[i].IsSit == true)
            {
                target.Remove(target[i]);
                break;
            }

            if (minDistance < 0)
            {
                minDistance = i;
            }

            else if (Vector3.Distance(transform.position, target[i].transform.position) < Vector3.Distance(transform.position, target[minDistance].transform.position) && i != 0)
            {
                minDistance = i;
            }
           *//* print(minDistance);*//*
        }
        return minDistance;
    }*/

    private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Interactable")
		{
			var objects = other.gameObject.GetComponent<IInteractable>();
			objects.InteraksiPelayan(gameObject);
		}
	}

/*	void CariKursi()
    {
		Kursi[] kursi = Object.FindObjectsOfType<Kursi>();
		for(int i=0; i<kursi.Length; i++)
        {
			target.Add(kursi[i]);
        }
    }*/
}