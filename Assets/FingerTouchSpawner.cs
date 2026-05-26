using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Management;

public class FingerTouchSpawner : MonoBehaviour
{
    public Transform leftSphere;
    public Transform rightSphere;

    public GameObject objectPrefab;

    public float touchDistance = 0.03f;

    private XRHandSubsystem handSubsystem;

    private bool hasSpawned = false;

    void Start()
    {
        handSubsystem =
            XRGeneralSettings.Instance.Manager.activeLoader
            .GetLoadedSubsystem<XRHandSubsystem>();
    }

    void Update()
    {
        if (handSubsystem == null)
            return;

        UpdateHandTracking();

        CheckFingerTouch();
    }

    void UpdateHandTracking()
    {
        XRHand leftHand = handSubsystem.leftHand;
        XRHand rightHand = handSubsystem.rightHand;

        if (leftHand.isTracked)
        {
            XRHandJoint joint =
                leftHand.GetJoint(XRHandJointID.IndexTip);

            if (joint.TryGetPose(out Pose pose))
            {
                leftSphere.position = pose.position;
            }
        }

        if (rightHand.isTracked)
        {
            XRHandJoint joint =
                rightHand.GetJoint(XRHandJointID.IndexTip);

            if (joint.TryGetPose(out Pose pose))
            {
                rightSphere.position = pose.position;
            }
        }
    }

    void CheckFingerTouch()
    {
        float distance =
            Vector3.Distance(
                leftSphere.position,
                rightSphere.position);

        if (distance < touchDistance)
        {
            if (!hasSpawned)
            {
                SpawnObject();
                hasSpawned = true;
            }
        }
        else
        {
            hasSpawned = false;
        }
    }

    void SpawnObject()
    {
        Vector3 spawnPos =
            (leftSphere.position + rightSphere.position) / 2f;

        Instantiate(
            objectPrefab,
            spawnPos,
            Quaternion.identity);
    }
}