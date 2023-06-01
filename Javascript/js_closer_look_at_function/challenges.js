"use strict";

// coding challenge -1
/* const poll = {
    question: "What is your favourite programming language?",
    options: ["0: JavaScript", "1: Python", "2: Rust", "3: C++"],
    // This generates [0, 0, 0, 0]. More in the next section!
    answers: new Array(4).fill(0),
    registerNewAnswer() {
        const answer = Number(prompt(`
        What is your favourite programming language?
        0: JavaScript
        1: Python
        2: Rust
        3: C++
        `));
    
        // if(question1 < 4) {
        //     this.answers[question1]+= 1;
        //     
        // } else {
        //     console.log(`answer ${question1} wouldn't make sense, right?`);
        // }
        typeof answer === 'number' && answer < this.answers.length && this.answers[answer]++; 
        console.log(this.answers);

        this.displayResults();
        this.displayResults('string');
    },
    displayResults(type = 'array') {
        if(type === 'array') {
            console.log(this.answers);
        } else if(type === 'string') {
            console.log(`poll result are ${this.answers.join(',')}`);
        }
    }
};

document.body.querySelector('.poll').addEventListener('click', poll.registerNewAnswer.bind(poll)); 

poll.displayResults.call({answers: [5,2,3]});    
poll.displayResults.call({answers: [1, 5, 3, 9, 6, 1]}, 'string');   */

// coding challenege 2

(function () {
  const header = document.querySelector("h1");
  header.style.color = "blue";

  document.body.addEventListener("click", (e) => {
    e.preventDefault();
    header.style.color = random_color();
  });
})();

function random_color() {
  var r = Math.random();
  var g = Math.random();
  var b = Math.random();
  return `rgb(${r * 255},${g * 255},${b * 255})`;
}
