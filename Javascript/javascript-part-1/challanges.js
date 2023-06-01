/* // coding challenge -1
// const m_mass = 78;
// const m_height = 1.69;
// const j_mass = 92;
// const j_height = 1.95;

const m_mass = 95;
const m_height = 1.88;
const j_mass = 85;
const j_height = 1.76;

const m_BMI =  m_mass / m_height ** 2 ;
const j_BMI =  j_mass / j_height ** 2 ;

let markHigherBMI = (m_BMI > j_BMI);

console.log(m_BMI,j_BMI ,markHigherBMI);

// coding challenge 2

if(m_BMI > j_BMI) {
    console.log("Mark's BMI is higher than John's!");
    console.log(`Mark's BMI ${m_BMI} is higher than John's ${j_BMI}!`);
}
else {
    console.log("John's BMI is higher than Mark's!");
    console.log(`John's BMI ${j_BMI} is higher than Mark's ${m_BMI}!`);
} */

// coding challenge 3
/* const Dolphin_score = (96+108+89)/3;
const Koalas_score = (88+91+110)/3;
console.log(Dolphin_score, Koalas_score);

if(Dolphin_score > Koalas_score) {
    console.log('the winner team is Dolphins');
}else if(Dolphin_score < Koalas_score){
    console.log('the winner team is koalas');
}else if(Dolphin_score === Koalas_score) {
    console.log('the match is draw');
} */

// bonus 1
/* const Dolphin_score = (97+112+101)/3;
const Koalas_score = (109+95+123)/3;
console.log(Dolphin_score, Koalas_score);

if(Dolphin_score > Koalas_score && Dolphin_score>= 100) {
    console.log('the winner team is Dolphins');
}else if(Dolphin_score < Koalas_score && Koalas_score >= 100){
    console.log('the winner team is koalas');
}else if(Dolphin_score === Koalas_score ){
    console.log('the match is draw');
}else {
    console.log('no wins trophy');
} */

// bonus 2
/* const Dolphin_score = (97+112+101)/3;
const Koalas_score = (109+95+106)/3;
console.log(Dolphin_score, Koalas_score);

if(Dolphin_score > Koalas_score && Dolphin_score>= 100) {
    console.log('the winner team is Dolphins');
}else if(Dolphin_score < Koalas_score && Koalas_score >= 100){
    console.log('the winner team is koalas');
}else if(Dolphin_score === Koalas_score && Dolphin_score >= 100 && Koalas_score >= 100){
    console.log('the match is draw');
}else {
    console.log('no wins trophy');
} */

// coding challenge 4

const bill = 275;
const tip = (bill > 50 && bill < 300) ? (bill * (15/100)): (bill * (20/100));

console.log(`The bill was  ${bill}, the tip was ${tip}, and the total value ${bill+tip}`);
