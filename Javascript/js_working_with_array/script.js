"use strict";

/////////////////////////////////////////////////
/////////////////////////////////////////////////
// BANKIST APP

// Data
const account1 = {
  owner: "Jonas Schmedtmann",
  movements: [200, 450, -400, 3000, -650, -130, 70, 1300],
  interestRate: 1.2, // %
  pin: 1111,
};

const account2 = {
  owner: "Jessica Davis",
  movements: [5000, 3400, -150, -790, -3210, -1000, 8500, -30],
  interestRate: 1.5,
  pin: 2222,
};

const account3 = {
  owner: "Steven Thomas Williams",
  movements: [200, -200, 340, -300, -20, 50, 400, -460],
  interestRate: 0.7,
  pin: 3333,
};

const account4 = {
  owner: "Sarah Smith",
  movements: [430, 1000, 700, 50, 90],
  interestRate: 1,
  pin: 4444,
};

const accounts = [account1, account2, account3, account4];

// Elements
const labelWelcome = document.querySelector(".welcome");
const labelDate = document.querySelector(".date");
const labelBalance = document.querySelector(".balance__value");
const labelSumIn = document.querySelector(".summary__value--in");
const labelSumOut = document.querySelector(".summary__value--out");
const labelSumInterest = document.querySelector(".summary__value--interest");
const labelTimer = document.querySelector(".timer");

const containerApp = document.querySelector(".app");
const containerMovements = document.querySelector(".movements");

const btnLogin = document.querySelector(".login__btn");
const btnTransfer = document.querySelector(".form__btn--transfer");
const btnLoan = document.querySelector(".form__btn--loan");
const btnClose = document.querySelector(".form__btn--close");
const btnSort = document.querySelector(".btn--sort");

const inputLoginUsername = document.querySelector(".login__input--user");
const inputLoginPin = document.querySelector(".login__input--pin");
const inputTransferTo = document.querySelector(".form__input--to");
const inputTransferAmount = document.querySelector(".form__input--amount");
const inputLoanAmount = document.querySelector(".form__input--loan-amount");
const inputCloseUsername = document.querySelector(".form__input--user");
const inputClosePin = document.querySelector(".form__input--pin");
const logOut = document.querySelector(".log_out");

// ----------------------------------------------------------------------------------------------------------------------------------

// showing data
const date = new Date();
labelDate.innerHTML = `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}   ${date.getHours()}:${date.getMinutes()}`;

// log-out features
logOut.addEventListener("click", function(e) {
    e.preventDefault();
    containerApp.style.opacity = 0;
    labelWelcome.textContent = `Log in to get started`;
    inputLoginUsername.classList.remove("hidden");
    inputLoginPin.classList.remove("hidden");
    logOut.classList.add("hidden");
    btnLogin.classList.remove("hidden");
});

// 008-lec creating DOM elements
const displayMovements = function (movements, sort = false) {
  containerMovements.innerHTML = "";
  // .textContent = 0

  const movs = sort ? movements.slice().sort((a, b) => a - b) : movements;
  movs.forEach((mov, i) => {
    const type = mov > 0 ? "deposit" : "withdrawal";

    const html = `
    <div class="movements__row">
      <div class="movements__type movements__type--${type}">${
      i + 1
    } ${type}</div>
      <div class="movements__value">${mov}</div>
    </div>
      `;
    containerMovements.insertAdjacentHTML("afterbegin", html);
  });
};
// ----------------------------------------------------------------------------------------------------------------------------------

// 014-lec work
const calcDisplayBalance = function (account) {
  account.balance = account.movements.reduce((acc, curr) => acc + curr, 0);
  labelBalance.innerHTML = `${account.balance}€`;
};
// ----------------------------------------------------------------------------------------------------------------------------------

// 016-lec work
const calcDisplaySummary = function (account) {
  const incomes = account.movements
    .filter((mov) => mov > 0)
    .reduce((acc, mov) => acc + mov, 0);
  labelSumIn.innerHTML = `${incomes}€`;

  const outgones = account.movements
    .filter((mov) => mov < 0)
    .reduce((acc, mov) => acc + mov, 0);
  labelSumOut.innerHTML = `${Math.abs(outgones)}€`;

  // calculate the interest on deposits
  const interest = account.movements
    .filter((mov) => mov > 0)
    .map((deposit) => (deposit * account.interestRate) / 100)
    .filter((int, i, arr) => {
      // console.log(arr);
      return int >= 1;
    })
    .reduce((acc, curr) => acc + curr, 0);
  labelSumInterest.innerHTML = `${interest}€`;
};
// ----------------------------------------------------------------------------------------------------------------------------------

// 012-lec Computing Usernames
const createUsernames = function (accs) {
  accs.forEach((account) => {
    account.userName = account.owner
      .toLowerCase()
      .split(" ")
      .map((name) => name[0])
      .join("");
  });
};
createUsernames(accounts);
// console.log(accounts);
// ----------------------------------------------------------------------------------------------------------------------------------

// 020-lec work
function updateUi(acc) {
  // Display movements
  displayMovements(acc.movements);
  // Display balance
  calcDisplayBalance(acc);
  // Display summary
  calcDisplaySummary(acc);
}
// ----------------------------------------------------------------------------------------------------------------------------------

// 019-lec Implementing Login
let currentAccount;
// Event handler
btnLogin.addEventListener("click", function (e) {
  e.preventDefault();

  currentAccount = accounts.find(
    (acc) => acc.userName === inputLoginUsername.value
  );
  // console.log(currentAccount);

  if (currentAccount?.pin === Number(inputLoginPin.value)) {
    // Display ui and welcome message
    labelWelcome.textContent = `Welcome back, ${
      currentAccount.owner.split(" ")[0]
    }`;
    containerApp.style.opacity = 100;

    // clear input fields
    inputLoginUsername.value = inputLoginPin.value = "";
    inputLoginPin.blur();

    // show-logOut button
    logOut.classList.remove("hidden");
    inputLoginUsername.classList.add("hidden");
    inputLoginPin.classList.add("hidden");
    btnLogin.classList.add("hidden");
    // update Ui
    updateUi(currentAccount);
  }
});
// ----------------------------------------------------------------------------------------------------------------------------------

// 020-lec Implementing transfer
btnTransfer.addEventListener("click", function (e) {
  e.preventDefault();
  const amount = Number(inputTransferAmount.value);
  const receiverAcc = accounts.find(
    (acc) => acc.userName === inputTransferTo.value
  );

  //  console.log(amount, receiverAcc);
  inputTransferAmount.value = inputTransferTo.value = "";

  if (
    amount > 0 &&
    amount <= currentAccount.balance &&
    receiverAcc?.userName !== currentAccount.userName
  ) {
    //  Doing the transfer
    currentAccount.movements.push(-amount);
    receiverAcc.movements.push(amount);
    // update Ui
    updateUi(currentAccount);
  }
});
// ----------------------------------------------------------------------------------------------------------------------------------

// 022-lec work
btnLoan.addEventListener("click", function (e) {
  e.preventDefault();

  const amount = Number(inputLoanAmount.value);

  if (
    amount > 0 &&
    currentAccount.movements.some((mov) => mov >= amount * 0.1)
  ) {
    // add movements
    currentAccount.movements.push(amount);

    // update ui
    updateUi(currentAccount);
  }
});
// ----------------------------------------------------------------------------------------------------------------------------------

// 021-lec the findIndex method
btnClose.addEventListener("click", function (e) {
  e.preventDefault();
  const closeAccountName = inputCloseUsername.value;
  const closeAccountPin = inputClosePin.value;

  if (
    currentAccount.userName === closeAccountName &&
    currentAccount.pin == Number(closeAccountPin)
  ) {
    const closeAccIndex = accounts.findIndex(
      (acc) => acc.userName === closeAccountName
    );
    console.log(closeAccIndex);

    // Delete account
    accounts.splice(closeAccIndex, 1);

    // Hide UI
    containerApp.style.opacity = 0;
  }
  inputCloseUsername.value = inputClosePin.value = "";

  // fix my message issue 
  labelWelcome.textContent = `Log in to get started`;
});
// ----------------------------------------------------------------------------------------------------------------------------------

// 024-lec work sorting movements
let sorted = false;
btnSort.addEventListener("click", function (e) {
  e.preventDefault();
  // display movements
  displayMovements(currentAccount.movements, !sorted);

  sorted = !sorted;
});
/////////////////////////////////////////////////
/////////////////////////////////////////////////
// LECTURES

const currencies = new Map([
  ["USD", "United States dollar"],
  ["EUR", "Euro"],
  ["GBP", "Pound sterling"],
]);

const movements = [200, 450, -400, 3000, -650, -130, 70, 1300];

// ----------------------------------------------------------------------------------------------------------------------------------

// 003-lec Simple Array Methods
/* let arr = ['a', 'b', 'c', 'd', 'e'];

// SLICE
console.log(arr.slice(2));
console.log(arr.slice(2, 4));
console.log(arr.slice(-2));
console.log(arr.slice(-1));
console.log(arr.slice(1, -2));
console.log(arr.slice());
console.log([...arr]);

// SPLICE
console.log(arr.splice(2));
arr.splice(-1);
console.log(arr);
arr.splice(1, 2);
console.log(arr);

// REVERSE
arr = ['a', 'b', 'c', 'd', 'e'];
const arr2 = ['j', 'i', 'h', 'g', 'f'];
console.log(arr2.reverse());
console.log(arr2);

// CONCAT
const letters = arr.concat(arr2);
console.log(letters);
console.log([...arr, ...arr2]);
console.log(arr);

// // JOIN
console.log(letters.join(' '));
console.log(letters); */

///////////////////////////////////////
// 004-lec The new at Method
/* const arr = [23, 11, 64];
console.log(arr[0]);
console.log(arr.at(0));

// getting last array element
console.log(arr[arr.length - 1]);
console.log(arr.slice(-1)[0]);
console.log(arr.at(-1));

console.log('jonas'.at(0));
console.log('jonas'.at(-1)) */
// ----------------------------------------------------------------------------------------------------------------------------------

// 005-lec Looping Arrays: forEach
// const movements = [200, 450, -400, 3000, -650, -130, 70, 1300];

// for (const movement of movements) {
/* for (const [i, movement] of movements.entries()) {
  if (movement > 0) {
    console.log(`Movement ${i + 1}: You deposited ${movement}`);
  } else {
    console.log(`Movement ${i + 1}: You withdrew ${Math.abs(movement)}`);
  }
}

console.log('---- FOREACH ----');
movements.forEach(function (mov, i) {
  if (mov > 0) {
    console.log(`Movement ${i + 1}: You deposited ${mov}`);
  } else {
    console.log(`Movement ${i + 1}: You withdrew ${Math.abs(mov)}`);
  }
}); 
// 0: function(200)
// 1: function(450)
// 2: function(400)
// ...*/
// ----------------------------------------------------------------------------------------------------------------------------------

// 006-lec forEach With Maps and Sets
/* // Map
const currencies = new Map([
  ['USD', 'United States dollar'],
  ['EUR', 'Euro'],
  ['GBP', 'Pound sterling'],
]);

currencies.forEach(function (value, key, map) {
  console.log(`${key}: ${value}`);
});

// Set
const currenciesUnique = new Set(['USD', 'GBP', 'USD', 'EUR', 'EUR']);
console.log(currenciesUnique);
currenciesUnique.forEach(function (value) {
  console.log(`${value}: ${value}`);
}); */

// 011-lec The map method
/* const eurToUsd = 1.1;

// simple function
// const movementsUSD = movements.map(function(mov) {
//   return mov * eurToUsd;
// });

// arrow function
const movementsUSD = movements.map(mov => mov * eurToUsd);

console.log(movements);
console.log(movementsUSD);

const movementsDescription = movements.map((mov,i) => 
  `Movement ${i + 1}: You ${mov > 0 ? 'deposited' : 'withdrew'}`
);

console.log(movementsDescription); */
// ----------------------------------------------------------------------------------------------------------------------------------

// 013-lec filter method
/* 
// const deposits = movements.filter(function (mov) {
//   return mov < 0;
// });

const deposits = movements.filter(mov => mov > 0);
console.log(movements);
console.log(deposits);

const withdrews = movements.filter(mov => mov < 0);
console.log(withdrews); */
// ----------------------------------------------------------------------------------------------------------------------------------

// 014-lec reduce method
/* const balance = movements.reduce((acc, curr, i, arr) => {
  return acc + curr;
}, 0);
console.log(balance);

let sum = 0;
for (const item of movements) {
   sum += item;
}
console.log(sum);

// return maximum value of movements
const max_value = movements.reduce((acc, curr) => 
  acc < curr ? acc = curr:curr = acc, movements[0]
);
console.log(max_value);  */
// ----------------------------------------------------------------------------------------------------------------------------------

// 016-lec The magic of chaining methods
/* const eurToUsd = 1.1;

// PIPELINE 
const totalDepositsUSD = movements
.filter(mov => mov > 0)
.map((mov) => mov * eurToUsd)
.reduce((acc, mov) => acc + mov, 0);

console.log(totalDepositsUSD);  */
// ----------------------------------------------------------------------------------------------------------------------------------

// 018-lec find method
/* const firstWithdrawal = movements.find(mov => mov < 0);
console.log(movements);
console.log(firstWithdrawal);

console.log(accounts);

const account = accounts.find(acc => acc.owner === "Jessica Davis");
console.log(account); */
// ----------------------------------------------------------------------------------------------------------------------------------

// 022-lec some and every
/* console.log(movements); 
// check equality
console.log(movements.includes(-130)); 

// check condition :some method
console.log(movements.some(mov => mov === -130));
const anyDeposits = movements.some(mov => mov > 0);
console.log(anyDeposits);

// every method
console.log(movements.every(mov => mov > 0)); 
console.log(account4.movements.every(mov => mov > 0));

// separate callback
const deposit = mov => mov < 0;
console.log(movements.some(deposit));
console.log(movements.every(deposit));
console.log(movements.filter(deposit)); */
// ----------------------------------------------------------------------------------------------------------------------------------

// 023 flat and flatMap method
/*const arr = [[1,2,3], [4,5,6], 7,8];
console.log(arr.flat());

const arrDeep = [[[1,2],3], [[4,5],6], [7,8]];
console.log(arrDeep.flat(2));

// calculate the all accounts movements some
// const accountMovements = accounts.map(acc => acc.movements);
// console.log(accountMovements);
// const allMovements = accountMovements.flat();
// console.log(allMovements);

// const overalBalance = allMovements.reduce((acc, sum, i) => acc + sum, 0);
// console.log(overalBalance);

// flat mathod
const overalBalance = accounts
.map(acc => acc.movements)
.flat()
.reduce((acc, sum, i) => acc + sum, 0);

console.log(overalBalance);

// flatMap Method
const overalBalance2 = accounts
.flatMap(acc => acc.movements)
.reduce((acc, sum, i) => acc + sum, 0);

console.log(overalBalance2);  */
// ----------------------------------------------------------------------------------------------------------------------------------

// 024-lec sorting array
/* const owners = ['jonas', 'Zach', 'Adam', 'Martha'];
console.log(owners.sort());
console.log(owners);

console.log(movements);
console.log(movements.sort()); // this sort according to the string

// accending order
// console.log(movements.sort((a,b) => {
//   if (a > b) return 1;
//   if (a < b) return -1;
// }));
console.log(movements.sort((a,b) => a-b));

// desending order
// console.log(movements.sort((a,b) => {
//   if (a > b) return -1;
//   if (a < b) return 1;
// }));
console.log(movements.sort((a,b) => b - a)); */
// ----------------------------------------------------------------------------------------------------------------------------------

// 025-lec Ways of creating and filling Arrays
/* const arr = [1, 2, 3, 4, 5, 6, 7];
console.log(new Array(1, 2, 3, 4, 5, 6, 7));

// Emprty arrays + fill method
const x = new Array(7);
console.log(x);
// console.log(x.map(() => 5));
x.fill(1, 3, 5);
x.fill(1);
console.log(x);

arr.fill(23, 2, 6);
console.log(arr);

// Array from() 
const y = Array.from( {length : 7}, () => 1);
console.log(y);

const z = Array.from({length : 7}, (_, i) => i + 1);
console.log(z);

labelBalance.addEventListener('click', function () {
  const movementsUI = Array.from(
    document.querySelectorAll('.movements__value'),
    el => Number(el.textContent.replace('€', ''))
  );
  console.log(movementsUI);

  const movementsUI2 = [...document.querySelectorAll('.movements__value')];
  console.log(movementsUI2);
}); */
// ----------------------------------------------------------------------------------------------------------------------------------

// 027-lec simple array method

/* // 1.
const depositTotal = accounts
.flatMap(acc => acc.movements)
.filter(mov => mov > 0)
.reduce((acc, curr) => acc + curr, 0);

console.log(depositTotal);

// 2.
const atLeast1000 = accounts.flatMap(acc => acc.movements)
.filter(mov => mov >= 1000).length;

console.log(atLeast1000);

// 3.
const sums = accounts
.flatMap(acc => acc.movements)
.reduce((sum, cur) => {
  // cur > 0 ? (sum.deposits += cur) : (sum.withdrawals += cur);
  sum[cur > 0 ? 'deposits' : 'withdrawals']+= cur;
  return sum;
},
 {deposits: 0, withdrawals: 0}
);

console.log(sums);

// 4.
// this is a nice title -> This Is a Nice Title
const convertTitleCase = function(title) {
    const exceptions = ['a', 'an', 'the', 'but', 'or', 'on', 'in', 'with'];

    const titleCase = title.toLowerCase().split(' ')
    .map(ele => exceptions.includes(ele) ? ele : ele[0].toUpperCase() + ele.slice(1))
    .join(" ");

    console.log(titleCase);
};

convertTitleCase("this is a nice title"); */
