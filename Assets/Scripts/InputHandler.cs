using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour {
	
	public cInputState mGlobalInputState;
	public eInputController mCurrentInputController;

    public static readonly float kMinTapTime = .5f;

    // current input type
    public enum eInputController {
        NONE,
        MOUSE,
        KEYBOARD,
        TOUCH
    };

    public enum eInputCommand {
        ACCEPT,
        CANCEL,
        MENU,
        UP,
        DOWN,
        LEFT,
        RIGHT,
        HATUP,
        HATDOWN,
        HATLEFT,
        HATRIGHT,
        TOUCH,
        MOUSE,
        AXIS
    }

    public class cInputElement {
        public eInputCommand mCommandName;
        public bool bActive;
        public bool bHeld;
        public bool bPressed;
        public bool bReleased;
        public bool bTapped;
        public float fTimeHeld; // Note, this is _remembered_ after the element has been deactivated. It's only reset upon becoming active again.

        public cInputElement(eInputCommand _commandName) {
            mCommandName = _commandName;
            bActive = false;
            bHeld = false;
            bPressed = false;
            bTapped = false;
            fTimeHeld = 0.0f;
        }

        public void justPressed() {
            bActive = true;
            bHeld = true;
            bPressed = true;
            bReleased = false;
            bTapped = false;
            fTimeHeld = 0.0f;
        }

        public void justReleased() {
            bActive = false;
            bPressed = false;

            if (fTimeHeld < kMinTapTime) {
                bTapped = true;
                bHeld = false;
            } else {
                bTapped = false;
                bHeld = true;
            }
        }
    }

    public class cInputContact : cInputElement {
        public Vector2 vPosition;
        public Vector2 vVelocity;

        public cInputContact(eInputCommand _commandName)
            : base(_commandName) {
            vPosition = Vector2.zero;
            vVelocity = Vector2.zero;
        }
    }

	// abstract state (type agnostic)
	public class cInputState {

        public Dictionary<eInputCommand, cInputElement> mButtons;
        public List<cInputContact> mContactPoints;

		public cInputState () {
            mContactPoints = new List<cInputContact>();
            mButtons = new Dictionary<eInputCommand, cInputElement>();

            mButtons.Add(eInputCommand.ACCEPT, new cInputElement(eInputCommand.ACCEPT));
            mButtons.Add(eInputCommand.CANCEL, new cInputElement(eInputCommand.CANCEL));
            mButtons.Add(eInputCommand.MENU, new cInputElement(eInputCommand.MENU));
            mButtons.Add(eInputCommand.UP, new cInputElement(eInputCommand.UP));
            mButtons.Add(eInputCommand.DOWN, new cInputElement(eInputCommand.DOWN));
            mButtons.Add(eInputCommand.LEFT, new cInputElement(eInputCommand.LEFT));
            mButtons.Add(eInputCommand.RIGHT, new cInputElement(eInputCommand.RIGHT));
		}
	}

	// Use this for initialization
	void Start () {

		mGlobalInputState = new cInputState ();
		mCurrentInputController = eInputController.NONE;
	}
	
	// Update is called once per frame
	void Update () {
	
		keyboardUpdate ();
		mouseUpdate ();
		touchUpdate ();
	}

    void keyboardUpdate() {
        if (Input.GetKeyDown("left"))
            mGlobalInputState.mButtons[eInputCommand.LEFT].justPressed();
        if (Input.GetKeyDown("right"))
            mGlobalInputState.mButtons[eInputCommand.RIGHT].justPressed();
        if (Input.GetKeyDown("down"))
            mGlobalInputState.mButtons[eInputCommand.DOWN].justPressed();
        if (Input.GetKeyDown("up"))
            mGlobalInputState.mButtons[eInputCommand.UP].justPressed();
        if (Input.GetKeyDown("enter"))
            mGlobalInputState.mButtons[eInputCommand.ACCEPT].justPressed();
        if (Input.GetKeyDown("escape")) {
            mGlobalInputState.mButtons[eInputCommand.CANCEL].justPressed();
            mGlobalInputState.mButtons[eInputCommand.MENU].justPressed();
        }

    }

	void mouseUpdate() {

	}

	void touchUpdate() {

	}
}
