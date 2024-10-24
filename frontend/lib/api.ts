import { Rule } from "@/types/interfaces";

const API_URL = process.env.NEXT_PUBLIC_API_URL;

export const api = {
  async getAllGames() {
    console.log(`${API_URL}/Game`);
    const response = await fetch(`${API_URL}/Game`);
    console.log(response);
    if (!response.ok) throw new Error("Failed to fetch games");
    return response.json();
  },

  async createGame(game: { name: string; author: string; rules: Rule[] }) {
    console.log(JSON.stringify(game));
    const response = await fetch(`${API_URL}/Game`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(game),
    });
    if (!response.ok && response.body) {
      const errorMessage = await response.text();
      console.error("Failed to create game:", errorMessage);
      throw new Error("Failed to create game: " + errorMessage);
    }
    const data = await response.json();
    return data;
  },

  async startGameSession(
    gameId: string,
    playerName: string,
    durationSeconds: number
  ) {
    const response = await fetch(`${API_URL}/GameSession/start`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ gameId, playerName, durationSeconds }),
    });
    if (!response.ok) throw new Error("Failed to start game session");
    return response.json();
  },

  async submitAnswer(
    sessionId: string,
    questionId: string,
    playerAnswer: string
  ) {
    const response = await fetch(`${API_URL}/GameSession/answer`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ sessionId, questionId, playerAnswer }),
    });
    if (!response.ok) throw new Error("Failed to submit answer");
    return response.json();
  },

  async getSessionStats(sessionId: string) {
    const response = await fetch(`${API_URL}/GameSession/${sessionId}/stats`);
    if (!response.ok) throw new Error("Failed to fetch session stats");
    return response.json();
  },
};
