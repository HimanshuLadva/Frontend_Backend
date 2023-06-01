'use strict'; 

// SCOPING IN PRATICE
/* function calcAge(birthYear) {
    const age = 2037 - birthYear;

    function printAge() {
        let output= `${firstName}, You are ${age}, born in ${birthYear}`;
        console.log(output);

        if(birthYear >= 1981 && birthYear <= 1996) {
            var millenial = true;
            // Creating new variable with same name as outer scope's variable
            const firstName = 'ladva';
   
            // Reassignning outer scope's variable
            output = 'NEW OUTPUT!';
            const str = `Oh, and you are a millenial , ${firstName}`;
            console.log(str);

            function add(a,b) {
                return a+b;
            }
        } 
        // console.log(str);
        console.log(millenial);
        console.log(output);
        // console.log(add(2,3)); 
    }
    printAge();

    return age;
}

// console.log(millenial);
const firstName = 'himanshu';
calcAge(1991); 
// console.log(age);
// printAge(); */
// ------------------------------------------------------------------------

// Hoisting in javascript

/* // hoisting in variable
console.log(me);
// console.log(job);
// console.log(year);

var me = "himanshu";
let job = 'teacher';
const year = 1991; 

// hoisting in function
console.log(addDecl(2,3));
// console.log(addExpr(2,3)); 
console.log(addArrow);
// console.log(addArrow(2,3));

function addDecl(a,b) {
    return a+b;
}

const addExpr = function(a,b) {
    return a + b;
}

var addArrow = (a,b) => a+b; 

// examples  
console.log(undefined);
if(!numProducts) deleteShoppingCart();

var numProducts = 10;
  
function deleteShoppingCart() {
    console.log('all products deleted!');
}

var x = 1;
let y = 2;
const z = 3;

console.log(x === window.x);
console.log(y === window.y);
console.log(z === window.z); */
// ---------------------------------------------------------------------

// this keyword 
/* console.log(this); 

const calcAge = function(birthYear) {
    console.log(2037 - birthYear);
    console.log(this);
};
calcAge(2001);

const calcAgeArrow = (birthYear) => {
    console.log(2037 - birthYear);
    console.log(this);
};
calcAgeArrow(2001);

const himanshu = {
    year : 10,
    calcAge: function() {
        console.log(this);
        console.log(2037 - this.year);
    },
};

himanshu.calcAge();

const matilda = {
    year: 20,
};

matilda.calcAge = himanshu.calcAge;
matilda.calcAge();

const f = himanshu.calcAge;
f(); */
// -----------------------------------------------------------------------

// regular function vs arrow function
/* // var  firstName = "himanshu ladva";

const himanshu = {
    year : 10,
    calcAge: function() {
        console.log(this);
        console.log(2037 - this.year);
        
        // Solutioon -1
        // const self = this;
        // const isEligible = function() {
        //     console.log(self); 
        //     console.log(self.year >= 1981 && self.year <= 1996);
        // }

        // Solution -2
        const isEligible = () => {
            console.log(this); 
            console.log(this.year >= 1981 && this.year <= 1996);
        }
        isEligible();
    },

    // greet: () => {
    //     console.log(this);
    //     console.log(`hey ${this.firstName}`);
    // },
    greet: function() {
        console.log(this);
        console.log(`hey ${this.firstName}`);
    }
};

// himanshu.greet();
himanshu.calcAge();

// argument keyword
const addExpr = function(a,b) {
    console.log(arguments);
    return a + b;
}
addExpr(2, 5);
addExpr(2, 5 ,7,8);

var addArrow = (a,b) => {
    console.log(arguments);
    return a+b
}; 
// addArrow(2,5,8);

function myCalc(a ,b) {
     console.log(arguments);
     console.log("hello world");
}

myCalc(2,4); */
// --------------------------------------------------------------------------

// Primitives vs Objects in pratice

// primitive types
let lastName = 'himanshu';
let oldLastName = lastName;
lastName = 'ladva';
console.log(lastName, oldLastName);

// Reference type
const detail  = {
    firstName : 'himanshu',
    lastName : 'ladva',
    age: 21,
};
const brotherDetail = detail;
brotherDetail.firstName = "darshit";
console.log('my detail '+detail);
console.log('brother detail '+brotherDetail);

// brotherDetail = {}; 

// Copying objects
const detailMe  = {
    firstName : 'himanshu',
    lastName : 'ladva',
    age: 21,
    family: ['Vishal', 'jamod'],  
}; 

// this is for shallow copy
const detailYou = Object.assign({}, detailMe);
detailYou.firstName = 'darshit';

console.log('my detail ', detailMe);
console.log('your detail ', detailYou);

// deep copy is achieved with help of Lo-dash library 
detailYou.family.push('adam ');
detailYou.family.push('john');

console.log('my detail ', detailMe);
console.log('your detail ', detailYou);