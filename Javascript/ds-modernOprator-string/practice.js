const people = [
    {
      name: 'Mike Smith',
      family: {
        mother: 'Jane Smith',
        father: 'Harry Smith',
        sister: 'Samantha Smith'
      },
      age: 35
    },
    {
      name: 'Tom Jones',
      family: {
        mother: 'Norah Jones',
        father: 'Richard Jones',
        brother: 'Howard Jones'
      },
      age: 25
    }
  ];
  
  const [{name: n , family: {father:fatherName}}] = people;
  console.log(n, fatherName);
  
//   map for loop
let mp=new Map()
 
mp.set("a",1);
mp.set("b",2);
mp.set("c",3);

mp.forEach((key,value) => {
    console.log(key,value);
});

let person = {firstName:"John", lastName:"Doe", age:50, eyeColor:"blue"};

for(const [key, value] of Object.entries(person)) {
    console.log(key, value);
}

console.log(Object.entries(person));

// -----------------------------------------------------------------------------------------------------------------

/* const obj = {
    name: "himanshu",
    {lastName: "ladva"}:"value",
};

console.log(obj); */

let values = ["Hare", "Krishna", "Hare", "Krishna",
  "Krishna", "Krishna", "Hare", "Hare", ":-O"
];

const mySet = new Set();
values.forEach((value) => {
    mySet.add(value);
})

console.log(mySet);

function aclean(arr) {
    let map = new Map();
  
    for (let word of arr) {
      // split the word by letters, sort them and join back
      console.log("himanshu");
      let sorted = word.toLowerCase().split('').sort().join(''); // (*)
      map.set(sorted, word);
    }
  
    return Array.from(map.entries());
  }
  
//   let arr = ["nap", "teachers", "cheaters", "PAN", "ear", "era", "hectares"];
  let arr = ["himanshu", "darshit", "yash", "vishal", "dhval","nikhil", "manhishu"];
//   console.log(arr[0].toLowerCase().split('').sort().join(''));
  console.log(aclean(arr));

  const fruits = new Map([
    ["apples", 500],
    ["bananas", 300],
    ["oranges", 200]
  ]);

//   console.log(Object.fromEntries(fruits.entries()));

  const obj = {
    name: "himanshu",
    age: 20,
};

// console.log(Object.entries());

Object.entries(obj).forEach(([value,key])=> {
    console.log(value, key);
})

// arr.forEach((value,key)=> {
//     console.log(value, key);
// })

// for(const [i, item] of Object.entries(arr)) {
//     console.log(i, typeof item);
// }

// for(const [i, item] of arr.entries()) {
//     console.log(i, item);
// }

// for(const [key, value] of Object.entries(obj)) {
//     console.log(key, value);
// }
// ----------------------------------------------------------------------

/* console.log(obj.myName?.("himanshu") ?? "Method not exits");

const airline = 'TAP Air Portugal';
console.log(airline.slice(2,4)); */

// jhonty sir
console.log('----------jhonty Sir------------');
// const value = null || undefined ?? "test";  // this not work 
const value = (null || undefined) ?? "test";
console.log(value);

// this print same element key === item
for(const [key, item] of mySet.entries()) {
  console.log(key, item);
}

// this also print an object in displaying
const mySet2 = new Set([1, "text", {a:2, b:3}]);
console.log(mySet2);

const obj1 = {
  name: "himanshu",
  age: 21,
};
const obj2 = {
  name: "vishal",
  age: 22,
};

mySet2.add(obj1);
mySet2.add(obj2);
console.log(mySet2);