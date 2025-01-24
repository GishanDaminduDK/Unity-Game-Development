# Castle of Light Unity Game Design - Team SkyLine

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
