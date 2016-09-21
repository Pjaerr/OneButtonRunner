using UnityEngine;
using System.Collections;


//This script moves every object that is a child of the Level gameobject to the left at the specified movement speed. Default = 5;
public class LevelMovement : MonoBehaviour
{
    public float levelMovementSpeed = 5;
    public Transform levelObjects;
	
	void Update ()
    {
        levelObjects.Translate(Vector2.left * Time.deltaTime * levelMovementSpeed);
    }
}
