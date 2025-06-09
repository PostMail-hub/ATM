using UnityEngine;

public class BankUIController : MonoBehaviour
{
    public GameObject LoginPanel;

    public GameObject BankPanel;

    public GameObject ATMPanel;

    public GameObject DepositPanel;

    public GameObject WithdrawalPanel;

    public GameObject BalancePopupBG;

    public GameObject ErrorPopup;

    public GameObject RegisterPopup;

    // 오브젝트가 없을 경우에 아래와 같은 이름의 오브젝트를 자동으로 참조
    private void Awake()
    {

        if (BankPanel == null)
        {
            GameObject bank = GameObject.Find("PopupBank");
            if (bank != null)
            {
                BankPanel = bank;
            }
        }

        if (LoginPanel == null)
        {
            GameObject login = GameObject.Find("PopupLogin");
            if (login != null)
            {
                LoginPanel = login;
            }
        }

        if (ATMPanel == null)
        {
            GameObject atm = GameObject.Find("ATM");
            if (atm != null)
            {
                ATMPanel = atm;
            }
        }

        if (DepositPanel == null)
        {
            GameObject deposit = GameObject.Find("Deposit");
            if (deposit != null)
            {
                DepositPanel = deposit;
            }
        }

        if (WithdrawalPanel == null)
        {
            GameObject withdrawal = GameObject.Find("Withdraw");
            if (withdrawal != null)
            {
                WithdrawalPanel = withdrawal;
            }
        }

        if (BalancePopupBG == null)
        {
            GameObject balancePopupBG = GameObject.Find("BalancePopupBG");
            if (balancePopupBG != null)
            {
                BalancePopupBG = balancePopupBG;
            }
        }

        if (BalancePopupBG == null)
        {
            GameObject balancePopupBG = GameObject.Find("BalancePopupBG");
            if (balancePopupBG != null)
            {
                BalancePopupBG = balancePopupBG;
            }
        }

        if (ErrorPopup == null)
        {
            GameObject errorpopupObj = GameObject.Find("ErrorPopup");
            if (errorpopupObj != null)
            {
                ErrorPopup = errorpopupObj;
            }
        }

        if (RegisterPopup == null)
        {
            GameObject registerPopupObj = GameObject.Find("RegisterPopup");
            if (registerPopupObj != null)
            {
                RegisterPopup = registerPopupObj;
            }
        }

    }

    // 게임 시작 시 , 로그인 창과 atm 버튼을 냅두고 모두 닫는다.
    private void Start()
    {
        LoginPanel?.SetActive(true);
        BankPanel?.SetActive(false);
        ATMPanel?.SetActive(true);
        DepositPanel?.SetActive(false);
        WithdrawalPanel?.SetActive(false);
        BalancePopupBG?.SetActive(false);
        ErrorPopup?.SetActive(false);
        RegisterPopup?.SetActive(false);
    }

    // 확장성을 고려하여 다른 판넬을 닫고 원하는 판넬만 true로 킬 수 있도록 스위치 판넬 함수로 관리
    public void SwitchPanel(GameObject targetPanel)
    {
        ATMPanel.SetActive(false);
        DepositPanel.SetActive(false);
        WithdrawalPanel.SetActive(false);

        targetPanel.SetActive(true);
    }

    // 로그인 성공 시, 로그인 화면은 끄고 뱅크 화면으로 진입한다.
    public void OnClickLogin()
    {
        LoginPanel?.SetActive(false);
        SwitchPanel(BankPanel);
    }

    // 스위치 판넬 함수를 DepositPanel 판넬을 타겟으로 하여 실행
    // ATM 화면에서 입금 버튼 클릭 시 , 입금 화면 출력
    public void OnClickDeposit()
    {
        SwitchPanel(DepositPanel);
    }

    // 스위치 판넬 함수를 WithdrawalPanel 판넬을 타겟으로 하여 실행
    // ATM 화면에서 출금 버튼 클릭 시 , 출금 화면 출력
    public void OnClickWithdrawal()
    {
        SwitchPanel(WithdrawalPanel);
    }

    // 스위치 판넬 함수를 ATMPanel 판넬을 타겟으로 하여 실행
    // 입금 , 출금 화면에서 뒤로 가기 버튼 클릭 시, ATM 화면 출력
    public void OnClickATM()
    {
        SwitchPanel(ATMPanel);
    }

    // 잔액이 부족할 경우에 띄울 화면
    public void ShowBalanceWarningPopup()
    {
        BalancePopupBG?.SetActive(true);
    }

    // 화면에 떠있는 잔액 부족 창만 끄면 되므로 직접 fasle로 변경
    public void OnClickClosePopup()
    {
        BalancePopupBG?.SetActive(false);
    }
}
