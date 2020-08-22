using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTetromino : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        tag = "current_ghost_tetromino";

        foreach (Transform mino in transform)
        {
            mino.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .2f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        FollowActiveTetromino();

        MoveDown();
    }

    void FollowActiveTetromino()
    {
        Transform currentActiveTetrominoTransform = GameObject.FindGameObjectWithTag("current_active_tetromino").transform;

        transform.position = currentActiveTetrominoTransform.position;
        transform.rotation = currentActiveTetrominoTransform.rotation;
    }

    void MoveDown()
    {
        while (CheckIsValidPosition())
        {
            transform.position += new Vector3(0, -1, 0);
        }

        if (!CheckIsValidPosition())
        {
            transform.position += new Vector3(0, 1, 0);
        }
    }

    bool CheckIsValidPosition()
    {
        foreach (Transform mino in transform)
        {
            Vector2 pos = FindObjectOfType<Game>().Round(mino.position);

            if (FindObjectOfType<Game>().CheckInsideGrid(pos) == false)
                return false;

            if (FindObjectOfType<Game>().GetTransformAtGridPosition(pos) != null && FindObjectOfType<Game>().GetTransformAtGridPosition(pos).parent.tag == "current_active_tetromino")
                return true;

            if (FindObjectOfType<Game>().GetTransformAtGridPosition(pos) != null && FindObjectOfType<Game>().GetTransformAtGridPosition(pos).parent != transform)
                return false;
        }

        return true;
    }


}
