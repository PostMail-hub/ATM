using UnityEngine;


public class PopupBank : MonoBehaviour
{
    [SerializeField]
    public GameObject ATMPanel;

    [SerializeField]
    public GameObject DepositPanel;

    [SerializeField]
    public GameObject WithdrawalPanel;

    private GameManager gm;

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
    }

    // 게임 시작 시 , ATM 외의 모든 창은 닫는다.
    private void Start()
    {
        ATMPanel?.SetActive(true);
        DepositPanel?.SetActive(false);
        WithdrawalPanel?.SetActive(false);
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
}
