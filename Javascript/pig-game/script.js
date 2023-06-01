'use strict';

// targeting the html elements
const dice = document.querySelector('.dice');
const btnRoll = document.querySelector('.btn--roll');
const btnHold = document.querySelector('.btn--hold');
const player0 = document.querySelector('.player--0');
const player1 = document.querySelector('.player--1');
const score0 = document.getElementById('score--0');
const score1 = document.getElementById('score--1');
const current0 = document.getElementById('current--0');
const current1 = document.getElementById('current--1');
const btnNew = document.querySelector('.btn--new');

let curr_score = 0, playingAble = true ,activePlayer = 0, randomNumber;

// switch player
const swtichPlayer = function () {
    console.log("ActivePlyer " + activePlayer);
    document.getElementById(`current--${activePlayer}`).innerHTML = 0;
    curr_score = 0;
    activePlayer = activePlayer === 0 ? 1 : 0;
    player0.classList.toggle('player--active');
    player1.classList.toggle('player--active');
}

function reset() {
    score0.innerHTML = 0;
    score1.innerHTML = 0;
    current0.innerHTML = 0;
    current1.innerHTML = 0;

    dice.classList.add('hidden');
    player0.classList.remove('player--winner');
    player1.classList.remove('player--winner');
    player0.classList.add('player--active');
    player1.classList.remove('player--active');
}
reset();

function rollDice() {
    if (playingAble) {
        randomNumber =Math.trunc(Math.random() * 6) + 1;

        dice.classList.remove('hidden');
        dice.src = `dice-${randomNumber}.png`;
        if (randomNumber != 1) {
            curr_score += randomNumber;
            document.getElementById(`current--${activePlayer}`).innerHTML = curr_score;
        } else {
            swtichPlayer();
        }
    }
}

function holdButton() {
    if (playingAble) {
        document.getElementById(`score--${activePlayer}`).innerHTML -= (-Number(curr_score));

        if (document.getElementById(`score--${activePlayer}`).innerHTML >= 100) {
            playingAble = false;
            dice.classList.add('hidden');

            document
                .querySelector(`.player--${activePlayer}`)
                .classList.add('player--winner');
            document
                .querySelector(`.player--${activePlayer}`)
                .classList.remove('player--active');
        } else {
            swtichPlayer();
        }
    }
}
// press dice button
btnRoll.addEventListener('click', rollDice);

// press the hold button
btnHold.addEventListener('click', holdButton);
// reset the game
btnNew.addEventListener('click', reset);