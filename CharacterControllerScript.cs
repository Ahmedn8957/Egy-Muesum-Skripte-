using TMPro;
using UnityEngine;
using UnityEngine.Video;
public class CharacterControllerScript : MonoBehaviour
{
    public float walkSpeed = 2.0f;
    public float rotationSpeed = 5.0f;
    public float mouseSensitivity = 2.0f;
    public Transform cameraTransform;
    public float eyeHeight;
    public AudioSource footstepAudio;
    public GameObject pauseMenuPanel;
    public GameObject backgroundMusic;
    public GameObject videoObject;

    private CharacterController controller;
    private Animator anim;
    private Vector3 moveDirection;
    private float gravity = 9.81f;
    private string currentArtifactName = null;
    private bool isPaused = false;
    private VideoPlayer videoPlayer;

    [SerializeField] private float yaw;
    [SerializeField] private float pitch;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = transform.GetChild(0).GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yaw = transform.eulerAngles.y;

        if (videoObject != null)
        {
            videoPlayer = videoObject.GetComponent<VideoPlayer>();
            if (videoPlayer == null)
                Debug.LogWarning("VideoPlayer component not found on videoObject.");
        }
    }
    void Update()
    {
        if (isPaused)
            return;

        HandleMouseLook();
        HandleMovement();
        UpdateCameraPosition();

        if (Input.GetKeyDown(KeyCode.Space) && !string.IsNullOrEmpty(currentArtifactName))
        {
            MuseumManager.instance.readyToShowText = true;
            MuseumManager.instance.containerBox.SetActive(true);
            MuseumManager.instance.ShowArtifactInfo(currentArtifactName);
            currentArtifactName = null;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuPanel.activeSelf)
                ResumeGame();
            else
                PauseGame();
        }
    }
    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = forward * vertical + right * horizontal;
        movement.Normalize();

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? 7f : walkSpeed;

        if (controller.isGrounded)
        {
            moveDirection = movement * currentSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if (movement.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        bool isMoving = movement.magnitude > 0.1f;
        anim.SetBool("isWalking", isMoving);
        anim.SetBool("isRunning", isMoving && isRunning);

        if (isMoving && controller.isGrounded)
        {
            if (!footstepAudio.isPlaying)
                footstepAudio.Play();

            footstepAudio.pitch = isRunning ? 1.5f : 1.0f;
        }
        else
        {
            footstepAudio.Stop();
        }
    }
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -80f, 45f);

        transform.rotation = Quaternion.Euler(0, yaw, 0);
    }
    void UpdateCameraPosition()
    {
        cameraTransform.position = transform.position + Vector3.up * eyeHeight;
        cameraTransform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Art"))
        {
            var info = other.gameObject.GetComponentInChildren<ArtifactInfo>();
            if (info != null)
            {
                currentArtifactName = other.gameObject.name;
                MuseumManager.instance.Interactiontext.GetComponentInChildren<TMP_Text>().text = "Press Spacebar to see info";
                MuseumManager.instance.Interactiontext.SetActive(true);
            }
            else
            {
                currentArtifactName = null;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Art"))
        {
            MuseumManager.instance.containerBox.SetActive(false);
            MuseumManager.instance.Interactiontext.SetActive(false);
            currentArtifactName = null;
        }
    }
    private bool IsPlayerNearVideo()
    {
        if (videoObject == null) return false;
        float distance = Vector3.Distance(transform.position, videoObject.transform.position);
        return distance < 3f; // Adjust this threshold as needed
    }
    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        if (footstepAudio != null && footstepAudio.isPlaying)
            footstepAudio.Stop();

        if (backgroundMusic != null)
            backgroundMusic.SetActive(false);

        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            videoPlayer.SetDirectAudioMute(0, true);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (backgroundMusic != null)
            backgroundMusic.SetActive(true);

        if (videoPlayer != null && IsPlayerNearVideo())
        {
            videoPlayer.SetDirectAudioMute(0, false);
            videoPlayer.Play();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void resumeVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.SetDirectAudioMute(0, false);
            videoPlayer.Play();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}