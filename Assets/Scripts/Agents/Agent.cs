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

        private float timeModifier;

        private int currentWaypoint;

        private Sequence pathSequence;

        private Guid agentGuid;

        public void Initialize(int gameSpeed)
        {
            agentGuid = Guid.NewGuid();

            SetTimeModifier(gameSpeed);
            SetNewPath();
        }

        public void Remove()
        {
            transform.DOKill();
            pathSequence.Kill();

            StopAllCoroutines();

            Destroy(gameObject);
        }

        public void SetTimeModifier(int value)
        {
            timeModifier = value;

            SetAnimatorValues(currentWaypoint == 0 ? 0 : moveSpeed * value);

            if (pathSequence == null || !pathSequence.IsActive())
            {
                return;
            }

            if (value == 0)
            {
                pathSequence.Pause();
            }
            else if (value > 0 && !pathSequence.IsPlaying())
            {
                pathSequence.Play();
            }
        }

        private void SetAnimatorValues(float value)
        {
            animator.SetFloat("Speed", value == 0 ? 0 : moveSpeed);
            animator.SetFloat("MotionSpeed", value / 2);
        }

        private void SetNewPath()
        {
            var x = UnityEngine.Random.Range(-6, 6);
            var z = UnityEngine.Random.Range(-6, 6);

            seeker.StartPath(transform.position, new(x, 0, z), ProgressWaypoint);
            SetAnimatorValues(moveSpeed * timeModifier);
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
            var pathSpeed = Vector3.Distance(nextWaypointPos, transform.position) / (moveSpeed * timeModifier);

            var angle = Vector3.Angle(Vector3.forward, nextWaypointPos - transform.position);
            var cross = Vector3.Cross(Vector3.forward, nextWaypointPos - transform.position);

            if (cross.y < 0)
            {
                angle = -angle;
            }

            var newRotation = new Vector3(0, angle, 0);

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

            SetAnimatorValues(0);

            OnDestinationReached?.Invoke(agentGuid.ToString());

            StartCoroutine(DelayedSetPath());
        }

        private IEnumerator DelayedSetPath()
        {
            yield return new WaitForSeconds(timeModifier > 0 ? 1f / timeModifier : 1f);

            while (timeModifier == 0)
            {
                yield return new WaitForEndOfFrame();
            }

            SetNewPath();
        }
    }
}
