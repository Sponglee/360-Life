using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public float currentAngleSpeed;
    public float currentAngle;
    public float maxRotateSpeed;
    public Vector3 startPosition;
    public List<float> speedHistory;
    public float minSwipeDistX;

    public float rotateSpeed;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        this.speedHistory = new List<float>();
       
    }


    void Update()
    {
        if (!GameManager.Instance.gameOver)
        {
            this.UpdateInput();
            
            this.currentAngleSpeed = Mathf.Lerp(this.currentAngleSpeed, 0f, 5f * Time.deltaTime);
            
            this.currentAngle += this.currentAngleSpeed * Time.deltaTime;
            //
            //int num = (int)(this.currentAngle / 10f);
            //this.currentAngleRotLocal = Mathf.Lerp(this.currentAngleRotLocal, (float)(num * 10), 20f * Time.deltaTime);
            base.transform.localRotation = Quaternion.Euler(new Vector3(0f, this.currentAngle, 0f));
    }
}

   
    private void UpdateInput()
    {
        //
        Vector3 moveVector = new Vector3(Input.mousePosition.x, 0f, 0f) - new Vector3(this.startPosition.x, 0f, 0f);
        //
        float moveX = Mathf.Clamp(moveVector.magnitude, 0f, this.maxRotateSpeed);
        //
        float screenWidth = ((float)Screen.width);
       
        //
        float moveXPercent = moveX / screenWidth;
        //
        float speed = (-Mathf.Sign(Input.mousePosition.x - this.startPosition.x) * moveXPercent) * this.rotateSpeed;
        if (!GameManager.Instance.gameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //
                this.speedHistory.Clear();
                this.currentAngleSpeed = 0f;
                this.startPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                //
                this.currentAngleSpeed = 0f;
                if (moveXPercent > this.minSwipeDistX)
                {
                    this.speedHistory.Add(speed);
                }
                else
                {
                    this.speedHistory.Add(0f);
                }
                if (this.speedHistory.Count > 4)
                {
                    this.speedHistory.RemoveAt(0);
                }
                this.currentAngle += speed;
                this.startPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0) && (moveX > this.minSwipeDistX))
            {
                //
                float speedX = 0f;
                for (int i = 0; i < this.speedHistory.Count; i++)
                {
                    speedX += this.speedHistory[i];
                }
                this.currentAngleSpeed = 6f * speedX;
                this.startPosition = Input.mousePosition;
            }
        }
    }
}
