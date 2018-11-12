﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : MonoBehaviour {

    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwiperDist = 50.0f;
    private float maxSwipeTime = 1.5f;

    private GameManager gm;

    private void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if(gm.State==GameState.Playing && Input.touchCount>0)
        {
            foreach(Touch  touch in Input.touches)
            {
                switch(touch.phase)
                {
                    case TouchPhase.Began:
                        isSwipe = true;
                        fingerStartTime = Time.time;
                        fingerStartPos = touch.position;
                        break;

                    case TouchPhase.Canceled:
                        isSwipe = false;
                        break;

                    case TouchPhase.Ended:
                        float gestureTime = Time.time - fingerStartTime;
                        float gestureDist = (touch.position - fingerStartPos).magnitude;

                        if(isSwipe && gestureTime<maxSwipeTime && gestureDist>minSwiperDist)
                        {
                            Vector2 direction = touch.position - fingerStartPos;
                            Vector2 swipeType = Vector2.zero;

                            if(Mathf.Abs(direction.x)>Mathf.Abs(direction.y))
                            {
                                swipeType = Vector2.right * Mathf.Sign(direction.x);
                            }
                            else
                            {
                                swipeType = Vector2.up * Mathf.Sign(direction.y);
                            }

                            if(swipeType.x!=0.0f)
                            {
                                if(swipeType.x>0.0f)
                                {
                                    gm.Move(MoveDirection.Right);
                                }
                                else
                                {
                                    gm.Move(MoveDirection.Left);
                                }
                            }

                            if (swipeType.y != 0.0f)
                            {
                                if (swipeType.y > 0.0f)
                                {
                                    gm.Move(MoveDirection.Up);
                                }
                                else
                                {
                                    gm.Move(MoveDirection.Down);
                                }
                            }
                        }
                        break;
                }
            }
        }
	}
}
