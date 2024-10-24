"use client";

import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { Game } from "@/types/interfaces";
import { api } from "@/lib/api";

export default function GameList() {
  const [games, setGames] = useState<Game[]>([]);
  const [error, setError] = useState<string | null>(null);
  const router = useRouter();

  useEffect(() => {
    const fetchGames = async () => {
      try {
        const gamesData = await api.getAllGames();
        setGames(gamesData);
        console.log(gamesData);
      } catch (error) {
        // Handling error
        console.error(error);
        setError(`${error}`); // Set error message to state
      }
    };
    fetchGames();
  }, []);

  const PlayBtn = ({ game }: { game: (typeof games)[number] }) => {
    return (
      <button
        className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
        onClick={() => router.push(`/game/${game.gameId}`)}
      >
        Play
      </button>
    );
  };

  return (
    <div>
      {error ? ( // Display error message if it exists
        <div className="text-red-500 text-center py-4">
          Failed to load games: {error}
        </div>
      ) : (
        <table className="min-w-full table-auto">
          <thead>
            <tr>
              <th className="px-4 py-2">Game Name</th>
              <th className="px-4 py-2">Author</th>
              <th className="px-4 py-2">Rules</th>
              <th className="px-4 py-2">Action</th>
            </tr>
          </thead>
          <tbody>
            {games.map((game) => (
              <tr key={game.gameId} className="border-t">
                <td className="px-4 py-2 font-semibold text-center">
                  {game.name}
                </td>
                <td className="px-4 py-2 text-gray-600 text-center">
                  {game.author}
                </td>
                <td className="px-4 py-2 text-center">
                  <ul className="list-decimal list-inside">
                    {game.rules.map((rule, index) => (
                      <li key={index}>
                        Divisible by {rule.divisibleBy} → &quot;
                        {rule.replaceWith}&quot;
                      </li>
                    ))}
                  </ul>
                </td>
                <td className="px-4 py-2 text-center">
                  <PlayBtn game={game} />
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}
