using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public InputField idInputField;
    public InputField passwordInputField;

    public GameObject loginUI;
    public GameObject bankUI;

    public GameObject ErrorPopup;
    public TextMeshProUGUI errorMessage;

    public GameObject registerPopup;
    public TMP_InputField registerNameInput;

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


    public void OnLoginButtonClicked()
    {
        string inputId = idInputField.text;
        string inputPassword = passwordInputField.text;

        GameManager.Instance.LoadUserData();
        var matchedUser = GameManager.Instance.allUsers
            .Find(user => user.ID == inputId && user.PassWord == inputPassword);

        if (matchedUser != null)
        {
            Debug.Log("로그인에 성공하셨습니다.");
            GameManager.Instance.userData = matchedUser;

            loginUI.SetActive(false);
            bankUI.SetActive(true);

            FindObjectOfType<UserMoneyFormatter>()?.Refresh();
        }
        else
        {
            ShowError("아이디 또는 비밀번호가 틀렸습니다.");
        }
    }

    public void OnRegisterButtonClicked()
    {
        GameManager.Instance.LoadUserData();

        bool isDuplicate = GameManager.Instance.allUsers
    .Any(user => user.ID == idInputField.text);

        if (isDuplicate)
        {
            ShowError("이미 존재하는 아이디입니다.");
            return;
        }

        string password = passwordInputField.text;

        if (string.IsNullOrWhiteSpace(password) || password.Length < 4)
        {
            ShowError("비밀번호는 4글자 이상 입력해주세요.");
            return;
        }

        // 아이디가 중복이 아니며, 비밀번호의 글자 제한도 만족할 경우, 회원가입 완료 이후 이름 입력 UI 띄우기
        registerPopup.SetActive(true);
    }

    public void OnRegisterConfirmButtonClicked()
    {
        string inputName = registerNameInput.text;
        string inputId = idInputField.text;
        string inputPassword = passwordInputField.text;

        if (string.IsNullOrWhiteSpace(inputName))
        {
            ShowError("이름을 입력하세요.");
            return;
        }

        if (!Regex.IsMatch(inputName, @"^[가-힣]+$"))
        {
            ShowError("이름은 한글만 입력할 수 있습니다.");
            return;
        }

        if (inputName.Length > 5)
        {
            ShowError("이름은 5자 이하로 입력하세요.");
            return;
        }

        UserData newUser = new UserData(inputName, 100000, 50000, inputId, inputPassword);
        GameManager.Instance.allUsers.Add(newUser);
        GameManager.Instance.SaveUserData();

        registerPopup.SetActive(false);
        idInputField.text = "";
        passwordInputField.text = "";
    }

    void ShowError(string message)
    {
        ErrorPopup.SetActive(true);
        errorMessage.text = message;
    }

    public void OnErrorConfirmButtonClicked()
    {
        ErrorPopup.SetActive(false);
    }


    // 로그인 매니저 구조에 대해 먼저 작성하기
    // 아이디와 패스워드의 인풋 텍스트를 받아온다.

    // 로그인 버튼을 눌렀을 경우, 아이디와 비밀번호가 데이터 상에서 존재하고, 동일한지 비교한다.

    // 동일할 경우, 그 아이디와 비밀번호를 가진 유저 데이터의 이름과 금액이 저장되어 있다면 불러온다.

    // 데이터가 존재하지 않을 경우, 임의로 금액을 조정하여 데이터를 설정한다.

    // 그리고 최종적으로 로그인 UI를 비활성화 시키고 뱅크 UI 를 활성화 시킨다.

    // 만일 아이디와 비밀번호가 하나라도 동일하지 않을 경우 아이디나 비밀번호가 틀렸다는 오류 창을 띄워주고 확인 버튼을 누르면 꺼지도록 만들어준다.


    // 회원가입 버튼을 눌렀을 경우, 아이디가 데이터 상에 존재하는 지 비교한다.

    // 아이디의 데이터가 중복이 아니라면, 이름을 입력하는 창과 입력 완료 버튼을 위에 띄운다.

    // 그리고 한글을 알맞게 입력했을 시 중복이 아니라면 , 유저 데이터에 이름과 아이디 , 비밀번호를 저장해준다.

    // 이후에 이름을 작성하던 창은 지우고 아이디와 비밀번호가 입력된 인풋 필드의 텍스트 값은 공백으로 다시 초기화 시켜준다.

    // 만일 아이디의 데이터가 중복이라면, 이미 중복된 아이디라는 오류 창을 띄워주고 확인 버튼을 누르면 꺼지도록 만들어준다.
}
