'use strict';

// Data needed for a later exercise
const flights =
  '_Delayed_Departure;fao93766109;txl2133758440;11:25+_Arrival;bru0943384722;fao93766109;11:45+_Delayed_Arrival;hel7439299980;fao93766109;12:05+_Departure;fao93766109;lis2323639855;12:30';

const weekdays = ['mon', 'tue', 'wed', 'thu', 'fri', 'sat', 'sun'];
const openingHours = {
  [weekdays[3]]: {
    open: 12,
    close: 22,
  },
  [weekdays[4]]: {
    open: 11, 
    close: 23,
  },
  [weekdays[5]]: {  
    open: 0 , // Open 24 hours
    close: 24 + 12,
  },
};

// Data needed for first part of the section
const restaurant = {
  name: 'Classico Italiano',
  location: 'Via Angelo Tavanti 23, Firenze, Italy',
  categories: ['Italian', 'Pizzeria', 'Vegetarian', 'Organic'],
  starterMenu: ['Focaccia', 'Bruschetta', 'Garlic Bread', 'Caprese Salad'],
  mainMenu: ['Pizza', 'Pasta', 'Risotto'],

  // ES6 enhanced object literals
  openingHours,

  order(starterIndex, mainIndex) {
    return [this.starterMenu[starterIndex], this.mainMenu[mainIndex]];
  },

  orderDelivery({starterIndex = 1, mainIndex = 0, address, time = '20:00'}) {
    console.log(`Order received! ${this.starterMenu[starterIndex]} and ${this.mainMenu[mainIndex]} will be 
    delivered to ${address} at the time ${time}`);
  },

  orderPasta(ing1, ing2, ing3) {
    console.log(`Here is your delicious pasta with ${ing1}, ${ing2}, ${ing3}`);
  },
  orderPizza(mainIngredient, ...otherIngredients) {
    console.log(mainIngredient);
    console.log(otherIngredients);
  },
};

// String methods practice
const getCode = str => str.slice(0, 3).toUpperCase();

console.log(flights.split('+'));
for(const fight of flights.split('+')) {
  const [type, from, txt, time] = fight.split(';');

  const output = `${type.startsWith('_Delayed') ? '🔴': ' '} ${type.replaceAll('_',' ')} ${getCode(from)} to ${getCode(txt)} ${time.replace(':','h')}`;
  console.log(output.padStart(40));
}
// -------------------------------------------------------------------------------------------------------------------------------

// Working With Strings - Part 3
// Split and join
/* console.log('a+very+nice+string'.split('+'));
console.log('Himanshu ladva'.split(' '));

const [firstName, lastName] = 'Jonas Schmedtmann'.split(' ');

const newName = ['Mr.', firstName, lastName.toUpperCase()].join(' ');
console.log(newName);      

const capitalizeName = function (name) {
  const names = name.split(' ');
  const namesUpper = [];

  for (const n of names) {
    // namesUpper.push(n[0].toUpperCase() + n.slice(1));
    namesUpper.push(n.replace(n[0], n[0].toUpperCase()));
  }
  console.log(namesUpper.join(' '));
};

capitalizeName('himanshu ladva darshit makadia');
capitalizeName('himanshu ladva');

// Padding 
const message = 'Go to gate 23!';
console.log(message.padStart(20, '+').padEnd(30, '+'));
console.log('Jonas'.padStart(20, '+').padEnd(30, '+'));

const maskCreditCard = function (number) {
  const str = number + '';
  const last = str.slice(-4);
  return last.padStart(str.length, '*');
};

console.log(maskCreditCard(64637836));
console.log(maskCreditCard(43378463864647384));
console.log(maskCreditCard('334859493847755774747'));

// Repeat
const message2 = 'Bad waether... All Departues Delayed... ';
console.log(message2.repeat(5));

const planesInLine = function (n) {
  console.log(`There are ${n} planes in line ${'🛩'.repeat(n)}`);
};
planesInLine(5);
planesInLine(3);
planesInLine(12); */
// -------------------------------------------------------------------------------------------------------------------------------

// working with string -part-2
/* const airline = 'TAP Air Portugal'; 

console.log(airline.toLowerCase());
console.log(airline.toUpperCase());

// Fix capitalization in name 
const passenger = 'hImaNShU'; // Jonas
const passengerLower = passenger.toLowerCase();
const passengerCorrect = passengerLower[0].toUpperCase() + passengerLower.slice(1);
console.log(passengerCorrect);

// Comparing emails
const email = 'hello@himanshu.io';
const loginEmail = '  Hello@Himanshu.Io \n';

// const lowerEmail = loginEmail.toLowerCase();
// const trimmedEmail = lowerEmail.trim();
const normalizedEmail = loginEmail.toLowerCase().trim();
console.log(normalizedEmail);
console.log(email === normalizedEmail);

// replacing
const priceGB = '288,97£';
const priceUS = priceGB.replace('£', '$').replace(',', '.');
console.log(priceUS);

const announcement =
  'All passengers come to boarding door 23. Boarding door 23!';

console.log(announcement.replace('door', 'gate'));
// console.log(announcement.replaceAll('door', 'gate'));
console.log(announcement.replace(/door/g, 'gate'));  // here g stand for global             

// Booleans
const plane = 'Airbus A320neo';
console.log(plane.includes('A320'));
console.log(plane.includes('Boeing'));
console.log(plane.startsWith('Airb'));

if (plane.startsWith('Airbus') && plane.endsWith('neo')) {
  console.log('Part of the NEW ARirbus family');
}

// Practice exercise
const checkBaggage = function (items) {
  const baggage = items.toLowerCase();

  if (baggage.includes('knife') || baggage.includes('gun')) {
    console.log('You are NOT allowed on board');
  } else {
    console.log('Welcome aboard!');
  }
};

checkBaggage('I have a laptop, some Food and a pocket Knife');
checkBaggage('Socks and camera');
checkBaggage('Got some snacks and a gun for protection'); */
// -------------------------------------------------------------------------------------------------------------------------------

// Working with strings -part 1
/* const airline = 'TAP Air Portugal';
const plane = 'A320';

console.log(plane[0]);
console.log(plane[1]);
console.log(plane[2]);
console.log('B737'[0]);

console.log(airline.length);
console.log('B737'.length);

console.log(airline.indexOf('r'));
console.log(airline.lastIndexOf('r'));
console.log(airline.indexOf('Portugal'));

console.log(airline.slice(4));
console.log(airline.slice(4, 7));

console.log(airline.slice(0, airline.indexOf(' ')));
console.log(airline.slice(airline.lastIndexOf(' ') + 1));

console.log(airline.slice(-6));
console.log(airline.slice(1, -1));

const checkMiddleSeat = function(seat) {
    // B and E are middle seat
    const s = seat.slice(-1);
    if(s === 'B' || s === 'E'){
      console.log("you got a middel seat");
    } else {
      console.log("you got top seat");
    }
}

checkMiddleSeat('11B');
checkMiddleSeat('23C');
checkMiddleSeat('3E');

console.log(typeof new String('himanshu'));
console.log(typeof new String('himanshu').slice(2)); */
// -------------------------------------------------------------------------------------------------------------------------------

// Summary Which data structure 
// Maps_iteration
/* const question = new Map([
  ['question', 'what is the best programming language'],
  [1, 'c'],
  [2, 'java'],
  [3, 'Javascript'],
  ['correct', 3],
  [true, 'correct'],
  [false, 'try again']
]);

console.log(question);

// Convert object to map
console.log(Object.entries(openingHours));
const hoursMap = new Map(Object.entries(openingHours));
console.log(hoursMap);

// quiz app
console.log(question.get('question'));
for(const [key, value] of question) {
  if(typeof key === 'number') console.log(`Answer ${key}: ${value}`); 
}
const answer = Number(prompt('Your answer'));
console.log(answer);

console.log(question.get(question.get('correct') === answer));
// (answer === question.get('correct')) ? console.log(question.get(true)) : console.log(question.get(false));
// Convert map to array
console.log([...question]);
console.log([...question.keys()]);
console.log([...question.values()]); */
// -------------------------------------------------------------------------------------------------------------------------------

// MAPS
/* const rest = new Map();
rest.set('name', 'classico italiano');
rest.set(1, 'firenze Italy');
console.log(rest.set(2, 'himanshu ladva'));

rest.set('categories',['Italian', 'Pizzeria', 'Vegetarian', 'Organic']).set('open', 11).
set('close', 23)
.set(true, 'We are open :D')
.set(false, 'We are closed :(');

console.log(rest);

console.log(rest.get('name'));
console.log(rest.get(true));
console.log(rest.get(1));

const time = 11;
console.log(rest.get(time > rest.get('open') && time < rest.get('close')));

console.log(rest.has('categories'));
rest.delete(2);
// rest.clear();  
console.log(rest);
console.log(rest.size);

const arr = [1,2];
rest.set(arr, "test1");
console.log(rest.get(arr));

rest.set(document.querySelector('h1'), 'heading');
console.log(rest); */
// -------------------------------------------------------------------------------------------------------------------------------

// SETS
/* const orderSet = new Set(['pizza','mango','banana','sandwich','apple']);
console.log(orderSet);  

for (const order of orderSet) {
   console.log(order);
}

console.log(orderSet);
console.log(new Set('himanshu'));
console.log(orderSet.size);
orderSet.add('Garlic bread');
orderSet.add('Garlic bread');
orderSet.delete('banana');

// orderSet.clear();
console.log(orderSet);

const staff = [1,2,3,4,5,9,1,2,3];
const uniqueStaff = [...new Set(staff)];
console.log(uniqueStaff);

console.log(new Set([1,2,3,4,5,9,1,2,3]).size);
console.log(new Set("himanshu").size); */

// Looping objects, object keys,value, and entries
/* const properties = Object.keys(openingHours);
console.log(properties);

let openStr = `we are open on ${properties.length} days: `;
for(const day of properties) {
    openStr += `${day}, `
}

console.log(openStr);

const entries = Object.entries(openingHours);
// console.log(entries);
  
for(const [key, {open, close}] of entries) {
  console.log(`${key} ${open} ${close}`);
} */
// -------------------------------------------------------------------------------------------------------------------------------

// Optional chaining
/* if(restaurant.openingHours && restaurant.openingHours.mon) {
  console.log(restaurant.openingHours.mon.open);
}

// with optional chaining
console.log(restaurant.openingHours?.mon?.open);
const days = ['mon', 'tue', 'wed', 'thu', 'fri', 'sat', 'sun'];

for(const day of days) {
  const open = restaurant.openingHours[day]?.open ?? "closed";
  console.log(`On ${day}, we open at ${open}`);
}

// Methods
console.log(restaurant.orderRHrhrt?.(0, 1) ?? 'Method does no exist');

// Arrays
const users = [{name: 'himanshu',
   email: 'hello@himanshu.io'
}];

// const users = [];
console.log(users[0]?.name ?? "User array empty");
 */

// Enhanced Object literals
// 1) moving the defination of inner object out of parent object
// 2) remove in function keyword in object method
// 3) create the 
// -------------------------------------------------------------------------------------------------------------------------------

// The for-of Loop
/* const menu = [...restaurant.starterMenu, ...restaurant.mainMenu];

for (const item of menu) {
  console.log(item);
} 

for(let [i, el] of menu.entries()) {
  console.log(`${i}: ${el}`);
}

// console.log([...menu.entries()]); */
// -------------------------------------------------------------------------------------------------------------------------------

// Logical Assignment Operators
/* const rest1 = {
  name: 'Capri',
  // numGuests: 20,
  numGuests: 0,
};

const rest2 = {
  name: 'La Piazza',
  owner: 'Giovanni Rossi',
};
 
// OR assignment operator
// rest1.numGuests = rest1.numGuests || 10;
// rest2.numGuests = rest2.numGuests || 10;
// rest1.numGuests ||= 10;
// rest2.numGuests ||= 10;

// nullish assignment operator (null or undefined)
rest1.numGuests ??= 10;
rest2.numGuests ??= 10;

// AND assignment operator
// rest1.owner = rest1.owner && '<ANONYMOUS>';
// rest2.owner = rest2.owner && '<ANONYMOUS>';
rest1.owner &&= '<ANONYMOUS>';
rest2.owner &&= '<ANONYMOUS>';

console.log(rest1);
console.log(rest2);
 */
// -------------------------------------------------------------------------------------------------------------------------------

// The Nullish Coalescing Operator
/* restaurant.numGuests = 0;
const guests = restaurant.numGuests || 10;
console.log(guests);

// Nullish: null and undefined (NOT 0 or '')
const guestCorrect = restaurant.numGuests ?? 10;
console.log(guestCorrect); 
 */
// -------------------------------------------------------------------------------------------------------------------------------

// Short Circuiting (&& and ||)
/* console.log('---- OR ----');
// Use ANY data type, return ANY data type, short-circuiting
console.log(3 || 'himanshu');
console.log('' || 'himanshu');
console.log(true || 0);
console.log(undefined || null);

console.log(undefined || 0 || '' || 'Hello' || 23 || null);

restaurant.numGuests = 0;
const guests1 = restaurant.numGuests ? restaurant.numGuests : 10;
console.log(guests1);

const guests2 = restaurant.numGuests || 10;
console.log(guests2);

console.log('---- AND ----');
console.log(0 && 'himanshu');
console.log(7 && 'himanshu');

console.log('' && 23 && null && 'himanshu');

// Practical example
if (restaurant.orderPizza) {
  restaurant.orderPizza('mushrooms', 'spinach');
}

restaurant.orderPizza && restaurant.orderPizza('mushrooms', 'spinach'); */

// -------------------------------------------------------------------------------------------------------------------------------

// Rest Pattern and Parameters
// 1) Destructuring

// SPREAD, because on RIGHT side of =
/* const arr = [1, 2, ...[3, 4]];

// REST, because on LEFT side of =
const [a, b, ...others] = [1, 2, 3, 4, 5];
console.log(a, b, others);

const [pizza, , risotto, ...otherFood] = [
  ...restaurant.mainMenu,
  ...restaurant.starterMenu,
];
console.log(pizza, risotto, otherFood);

// Objects
const { sat, ...weekdays } = restaurant.openingHours;
console.log(weekdays);

// 2) Functions
const add = function (...numbers) {
  let sum = 0;
  for (let i = 0; i < numbers.length; i++) sum += numbers[i];
  console.log(sum);
};

add(2, 3);
add(5, 3, 7, 2);
add(8, 2, 5, 3, 2, 1, 4);

const x = [23, 5, 7];
add(...x);

restaurant.orderPizza('mushrooms', 'onion', 'olives', 'spinach');
restaurant.orderPizza('mushrooms');
 */
// -------------------------------------------------------------------------------------------------------------------------------

// Spread operator
/* const arr = [7, 8, 9];
const badNewArr = [1, 2, arr[0], arr[1], arr[2]]; 
console.log(badNewArr);

const newArr = [1, 2, ...arr];
console.log(newArr);

console.log(...newArr);
console.log(1, 2, 7, 8, 9);

const newMenu = [...restaurant.mainMenu, 'Gnocci'];
console.log(newMenu);

// Copy array 
const mainMenuCopy = [...restaurant.mainMenu];
console.log(mainMenuCopy);

// Join 2 arrays
const menu = [...restaurant.starterMenu, ...restaurant.mainMenu];
console.log(menu); 

// Iteration: arrays, strings, maps, sets, NOT objects
const str = 'Himanshu';
const letters = [...str, '', '5.'];
console.log(letters);
console.log(...str);
// console.log(`${...str} Ladva`);

const ingredients = [
// prompt('Let\'s make pasta! Ingredients 1?'),
// prompt("Ingredient 2?"), 
// prompt("Ingerdient 3")
];

console.log(ingredients);

restaurant.orderPasta(ingredients[0], ingredients[1], ingredients[2]);
restaurant.orderPasta(...ingredients);

// Objects
const newRestaurant = {foundedIn: 1998, ...restaurant, founder: 'himanshu'};
console.log(newRestaurant);

const restaurantCopy = {...restaurant};
restaurantCopy.name = 'Ristorante Roma';
console.log(restaurantCopy.name);
console.log(restaurant.name); */
// -------------------------------------------------------------------------------------------------------------------------------

// destructuring object
/* restaurant.orderDelivery({
  time: '22:30',
  address: 'Via del Sole, 21',
  mainIndex: 2,
  starterIndex: 2,
});

restaurant.orderDelivery({
  address: 'Via del Sole, 21',
  starterIndex: 1,
});

const {name, openingHours, categories} = restaurant;
console.log(name , openingHours, categories); 

const {name: restaurantName, openingHours: hours, categories: tags} = restaurant;
console.log(restaurantName, hours, tags);

const {menu = [], starterMenu: starters = []} = restaurant;
console.log(menu, starters);

// Mutating varibles
let a = 111;
let b = 999;
const obj = { a: 23, b: 7, c: 14};
({a, b} = obj);
console.log(a, b);

// nested  objects
const {fri : {open : o, close : c}} = openingHours;
console.log(o, c); */
// -------------------------------------------------------------------------------------------------------------------------------

// destructuring Array
/* const arr = [1, 2, 3];
const a = arr[0];
const b = arr[0];
const c = arr[0];
       
const [x, y, z] = arr;
console.log(x, y, z);
console.log(arr);

let [main, secondary] = restaurant.categories;
console.log(main, secondary);

// swap logic
// const temp = main;
// main = secondary;
// secondary = main;
// console.log(main, secondary);

[main, secondary] = [secondary, main];
console.log(main, secondary);

// Receive 2 return valuers from a function  
console.log(restaurant.order(2, 1));
const [first, second] = restaurant.order(2, 1);
console.log(first, second);

// Nested destructuring
const nested = [2, 4, [5, 6]];
// const [i, ,j] = nested;
const [i, , [j , k]] = nested;
console.log(i, j, k);

// Default values
const [p=1, q=1, r=1] = [8, 9];
console.log(p ,q ,r); */