using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class KillingCam : MonoBehaviour
{
    public GameObject particleEffect;
    public Vector2 touchpos;
    private RaycastHit hit;
    private Camera cam;

    private int score = 0;

    [SerializeField] GameObject AppOrigin;
    private ApplicationManager _manager;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        _manager = AppOrigin.GetComponent<ApplicationManager>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount <= 0)
            return;

        touchpos = Input.GetTouch(0).position;

        Ray ray = cam.ScreenPointToRay(touchpos);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.tag == "Enemy")
            {
                _manager.EndGame(true);
                var clone = Instantiate(particleEffect, hitObj.transform.position, Quaternion.identity);
                clone.transform.localScale = hitObj.transform.localScale;
                Destroy(hitObj);
                score += 1;
                _manager.EditScore(score);
            }
        }
    }

    public void ResetScore()
    {
        score = 0;
    }
}
