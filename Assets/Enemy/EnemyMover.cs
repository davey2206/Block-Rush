using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField]
    List<Waypoint> path = new List<Waypoint>();
    [SerializeField]
    [Range(0f, 5f)]
    float Speed = 1f;

    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform waypoint in parent.transform)
        {
            Waypoint wp = waypoint.GetComponent<Waypoint>();

            if (wp != null)
            {
                path.Add(wp);
            }
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 StartPos = transform.position;
            Vector3 EndPos = waypoint.transform.position;
            float TrevelPrecent = 0f;

            transform.LookAt(EndPos);

            while (TrevelPrecent < 1f)
            {
                TrevelPrecent += Time.deltaTime * Speed;
                transform.position = Vector3.Lerp(StartPos, EndPos, TrevelPrecent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }
}
