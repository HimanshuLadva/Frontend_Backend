'use strict'; 

// USE-STRICT

/* let hasDriverLicense = false;
const passTest = true; 

if(passTest) hasDriverLicense = true;
if(hasDriverLicense) console.log('i can drive'); 

// const interface = 'audio';
// const private = 534;
// const if = 23;  */
// --------------------------------------------------

// FUNCTION
/* function logger() {
    console.log('my name is himanshu');
} 

logger();
logger();
logger();

function fruitProcessor(apple, orange) {
    console.log(apple, orange);
    const juice = `juice with ${apple} apples and ${orange} oranges`;
    return juice;
}

const appleJuice = fruitProcessor(5,0);
console.log(appleJuice);
console.log(fruitProcessor(5,0));

const appleOrangeJuice = fruitProcessor(2,5);
console.log(appleOrangeJuice);

const num = Number('23'); */
// ------------------------------------------------------------------

// FUNCTION DECELARATIONS VS EXPRESSIONS
/* function calcAge1(birthYear) {
    return 2037-birthYear;
}
const age1 = calcAge1(1991);

const calcAge2 = function (birthYear) {
    return 2037-birthYear;
}
const age2 = calcAge2(1991);

console.log(age1,age2); */
// ------------------------------------------------------------------

// ARROW FUNCTION
/* const calcAge3 = birthYear => birthYear;
const age3 = calcAge3(1990);
console.log(age3);

const yearUntilRetirement = (birthYear, firstName) => {
    const age = 2037 - birthYear;
    const retirement = 65-age;
    return `${firstName} retires in ${retirement} years`;
}

console.log(yearUntilRetirement(1991, 'himanshu'));
console.log(yearUntilRetirement(1980, 'bob')); */

// FUNTIONS CALLING ANOTHER FUNCTION -pacchu jovu
/* function cutFruitPieces(fruit) {
    return fruit * 4;
}

function fruitProcessor(apple, orange) {
    const applePieces = cutFruitPieces(apple);
    const orangePieces = cutFruitPieces(orange);
    
    const juice = `juice with ${applePieces} piece of
    apple and ${orangePieces} pieces of orange.`;
    
    return juice;
}
console.log(fruitProcessor(2,3)); */

// INTRODUCTION TO ARRAY
/* const friends = ['himanshu','vishal','darshit'];
console.log(friends);

const years = new Array(2021,2022,2023,2024);

console.log(friends[0]);
console.log(friends[1]);

console.log(friends.length);
console.log(friends[friends.length -1]);

friends[2] = 'jay';
console.log(friends);
// friends = ['raj', 'hardik'];

const firstName = 'himanshu';
const himanshu = [firstName, 'ladva', 2001, 'programmer'
,friends];

console.log(himanshu);
console.log(himanshu.length);

// EXERCISE
const calcAge2 = function (birthYear) {
    return 2037-birthYear;
}

const years2 = [1991,1992,1993,1994,1995];
const age1 = calcAge2(years2[0]);
const age2 = calcAge2(years2[1]);
const age3 = calcAge2(years2[2]);

console.log(age1,age2,age3);

const ages = [calcAge2(years2[0]),calcAge2(years2[1]),calcAge2(years2[2])];
console.log(ages); */

// BASIC ARRAY OPERATIONS (METHOD)
/* const friends = ['himanshu','vishal','darshit'];

// Add Elements
const newLength = friends.push('jay'); //push function returns the length of array
console.log(friends);
console.log(newLength);

friends.unshift('raj'); // add to the beginnig
console.log(friends);

// Remove element
friends.pop(); // remove from the last
console.log(friends);

friends.shift(); //remove from the beginning
console.log(friends);

//indexOF method
console.log(friends.indexOf('vishal'));
console.log(friends.indexOf('ravan'));

friends.push(23);
//includes method
console.log(friends.includes('vishal'));
console.log(friends.includes('ravan'));
console.log(friends.includes('23'));

if(friends.includes('vishal')) console.log('you have a friend called vishal');
 */

// INTRODUCTION TO OBJECTS
/* const details = {
    firstName : 'himanshu',
    lastName : 'ladva',
    age : 21,
    job : 'programmer',
    friends: ['himanshu','vishal','darshit']
}; */

// DOT VS BRACKET NOTATION
/* const detail = {
    firstName : 'himanshu',
    lastName : 'ladva',
    age : 21,
    job : 'programmer',
    friends: ['himanshu','vishal','darshit']
};

console.log(detail.lastName);
console.log(detail['lastName']);

const nameKey = 'Name';
console.log(detail['first' + nameKey]);
console.log(detail['last' + nameKey]);

const interestedIn = prompt('what do you want to know about me');

if(detail[interestedIn]) console.log(detail[interestedIn]);
else console.log('wrong interest');

detail.location = 'shapar';
detail['instagram'] = '@ladva.himanshu';
console.log(detail);

// access third friend
console.log(detail.friends[2]); */

// OBJECT METHODS
/* const detail = {
    firstName : 'himanshu',
    lastName : 'ladva',
    birthYear : 2001,
    job : 'programmer', 
    friends: ['himanshu','vishal','darshit'],
    // calcAge: function(birthYear) {
    //     return 2037-birthYear;
    // }, 
    // calcAge: function() {
    //     return 2037-this.birthYear;
    // },
    calcAge: function() {
        this.age = 2037-this.birthYear;
        return this.age;
    },  
    getSummary : function() {
        return `${this.firstName} is a ${this.calcAge()} -year old ${this.job}`;
    }, 
};

// console.log(detail.calcAge(1990));
// console.log(detail['calcAge'](1990));

console.log(detail.calcAge());
console.log(detail.age);
console.log(detail.getSummary()); */

// ITERATION: THE FOR LOOP
/* for(let rep = 0; rep <= 10; rep++) {
    console.log(`lifting weights repetition ${rep}`);
} */

// LOOPING ARRAYS, BREAKING AND CONTINUING
/* const himanshu = [
  "himanshu",
  "ladva",
  2001,
  "programmer",
  ["darshit", "vishal", "raj"],
];

for(let i = himanshu.length -1; i >= 0; i--) {
    console.log(himanshu[i]);
}

for(let i = 0; i<5;i++) {
    console.log('hello-------------------------');
    for(let j = 0;j<5;j++){
        console.log('world');
    }
} */
// ------------------------------------------------------------------

// THE WHILE LOOP
/* let i = 0;
while(i <= 10) {
    console.log('hello world');
    i++;
}

do {
  console.log("hello world");
  i++;
} while (i <= 10);

let dice = Math.trunc(Math.random()*6) + 1;

while(dice!==6) {
    console.log(`you rolled a ${dice}`);
    dice = Math.trunc(Math.random() * 6) + 1;

    if(dice === 6) {
        console.log('loop is about to end.....');
    }
} */
