// import React, { useState,useRef, useContext, useEffect } from 'react'
// import './Quiz.css'
// //import { data } from '../../assets/data'
// import { useNavigate } from 'react-router-dom'
// import axios from 'axios'

// const Quiz = () => {
//     let [index, setIndex] = useState(0);
//     //let [question,setQuestion] = useState(data[index]);
//     let [question,setQuestion] = useState(null);
//     let [lock,setLock]= useState(false);
//     let [score,setScore]=useState(0);
//     let [result,setResult]=useState(false);
//     const [givenAnswers, setGivenAnswers] = useState(Array(10).fill(0));

//     let Option1 = useRef(null);
//     let Option2 = useRef(null);
//     let Option3 = useRef(null);
//     let Option4 = useRef(null);

//     let option_array = [Option1,Option2,Option3,Option4];

//     const checkAns = async (e, ans) => {
//       if (!lock) {
//         if (question.ans === ans) {
//           e.target.classList.add("correct");
//           setLock(true);
//           setScore(prev => prev + 1);
//         } else {
//           e.target.classList.add("wrong");
//           setLock(true);
//           option_array[question.ans - 1].current.classList.add("correct");
//         }
//         console.log(givenAnswers);
//         let updatedAnswers = [...givenAnswers];
//         console.log(updatedAnswers)
//         updatedAnswers[index] = ans;

        
//         setGivenAnswers(updatedAnswers);
     
  
//         // const jsonData = {
//         //   playerID: "9",
//         //   playerUsername: "yasindu",
//         //   que_1: updatedAnswers[0],
//         //   que_2: updatedAnswers[1],
//         //   que_3: updatedAnswers[2],
//         //   que_4: updatedAnswers[3],
//         //   que_5: updatedAnswers[4],
//         //   que_6: updatedAnswers[5],
//         //   que_7: updatedAnswers[6],
//         //   que_8: updatedAnswers[7],
//         //   que_9: updatedAnswers[8],
//         //   que_10: updatedAnswers[9]
          
//         // };
//         // console.log(updatedAnswers)
        
//         // try {
//         //   await axios.put('http://localhost:8080/api/v1/player/updatePlayer', jsonData);
//         // } catch (error) {
//         //   console.error("Error while updating player:", error);
//         //   // Handle error here
//         // }
  
//         // console.log(ans);
//         // console.log(question.ans);
//       }
//     };
  
//   const next = () => {
//     if(lock === true){
//       if(index === data.length -1){
//         setResult(true);
//         return 0;
//       }
//       setIndex(++index)
//       setQuestion(queList[index]);
//       setLock(false);
//       option_array.map((option)=>{
//         option.current.classList.remove("wrong");
//         option.current.classList.remove("correct");
//         return null;
//       })
//     }
//   }

//   const handleClick = async () => {
//     try {
//       const response = await axios.get('http://localhost:8081/api/questions/1');
//       console.log(response.data);
//       const jsonData = response.data;
//       const queList = JSON.parse(jsonData.que);
//       console.log(queList);
//       setQuestion(queList[index]); // Set the first question when data is fetched
//     } catch (error) {
//       console.error('Error fetching data:', error);
//     }
//   };
//   useEffect(() => {
//     handleClick();
  
//   }, []);
//   const navigate = useNavigate();
//   return (
//     <div className='container_1'>
//         <h1>Quiz App</h1>
//         <hr />
//         {result?<></>:<><h2>{index+1}. {question.question}</h2>
//         <ul>
//             <li ref={Option1} onClick={(e) => {checkAns(e,1)}}>{question.option1}</li>
//             <li ref={Option2} onClick={(e) => {checkAns(e,2)}}>{question.option2}</li>
//             <li ref={Option3} onClick={(e) => {checkAns(e,3)}}>{question.option3}</li>
//             <li ref={Option4} onClick={(e) => {checkAns(e,4)}}>{question.option4}</li>
//         </ul>
//         <button onClick={next}>Next</button>
//         <div className = "index">{index+1} of {data.length} questions</div></>}
//         {result?<><h2>You Scored {score} out of {data.length}</h2>
//         <button onClick={()=> navigate('/main/:id:/quiz/review')}>Review</button></>:<></>}
        
//     </div>
//   )
// }

// export default Quiz
import React, { useState, useRef, useEffect } from 'react';
import './Quiz.css';
import axios from 'axios';
import { useNavigate } from 'react-router-dom'

const Quiz = () => {
  const [index, setIndex] = useState(0);
  const [question, setQuestion] = useState(null);
  const [lock, setLock] = useState(false);
  const [score, setScore] = useState(0);
  const [result, setResult] = useState(false);
  const [givenAnswers, setGivenAnswers] = useState(Array(10).fill(0));
  const [queList, setQueList] = useState([]);
  const [loading, setLoading] = useState(true);

  const Option1 = useRef(null);
  const Option2 = useRef(null);
  const Option3 = useRef(null);
  const Option4 = useRef(null);

  const optionRefs = [Option1, Option2, Option3, Option4];  // Extracting token jwt token and id
  console.log(window.location.href);
  let parts = window.location.href.split('/');
  let id_value = parts[parts.length - 2];
  console.log(id_value);
  let id_value_int = parseInt(id_value);
  console.log("id_value_int", id_value_int);
  let jwt_token = parts[parts.length - 1];
  console.log(jwt_token);

  const checkAns = async (e, ans) => {
    if (!lock) {
      const selectedOption = e.target;
      const correctOption = question.ans;
      if (ans === correctOption) {
        selectedOption.classList.add('correct1');
        setScore(prevScore => prevScore + 1);
      } else {
        selectedOption.classList.add('wrong1');
        //optionRefs[correctOption - 1].current.classList.add('correct');
      }
      setLock(true);

      const updatedAnswers = [...givenAnswers];
      updatedAnswers[index] = ans;
      //console.log(givenAnswers);
              
      console.log(updatedAnswers)
      if (updatedAnswers[9] !== 0) {                     // To save player answers inside of react js database  , We dont save score we calculate it with front end logic
        const saveAnswers = async () => {
          console.log(id_value_int);
          try {
            const response = await axios.post('http://localhost:8081/api/v1/player/saveAnswers', {
              id: id_value_int,
              answers_array: JSON.stringify(updatedAnswers) // Convert array to string
            }, {
              headers: {
                Authorization: `Bearer ${jwt_token}`
              }
            });
            console.log('Answers saved successfully:', response.data);
          } catch (error) {
            console.error('Error saving answers:', error);
            // Handle error appropriately
          }
        };
        saveAnswers();
      }
      
      

      updatedAnswers[index] = ans;
      
              
      //setGivenAnswers(updatedAnswers);
           
      setGivenAnswers(updatedAnswers);

    }
  };

  const next = () => {
    // Save queList to local storage
    localStorage.setItem('queList', JSON.stringify(queList));
  
    if (lock === true) {
      if (index === queList.length - 1) {
        setResult(true);
        return 0;
      }
      setIndex(prevIndex => prevIndex + 1);
      setQuestion(queList[index]);
      setLock(false);
      optionRefs.forEach(optionRef => {
        optionRef.current.classList.remove('wrong1');
        optionRef.current.classList.remove('correct1');
      });
    }
  };
  

  useEffect(() => {
    const fetchData = async () => {
        try {
            const response = await axios.get('http://localhost:8081/api/questions/1', {  // Getting questions and answers
                headers: {
                    Authorization: `Bearer ${jwt_token}`
                }
            });
            const jsonData = response.data;
            const fetchedQueList = JSON.parse(jsonData.que);

            setQueList(fetchedQueList);
            setQuestion(fetchedQueList[index]);
            setLoading(false);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    fetchData();
}, []);

  // Added jwt_token to the dependency array
  const navigate = useNavigate();

  if (loading) {
    return <div>Loading...</div>;
  }
  
  return (
    <div className='container_1'>
      <h1>Questionnaire</h1>
      <hr />
      {result ? (
        <>
          <h2>You Scored {score} out of {queList.length}</h2>
          <button onClick={() => navigate(`/main/quiz/review/${id_value_int}/${jwt_token}`)}>Review</button>
        </>
      ) : (
        <>
          <h2>{index + 1}. {question && question.question}</h2>
          <ul>
            {question && (
              <>
                <li ref={Option1} onClick={(e) => checkAns(e, 1)}>{question.option1}</li>
                <li ref={Option2} onClick={(e) => checkAns(e, 2)}>{question.option2}</li>
                <li ref={Option3} onClick={(e) => checkAns(e, 3)}>{question.option3}</li>
                <li ref={Option4} onClick={(e) => checkAns(e, 4)}>{question.option4}</li>
              </>
            )}
          </ul>
          <button onClick={next}>Next</button>
          <div className="index">{index + 1} of {queList.length} questions</div>
        </>
      )}
    </div>
  );
};

export default Quiz;
