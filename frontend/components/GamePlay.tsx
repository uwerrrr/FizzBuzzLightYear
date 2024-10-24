"use client";

import { useState, useEffect } from "react";
// import { useRouter } from "next/navigation";
import { GameSessionResponse, GameSessionStats } from "@/types/interfaces";
import { api } from "@/lib/api";

// TODO: break down to components
export default function GamePlay({ gameId }: { gameId: string }) {
  // const router = useRouter();
  const [playerName, setPlayerName] = useState("");
  const [duration, setDuration] = useState(60);
  const [session, setSession] = useState<GameSessionResponse | null>(null);
  const [answer, setAnswer] = useState("");
  const [timeLeft, setTimeLeft] = useState(0);
  const [stats, setStats] = useState<GameSessionStats | null>(null);

  useEffect(() => {
    // when session starts and is active, start timer
    if (session && session.isActive) {
      const timer = setInterval(() => {
        const now = new Date();
        const end = new Date(session.endTime);
        const remaining = Math.max(
          0,
          Math.floor((end.getTime() - now.getTime()) / 1000)
        );
        setTimeLeft(remaining);

        if (remaining === 0) {
          clearInterval(timer);
          api.getSessionStats(session.sessionId).then(setStats); // when time's up, get stats from api
        }
      }, 1000);

      return () => clearInterval(timer);
    }
  }, [session]);

  const startGame = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const newSession = await api.startGameSession(
        gameId,
        playerName,
        duration
      );
      setSession(newSession);
      setTimeLeft(duration);
    } catch (error) {
      console.error("Failed to start game:", error);
    }
  };

  const submitAnswer = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!session) return;

    try {
      const response = await api.submitAnswer(
        session.sessionId,
        session.currentQuestion.questionId,
        answer
      );

      if (response.gameEnded) {
        const stats = await api.getSessionStats(session.sessionId);
        setStats(stats);
        setSession(null);
      } else if (response.nextQuestion) {
        setSession({
          ...session,
          currentQuestion: response.nextQuestion,
        });
      }

      setAnswer("");
    } catch (error) {
      console.error("Failed to submit answer:", error);
    }
  };

  // if game is over, show stats
  if (stats) {
    return (
      <div className="max-w-md mx-auto text-center">
        <h2 className="text-2xl font-bold mb-4">Game Over!</h2>
        <div className="space-y-2">
          <p>Correct Answers: {stats.correctAnswerNum}</p>
          <p>Incorrect Answers: {stats.incorrectAnswerNum}</p>
          <button
            onClick={() => {
              setStats(null);
              setSession(null);
              setPlayerName("");
            }}
            className="mt-4 bg-blue-600 text-white py-2 px-4 rounded hover:bg-blue-700"
          >
            Play Again
          </button>
        </div>
      </div>
    );
  }

  // if no session, show start form
  if (!session) {
    return (
      <form onSubmit={startGame} className="max-w-md mx-auto space-y-4">
        <div>
          <label className="block mb-1">Player Name</label>
          <input
            type="text"
            value={playerName}
            onChange={(e) => setPlayerName(e.target.value)}
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <div>
          <label className="block mb-1">Duration (seconds)</label>
          <input
            type="number"
            value={duration}
            onChange={(e) => setDuration(parseInt(e.target.value))}
            min="10"
            max="300"
            className="w-full p-2 border rounded"
            required
          />
        </div>
        <button
          type="submit"
          className="w-full bg-blue-600 text-white py-2 px-4 rounded hover:bg-blue-700"
        >
          Start Game
        </button>
      </form>
    );
  }

  // if session is active, show gameplay
  return (
    <div className="max-w-md mx-auto">
      <div className="mb-4 text-center">
        <div className="text-2xl font-bold">Time Left: {timeLeft}s</div>
      </div>
      <div className="p-4 border rounded-lg mb-4">
        <h3 className="text-xl font-semibold mb-2">Rules:</h3>
        <ul className="list-disc list-inside">
          {session.rules.map((rule, index) => (
            <li key={index}>
              Divisible by {rule.divisibleBy} → &quot;{rule.replaceWith}&quot;
            </li>
          ))}
        </ul>
      </div>
      <div className="text-center mb-4">
        <div className="text-4xl font-bold">
          {session.currentQuestion.number}
        </div>
      </div>
      <form onSubmit={submitAnswer} className="space-y-4">
        <input
          type="text"
          value={answer}
          onChange={(e) => setAnswer(e.target.value)}
          className="w-full p-2 border rounded"
          placeholder="Enter your answer"
          required
        />
        <button
          type="submit"
          className="w-full bg-blue-600 text-white py-2 px-4 rounded hover:bg-blue-700"
        >
          Submit
        </button>
      </form>
    </div>
  );
}
