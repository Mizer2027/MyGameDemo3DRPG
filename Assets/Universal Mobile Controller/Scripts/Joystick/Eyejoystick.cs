using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UniversalMobileController {
    public class Eyejoystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
    {
        [SerializeField] private RectTransform joystickBackground;
        [SerializeField] private RectTransform joyStick;

        [Range(0, 10f)] [SerializeField] private float joystickMovementRange = 1f;

        public  Vector2 joyStickInput = Vector2.zero;
        //小眼睛位置
        public Transform eye;
        //小眼睛旋转目标中心点位置
        public Transform target;
        public FloatingJoyStick joystick;

        private Vector2 joystickCurrentPosition = new Vector2(0, 0);
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color pressedColor = Color.white;
        [SerializeField] private State joystickState;
        /*List of events*/
        public UnityEvent onGameStart;
        public UnityEvent onPressedJoystick;
        public UnityEvent onStartedDraggingJoystick;
        public UnityEvent onStoppedDraggingJoystick;
        public bool ISrun=false;
        public float Speed = 8f;
        public float Speedeye = 8f;


        private void Awake()
        {
            
        }
        private void Start()
        {
            onGameStart.Invoke();
            SetJoystickColor(normalColor);
        }
        private void Update()
        {
           
            if (ISrun==true)
            {
                target = GameObject.Find("Player/target").gameObject.transform;
                eye = GameObject.Find("Player/target/eye").gameObject.transform;
                if (eye != null&&target != null)
                {
                    Lerp();
                    if (eye.rotation != target.rotation)
                    {
                        // eye.eulerAngles = Vector3.Lerp(eye.eulerAngles, target.eulerAngles, Time.deltaTime * 3);
                        eye.rotation = Quaternion.Lerp(eye.rotation, target.rotation, Time.deltaTime * 5);
                        //Debug.Log(eye.rotation);

                    }
                }
               
            }
        }

        public void OnPointerDown(PointerEventData eventdata)
        {
            if (UniversalMobileController_Manager.editMode || joystickState == State.Un_Interactable) { return; }
            SetJoystickColor(pressedColor);
            joystickCurrentPosition = eventdata.position;
            joystickBackground.position = eventdata.position;
            joyStick.anchoredPosition = Vector2.zero;
            ISrun = false;
            OnDrag(eventdata);
            onPressedJoystick.Invoke();
           
        }
        public void OnPointerUp(PointerEventData eventdata)
        {
            if (UniversalMobileController_Manager.editMode || joystickState == State.Un_Interactable) { return; }
            SetJoystickColor(normalColor);
            //joyStickInput = new Vector2(0, 0);
            ISrun = true;
           
            joyStick.anchoredPosition = new Vector2(0, 0);
            onStoppedDraggingJoystick.Invoke();
        }
        //插值运算，渐变为0；
        public void Lerp()
        {
            joyStickInput = Vector2.Lerp(joyStickInput, Vector2.zero, Time.deltaTime*Speed);
          

            
            

        }
        public void OnDrag(PointerEventData eventdata)
        {
            ISrun = false;
            if (UniversalMobileController_Manager.editMode || joystickState == State.Un_Interactable) { return; }

            onStartedDraggingJoystick.Invoke();
            Vector2 direction = eventdata.position - joystickCurrentPosition;

            if (direction.magnitude > joystickBackground.sizeDelta.x / 2f)
            {
                joyStickInput = direction.normalized;
            }
            else
            {
                joyStickInput = direction / (joystickBackground.sizeDelta.x / 2f);
            }

            joyStick.anchoredPosition = (joyStickInput * joystickBackground.sizeDelta.x / 2f) * joystickMovementRange;
        }

        private void SetJoystickColor(Color color)
        {
            joystickBackground.gameObject.GetComponent<Image>().color = color;
            joyStick.gameObject.GetComponent<Image>().color = color;
        }

        public float GetVerticalValue()
        {
            return joyStickInput.y;
        }
        public float GetHorizontalValue()
        {
            return joyStickInput.x;
        }
        public Vector2 GetHorizontalAndVerticalValue()
        {
            return joyStickInput;
        }
        public void SetState(State state)
        {
            joystickState = state;
        }

        public State GetState()
        {
            return joystickState;
        }
    }
    
}