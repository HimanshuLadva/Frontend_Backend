"use strict";
// Basic types
let id = 5;
let company = "Himanshu Ladva";
let isPublished = true;
let x = "Hello";
x = "world";
console.log("ID:", id);
let ids = [1, 2, 3, 4, 5];
let arr = [1, true, 'hello'];
// Tuple 
// here is order matter
let person = [1, "hello", true];
// tuple array
let employee;
employee = [
    [1, "H"],
    [2, "B"],
    [3, "C"]
];
// Union  
let pid;
pid = '22';
// enums
var Direction1;
(function (Direction1) {
    Direction1[Direction1["Up"] = 1] = "Up";
    Direction1[Direction1["Down"] = 2] = "Down";
    Direction1[Direction1["Left"] = 3] = "Left";
    Direction1[Direction1["Right"] = 4] = "Right";
})(Direction1 || (Direction1 = {}));
var Direction2;
(function (Direction2) {
    Direction2["Up"] = "Up";
    Direction2["Down"] = "Down";
    Direction2["Left"] = "Left";
    Direction2["Right"] = "Right";
})(Direction2 || (Direction2 = {}));
console.log(Direction2.Up);
const user = {
    id: 1,
    name: 'Himanshu',
};
// type assertion 
let cid = 1;
let customerId = cid;
console.log(customerId);
// function
function addNum(x, y) {
    return x + y;
}
console.log(addNum(1, 2));
// Void
function log(message) {
    console.log(message);
}
log("Hello world");
const user1 = {
    id: 1,
    name: 'Himanshu'
};
user1.name = "Ladva";
const add = (x, y) => x + y;
const sub = (x, y) => x - y;
// Classes
class Person {
    constructor(id, name) {
        this.id = id;
        this.name = name;
        console.log(123);
    }
    register() {
        return `${this.name} is now registered`;
    }
}
;
const brad = new Person(1, "Himanshu");
const mike = new Person(2, "Universal King");
console.log(brad.register());
console.log(brad, mike);
// Subclass
class Employee extends Person {
    constructor(id, name, position) {
        super(id, name);
        this.position = position;
    }
}
const emp = new Employee(3, "darshit", 'developer');
console.log(emp.register());
// Generic
function getArray(items) {
    return new Array().concat(items);
}
let numArray = getArray([1, 2, 3, 4]);
let strArray = getArray(['him', 'kar', 'dar']);
numArray.push(15);
console.log(numArray);
