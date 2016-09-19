using UnityEngine;
using System.Collections;

public class LevelMovement : MonoBehaviour
{
    public float levelMovementSpeed = 1;
    public Transform levelObjects;
	
	void Update ()
    {
        levelObjects.Translate(Vector2.left * Time.deltaTime * levelMovementSpeed);
    }
}
