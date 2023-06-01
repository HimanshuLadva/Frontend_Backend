// Basic types
let id: number = 5;
let company: string = "Himanshu Ladva";
let isPublished: boolean = true;
let x: any = "Hello";
x = "world";
console.log("ID:", id);

let ids: number[] = [1, 2, 3, 4, 5];
let arr: any[] = [1, true, 'hello'];

// Tuple 
// here is order matter
let person: [number, string , boolean] = [1, "hello", true]; 
// tuple array
let employee: [number, string][] =  [
    [1, "H"],
    [2, "B"],
    [3, "C"]
];

// Union  
let pid: string | number;
pid = '22';

// enums
enum Direction1 {
    Up = 1,
    Down,
    Left,
    Right
}
enum Direction2 {
    Up = 'Up',
    Down = 'Down',
    Left = 'Left',
    Right = 'Right'
}
console.log(Direction2.Up);

// Objects
type User = {
    id: number,
    name: string
}

const user: User = {
    id: 1,
    name: 'Himanshu',
}

// type assertion 
let cid: any = 1;
let customerId =  <number>cid;
console.log(customerId);

// function
function addNum(x: number,y: number):number {
     return x+y;
}

console.log(addNum(1,2));

// Void
function log(message: string | number):void {
    console.log(message); 
}

log("Hello world");

// interfaces
interface UserInterface {
    readonly id: number //readonly property
    name: string
    age?: number // optional property
}

const user1: UserInterface = {
    id: 1,
    name: 'Himanshu'
}
user1.name = "Ladva";

// we cannot edit read-only property
// user1.id = 2;
// type are only use with primitives and unions

interface MathFunc {
    (x: number, y: number): number
}

const add: MathFunc = (x: number, y: number): number => x + y;
const sub: MathFunc = (x: number, y: number): number => x - y;


interface PersonInterface {
    id: number
    name: string
    register(): string
}
// Classes
class Person implements PersonInterface {
   id: number
   name: string   

   constructor(id: number, name: string) {
     this.id = id;
     this.name = name;
     console.log(123);
   } 

   register() {
    return `${this.name} is now registered`
   }
};

const brad = new Person(1 ,"Himanshu");
const mike = new Person(2, "Universal King");

console.log(brad.register());
console.log(brad, mike);

// Subclass
class Employee extends Person {
    position: string

    constructor(id: number, name: string, position: string) {
    super(id, name);
    this.position = position;
    }
}

const emp = new Employee(3, "darshit", 'developer');
console.log(emp.register());

// Generic
function getArray<T>(items: T[]):T[] {
    return new Array().concat(items);
}
let numArray = getArray<number>([1,2,3,4]);
let strArray = getArray<string>(['him','kar','dar']);

numArray.push(15);

console.log(numArray);