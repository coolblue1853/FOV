using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class Player : MonoBehaviour
{
    [SerializeField] private float Speed;
    float angle;
    Vector2 target, mouse;

    [SerializeField] private myFieldOfView fieldOfView;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        */


        Vector3 targetposition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDir = (targetposition - transform.position).normalized;


        fieldOfView.SetAimDir(aimDir);
        fieldOfView.setOrigin(transform.position);


        // 이동

        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(X, Y) * Time.deltaTime * Speed);
    }
}
