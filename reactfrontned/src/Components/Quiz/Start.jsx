// import React, { useState, useEffect } from 'react';
// import './Start.css';
// import { useNavigate } from 'react-router-dom';
// import axios from 'axios';

// const Start = () => {
//   const [isValidFirstTimeAttempt, setIsValidFirstTimeAttempt] = useState(false);
//   const navigate = useNavigate();

//   useEffect(() => {
//     console.log(window.location.href);
//     let parts = window.location.href.split('/');
//     let id_value = parts[parts.length - 2];
//     console.log(id_value);
//     let id_value_int = parseInt(id_value);
//     console.log("id_value_int", id_value_int);
//     let jwt_token = parts[parts.length - 1];
//     console.log(jwt_token);
    
  
//   }, []); // Empty dependency array ensures this effect runs only once after the initial render

//   const handleClick = async () => {
//     try {
//       const response = await axios.get('http://localhost:8081/api/questions/1', {
//         headers: {
//           Authorization: `Bearer ${jwt_token}`
//         }
//       });
//       console.log(response.data);
//       const queList = response.data.que;
//       console.log(queList);
//     } catch (error) {
//       console.error('Error fetching data:', error);
//     }
//   };
  
//   return (
//     <div className='container_3'>
//     <h1>Welcome to the Quiz</h1>
//     <hr />
//     <h2>Attempts allowed is one</h2>
//     <h2>There are 10 MCQ questions</h2>
//     <h2>Click on start to begin</h2>
    
//     <button onClick={() => {
//         if (!isValidFirstTimeAttempt) {
//             navigate(`/main/quiz/${id_value_int}/${jwt_token}`);
//         }
//     }}>Start</button>
// </div>

//   );
// };

// export default Start;
import React, { useState, useEffect } from 'react';
import './Start.css';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const Start = () => {
  const [isValidFirstTimeAttempt, setIsValidFirstTimeAttempt] = useState(false);
  const navigate = useNavigate();

  let id_value_int;
  let jwt_token;

  useEffect(() => {
    console.log(window.location.href);
    let parts = window.location.href.split('/');
    let id_value = parts[parts.length - 2];
    console.log(id_value);
    id_value_int = parseInt(id_value);
    console.log("id_value_int", id_value_int);
    jwt_token = parts[parts.length - 1];
    console.log(jwt_token);
    handleClick();
  
  }, []); // Empty dependency array ensures this effect runs only once after the initial render

  const handleClick = async () => {
    try {
      const response = await axios.get('http://localhost:8081/api/questions/1', {
        headers: {
          Authorization: `Bearer ${jwt_token}`
        }
      });
      console.log(response.data);
      const queList = response.data.que;
      console.log(queList);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };
  
  return (
    <div className='container_3'>
      <h1>Welcome to the Quiz</h1>
      <hr />
      <h2>Attempts allowed is one</h2>
      <h2>There are 10 MCQ questions</h2>
      <h2>Click on start to begin</h2>
      
      <button onClick={() => {
          if (!isValidFirstTimeAttempt) {
              navigate(`/main/quiz/${id_value_int}/${jwt_token}`);
          }
      }}>Start</button>
    </div>
  );
};

export default Start;
