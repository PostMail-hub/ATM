using System.IO;
using UnityEngine;

/// GameManager는 게임 전반의 데이터를 관리하는 싱글톤 클래스

/// 게임 시작 시 UserData를 초기화하여 유저 정보를 저장한다.
/// Instance를 통해 어디서든 전역 접근이 가능하다.

public class GameManager : MonoBehaviour
{
    // GameManager의 싱글톤 인스턴스
    // 다른 스크립트에서 GameManager.Instance로 접근할 수 있다.
    public static GameManager Instance;

    // 유저 정보를 저장하는 데이터 객체
    // 이름, 현금, 통장 잔액 등의 정보를 포함합니다.
    public UserData userData;

    // JSON 파일이 저장될 경로
    private string path;

    // 저장될 JSON 파일 이름
    private string fileName = "/save";

    // 게임이 시작될 때 호출되며,
    // 싱글톤 인스턴스를 초기화하고 유저 데이터를 불러온다.
    // 만약, 데이터가 없다면 최초 기본 데이터를 제공하고 저장한다.
    private void Awake()
    {
        Instance = this;

        path = Application.persistentDataPath + fileName;

        if (File.Exists(path))
        {
            LoadUserData();     // 기존 저장된 데이터 불러오기
        }
        else
        {
            // 최초 실행이라면 기본값 세팅
            userData = new UserData("이희민", 100000, 50000);
            SaveUserData();    // 기본 데이터 저장
        }
    }

    // 현재 userData 객체를 JSON 문자열로 직렬화하여 파일로 저장한다.
    // 값이 변경될 때마다 이 함수를 호출하여 자동 저장이 가능하도록 만들 수 있다.
    public void SaveUserData()
    {
        string data = JsonUtility.ToJson(userData);
        File.WriteAllText(path, data);
    }

    // 저장된 JSON 파일을 읽어 userData 객체로 역직렬화한다.
    // 파일이 존재하지 않으면 기본 데이터를 저장한 뒤 불러온다.
    public void LoadUserData()
    {
        if (!File.Exists(path))
        {
            SaveUserData();        // 파일이 없을 경우 기본 데이터 저장
        }

        string data = File.ReadAllText(path);
        userData = JsonUtility.FromJson<UserData>(data);
    }
}