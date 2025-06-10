using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public InputField idInputField;             // 아이디 입력 인풋 필드
    public InputField passwordInputField;       // 비밀번호 입력 인풋 필드
                                                
    public GameObject loginUI;                  // 로그인 UI 창
    public GameObject bankUI;                   // 뱅크 UI 창
                                                
    public GameObject ErrorPopup;               // 에러 UI 창
    public TextMeshProUGUI errorMessage;        // 에러 UI 에서 뜨는 TextMeshPro 메시지

    public GameObject registerPopup;            // 회원가입 성공 시, 이름을 입력하는 UI 창
    public TMP_InputField registerNameInput;    // 이름 입력 인풋 필드


    // UI 오브젝트들이 인스펙터에서 할당되지 않은 경우, 씬에서 찾아 자동 연결
    private void Awake()
    {
        if (idInputField == null)
        {
            GameObject idObj = GameObject.Find("InPutID");
            if (idObj != null)
            {
                idInputField = idObj.GetComponent<InputField>();
            }
        }

        if (passwordInputField == null)
        {
            GameObject passwordObj = GameObject.Find("InputPassword");
            if (passwordObj != null)
            {
                passwordInputField = passwordObj.GetComponent<InputField>();
            }
        }

        if (registerNameInput == null)
        {
            GameObject nameObj = GameObject.Find("InputName");
            if (nameObj != null)
            {
                registerNameInput = nameObj.GetComponent<TMP_InputField>();
            }
        }

        if (loginUI == null)
        {
            GameObject bank = GameObject.Find("PopupLogin");
            if (bank != null)
            {
                loginUI = bank;
            }
        }

        if (bankUI == null)
        {
            GameObject bank = GameObject.Find("PopupBank");
            if (bank != null)
            {
                bankUI = bank;
            }
        }

        if (ErrorPopup == null)
        {
            GameObject popupObj = GameObject.Find("ErrorPopup");
            if (popupObj != null)
            {
                ErrorPopup = popupObj;
            }
        }

        if (errorMessage == null)
        {
            GameObject textObj = GameObject.Find("ErrorText");
            if (textObj != null)
            {
                errorMessage = textObj.GetComponent<TextMeshProUGUI>();
            }
        }

        if (registerPopup == null)
        {
            GameObject registerPopupObj = GameObject.Find("RegisterPopup");
            if (registerPopupObj != null)
            {
                registerPopup = registerPopupObj;
            }
        }
    }

    // 로그인 버튼 클릭 시 호출되는 메서드
    public void OnLoginButtonClicked()
    {
        string inputId = idInputField.text;
        string inputPassword = passwordInputField.text;

        // 저장된 유저 데이터를 불러옴
        GameManager.Instance.LoadUserData();

        // 아이디와 비밀번호가 일치하는 유저 탐색
        var matchedUser = GameManager.Instance.allUsers
            .Find(user => user.ID == inputId && user.PassWord == inputPassword);

        if (matchedUser != null)
        {
            // 로그인 성공 시 유저 데이터를 적용하고 UI 전환
            Debug.Log("로그인에 성공하셨습니다.");
            GameManager.Instance.userData = matchedUser;

            loginUI.SetActive(false);
            bankUI.SetActive(true);

            // 유저 잔액 등 데이터 새로 고침
            FindObjectOfType<UserMoneyFormatter>()?.Refresh();
        }
        else
        {
            // 로그인 실패 시 에러 팝업 표시
            ShowError("아이디 또는 비밀번호가 틀렸습니다.");
        }
    }

    // 회원가입 버튼 클릭 시 호출되는 메서드
    public void OnRegisterButtonClicked()
    {
        GameManager.Instance.LoadUserData();

        // 이미 존재하는 아이디인지 확인
        bool isDuplicate = GameManager.Instance.allUsers
    .Any(user => user.ID == idInputField.text);

        if (isDuplicate)
        {
            ShowError("이미 존재하는 아이디입니다.");
            return;
        }

        // 비밀번호 유효성 검사
        string password = passwordInputField.text;

        if (string.IsNullOrWhiteSpace(password) || password.Length < 4)
        {
            ShowError("비밀번호는 4글자 이상 입력해주세요.");
            return;
        }

        // 조건을 만족하면 이름 입력창을 활성화하여 회원가입 계속 진행
        registerPopup.SetActive(true);
    }

    // 회원가입 이름 확인 버튼 클릭 시 호출되는 메서드
    public void OnRegisterConfirmButtonClicked()
    {
        string inputName = registerNameInput.text;
        string inputId = idInputField.text;
        string inputPassword = passwordInputField.text;

        // 이름 입력 확인
        if (string.IsNullOrWhiteSpace(inputName))
        {
            ShowError("이름을 입력하세요.");
            return;
        }

        // 이름 유효성 검사 (한글만 허용)
        if (!Regex.IsMatch(inputName, @"^[가-힣]+$"))
        {
            ShowError("이름은 한글만 입력할 수 있습니다.");
            return;
        }

        // 이름 유효성 검사(5글자 이하만 허용)
        if (inputName.Length > 5)
        {
            ShowError("이름은 5자 이하로 입력하세요.");
            return;
        }

        // 새 유저 생성 및 저장
        UserData newUser = new UserData(inputName, 100000, 50000, inputId, inputPassword);
        GameManager.Instance.allUsers.Add(newUser);
        GameManager.Instance.SaveUserData();

        // 회원가입 UI 종료 및 입력 필드 초기화
        registerPopup.SetActive(false);
        idInputField.text = "";
        passwordInputField.text = "";
    }

    // 에러 팝업 활성화 및 메시지 출력
    void ShowError(string message)
    {
        ErrorPopup.SetActive(true);
        errorMessage.text = message;
    }

    // 에러 팝업 확인 버튼 클릭 시 호출
    public void OnErrorConfirmButtonClicked()
    {
        ErrorPopup.SetActive(false);
    }
}
