// let js = 'amazing';
// if (js === 'amazing') alert('javascript fun');

// console.log(40 + 8 + 23 - 10);
// -------------------------------------------------------

// Data types
/* let javascriptIsFun = true;
console.log(javascriptIsFun); 
console.log(typeof true);
console.log(typeof javascriptIsFun);
console.log(typeof 23);
console.log(typeof 'himanshu');

javascriptIsFun = 'ladva';
console.log(typeof javascriptIsFun);

let year;
console.log(typeof year);

console.log(typeof null); */
// ----------------------------------------------------------

// let,var and const
/* let age = 30;
age = 31;

const birthYear =  2001;
// birthYear = 2002;

var job = 'programmer';
job = 'himanshu';
console.log(job);

lastName = "himanshu ladva";
console.log(lastName); */
// -------------------------------------------------------------

/* // operators
// math operator
const now = 2022;
const ageLadva = 2022 - 2001;
const ageKLadva = 2022 - 2005;
console.log(ageLadva, ageKLadva);

console.log(ageLadva * 2, ageKLadva / 10, 2 ** 3);

const firstName = "himanshu";
const lastName = " ladva";
console.log(firstName + lastName);

// Assignment Operator
let x = 10 + 5;
x += 10;
x *= 4; 
console.log(x);

// comparison operator
console.log(ageLadva > ageKLadva);
console.log(ageLadva >= 25);

console.log(now - 2022 > now - 2025); */
//--------------------------------------------------------------------------------

// 10-operator precedence
/* const now = 2022;
const ageLadva = 2022 - 2001;
const ageKLadva = 2022 - 2005;

console.log(now - 2022 > now - 2025);  

console.log(25-10-5); 

let x,y; 
x = y = 25-10-5;
console.log(x,y);

const averageAge = ageLadva + ageKLadva / 2;
console.log(ageLadva, ageKLadva, averageAge); */
// -----------------------------------------------------------------

// string template 
/* const firstName = 'himanshu';
const job = 'programmer';
const birthYear = 1991;
const year = 2022;

const him = "i am "+firstName+" a "+(year-birthYear)+" old "+job;
console.log(him);

const himanshuNew = `I am ${firstName} a ${year-birthYear} year old ${job }`;
console.log(himanshuNew);

console.log(`just a regular string......`);

console.log('String with \n\ mutiple \n\ lines');

console.log(`String
multiple
lines`); */
//----------------------------------------------------------------------

// Taking decision
/* const age = 19;
// const isOldEnough = age >= 18;

// if(isOldEnough) {
//     console.log('himanshu can driving licecne');
// }
if(age >= 18){
    console.log('himanshu can driving licecne');
}
else {
    const yearLeft = 18- age; 
    console.log(`himanshu is too young, Wait another ${yearLeft} years`);
} 

const birthYear = 1991;
let century;
if(birthYear <= 2000) {
    century = 20;
}
else {
    century = 21;
}
console.log(century); */
//-----------------------------------------------------------
 
// type convrsion 
/* const inputYear = '1991';
console.log(Number(inputYear), inputYear);
console.log(Number(inputYear) + 18);

console.log(Number('himanshu'));
console.log(typeof NaN);

console.log(String(23), 23);

// type coercion
console.log('I am '+ 23 + 54);
console.log('23' - 10 - 3);
console.log('23' / '2');

let n = '1' + 1;
n = n- 1;
console.log(n); */
// --------------------------------------------------------------------

// Truthy and falsy values
// 5 falsy values : 0, '', undefined, null, NaN
/* console.log(Boolean(0));
console.log(Boolean(undefined));
console.log(Boolean('him'));
console.log(Boolean({}));
console.log(Boolean(''));

const money = 100;
if(money) {
    console.log('dont spend it');
}
else {
    console.log('you should get a job');
}

let height;
if(height) {
    console.log('height is defined');
}
else {
    console.log('height is not defined');
} */
// -------------------------------------------------------  ---------

// equality opeators == vs ===
/* const age = 18; 
//  === is not perfrom type corecion 
if(age === 18) console.log('you just become an adult strict');
if(age == 18) console.log('you just become an adult loose');

const fav = prompt('what is your fav number?');
console.log(fav);
console.log(typeof fav);

if(fav === 23) {
    console.log('23 is my number'); 
}else if(fav === 7){
    console.log('7 is my number');
}else if(fav === '9'){
    console.log('9 is my number');
}
else {
    console.log('number is not a 23 and 7');
}  

if(fav !== 23) console.log('why not 23?'); */
// --------------------------------------------------------------

// logical operator
/* const hasDriversLicense = true;
const hasGoodVision  = false; 

console.log(hasDriversLicense && hasGoodVision);
console.log(hasDriversLicense || hasGoodVision);
console.log(!hasDriversLicense);

const shouldDrive = hasDriversLicense && hasGoodVision;

// if(shouldDrive) {
//     console.log('him is able to drive');
// } else {
//     console.log('him is not able to drive');
// }

const isTired = true;
console.log(hasDriversLicense || hasGoodVision || isTired);

if(hasDriversLicense && hasGoodVision && !isTired) {
    console.log('him is able to drive');
}else {
    console.log('him is not able to drive');
} */
// -----------------------------------------------------------------------

// switch statement

/* const day = 'monday';

switch(day) {
    case 'monday':
        console.log('plan course structure');
        console.log('go to coding meetup');
        break;
    case 'tuesday':
        console.log('preapare theory videos');
        break;
    case 'wednesday':
    case 'thrusday':
        console.log('write code examples');
        break;
    case 'saturday':
    case 'sunday':
        console.log('Enjoy the weekend');
        break;
    default:
        console.log('not a valid day');
} */

// statements and expresssion  
/* 
3+4
1991
true && false && !false  // expression

if( 23 > 10 ) {
    const str = '23 is bigger';
}  

const me = 'him';
console.log(`i am ${2022-1991} years old ${me}`); */

// -----------------------------------------

// ternary operator
const age = 23;

// age >= 18 ? console.log('i like to drink wine') : console.log('i like to drink water');

const drink = age >= 18 ? 'wine': 'water';
console.log(drink);

console.log(`i like to drink ${age >= 18 ? 'wine': 'water'}`);

