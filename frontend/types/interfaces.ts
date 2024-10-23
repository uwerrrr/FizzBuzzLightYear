export interface Rule {
  divisibleBy: number;
  replaceWith: string;
}

export interface RuleResponse extends Rule {
  ruleId: string;
  
}

export interface Game {
  gameId: string;
  name: string;
  author: string;
  createdDate: string;
  rules: Rule[];
}

export interface GameSession {
  sessionId: string;
  game: Game;
  startTime: string;
  endTime: string;
  isActive: boolean;
  questions: Question[];
}

export interface Question {
  questionId: string;
  sessionId: string;
  number: number;
  expectedAnswer: string;
  playerAnswer?: string;
  isCorrect?: boolean;
  generatedAt: string;
}

export interface GameSessionResponse {
  sessionId: string;
  rules: Rule[];
  startTime: string;
  endTime: string;
  isActive: boolean;
  currentQuestion: NextQuestion;
}

export interface AnswerResponse {
  isCorrect: boolean;
  correctAnswer: string;
  playerAnswer: string;
  nextQuestion: NextQuestion | null;
  gameEnded: boolean;
}

export interface NextQuestion {
  questionId: string;
  number: number;
}

export interface GameStats {
  correctAnswerNum: number;
  incorrectAnswerNum: number;
}
