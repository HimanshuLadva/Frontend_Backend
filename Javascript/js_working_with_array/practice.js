let arr = ['a', 'b', 'c', 'd', 'e'];
console.log(arr.slice(2,3));
console.log(arr);

console.log(arr.reverse());
console.log(arr);

const y = Array.from( {length : 7}, () => 1);
console.log(y);

const z = Array.from({length : 7}, (_, i) => i + 1);
console.log(z);g