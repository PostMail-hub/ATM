using UnityEngine;
using UnityEngine.UI;


public class PopupBank : MonoBehaviour
{
    BankUIController bankUIController;

    private GameManager gm;

    private UserData userData;

    public UserMoneyFormatter userMoneyFormatter;

    public InputField DepositInputField;
    public InputField WithdrawInputField;

    // 오브젝트가 없을 경우에 아래와 같은 이름의 오브젝트를 자동으로 참조
    private void Awake()
    {
        gm = GameManager.Instance;

        if (bankUIController == null)
        {
            GameObject uiControllerObj = GameObject.Find("UIController");
            if (uiControllerObj != null)
            {
                bankUIController = uiControllerObj.GetComponent<BankUIController>();
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
            bankUIController.ShowBalanceWarningPopup();
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
            bankUIController.ShowBalanceWarningPopup();
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
