using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlatformMover : NetworkBehaviour
{
    // Start is called before the first frame update
    public float speed = 2f;

    
    public Rigidbody2D rbPlatform;

    [ServerCallback]
    private void Update() {
        float xPos = this.gameObject.transform.position.x;
        if (xPos <= 35.68116) {
            rbPlatform.velocity = new Vector2 (speed,0);
        } else if(xPos >= 42.92117) {
            rbPlatform.velocity = new Vector2 (-speed,0);
        }
}
}
