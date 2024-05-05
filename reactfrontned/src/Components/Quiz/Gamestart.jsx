import React, { useState, useEffect } from 'react'; // Import useState and useEffect

export default function Gamestart() {
   // Define state for the score
   const [questionarie_updated_Score, setQuestionarie_updated_Score] = useState(0);
   
   // Retrieve score from localStorage
   const storedScore = localStorage.getItem('quizScore');
   const score = storedScore ? JSON.parse(storedScore) : null;
   console.log('Score retrieved from localStorage:', score);
  
  // Function to calculate gold coins based on the score
  const goldcoinsset = () => {
    let goldcoins = score * 10; // Multiply the score by 10 to get gold coins
    console.log(goldcoins)
    setQuestionarie_updated_Score(goldcoins);  // Update the state with the calculated gold coins
  }
  
  // Run goldcoinsset function once on component mount
  useEffect(() => {
    goldcoinsset(); ////On questionnaire submission, show the result as instructed 
  }, [])

  // Render UI
  return (
    <div>
      <div style={{ fontSize: '50px', marginTop: '-200px' }}>
        <h1>Congratulations!</h1>
      </div>
      <div style={{ fontSize: '50px', marginTop: '-100px' }}>
        <h1>You have won {questionarie_updated_Score} Gold Coins to continue the game.</h1>     
      </div>
    </div>
  );
}
