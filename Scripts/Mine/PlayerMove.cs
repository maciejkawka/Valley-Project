using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    //Moving Variables
    private CharacterController rg = new CharacterController();
    private bool Is_Walking;
    private Vector3 movingVector = new Vector3(0.0f, 0.0f, 0.0f);
    public float gravity;
    public float Walking_Speed;
    public float Running_Speed;
    public float Jump_Speed;
    public float mouseSensitivity;
    public float maxY;
    public float minY;
    float xMouseClamp = 0.0f;
    private float tempTime = 0;
    Quaternion startRot = new Quaternion();

    //FMOD
    [FMODUnity.EventRef]
    public string Patch;

    FMOD.Studio.EventInstance FootSound;


    FMOD.Studio.ParameterInstance WoodParameter;
    FMOD.Studio.ParameterInstance ForestParameter;
    FMOD.Studio.ParameterInstance GravelParameter;
    FMOD.Studio.ParameterInstance SandParameter;
    FMOD.Studio.ParameterInstance StoneParameter;

    //Player State
    private bool isMoving;
    private bool isJumping;
    private bool isInBush;
    Vector3 oldRotation;

    //FMOD Parameters
    private float WoodValue;
    private float ForestValue;
    private float SandValue;
    private float GravelValue;
    private float StoneValue;



    void Start()
    {
        rg = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        FootSound = FMODUnity.RuntimeManager.CreateInstance(Patch);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(FootSound, GetComponent<Transform>(), GetComponent<Rigidbody>());

        FootSound.getParameter("Gravel", out GravelParameter);
        FootSound.getParameter("Forest", out ForestParameter);
        FootSound.getParameter("Sand", out SandParameter);
        FootSound.getParameter("Wood", out WoodParameter);
        FootSound.getParameter("Stone", out StoneParameter);

        WoodValue = 0f;
        ForestValue = 1f;
        SandValue = 0f;
        GravelValue = 0f;
        StoneValue = 0f;

        isMoving = false;
        isJumping = false;
        isInBush = false;
        oldRotation = this.transform.rotation.eulerAngles;
    }

    void FixedUpdate()
    {
        tempTime += Time.deltaTime;

        FootSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

        PlayerLook();
        PlayerMoveing();
        Jump();

        if (Input.GetAxis("Vertical") >= 0.01f || Input.GetAxis("Horizontal") >= 0.01f || Input.GetAxis("Vertical") <= -0.01f || Input.GetAxis("Horizontal") <= -0.01f)
        {
            if (rg.isGrounded == true)
                isMoving = true;
            else if (rg.isGrounded == false)
                isMoving = false;
        }
        else if (Input.GetAxis("Vertical") == 0 || Input.GetAxis("Horizontal") == 0)
            isMoving = false;

        if (Input.GetButton("Fire3"))
        {
            if (tempTime >= 0.4f)
            {
                tempTime -= 0.4f;
                FootPlay();
            }
        }

        if (tempTime >= 0.6f)
        {
            tempTime -= 0.6f;
            FootPlay();
        }

        if (isJumping && rg.isGrounded)
        {
            FootSound.setPitch(2);
            FootSound.start();
            isJumping = false;
            FootSound.setPitch(1);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && rg.isGrounded)
        {
            movingVector.y = Jump_Speed;
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Player/Jump", this.gameObject);
        }

        if (Input.GetButtonDown("Jump") && !rg.isGrounded)
        {
            return;
        }
    }

    void PlayerLook()
    {
        float mouseLookX = Input.GetAxis("Mouse X");
        float mouseLookY = Input.GetAxis("Mouse Y");
        float rotAmountX = mouseLookX * mouseSensitivity;
        float rotAmountY = mouseLookY * mouseSensitivity;

        Vector3 targetRot = transform.rotation.eulerAngles;

        targetRot.x -= rotAmountY;
        targetRot.y += rotAmountX;
        xMouseClamp -= rotAmountY;

        if (xMouseClamp > maxY)
        {
            xMouseClamp = maxY;
            targetRot.x = maxY;
        }
        else if (xMouseClamp < minY)
        {
            xMouseClamp = minY;
            targetRot.x = minY;
        }

        transform.rotation = Quaternion.Euler(targetRot);

        if (oldRotation == this.transform.rotation.eulerAngles)
        {

        }
        else
        {
            oldRotation = this.transform.rotation.eulerAngles;
            
        }

    }

    void PlayerMoveing()
    {
        float MoveHoriz = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");
        Vector3 DirFor = new Vector3();
        Vector3 DirRig = new Vector3();

        if (Input.GetButton("Fire3"))
        {
            DirRig = transform.right * MoveHoriz * Running_Speed;
            DirFor = transform.forward * MoveVertical * Running_Speed;
        }

        else
        {
            DirRig = transform.right * Walking_Speed * MoveHoriz;
            DirFor = transform.forward * Walking_Speed * MoveVertical;
        }

        movingVector.y -= gravity * Time.deltaTime;
        movingVector.x = DirFor.x + DirRig.x;
        movingVector.z = DirFor.z + DirRig.z;
        rg.Move(movingVector * Time.deltaTime);

        if (movingVector.y <= Jump_Speed && movingVector.y >= 0)
            isJumping = true;   
    }

    void OnTriggerStay(Collider MaterialCheck)
    {
        if (MaterialCheck.name == "TriggerWood")
        {
            WoodValue = 1f;
            ForestValue = 0f;
            SandValue = 0f;
            GravelValue = 0f;
            StoneValue = 0f;
        }

        else if (MaterialCheck.name == "TriggerGravel")
        {
            WoodValue = 0f;
            ForestValue = 0f;
            SandValue = 0f;
            GravelValue = 1f;
            StoneValue = 0f;
        }

        else if (MaterialCheck.name == "TriggerSand")
        {
            WoodValue = 0f;
            ForestValue = 0f;
            SandValue = 1f;
            GravelValue = 0f;
            StoneValue = 0f;
        }

        else if (MaterialCheck.name == "TriggerStone")
        {
            WoodValue = 0f;
            ForestValue = 0f;
            SandValue = 0f;
            GravelValue = 0f;
            StoneValue = 1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        WoodValue = 0f;
        ForestValue = 1f;
        SandValue = 0f;
        GravelValue = 0f;
        StoneValue = 0f;
        isInBush = false;
    }

    void FootPlay()
    {
        if (isMoving)
        {
            if (isInBush)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Player/BushNoiseFootStep", this.transform.position);
                isInBush = false;
            }

            ForestParameter.setValue(ForestValue);
            SandParameter.setValue(SandValue);
            WoodParameter.setValue(WoodValue);
            GravelParameter.setValue(GravelValue);
            StoneParameter.setValue(StoneValue);
            FootSound.start();
        }
    }

    public void BushCollider()
    {
        isInBush = true;
    }

    public bool GetisMoving()
    {
        return isMoving;
    }
}
