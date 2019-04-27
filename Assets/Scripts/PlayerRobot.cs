using UnityEngine;
using System.Collections;

public class PlayerRobot : MonoBehaviour
{
    public float speed;

    void Update()
    {
        int direction = 0;
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            direction = -1;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            direction = 1;
        }

        if(direction != 0)
        {
            float xMove = speed * direction * Time.deltaTime;
            Vector3 move = new Vector3(xMove, 0, 0);

            transform.position += move;
        }
    }
}
