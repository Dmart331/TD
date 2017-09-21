using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    public Transform Player;
    public float speed = 2f;
    private float minDistance = 0.2f;
    private float range;

    // Use this for initialization
    void Start () {
		Player = GameObject.FindWithTag("Player").transform;
	}

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Something Hit");
                if (raycastHit.collider.name == "Enemy")
                {
                    Debug.Log("Enemy clicked");
                }

            }
        }
    }
}
