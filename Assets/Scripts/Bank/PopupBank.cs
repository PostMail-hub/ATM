using UnityEngine;
using UnityEngine.UI;


public class PopupBank : MonoBehaviour
{
    public GameObject ATMPanel;

    public GameObject DepositPanel;

    public GameObject WithdrawalPanel;

    public GameObject BalancePopupBG;

    private GameManager gm;

    private UserData userData;

    public UserMoneyFormatter userMoneyFormatter;

    public InputField DepositInputField;
    public InputField WithdrawInputField;

    // 오브젝트가 없을 경우에 아래와 같은 이름의 오브젝트를 자동으로 참조
    private void Awake()
    {
        gm = GameManager.Instance;

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

        if (userMoneyFormatter == null)
        {
            GameObject infoObject = GameObject.Find("UserInfo");
            if (infoObject != null)
            {
                userMoneyFormatter = infoObject.GetComponent<UserMoneyFormatter>();
            }
        }

        if (DepositInputField == null)
        {
            GameObject depositInput = GameObject.Find("DepositInputField");
            if (depositInput != null)
            {
                DepositInputField = depositInput.GetComponent<InputField>();
            }
        }

        if (WithdrawInputField == null)
        {
            GameObject withdrawInput = GameObject.Find("WithdrawInputField");
            if (withdrawInput != null)
            {
                WithdrawInputField = withdrawInput.GetComponent<InputField>();
            }
        }
    }

    // 게임 시작 시 , ATM 외의 모든 창은 닫는다.
    private void Start()
    {
        ATMPanel?.SetActive(true);
        DepositPanel?.SetActive(false);
        WithdrawalPanel?.SetActive(false);
        BalancePopupBG?.SetActive(false);
    }

    // 확장성을 고려하여 다른 판넬을 닫고 원하는 판넬만 true로 킬 수 있도록 스위치 판넬 함수로 관리
    public void SwitchPanel(GameObject targetPanel)
    {
        ATMPanel.SetActive(false);
        DepositPanel.SetActive(false);
        WithdrawalPanel.SetActive(false);

        targetPanel.SetActive(true);
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

    // 화면에 떠있는 잔액 부족 창만 끄면 되므로 직접 fasle로 변경
    public void OnClickClosePopup()
    {
        BalancePopupBG?.SetActive(false);
    }

    // 버튼에 할당된 amount 값을 불러와서 그 금액만큼 현금을 - 해주고 은행 잔고를 + 해준다.
    public void Deposit(int amount)
    {
        UserData userData = GameManager.Instance.userData;

        // 만약 현재 보유 중인 현금이 은행에 넣어줄 잔고보다 크거나 같다면
        if (amount <= userData.Cash)
        {
            userData.Cash -= amount;             // 현재 현금을 지정된 값 만큼 빼준다.
            userData.AccountBalance += amount;   // 은행 잔고를 지정된 값 만큼 증가 시켜준다.
            GameManager.Instance.SaveUserData(); // 데이터를 저장해준다.
        }
        // 금액이 부족하다면 금액이 부족하다는 알림 팝업 열기
        else
        {
            BalancePopupBG?.SetActive(true);
        }

        userMoneyFormatter.Refresh();           // UI에 변경 사항을 즉시 적용
    }

    // 버튼에 할당된 amount 값을 불러와서 그 금액만큼 은행 잔고를 - 해주고 현금을 + 해준다.
    public void Withdrawal(int amount)
    {
        UserData userData = GameManager.Instance.userData;

        // 만약 현재 은행 잔고가 현금으로 인출할 금액보다 크거나 같다면
        if (amount <= userData.AccountBalance)
        {
            userData.AccountBalance -= amount;      // 은행 잔고를 지정된 값 만큼 증가 시켜준다.
            userData.Cash += amount;                // 현재 현금을 지정된 값 만큼 빼준다.
            GameManager.Instance.SaveUserData();    // 데이터를 저장해준다.
        }
        // 금액이 부족하다면 금액이 부족하다는 알림 팝업 열기
        else
        {
            BalancePopupBG?.SetActive(true);
        }

        userMoneyFormatter.Refresh();              // UI에 변경 사항을 즉시 적용
    }

    // 직접 입력한 값이 문자열이니 이를 int로 변환하여 Deposit 에 넣어준다.
    public void TryDeposit()
    {
        if (int.TryParse(DepositInputField.text, out int amount))
        {
            Deposit(amount);  // 입금 메서드 호출
        }
    }

    // 직접 입력한 값이 문자열이니 이를 int로 변환하여 Withdrawal 에 넣어준다.
    public void TryWithdraw()
    {
        if (int.TryParse(WithdrawInputField.text, out int amount))
        {
            Withdrawal(amount);  // 출금 메서드 호출
        }
    }
}
