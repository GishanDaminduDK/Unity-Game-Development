import React from 'react'
import Quiz from './Components/Quiz/Quiz'
import Review from './Components/Quiz/Review'
import Start from './Components/Quiz/Start'
import Gamestart from './Components/Quiz/Gamestart'
import { BrowserRouter, Routes, Route } from 'react-router-dom'

const App = () => {
  return (
    <div>
      <BrowserRouter>
        <Routes>
        <Route path="/main/:id/:token" element={<Start />} />
        <Route path="/main/quiz/:id/:token" element={<Quiz />} />
        <Route path="/main/quiz/review/:id/:token" element={<Review />} />
        <Route path="/main/quiz/review/startgame" element={<Gamestart />} />


        </Routes>
      </BrowserRouter>
    </div>
  )
}

export default App
