using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float move;
    public float rot;

    public Vector3 moveOffset;
    public Vector3 rotOffset;
    public Transform playerTarget;

    public UIInterface interfaceUIPlayer1;

    private ThatsHowWeRoll player;

    void Start()
    {
        player = playerTarget.GetComponentInParent<ThatsHowWeRoll>();

        if (player != null)
        {
            if (interfaceUIPlayer1 != null)
            {
                interfaceUIPlayer1.SetPlayer(player);
                interfaceUIPlayer1.UpdateInfo();
            }
        }
    }

    /// <summary>
    /// Update the player interface
    /// </summary>
    void Update()
    {
        if (player != null)
        {
            if (player == interfaceUIPlayer1.player)
            {
                interfaceUIPlayer1.UpdateInfo();
            }
        }
    }

    void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        HandleMovement();
        HandleRotation();
    }

    /// <summary>
    /// Move with the player
    /// </summary>
    void HandleMovement()
    {
        Vector3 targetPos = playerTarget.TransformPoint(moveOffset);
        transform.position = Vector3.Lerp(transform.position, targetPos, move * Time.deltaTime);
    }

    /// <summary>
    /// Rotate with the player
    /// </summary>
    void HandleRotation()
    {
        var direction = playerTarget.position - transform.position;
        var rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rot + Time.deltaTime);
    }
}