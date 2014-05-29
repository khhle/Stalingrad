using UnityEngine;

/// <summary>
/// Simply moves the current game object
/// </summary>
public class moveScript2 : MonoBehaviour
{
	// 1 - Designer variables
	
	/// <summary>
	/// Object speed
	/// </summary>
	public Vector2 speed = new Vector2(10, 10);
	//public bool isInit = false;
	//public float speedY = 0;
	/// <summary>
	/// Moving direction
	/// </summary>
	public Vector2 direction = new Vector2(0, -1);
	
	private Vector2 movement;
	
	void Start()
	{
		//speed = new Vector2(10, speedY);
	}
	
	void Update()
	{
		
		//if (isInit) {
		//	 speed = new Vector2(3, 3);
		//		}
		
		// 2 - Movement
		movement = new Vector2(
			speed.x * direction.x,
			speed.y * direction.y);
		
		
		
		
	}
	
	void FixedUpdate()
	{
		// Apply movement to the rigidbody
		rigidbody2D.velocity = movement;
	}
}