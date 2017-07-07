//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CatAnimation : MonoBehaviour
//{
//    private Animator animator;

//    void Start()
//    {
//        animator = GetComponent<Animator>();
//    }

//    void Update()
//    {
//        if (GameData.checkTouchblock)
//            animations();
//        else if (GameData.skillKind == 0)
//        {
//            animator.SetInteger("Status", 0);
//        }
//        else
//        {
//            GameData.skillKind = 0;
//        }
//    }

//    private void animations()
//    {
//        switch (GameData.skillKind) // GameData.skillKind: 스킬블럭 상태 받아오는 거 표현한 임시변수
//        {
//            case 1:
//                animator.SetInteger("Status", 1);
//                Debug.Log("skillNum_1");
//                break;
//            case 2:
//                animator.SetInteger("Status", 2);
//                Debug.Log("skillNum_2");
//                break;
//            case 3:
//                animator.SetInteger("Status", 3);
//                Debug.Log("skillNum_3");
//                break;
//            default:
//                break;
//        }
//    }
//}
