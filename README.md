### 🎮 프로젝트 개요
이 프로젝트는 **저희 20 0만 지르조가 직접 개발한** 콘솔 기반의 **텍스트 RPG 게임**입니다.
플레이어는 다양한 직업의 캐릭터를 선택하고, 직업별 아이템을 장착하며, 던전을 탐험하고, 몬스터와 전투를 벌이는 등 전형적인 RPG 요소를 텍스트 인터페이스를 통해 경험할 수 있습니다.

### ✨ 주요 기능

-   **캐릭터 생성 및 관리:**
    
    -   다양한 **직업** 선택 (예: 전사, 마법사, 궁수).
        
    -   캐릭터의 **능력치(스탯)** 관리 (공격력, 방어력, 체력 등).
        
    -   **인벤토리 시스템**을 통한 아이템 관리.
        
-   **아이템 시스템:**
    
    -   **장비 아이템** (무기, 방어구, 장신구 등) 장착 및 해제.
        
    -   **소모품** 사용 (체력 회복 물약 등).
        
    -   아이템별 **직업 제한** 기능.
        
    -   **스택형 아이템** 관리 (소모품 등).
    
    - 아이템 랜덤 박스로 무기, 방어구, 장신구 랜덤으로 뽑기 가능.
        
-   **전투 시스템:**
    
    -   **몬스터**와의 턴제 전투.
        
    -   아이템 장착에 따른 능력치 변화 실시간 반영.
        
-   **던전 탐험:**
    
    -   콘솔 화면에 표시되는 **간단한 맵**을 통한 이동.
        
    -   던전 내 **몬스터 출현 및 아이템 획득** 이벤트.
        
-   **UI/UX:**
    
    -   `AnsiColor`를 활용한 콘솔 텍스트 **색상 변경**으로 시각적인 정보 전달력 향상.
        
    -   **박스 드로잉** 등 콘솔 UI 향상 기능.
        

       

### 📂 프로젝트 구조 (예시)

```
TextRPG_Team20/
├── TextRPG_Team20.sln
├── TextRPG_Team20/
│   ├── Character.cs         # 캐릭터 기본 클래스 (플레이어, 몬스터의 상위 클래스)
│   ├── Inventory.cs         # 인벤토리 관리 클래스
│   ├── Status.cs            # 캐릭터 능력치 정보 클래스
│   ├── Item/                # 아이템 관련 폴더
│   │   ├── Item.cs          # 개별 아이템 인스턴스
│   │   ├── ItemData.cs      # 아이템의 고유 데이터 (이름, 스탯, 타입 등)
│   │   ├── ItemType.cs      # 아이템 타입 (Enum)
│   │   ├── JobType.cs       # 직업 타입 (Enum)
│   │   └── DataManager.cs   # JSON 등에서 아이템 데이터를 로드하는 클래스
│   ├── Dungeon/             # 던전 관련 폴더
│   │   └── DungeonManager.cs # 던전 생성 및 관리
│   ├── Scene/               # 화면/씬 관리 폴더
│   │   └── ConsoleUI.cs     # 콘솔 UI 렌더링 및 입력 처리
│   ├── Game.cs              # 게임 전체 흐름 관리
│   └── Program.cs           # 프로그램 진입점
└── README.md
