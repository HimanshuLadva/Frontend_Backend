'use strict';

// target html elements
const r_num = document.querySelector('.number');
const number1 = document.getElementById('e_num');
const check1 = document.getElementById('e_check');
const message = document.querySelector('.message');
const score = document.querySelector('.score');
const highScore = document.querySelector('.highscore');
const again = document.querySelector('.again');

let r_value, playingAble = true;
function generate_randomNumber() {
    r_value = Math.trunc(Math.random() * 20) + 1;
    console.log(r_value);
}

function reset() {
    score.innerHTML = 20;
    number1.value = null;
    check1.disabled = false;
    check1.style.backgroundColor = 'white';
    message.innerHTML = "Start guessing...";
    r_num.innerHTML = '?';
    document.body.style.backgroundColor = "#222";
    playingAble = true;
    generate_randomNumber();
}
reset();




// decrease score 
const decreseScore = function () {
    let scoreValue;
    scoreValue = parseInt(score.innerHTML);
    scoreValue--;
    score.innerHTML = scoreValue.toString();
}
// lost game message
const lostGame = function () {
    message.innerHTML = "💥 You Lost the Game";
    score.innerHTML = 0;
}

// checking number
function checkingNumber() {
    if (playingAble) {
        let num = number1.value;

        if (num == r_value) {
            // display message
            message.innerHTML = '🎉 Correct Number! and \n Click Again if you like to continue !    ';
            document.body.style.backgroundColor = '#60b347';
            r_num.innerHTML = r_value;
            check1.disabled = true;
            check1.style.backgroundColor = 'gray';
            playingAble = false;
     
            if (highScore.innerHTML == 0) {
                highScore.innerHTML = score.innerHTML;
            } else if (score.innerHTML > highScore.innerHTML) {
                highScore.innerHTML = score.innerHTML;
            }
        } else if (num == 0) {
            // display message
            message.innerHTML = "⛔ No Number";
        } else if(num != r_value){
            if (score.innerHTML >  1) {
                (num > r_value) ? message.innerHTML = "📈 Too High" : message.innerHTML = "📈 Too Low";
            decreseScore();
            } else {
                lostGame();
                playingAble = false;
            }
            
        }
    }
}
// add click event to the c heck button and check input value with randome value
check1.addEventListener('click', checkingNumber);

// add enter event to the input field       
number1.addEventListener('keypress', function (event) {
    if (event.key === "Enter") {
        checkingNumber();
        event.preventDefault();
    }
});

// add click event to the again button and reset all default parameter
again.addEventListener('click', reset);