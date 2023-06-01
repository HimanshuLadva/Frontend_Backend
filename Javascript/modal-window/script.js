'use strict';

const showModal = document.querySelectorAll('.show-modal');
const modal = document.querySelector('.modal');
const overLay = document.querySelector('.overlay');
const closeModal = document.querySelector('.close-modal');

const addHidden = function () {
    modal.classList.add('hidden');
    overLay.classList.add('hidden');
    modal.classList.remove(modal.classList[1]);
}
// METHOD-1 close-model
closeModal.addEventListener('click', () => {
     addHidden();
});

// METHOD-2 close-model

document.addEventListener('keydown', function(e) {
    if (e.key === 'Escape' && !modal.classList.contains('hidden') && modal.classList.contains('modal2')) {
      addHidden();
    }
});

// Close the modal popup when I click in the outer area
overLay.addEventListener('click', function() {
  if(modal.classList.contains('modal3'))
    addHidden();
});

// METHOD-1 show-modal
for (let i = 0; i < showModal.length; i++) {
  showModal[i].addEventListener('click', () => {
    console.log(showModal[i].classList[1]);
       modal.classList.add(showModal[i].classList[1])
       modal.classList.remove('hidden');
       overLay.classList.remove('hidden');
  });
}