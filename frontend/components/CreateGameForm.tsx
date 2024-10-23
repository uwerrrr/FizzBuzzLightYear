"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { Rule } from "@/types/interfaces";
import { api } from "@/lib/api";

export default function CreateGameForm() {
  const router = useRouter();
  const [name, setName] = useState("");
  const [author, setAuthor] = useState("");
  const [rules, setRules] = useState<Rule[]>([
    { divisibleBy: 0, replaceWith: "" },
    { divisibleBy: 0, replaceWith: "" },
    { divisibleBy: 0, replaceWith: "" },
  ]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await api.createGame({ name, author, rules });
      router.refresh();
    } catch (error) {
      console.error("Failed to create game:", error);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div>
        <label className="block mb-1">Game Name</label>
        <input
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
          className="w-full p-2 border rounded"
          required
        />
      </div>
      <div>
        <label className="block mb-1">Author</label>
        <input
          type="text"
          value={author}
          onChange={(e) => setAuthor(e.target.value)}
          className="w-full p-2 border rounded"
          required
        />
      </div>
      <div>
        <label className="block mb-1">Rules</label>

        {rules.map((rule, index) => (
          <div key={index} className="flex gap-2 mb-2">
            <input
              type="number"
              value={rule.divisibleBy == 0 ? "" : rule.divisibleBy}
              onChange={(e) => {
                const newRules = [...rules];
                newRules[index].divisibleBy = parseInt(e.target.value);
                setRules(newRules);
              }}
              className="w-1/3 p-2 border rounded"
              placeholder="Divisible by"
              required
            />
            <input
              type="text"
              value={rule.replaceWith}
              onChange={(e) => {
                const newRules = [...rules];
                newRules[index].replaceWith = e.target.value;
                setRules(newRules);
              }}
              className="w-2/3 p-2 border rounded"
              placeholder="Replace with"
              required
            />
          </div>
        ))}
      </div>
      <button
        type="submit"
        className="w-full bg-blue-600 text-white py-2 px-4 rounded hover:bg-blue-700"
      >
        Create Game
      </button>
    </form>
  );
}
