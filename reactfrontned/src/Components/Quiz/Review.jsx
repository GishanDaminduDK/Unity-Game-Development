// import React, { useState, useEffect, useRef } from 'react';
// import './Review.css';
// import { data } from '../../assets/data'


// const Review = () => {
//   const [questions, setQuestions] = useState(data);
//   const array = [3, 2, 3, 4, 1, 3, 3, 3, 3, 3];
//   const [score, setScore] = useState(0);
//   const [lock, setLock] = useState(false);

//   const optionRefs = useRef([]);

//   useEffect(() => {
//     calculateScore();
//     // eslint-disable-next-line react-hooks/exhaustive-deps
//   }, []); // Runs only once when component mounts

//   const calculateScore = () => {
//     let score = 0;
//     questions.forEach((q, index) => {
//       if (q.ans === array[index]) {
//         score++;
//       }
//       highlightAnswer(index, q.ans, array[index]);
//     });
//     setScore(score);
//     setLock(true);
//   };

//   const highlightAnswer = (index, correctAnswer, userAnswer) => {
//     optionRefs.current[index][correctAnswer - 1].classList.add("correct");
//     if (correctAnswer !== userAnswer) {
//       optionRefs.current[index][userAnswer - 1].classList.add("wrong");
//     }
//   };

//   return (
//     <div className='container_2'>
//       <h1>Quiz Review</h1>
//       <hr />
//       <div className="review-container">
//         {questions.map((q, index) => (
//           <div key={index}>
//             <h2>
//               {index + 1}. {q.question}
//             </h2>
//             <ul>
//               <li ref={(el) => (optionRefs.current[index] = el ? [...(optionRefs.current[index] || []), el] : null)}>
//                 {q.option1}
//               </li>
//               <li ref={(el) => (optionRefs.current[index] = el ? [...(optionRefs.current[index] || []), el] : null)}>
//                 {q.option2}
//               </li>
//               <li ref={(el) => (optionRefs.current[index] = el ? [...(optionRefs.current[index] || []), el] : null)}>
//                 {q.option3}
//               </li>
//               <li ref={(el) => (optionRefs.current[index] = el ? [...(optionRefs.current[index] || []), el] : null)}>
//                 {q.option4}
//               </li>
//             </ul>
//             <p className="feedback general-feedback"><strong>General Feedback:   </strong>{q.feedback.general}</p>
//             <p className="feedback selected-feedback"><strong>Specific Feedback:  </strong>{q.feedback.answers[array[index]-1]}</p>
//           </div>
//         ))}
//       </div>
//       <h3>You scored {score} out of {data.length} questions</h3>
//       <button>Back to the Game</button>
//     </div>
//   );
// };

// export default Review;
import React, { useState, useEffect, useRef } from 'react';
import axios from 'axios'; // Don't forget to import axios for making HTTP requests
import './Review.css';
import { useNavigate } from 'react-router-dom';


const Review = () => {
  const [questions, setQuestions] = useState([]); // Initialize with an empty array
  const navigate = useNavigate();
 
  const [score, setScore] = useState(10);
  const [lock, setLock] = useState(false);
  const [queList, setQueList] = useState([]);
  const [array, setArray] = useState([1,1,1,1,1,1,1,1,1,1]);
  const [dataFetched, setDataFetched] = useState(false);

  const optionRefs = useRef([]);

  useEffect(() => {
    console.log(window.location.href);
    let parts = window.location.href.split('/');
    let id_value = parts[parts.length - 2];
    console.log(id_value);
    let id_value_int = parseInt(id_value);
    console.log("id_value_int", id_value_int);
    const fetchData = async () => {
      try {      // Exception handling
        // Fetch questions
        const questionResponse = await axios.get('http://localhost:8081/api/questions/1'); // Get all questions and answers from database
        const questionData = questionResponse.data;
        console.log('Question data:', questionData); // Log the response data
        const fetchedQueList = JSON.parse(questionData.que);
        setQueList(fetchedQueList);
        setQuestions(fetchedQueList);
        let url="http://localhost:8081/api/v1/player/answer/"+id_value   // Check questionarier updated status with getting player answers
        // Fetch player answers
        const answerResponse = await axios.get(url);
        const answerData = answerResponse.data;
        console.log('Answer data:', answerData); // Log the response data
        const fetchedAnswerList = JSON.parse(answerData.content.answers_array);
        console.log(fetchedAnswerList);
        setArray(fetchedAnswerList);
        console.log(array);
        
        // Set flag to indicate data has been fetched
        setDataFetched(true);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };
  
    fetchData();
  }, []);
  
  useEffect(() => {
    if (dataFetched) {
      calculateScore();   // Player 
    }
  }, [dataFetched]);
  
  // useEffect(() => {
  //   calculateScore();
  // }, [questions]);
  
  const calculateScore = () => {
    let score = 0;
    questions.forEach((q, index) => {
      if (q.ans === array[index]) {
        score++;
      }
      highlightAnswer(index, q.ans, array[index]);
    });
    setScore(score);
    setLock(true);
  };

  const highlightAnswer = (index, correctAnswer, playerAnswer) => {
    optionRefs.current[index][correctAnswer - 1].classList.add("correct");
    if (correctAnswer !== playerAnswer) {
      optionRefs.current[index][playerAnswer - 1].classList.add("wrong");
    }
  };
  useEffect(() => {
    localStorage.setItem('quizScore', JSON.stringify(score));
  }, [score]);
  return (
    <div className='container_2'>
      <h1>Quiz Review</h1>
      <hr />
      <div className="review-container">
        {questions.map((q, index) => (
          <div key={index}>
            <h2>
              {index + 1}. {q.question}
            </h2>
            <ul>
              <li ref={(el) => (optionRefs.current[index] = el ? [...(optionRefs.current[index] || []), el] : null)}>
                {q.option1}
              </li>
              <li ref={(el) => (optionRefs.current[index] = el ? [...(optionRefs.current[index] || []), el] : null)}>
                {q.option2}
              </li>
              <li ref={(el) => (optionRefs.current[index] = el ? [...(optionRefs.current[index] || []), el] : null)}>
                {q.option3}
              </li>
              <li ref={(el) => (optionRefs.current[index] = el ? [...(optionRefs.current[index] || []), el] : null)}>
                {q.option4}
              </li>
            </ul>
            <p className="feedback general-feedback"><strong>General Feedback:  </strong>{q.feedback.general}</p>
            <p className="feedback selected-feedback"><strong>Specific Feedback:  </strong>{q.feedback.answers[array[index]-1]}</p>
          </div>
        ))}
      </div>
      <h3>You scored {score} out of {queList.length} questions</h3>
      
      <button onClick={() => navigate('/main/quiz/review/startgame')}>Back to the Game</button>
    </div>
  );
};

export default Review;
