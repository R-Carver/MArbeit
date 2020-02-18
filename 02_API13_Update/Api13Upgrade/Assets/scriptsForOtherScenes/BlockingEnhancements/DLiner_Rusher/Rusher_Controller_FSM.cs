using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockingEnhancements
{
    public class Rusher_Controller_FSM : MonoBehaviour, IResettable
    {
        #region Player vars

        public Transform Qb;

        //this is the current Target
        public Transform Target;
        [HideInInspector]
        public Rigidbody2D myRb;

        public float speed;
        public float rotationSpeed;

        public DefenderController_FSM defender;

        Vector2 startPos;
        Quaternion startRot;

        #endregion

        #region FSM
        public Rush_Base_State currentState;

        public readonly Rush_OpenField_State openField_State = new Rush_OpenField_State();
        public readonly Rush_OppVisible_State oppVisible_State = new Rush_OppVisible_State();
        public readonly Rush_QbChase_State qbChase_State = new Rush_QbChase_State();

        #endregion

        private void Awake()
        {
            myRb = GetComponent<Rigidbody2D>();

        }
        // Start is called before the first frame update
        void Start()
        {
            //register yourself with the GameManager
            GameManager.Instance.allPlayers.Add(this);

            //save startPos for reset later
            startPos = this.transform.position;
            startRot = this.transform.rotation;

            TransitionToState(openField_State);
        }

        // Update is called once per frame
        void Update()
        {
            //this it probably not a good idea since it makes this object depend on the defender
            //but for now its ok

            //not used while in raycast system
            /*if (defender.currentState is Def_KnockedOver_State && defender != null)
            {
                TransitionToState(qbChase_State);
            }*/
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //TODO: change the state implementation later to OnTriggerEnter instead of OnCollisionEnter
            currentState.OnCollisionEnter(this, other);
        }

        public void TransitionToState(Rush_Base_State state)
        {
            state.CleanUp(this);
            currentState = state;
            currentState.EnterState(this);
        }

        private void FixedUpdate()
        {
            currentState.Update(this);

            RayToQb();
        }

        public RaycastHit2D RayToQb()
        {   
            //only check for the oposing team
            LayerMask oposingMask = LayerMask.GetMask("OLiner");

            //show the ray
            Vector3 qbDir = Qb.transform.position - transform.position;
            Debug.DrawRay(transform.position, qbDir, Color.green);

            return Physics2D.Raycast(transform.position, qbDir, 3.0f, oposingMask);
        }

        public void Reset()
        {
            this.transform.position = startPos;
            this.transform.rotation = startRot;

            myRb.velocity = Vector2.zero;
            myRb.angularVelocity = 0;

            TransitionToState(openField_State);
        }
    }
}

