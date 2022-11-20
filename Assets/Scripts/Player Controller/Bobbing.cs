using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour
{
    public float walkingBobbingSpeed = 1f;
    public float bobbingAmount = 0.05f;
    public PlayerController playerController;

    float defaultPosY = 0;
    float timer = 0;

    void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    void Update()
    {
        if (Mathf.Abs(playerController.moveDirection.x) > 0.1f || Mathf.Abs(playerController.moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (playerController.state == PlayerController.MovementState.sprint ? walkingBobbingSpeed * 2f : playerController.state == PlayerController.MovementState.crouching ? walkingBobbingSpeed * 0.8f : walkingBobbingSpeed);
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * (playerController.state == PlayerController.MovementState.sprint ? bobbingAmount * 1.2f : playerController.state == PlayerController.MovementState.sprint ? bobbingAmount * 0.8f : bobbingAmount), transform.localPosition.z);
        }
        else
        {
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);
        }
    }

}
