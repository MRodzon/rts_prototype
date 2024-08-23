using DG.Tweening;
using Pathfinding;
using System;
using System.Collections;
using UnityEngine;

namespace RTSPrototype.Agents
{
    public class Agent : MonoBehaviour
    {
        public event Action<string> OnDestinationReached;

        [SerializeField]
        private Seeker seeker;
        [SerializeField]
        private Animator animator;

        [SerializeField]
        private float moveSpeed;

        private int currentWaypoint;

        private Sequence pathSequence;

        private Guid agentGuid;


        public void Initialize()
        {
            agentGuid = Guid.NewGuid();

            SetNewPath();
        }

        public void Remove()
        {
            transform.DOKill();
            pathSequence.Kill();

            StopAllCoroutines();

            Destroy(gameObject);
        }

        private void SetNewPath()
        {
            var targetPos = Vector3.one * UnityEngine.Random.Range(-6, 6);

            seeker.StartPath(transform.position, targetPos, ProgressWaypoint);

            animator.SetFloat("Speed", moveSpeed);
            animator.SetFloat("MotionSpeed", moveSpeed / 2);
        }

        private void ProgressWaypoint(Path path)
        {
            currentWaypoint++;

            if (currentWaypoint == path.vectorPath.Count)
            {
                OnPathComplete(path);
                return;
            }

            var nextWaypointPos = path.vectorPath[currentWaypoint];
            var pathSpeed = Vector3.Distance(nextWaypointPos, transform.position) / moveSpeed;

            var angle = Vector3.Angle(Vector3.forward, nextWaypointPos - transform.position);
            var cross = Vector3.Cross(Vector3.forward, nextWaypointPos - transform.position);

            if (cross.y < 0)
            {
                angle = -angle;
            }

            var newRotation = new Vector3(0, angle, 0);


            Debug.Log(newRotation);

            transform.DOKill();
            transform.DORotate(newRotation, 0.3f);

            pathSequence.Kill();
            pathSequence = DOTween.Sequence();
            pathSequence.Append(transform.DOMove(path.vectorPath[currentWaypoint], pathSpeed).SetEase(Ease.Linear));
            pathSequence.OnComplete(() => ProgressWaypoint(path));
        }

        private void OnPathComplete(Path path)
        {
            currentWaypoint = 0;
            animator.SetFloat("Speed", 0);

            OnDestinationReached?.Invoke(agentGuid.ToString());

            StartCoroutine(DelayedSetPath());
        }

        private IEnumerator DelayedSetPath()
        {
            yield return new WaitForSeconds(1f);

            SetNewPath();
        }
    }
}
