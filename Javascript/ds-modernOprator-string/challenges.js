const game = {
  team1: "Bayern Munich",
  team2: "Borrussia Dortmund",
  players: [
    [
      "Neuer",
      "Pavard",
      "Martinez",
      "Alaba",
      "Davies",
      "Kimmich",
      "Goretzka",
      "Coman",
      "Muller",
      "Gnarby",
      "Lewandowski",
    ],
    [
      "Burki",
      "Schulz",
      "Hummels",
      "Akanji",
      "Hakimi",
      "Weigl",
      "Witsel",
      "Hazard",
      "Brandt",
      "Sancho",
      "Gotze",
    ],
  ],
  score: "4:0",
  scored: ["Lewandowski", "Gnarby", "Lewandowski", "Hummels"],
  date: "Nov 9th, 2037",
  odds: {
    team1: 1.33,
    x: 3.25,
    team2: 6.5,
  },
};

// coding challenge 1
const [player1, player2] = game.players;

const [gk, ...fieldPlayers] = player1;

const allPlayers = [...player1, ...player2];

const players1Final = [...player1, "Thiago", "Coutinho", "Perisic"];

const {
  odds: { team1, x: draw, team2 },
} = game;
console.log(team1, draw, team2);

function printGoals(...players) {
  for (let i = 0; i <= players.length; i++) {
    console.log(players[i]);
    console.log(`player is scored ${players.length}`);
  }
}

printGoals(...game.scored);

team1 > team2 && console.log("team1 is more likely to win");
team2 > team1 && console.log("team2 is more likely to win");

// Coding challenge 2 
for (let [index, ele] of game.scored.entries()) {
  console.log(`${index + 1} ${ele}`);
}

console.log(Object.values(game.odds));

const odds = Object.values(game.odds);
let average = 0;
for (const odd of odds) {
  average += odd;
}
average /= odds.length;
console.log(average);

for(const [name, value] of Object.entries(game.odds)) {
  const teamStr = name === 'x' ? 'draw' : `victory ${game[name]}`
  console.log(`Odd of ${teamStr}: ${value}`);
}

// coding challenge 3
const gameEvents = new Map([
  [17, '⚽ GOAL'],
  [36, '🔁 Substitution'],
  [47, '⚽ GOAL'],
  [61, '🔁 Substitution'],
  [64, '🟨 Yellow card'],
  [69, '🔴 Red card'],
  [70, '🔁 Substitution'],
  [72, '🔁 Substitution'],
  [76, '⚽ GOAL'],
  [80, '⚽ GOAL'],
  [92, '🟨 Yellow card'],
]);

const events = new Set([...gameEvents.values()]);  
console.log(events);

console.log(gameEvents.delete(64));
console.log(gameEvents);

console.log(`An event happened on average every ${gameEvents.size / 90} minutes`);

for(const [name, value] of gameEvents) {
  const part = name <= 45 ?'FIRST' : 'SECOND'; 
  console.log(`[${part} HALF]${name}: ${value}`);
}

// coding challenge -4
document.body.append(document.createElement('textarea'));
document.body.append(document.createElement('button'));

document.querySelector('button').addEventListener('click', function() {
   const text = document.querySelector('textarea').value;
   const rows = text.split('\n');

   for(const [i, row] of rows.entries()) {
     const [first, second] = row.toLowerCase().trim().split('_');

     const output = `${first}${second.replace(second[0], second[0].toUpperCase())}`;

     console.log(`${output.padEnd(20)}${'✅'.repeat(i+1)}`);
   }
});

console.log('12'.repeat(5));