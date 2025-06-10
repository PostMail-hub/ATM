using System.Collections.Generic;
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

    // 전체 유저 데이터를 저장하는 리스트
    public List<UserData> allUsers = new List<UserData>();

    // 현재 로그인한 유저 정보
    public UserData currentUser;

    // JSON 파일이 저장될 경로
    private string path;

    // 저장될 JSON 파일 이름
    private string fileName = "/save";

    // 게임이 시작될 때 호출되며,
    // 싱글톤 인스턴스를 초기화하고 유저 데이터를 불러온다.
    // 만약, 데이터가 없다면 최초 기본 데이터를 제공하고 저장한다.
    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        Instance = this;

        // 파일 경로 설정
        path = Application.persistentDataPath + fileName;

        if (File.Exists(path))
        {
            LoadUserData();     // 기존 저장된 데이터 불러오기
        }
        else
        {
            // 데이터가 없으면 빈 리스트로 초기화 후 저장
            allUsers = new List<UserData>();
            SaveUserData();
        }
    }

    // 유저 데이터를 JSON 형식으로 저장한다.
    public void SaveUserData()
    {
        // 리스트를 감싸는 래퍼 클래스 사용 후 JSON으로 변환
        string data = JsonUtility.ToJson(new UserListWrapper(allUsers));
        // 지정된 경로에 파일 저장
        File.WriteAllText(path, data);
    }

    // JSON 형식으로 저장된 유저 데이터를 불러온다.
    public void LoadUserData()
    {
        // 파일에서 JSON 문자열을 읽어옴
        string data = File.ReadAllText(path);
        // 문자열을 객체로 역직렬화하여 리스트로 저장
        allUsers = JsonUtility.FromJson<UserListWrapper>(data).users;
    }

    // JsonUtility가 리스트를 처리할 수 있도록 래퍼 클래스를 정의
    [System.Serializable]
    public class UserListWrapper
    {
        public List<UserData> users;
        public UserListWrapper(List<UserData> list) => users = list;
    }
}