using System;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class Spaceship : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Private Serialized Properties:
    // ------------------------------
    //   pitchTorque
    //   rollTorque
    //   strafeThrust
    //   thrust
    //   upThrust
    //   yawTorque 
    //
    //   glideReductionLeftRight
    //   glideReductionThrust
    //   glideReductionUpDown
    // -------------------------------------------------------------------------

    #region .  Private Serialized Properties  .

    [Header("=== Spaceship Movement Settings ===")]
    [SerializeField] private float pitchTorque  = 1000f;
    [SerializeField] private float rollTorque   = 1000f;
    [SerializeField] private float strafeThrust = 50f;
    [SerializeField] private float thrust       = 100f;
    [SerializeField] private float upThrust     = 50f;
    [SerializeField] private float yawTorque    = 500f;

    [SerializeField, Range(0.001f, 0.999f)] private float glideReductionLeftRight = 0.111f;
    [SerializeField, Range(0.001f, 0.999f)] private float glideReductionThrust    = 0.999f;
    [SerializeField, Range(0.001f, 0.999f)] private float glideReductionUpDown    = 0.111f;

    #endregion


    // -------------------------------------------------------------------------
    // Private Properties:
    // -------------------
    //   _glide
    //   _rb
    //
    // Input values:
    // -------------
    //   _pitchYaw
    //   _roll1D;
    //   _strafe1D;
    //   _strafe1D;
    //   _thrust1D;
    //   _upDown1D;
    // -------------------------------------------------------------------------

    #region .  Private Properties  .

    private float     _glide = 0f;
    private Rigidbody _rb;

    // Input values:
    private Vector2 _pitchYaw;
    private float   _roll1D;
    private float   _strafe1D;
    private float   _thrust1D;
    private float   _upDown1D;

    #endregion


    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   OnPitchYaw()
    //   OnRoll()
    //   OnStrafe()
    //   OnThrust()
    //   OnUpDown()
    // -------------------------------------------------------------------------

    #region .  OnPitchYaw()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnPitchYaw()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void OnPitchYaw(InputAction.CallbackContext context)
    {
        this._pitchYaw = context.ReadValue<Vector2>();

    }   // OnPitchYaw()
    #endregion


    #region .  OnRoll()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnRoll()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void OnRoll(InputAction.CallbackContext context)
    {
        this._roll1D = context.ReadValue<float>();

    }   // OnRoll()
    #endregion


    #region .  OnStrafe()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnStrafe()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void OnStrafe(InputAction.CallbackContext context)
    {
        this._strafe1D = context.ReadValue<float>();

    }   // OnStrafe()
    #endregion


    #region .  OnThrust()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnThrust()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void OnThrust(InputAction.CallbackContext context)
    {
        this._thrust1D = context.ReadValue<float>();

    }   // OnThrust()
    #endregion


    #region .  OnUpDown()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnUpDown()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void OnUpDown(InputAction.CallbackContext context)
    {
        this._upDown1D = context.ReadValue<float>();

    }   // OnUpDown()
    #endregion


    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   FixedUpdate()
    //   HandleMovement()
    //   Start()
    // -------------------------------------------------------------------------

    #region .  FixedUpdate()  .
    // -------------------------------------------------------------------------
    //   Method.......:  FixedUpdate()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void FixedUpdate()
    {
        this.HandleMovement();

        this.ShowValues();

    }   // Update()
    #endregion


    #region .  HandleMovement()  .
    // -------------------------------------------------------------------------
    //   Method.......:  HandleMovement()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void HandleMovement()
    {
        // Roll.
        this._rb.AddRelativeTorque(Vector3.back * this._roll1D * this.rollTorque * Time.deltaTime);
        //this._rb.AddRelativeTorque(this._roll1D * this.rollTorque * Vector3.back);

        // Pitch.
        this._rb.AddRelativeTorque(Vector3.right * Mathf.Clamp(-this._pitchYaw.y, -1f, 1f) * this.pitchTorque * Time.deltaTime);
        //this._rb.AddRelativeTorque(Mathf.Clamp(-this._pitchYaw.y, -1f, 1f) * this.pitchTorque * Vector3.right);

        // Yaw.
        this._rb.AddRelativeTorque(Vector3.up * Mathf.Clamp(this._pitchYaw.x, -1f, 1f) * this.yawTorque * Time.deltaTime);
        //this._rb.AddRelativeTorque(Mathf.Clamp(this._pitchYaw.x, -1f, 1f) * this.yawTorque * Vector3.up);

        //// Thrust.
        //if (this._thrust1D > 0.1f || this._thrust1D < -0.1f)
        //{
        //    float currentThrust = this.thrust;

        //    this._rb.AddRelativeForce(Vector3.forward * currentThrust * this._thrust1D * Time.deltaTime);
        //    //this._rb.AddRelativeForce(currentThrust * this._thrust1D * Vector3.forward);
        //    this._glide = thrust;
        //}
        //else
        //{
        //    this._rb.AddRelativeForce(Vector3.forward * this._glide * Time.deltaTime);
        //    //this._rb.AddRelativeForce(this._glide * Vector3.forward);
        //    this._glide *= this.glideReductionThrust;
        //}

    }   // HandleMovement()
    #endregion


    #region .  ShowValues()  .
    // -------------------------------------------------------------------------
    //   Method.......:  ShowValues()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void ShowValues()
    {
        Debug.Log($"PitchYaw: {this._pitchYaw},  Roll: {this._roll1D},  Strafe: {this._strafe1D},  Thrust: {this._thrust1D},  UpDown: {this._upDown1D},  : {this._glide}");

    }   // ShowValues()
    #endregion


    #region .  Start()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Start()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Start()
    {
        this._rb = this.GetComponent<Rigidbody>();

    }   // Start()
    #endregion


}   // class Spaceship
