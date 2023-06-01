"use strict";

// Default Parameters
/* const bookings = [];

const createBooking = function(flightNum, numPassengers = 1, price = 199 * 1.2) {
    
    // ES5
    // numPassengers = numPassengers || 10;
    // price = price || 100;
    const booking = {
        flightNum,
        numPassengers,
        price
    }
    console.log(booking);
    bookings.push(booking);
}

createBooking('LH123');
createBooking('LH123',2, 700);
createBooking('LH123', 2);
// skiping the parameter
createBooking('LH123',undefined,100); */
// --------------------------------------------------------------------------------------------------------

// how passing arguments works: value vs. reference
/* const flight = 'LH234';
const himanshu = {
    name: 'Himanshu Ladva',
    passport: 2224583669,
}

const checkIn = function(flightNum, passenger) {
    flightNum = 'LH999';
    passenger.name = 'Mr. ' + passenger.name;

    if(passenger.passport === 2224583669) {
        console.log('check in');
    } else {
        console.log('wrong it!');
    }

}

checkIn(flight, himanshu);
console.log(flight);
console.log(himanshu);

const flightNum = flight;  
const passenger = himanshu;

passenger.passport = Math.trunc(Math.random() * 100000000000 + 1);

checkIn(flight, himanshu);
console.log(himanshu); */
// ---------------------------------------------------------------------------------------------

// function accepting callback functions
/* const oneWord = function(str) {
    return str.replace(/ /g, '').toLowerCase();
}

const upperFirstWord = function (str) {
    const [first, ...others] = str.split(' ');
    return [first.toUpperCase(), ...others].join(' ');
}

const transformer = function (str, fn) {
    console.log(`Original string: ${str}`);
    console.log(`Transformed string: ${fn(str)}`);
    console.log(`Transformed by: ${fn.name}`);
}

transformer('Javascript is the best!', upperFirstWord);
transformer('Javascript is the best!', oneWord);
 
const high5 = function() {
    console.log("hello himanshu");
}

document.body.addEventListener('click', high5); 

['himanshu', 'vishal', 'darshit'].forEach(high5); */
// ---------------------------------------------------------------------------------------------

// Function returning functions
/* // const greet =  function (greeting) {
//     return function(name) {
//         console.log(`${greeting} ${name}`);
//     }
// }

const greet = greeting => name =>  console.log(`${greeting} ${name}`);

// const greeterHey = greet ('hey');
// greeterHey('himanshu');
// greeterHey('darshit');
greet('hello')('himanshu'); */
// ---------------------------------------------------------------------------------------------

// The call and apply methods
/* const lufthansa = {
    airline: 'Lufthansa',
    iataCode: 'LH',
    booking: [],
    book(flightNum, name) {
       console.log(`${name} booked a seat on ${this.airline} flight ${this.iataCode}${flightNum}`);
       this.booking.push({flight: `${this.iataCode}${flightNum}`, name});
    },
};

lufthansa.book(239, 'himanshu ladva');
lufthansa.book(635, 'darshit makadia');
console.log(lufthansa);

const eurowings = {
    airline: 'Eurowings',
    iataCode: 'EW',
    booking:[],
};

const book = lufthansa.book;

// does not work
// book(23, "sarah ali khan");

// call method
book.call(eurowings, 23, "sarah ali khan");
console.log(eurowings);

book.call(lufthansa, 32, "Mary Copper");
console.log(lufthansa);

// Apply method
const clientData = [553, "vishal lakhani"];
book.apply(eurowings, clientData);
console.log(eurowings); 

book.call(lufthansa, ...clientData);
console.log(lufthansa); */
// ---------------------------------------------------------------------------------------------------

// The bind Method
/* const lufthansa = {
    airline: 'Lufthansa',
    iataCode: 'LH',
    booking: [],
    book(flightNum, name) {
       console.log(`${name} booked a seat on ${this.airline} flight ${this.iataCode}${flightNum}`);
       this.booking.push({flight: `${this.iataCode}${flightNum}`, name});
    },
};

const eurowings = {
    airline: 'Eurowings',
    iataCode: 'EW',
    booking:[],
};
// book.call(eurowings, 23, "sarah ali khan");
const book = lufthansa.book;

const bookEW = book.bind(eurowings);
const bookLH = book.bind(lufthansa);
bookEW(23, 'HA Ladva');
bookLH(45, 'hello ladva');
console.log(eurowings);
console.log(lufthansa);

const bookEW23 = book.bind(eurowings, 67);
bookEW23('kartik');
bookEW23('savaliya');
console.log(eurowings);

// // with event listener
lufthansa.planes = 300;
lufthansa.buyPlane = function() {
    console.log(this);
    this.planes++;

    console.log(this.planes);
}


// document.querySelector('.buy').addEventListener('click', lufthansa.buyPlane.bind(lufthansa));

// partial application

const addTax = (rate, value) => value + value * rate;
console.log(addTax(10, 100));

const addVAT = addTax.bind(null, 0.23);
console.log(addVAT(100));

const addMyTax = function(rate) {
    return function(value) {
        return value + value * rate;  
    }
}

// const addVAT2 = addMyTax(0.23);
// console.log(addVAT2(100));
console.log(addMyTax(0.23)(100)); */
// ------------------------------------------------------------------------------------------

// Immediately invoked function expression (IIFE)
/* (function () {
    console.log(`This will never run again`);
    var isPrivate = 23;
})();
 
(() => console.log("hello ladva"))(); */
// console.log(isPrivate);

// ------------------------------------------------------------------------------------------

// closures
/* const secureBooking = function() {
    let passengerCount = 0;
    console.log('hello ladva');
    return function () {
        let num = 0;
        passengerCount++;
        console.log(`pass = ${passengerCount}`);

        return function() {
            num++;
            console.log(`num = ${num}`);
        }
    }
}

const booker = secureBooking();
const numBook = booker();
booker();
booker();
booker();
numBook();
numBook();
numBook();
// secureBooking()()();
console.dir(numBook); 
// console.dir(booker);  */

// More Closure Examples
/* let f;

const g = function() {
    const a = 23;
    f = function() {
        console.log((a*2));
    };
}

const h = function() {
    const b = 50;
    f = function() {
        console.log((b*2));
    };
}
g();
f();
console.dir(f);

h();
f();
console.dir(f);

// example 2 
const boardPassengers = function(n, wait) {
    const perGroup = n /3; 
    setTimeout(() => {
        console.log(`we are now bording all ${n} passenger`);
        console.log(`There are a 3 groups, each with ${perGroup} passenger`);

    }, wait * 1000);

    console.log(`will start bording in ${wait} seconds`);
};

// const perGroup = 1000;
boardPassengers(180, 3); */