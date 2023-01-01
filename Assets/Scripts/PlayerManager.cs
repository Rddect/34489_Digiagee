using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;
using DG.Tweening.Core.Easing;
using System.Numerics;


public class PlayeerManager : MonoBehaviour
{
    public Transform player;
    private int numberCountStickman;
    [SerializeField] private TextMeshPro CounterTxt;
    [SerializeField] private GameObject stickman;
    [Range(0f,1f)][SerializeField] private float DistanceFactor, Radius;
    private Camera camera;
    //player mov

    public bool moveByTouch, gameState;
    private UnityEngine.Vector3 mouseStartPos, playerStartPos;
    public float playerSpeed, roadSpeed;

    [SerializeField] private Transform road;
    [SerializeField] private Transform enemy;
    private bool attack;
    public static PlayeerManager PlayerManagerInstance;
    public ParticleSystem blood;
    public GameObject SecondCam;
    public bool FinishLine, moveTheCamera;
    void Start()
    {
        player = transform;

        numberOfStickmans = transform.childCount - 1;

        CounterTxt.text = numberOfStickmans.ToString();

        camera = Camera.main;

        PlayerManagerInstance = this;

        gameState = false;
    }
    void Update()
    {
        
    }


    void MoveThePlayere()
    {
        if(Input.GetMouseButtonDown(0)&& gameState)
        {
            moveByTouch = true;
            var plane = new Plane(Vector3.up, 0f);
            var ray = Camera.ScreenPointToRay(Input.mousePosition);
            
            if(plane.Raycast(ray,out var distance))
            {
                mouseStartPos = ray.GetPoint(distance + 1f);
                playerStartPos = transform.position;
            }

        }
        if (Input.GetButtonUp(0))
        {
            moveByTouch = false;
        }

        if (moveByTouch)
        {
            var plane = new Plane(Vector3.up, 0f);
            var ray = Camera.ScreenPointToRay(Input.mousePosition);

            if(plane.Raycast(ray,out var distance))
            {
                var mousePos = ray.GetPoint(distance + 1f);
                var move = mousePos - mouseStartPos;
                var control = playerStartPos + move;

                transform.position = new Vector3(Mathf.Lerp(transform.position.x, control.x, Time.deltaTime * playerSpeed), transform.position.y, transform.position.z);
            }
        }

    }

    private void FormatStickMan()
    {
        for (int i = 0; i < player.childCount; i++)
        {
            var x = DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            var z = DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);

            var NewPos = new Vector3(x, -0.55f,z);

            player.transform.GetChild(i).DOLocalMove(NewPos, 1f).SetEase(Ease.OutBack);

        }
    }


    private void numberOfStickmans(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(stickman,transform.position, System.Numerics.Quaternion.identity,transform);
        }
        numberCountStickman = transform.childCount - 1;
        CounterTxt.text = numberCountStickman.ToString();
        FormatStickMan();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kapi"))
        {
            other.transform.parent.GetChild(0).GetComponent<Collider>().enabled = false;
            other.transform.parent.GetChild(0).GetComponent<Collider>().enabled = false; //kapi 1 ve 2 icin

            var GateManagere = other.GetComponent<GateManagere>();

            if (GateManagere.multiply)
            {
                numberOfStickmans(numberCountStickman * GateManagere.randomNumbeer);
            }
            else
            {
                numberOfStickmans(numberCountStickman * GateManagere.randomNumbeer);
            }
        }
    }
}
