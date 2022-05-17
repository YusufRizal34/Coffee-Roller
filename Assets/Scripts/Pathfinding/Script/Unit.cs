using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour
{


	public List<Kursi> target = new List<Kursi>();
	public int currentTarget;
	float speed = 1;
	Vector3[] path;
	int targetIndex;
	public Transform Kasir;
	public bool cekSampai;
	bool cekEksekusi;

	void Start()
	{
		CariKursi();
        currentTarget = CompareDistance();
        PathRequestManager.RequestPath(this.transform.position, target[currentTarget].transform.position, OnPathFound);
    }

	void LateUpdate()
	{
        if (cekSampai != true)
        {
            PathRequestManager.RequestPath(this.transform.position, target[currentTarget].transform.position, OnPathFound);
        }
        else
        {
/*            print("mbarang");*/
        }
    }

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
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
/*                    if (cekEksekusi != true)
                    {
                        print("tidak sampai");
                    }*/
                    yield break;
				}
				currentWaypoint = path[targetIndex];
/*                print("tes print");*/
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

    private int CompareDistance()
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
           /* print(minDistance);*/
        }
        return minDistance;
    }

    private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Interactable")
		{
			var objects = other.gameObject.GetComponent<IInteractable>();
			objects.Interaction();
		}
	}

	IEnumerator OtwKasir()
	{
		yield return new WaitForSeconds(5);
/*		print("OtwKasir " + Time.time);*/
	}

	void CariKursi()
    {
		Kursi[] kursi = Object.FindObjectsOfType<Kursi>();
		for(int i=0; i<kursi.Length; i++)
        {
			target.Add(kursi[i]);
        }
    }
}