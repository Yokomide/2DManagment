using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;
        public bool isDialog;
    public bool canMove = true;
        public Animator animator;
        

        public Vector2 dir;
        private void Start()
        {
          
        }


        private void Update()
        {
        if (gameObject != null)

                if (Input.GetKeyDown(KeyCode.X))
                {
                    PlayerPrefs.DeleteAll();
                    if (System.IO.Directory.Exists(Application.dataPath + "/VIDE/saves"))
                    {
                        System.IO.Directory.Delete(Application.dataPath + "/VIDE/saves", true);
#if UNITY_EDITOR
                        UnityEditor.AssetDatabase.Refresh();
#endif
                    }

                PlayerPrefs.DeleteAll();
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
            }

        if (!isDialog && canMove)
            {

                Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }
            else
        {
            GetComponent<Rigidbody2D>().velocity = dir * 0;
        }
        }
    }

