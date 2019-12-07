using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour, IUserAction
{
    public GameObject player;
    private bool gameOver = false;
    private bool MovingForward = true;
    private int enemyCount = 6;
    private GameObjectFactory mf;
    private MainCameraControl cameraControl;

    private void Awake()
    {
        GameDirector director = GameDirector.getInstance();
        director.currentSceneController = this;
        mf = Singleton<GameObjectFactory>.Instance;
        player = mf.getPlayer();
        cameraControl = GetComponent<MainCameraControl>();
        cameraControl.setTarget(player.transform);
    }

    // Use this for initialization
    void Start ()
    {
	    for (int i = 0; i < enemyCount; ++ i)
        {
            GameObject gb = mf.getTank();
            cameraControl.setTarget(gb.transform);
        }
        Player.destroyEvent += setGameOver;
        cameraControl.SetStartPositionAndSize();
	}
	
	void Update()
    {
        // camera position
        Camera.main.transform.position = new Vector3(player.transform.position.x, 20, player.transform.position.z);
        Camera.main.orthographicSize = 15;
	}

    public Vector3 getPlayerPos()
    {
        return player.transform.position;
    }

    public bool isGameOver()
    {
        return gameOver;
    }

    public void setGameOver()
    {
        gameOver = true;
    }

    public void moveForward()
    {
        MovingForward = true;
        player.GetComponent<Rigidbody>().velocity = player.transform.forward * 20;
    }

    public void moveBackWard()
    {
        MovingForward = false;
        player.GetComponent<Rigidbody>().velocity = player.transform.forward * -20;
    }

    public void turn(float offsetX)
    {
        float y = player.transform.localEulerAngles.y + offsetX * 2;
        float x = player.transform.localEulerAngles.x;
        player.transform.localEulerAngles = new Vector3(x, y, 0);
    }

    public bool isMovingForward()
    {
        return MovingForward;
    }

    public void shoot()
    {
        GameObject bullet = mf.getBullet(tankType.Player);
        // set position
        bullet.transform.position = new Vector3(player.transform.position.x, 1.5f, player.transform.position.z) + player.transform.forward * 1.5f;
        // set direction
        bullet.transform.forward = player.transform.forward;

        // shoot
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bullet.transform.forward * 20, ForceMode.Impulse);
    }
}
