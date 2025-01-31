# Castle of Light Unity Game Design - Team SkyLine

-**Click on Following thumbnail to watch the video**
<div align="center">
<a href="https://www.youtube.com/watch?v=I8CCBwNVUak">
  <img src="https://img.youtube.com/vi/I8CCBwNVUak/0.jpg" width="540" height="380" alt="Watch the video">
</a>
</div>

## Project Overview

This is a 2d isometric type game that made for promote energy saving among energy users. When a user enters to the game first ever time he/she is directed to a seperate web page which contains a quiz consists of 10 questions on energy saving strategies. Once he/she completes it, a amount of gems are gifted to the user as propotional to the correct answers given. Then again the user is redirected to the game then their are some activities he a user can do inside the game and the actual realtime data comes from user's home appears on the each users game environment effectively, these will be discussed later on.


## Quiz Web App
<div style="display: flex; justify-content: space-between; align-items: center;">
  <img src="https://drive.google.com/uc?id=1W51LEwKUBYpgmqBDI1NgVDs2BsOCWE0e" alt="Alt text" width="400">
  <img src="https://drive.google.com/uc?id=1Hbc6uWI94baHjARSk_FiNDUvrz3B1WtY" alt="Alt text" width="400">
  <img src="https://drive.google.com/uc?id=1toO5KQ29jI_r5wWKdSk3tRLeieulmraV" alt="Alt text" width="400">
  <img src="https://drive.google.com/uc?id=1EwqVZkdTaDy3l4VqktfMRt9b3GgQ5zHQ" alt="Alt text" width="400">
</div>

This Quiz is made using React+Vite frontend technology. After redirection from the Unity game first opens up a start page with the instructions that a user should comply with, Once the user proceeds with quiz he can't navigate back since quiz is a sequential at the end of the quiz he/she can review the answers given with correct answers. Finally he/she gifted a right amount of gems proportion to the accurate answers. For each accurate question 10 Gems and maximum of 100 for all correct answers.
-**Major Used APIs:** :

    - Before the Questionnaire
        - POST-http://localhost:8081/api/v1/player/log
        - GET-http://localhost:8081/api/v1/player/answer/{id}
        - GET-http://localhost:8081/api/questions/1
    - After the Questionnaire
        - POST-http://localhost:8081/api/v1/player/saveAnswers
        - GET-http://localhost:8081/api/v1/player/answer/{id}

-**Security System Used:** :

    - JWT Authentication:
        -Spring Boot generates JWT for GET requests from the frontend.
        - Ensures secure communication between frontend and backend.
        
## Unity Game
### Game Overview
Game story is woven arround a king and his kingdom this is a RPG game and player who plays the game will play the role of the king. The king should have to save money to make his kingdom protect from permenant dark lights. He should effectively expend his money to buy resourses like houses, wind malls, bars, life generators and forges which also strengthen the prosperity of the kingdom. Life time generators are essential for keeping the kings life time within the game play. The API data which contains users initial power usage affect to shrinks or expand the trees in the environment to make the user aware of the real time power usage. Considering his mid and long term energy usage there is an algorithm running to offer free golds and gems to the player, if the user waste energy the algorithm works vise-versa and reduce the existing amount of Gold and Gems. The Same algorithm decides the dimmimg of the enviromnet also.

<div style="display: flex; justify-content: space-between; align-items: center;">
  <img src="https://drive.google.com/uc?id=16lsK7pnL35aMut_tX30zhGl8urGHJsKH" alt="Alt text" width="400">
  <img src="https://drive.google.com/uc?id=1tJfbWn5pxsRCRJnijtiFkHCT7bIfU8Ll" alt="Alt text" width="400">
  <img src="https://drive.google.com/uc?id=1kz3BwNwnMXteeAGfq08T64fU4D019zNo" alt="Alt text" width="400">
  <img src="https://drive.google.com/uc?id=1-ZAdxFBzSxIFVYpC9n_5dmefG-zz178d" alt="Alt text" width="400">
</div>

### Game Technical Implementation
#### User Contollers

- left arrow  ← : Move left 
- Right Arrow  → : Move right
- Up Arrow  ↑ : Move up
- Down Arrow  ↓ : Move down

#### Scripts and Usage.

| C # Script | Usage | 
|----------|----------|
| CameraController | Controlls the main camera to move along with the player and Controlls the side map view camera | 
| CoinSpawner| Spawn coins Randomly within the game environment |
| DragDrop | Drag and Drop the Elements in the game environment to organize |
| EnergySavingAlgorithm | Run the proposed algorithm and send the results to the environment controller |
| EnvironmentController | Row 1, Col 2 |
| GameData | Row 1, Col 2 |
| GemGenerator | Generate Gems When Needed|
| GotoCastle | Shift to the Castle While in the Garden |
| GotoGarden | Shift to the Garden While in the Castle |
| HealthBar | Shows the Player Health |
| HideColliderColor | Demolishes the Color of Collider objects used |
| ItemCollector | Collect and update the count of each hearts, gems and coins cateories |
| ItemSpawn | Spawn houses and other building within the game environment |
| PlayerAnimation| Animate the player |
| PlayerMovements | Controlls the movements of the player |
| PopupMessageUI | Create a popup message when a message given|
| ShopItem | Blue Print of a shop object|
| ShopManager | Keep the Track of the shop items available and manage |
| ShopTemplate | Visualization of the shop|
| TipsGenerator | Random tips related to energy saving  |
| BackMethod | Implementationof the back to the main page |
| GotoQuetionnaire | Redirect to the quiz if a new player|
| PostMethod | REST API Post method implementation |
| QuitButton | Quit the game |
| reviewQuiz | If the player wants to review the quiz this can redirect the player |
| update_one_player | Update the player details |
| view_one_player | View Player Details |
| view_profile | Player full profile view |

#### Energy Saving Amount Representation Algorithm.

* Calculate Following Parameters
    - Parameter 1 = Power Usage of yesterday - Power usage of day before yesterday
    - Parameter 2 = Recent Week Average power usage - Week before the recent week Average power usage Final Score = (Parameter 1/30) *50 + (Parameter 2/5) *50
* Current power consumption data
    - The current power usage which is obtained from the API updated by every 10 seconds, of compared with 3 different threshold values which are set based on data an average play And this generated values is used to decide the life time of the player.
 
## Spring Boot Backend

This backend application is designed to handle authentication, player session management, and questionnaire functionalities. It validates JWT tokens from other Spring backend microservices and generates new JWT tokens for authorized players. The application supports CRUD operations for players, questionnaire data, and player answers while ensuring seamless integration with a MySQL database. Built with Spring Boot, the backend follows a modular architecture for scalability and maintainability.

## Features

- **JWT Authentication and Authorization**:  
  Implements JWT-based security to authenticate and authorize players. It includes utilities for generating, validating, and parsing JWT tokens.
  
- **Player Management**:  
  Handles CRUD operations for player entities, including login, registration, and updating player details.

- **Questionnaire Management**:  
  Manages questionnaire data, including questions and answers, and allows players to submit their responses.

- **Player Session Workflow**:  
  - If the player has not completed the questionnaire, they are prompted to complete it before proceeding.
  - If the questionnaire is already completed, the player can directly access the game or review their answers.

- **MySQL Database Integration**:  
  Stores and retrieves data for players, questionnaire details, player answers, and session statuses.

- **Error Handling**:  
  Custom exceptions ensure meaningful error messages and proper response codes for invalid requests.

## Package Structure

```plaintext
config/
  ├── JwtAuthorizationFilter
  ├── JwtUtil
  └── SecurityConfig

controller/
  ├── PlayerController
  ├── PlayerLoginController
  └── Que_Ans_Bank_Controller

dto/
  ├── ErrorRes
  ├── LoginReq
  ├── LoginRes
  ├── PlayerAnswersDTO
  ├── PlayerDTO
  └── ResponseDTO

entity/
  ├── Player
  ├── PlayerAnswers
  ├── que_ans_bank
  └── Usercredentials

exception/
  └── UserNotFoundException

repository/
  ├── PlayerAnswersRepo
  ├── PlayerRepo
  └── PlayerRepository

service/
  └── impl/
        └── PlayerServiceImpl

