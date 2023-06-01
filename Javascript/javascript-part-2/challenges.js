// coding challenge -1
/* function checkWinner(Dscore, Kscore) {
   if(Dscore >= 2 * Kscore) {
     console.log(`Dolphins wins (${Dscore} vs. ${Kscore})`);
   }
   else if(Kscore >= 2 * Dscore){
    console.log(`Koalas wins (${Kscore} vs. ${Dscore})`);
   }else {
    console.log('No team wins trophy');
   }
}
const calcAverage = (num1,num2,num3) => (num1 + num2 + num3)/3;

let avgDolphins = calcAverage(44,23,71);
let avgKoalas = calcAverage(65,54,49);

checkWinner(avgDolphins, avgKoalas);

// test 2
avgDolphins = calcAverage(85,54,41);
avgKoalas = calcAverage(23,34,27);
checkWinner(avgDolphins, avgKoalas); */

// coding challenge -2
/* const calcTip = function(value) {
    const tip = (value > 50 && value < 300) ? (value * (15/100)): (value * (20/100));
    console.log(tip); 
    return tip;  
};

const bills = [125,555,44];
const tips = [calcTip(bills[0]),calcTip(bills[1]),calcTip(bills[2])];

const total = [
  bills[0]+tips[0],
  bills[1]+tips[1],
  bills[2]+tips[2],
];

console.log(total); */

// coding challenge -3
/* const markDetail = {
  fullName : "Mark Miller",
  mass : 78,
  height : 1.69,
  calcBMI :function() {
    this.BMI = this.mass / this.height ** 2; 
    return this.BMI;
  }
};

const johnDetail = {
  fullName : "john Smith",
  mass : 92,
  height : 1.95,
  calcBMI :function() {
    this.BMI = this.mass / this.height ** 2;
    return this.BMI;
  }
};

if(johnDetail.calcBMI() > markDetail.calcBMI()) {
  console.log(`John's BMI ${johnDetail.BMI} is higher than Mark's ${markDetail.BMI}!`);
} else {
  console.log(`John's BMI ${markDetail.BMI} is higher than Mark's ${johnDetail.BMI}!`);
} */

// coding challenege -4
/* const calcTip = function (value) {
  const tip =
    value > 50 && value < 300 ? value * (15 / 100) : value * (20 / 100);
  // console.log(tip);
  tips.push(tip);
  return tip;
};

const calcAverage = function (arr) {
   let sum = 0;
   for(let i = 0; i < arr.length; i++) {
     sum +=arr[i];
   }

   return sum/arr.length ; 
}
const bills = [22, 295, 176, 440, 37, 105, 10, 1100, 86, 52];
const tips = [];
const total = [];

for(let i = 0; i< bills.length; i++) {
   
   total.push(calcTip(bills[i])+bills[i]);
}


console.log(bills, tips, total);
console.log(calcAverage(total));
console.log(calcAverage(tips));  */