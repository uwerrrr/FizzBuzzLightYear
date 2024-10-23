import CreateGameForm from "@/components/CreateGameForm";
import GameList from "@/components/GameList";
import { Suspense } from "react";

export default function Home() {
  return (
    <main className="container mx-auto px-4 py-8">
      <h1 className="text-4xl font-bold mb-8">Fizz Buzz Light Year</h1>
      <div className="flex flex-col gap-8">
        <div>
          <h2 className="text-2xl font-semibold mb-4 ">Available Games</h2>
          <Suspense fallback={<div>Loading games...</div>}>
            <GameList />
          </Suspense>
        </div>
        <div className="w-full flex justify-center">
          <div className="max-w-sm">
            <h2 className="text-2xl font-semibold mb-4 ">Create New Game</h2>
            <CreateGameForm />
          </div>
        </div>
      </div>
    </main>
  );
}
