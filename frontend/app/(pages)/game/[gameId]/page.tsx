import { Suspense } from "react";
import GamePlay from "@/components/GamePlay";

export default function GamePage({ params }: { params: { gameId: string } }) {
  return (
    <main className="container mx-auto px-4 py-8">
      <Suspense fallback={<div>Loading game...</div>}>
        <GamePlay gameId={params.gameId} />
      </Suspense>
    </main>
  );
}
