using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed = 3;

    private float playerSize;
    private bool shouldMove = false;

    private Vector2 previousInputDir;
    private Vector2 currentInputDir;

    private Vector2 moveTarget;

    private GridManager grid;

    private void Start()
    {
        playerSize = GetComponent<SpriteRenderer>().bounds.size.x;
        grid = GridManager.Instance;
    }

    private void Update()
    {
        if (!shouldMove) { return; }

        if (!grid.HasReachedCellCenterInMoveDirection(previousInputDir, transform.position))
        {
            moveTarget = GetTarget(previousInputDir);
        }
        else { moveTarget = GetTarget(currentInputDir); }

        Move();
    }

    private void Move()
    {
        Debug.DrawRay(transform.position, (Vector3)moveTarget - transform.position, Color.cyan);
        transform.position = Vector2.MoveTowards(transform.position, moveTarget, speed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            shouldMove = true;
            return;
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            shouldMove = false;
            return;
        }

        Vector2 newDirection = context.ReadValue<Vector2>();

        if (ShouldIgnoreDirection(newDirection)) { return; }

        HandleDirectionSwitch(newDirection);
    }

    private void HandleDirectionSwitch(Vector2 newDirection)
    {
        previousInputDir = currentInputDir;
        currentInputDir = newDirection;

        if (ShouldSwitchDirection())
        {
            previousInputDir = newDirection;
            currentInputDir = newDirection;
        }
    }

    private bool ShouldSwitchDirection()
    {
        if (previousInputDir == currentInputDir) { return false; }
        if (Mathf.Abs(previousInputDir.x) != Mathf.Abs(currentInputDir.x) ||
           Mathf.Abs(previousInputDir.y) != Mathf.Abs(currentInputDir.y)) { return false; }

        return true;
    }

    private bool ShouldIgnoreDirection(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) == 1 && Mathf.Abs(dir.y) == 0) { return false; }
        if (Mathf.Abs(dir.x) == 0 && Mathf.Abs(dir.y) == 1) { return false; }
        return true;
    }

    private Vector3 GetTarget(Vector3 direction)
    {
        if (direction == Vector3.zero) { return transform.position; }

        if (grid.IsNeigbourCellWalkable(transform.position, direction))
        {
            return grid.GetNeighbourPositionFromDirection(transform.position, direction);
        }

        return grid.GetCurrentCellPosition(transform.position);
    }
}
